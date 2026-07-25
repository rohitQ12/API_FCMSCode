using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.IRepository.PaymentIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Repository.AdminRepository;
using GlobalApi.Repository.AuthRepository;
using GlobalApi.Repository.MasterReopsitory;
using GlobalApi.Repository.MasterRepository;
using GlobalApi.Repository.PaymentRepository;
using GlobalApi.Service;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using BigBlueButtonAPI.Core;
using GolbalApi;
using GlobalApi;
using GlobalApi.CustomJson;

var builder = WebApplication.CreateBuilder(args);

// DB Context
builder.Services.AddDbContext<GlobalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddTransient<GlobalContext>();

// Identity
builder.Services.AddIdentity<AuthUser, AspNetRole>()
    .AddEntityFrameworkStores<GlobalContext>()
    .AddDefaultTokenProviders();

// Global Services
builder.Services.AddScoped<ClaimsHandle>();
builder.Services.AddScoped<FindUserId>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Auth Services
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<AuthenticationRepository>();
builder.Services.AddScoped<UserRepository>();

// Admin Services
builder.Services.AddScoped<RolesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<EnquiryIRepository, EnquiryRepository>();
builder.Services.AddScoped<IStudentQuestion, StudentQuestion>();

// Master Services
builder.Services.AddScoped<IPayment, PaymentRepository>();
builder.Services.AddScoped<ICustomerCallback, CustomerCallbackRepository>();
builder.Services.AddScoped<Ifeedback, feedbackRespository>();

// External Services & Config
builder.Services.AddSingleton(builder.Configuration.GetSection("FacebookAuthSettings").Get<FacebookAuthSetting>());
builder.Services.AddSingleton(builder.Configuration.GetSection("EmailSettings").Get<EmailConfiguration>());
builder.Services.AddTransient<IEMailService, EmailService>();
builder.Services.AddTransient<ISMSService, SMSService>();

// Video Conference
builder.Services.AddOptions();
builder.Services.Configure<BigBlueButtonAPISettings>(builder.Configuration.GetSection("VgslVCAPISettings"));
builder.Services.AddScoped<BigBlueButtonAPIClient>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<BigBlueButtonAPISettings>>().Value;
    var factory = provider.GetRequiredService<IHttpClientFactory>();
    return new BigBlueButtonAPIClient(settings, factory.CreateClient());
});

// Cookie & Form Options
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 91474836480; // 90 GB
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Angular default port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Authentication
var applicationUrl = builder.Configuration["ApplicationUrl"].TrimEnd('/');
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JWT:iss"],
        ValidAudience = builder.Configuration["JWT:aud"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
    options.Authority = applicationUrl;
});

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Bearer", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
              .RequireAuthenticatedUser();
    });
});

// Identity Server
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseFailureEvents = true;
})
.AddExtensionGrantValidator<PhoneNumberTokenGrantValidator>()
.AddDeveloperSigningCredential()
.AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
.AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
.AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
.AddInMemoryClients(IdentityServerConfig.GetClients_test())
.AddAspNetIdentity<AuthUser>();

// Caching
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");
    options.SchemaName = "dbo";
    options.TableName = "SQLCache";
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Controllers & JSON Options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
    })
    .AddNewtonsoftJson()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var result = new ValidationFailedResult(context.ModelState);
            result.ContentTypes.Add(MediaTypeNames.Application.Json);
            result.ContentTypes.Add(MediaTypeNames.Application.Xml);
            return result;
        };
    });


// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = IdentityServerConfig.ApiFriendlyName, Version = "v1" });
    c.OperationFilter<AuthorizeCheckOperationFilter>();
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("/connect/token", UriKind.Relative),
                Scopes = new Dictionary<string, string>
                {
                    { IdentityServerConfig.ApiName, IdentityServerConfig.ApiFriendlyName }
                }
            }
        }
    });
});

// Localization
var culture = CultureInfo.CreateSpecificCulture("en-US");
culture.DateTimeFormat = new DateTimeFormatInfo
{
    ShortDatePattern = "dd/MM/yyyy",
    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
};
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(culture);
    options.SupportedCultures = new[] { culture };
    options.SupportedUICultures = new[] { culture };
});

// Build app
var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Swagger UI - Global API";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{IdentityServerConfig.ApiFriendlyName} V1");
        c.OAuthClientId(IdentityServerConfig.SwaggerClientID);
        c.OAuthClientSecret("no_password");
    });
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images")),
    RequestPath = new PathString("/wwwroot/Images")
});

app.UseRequestLocalization();
app.UseCors("AllowAngular");

app.UseCors("CorsApi");
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllers();
app.Run();
