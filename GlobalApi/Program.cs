using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AuthIRepository;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Repository.AuthRepository;
using GlobalApi.Repository.AdminRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Text;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Repository.MasterRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using GlobalApi;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;
using GolbalApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using NLog.Extensions.Logging;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using BigBlueButtonAPI.Core;
using Microsoft.Extensions.Options;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DBContext
builder.Services.AddTransient<GlobalContext>();
builder.Services.AddDbContext<GlobalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

//Authentication
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<AuthenticationRepository>();
builder.Services.AddScoped<UserRepository>();

//Global class

builder.Services.AddScoped<ClaimsHandle>();
builder.Services.AddScoped<FindUserId>();
builder.Services.AddTransient<IEMailService, EmailService>();
builder.Services.AddSingleton<FacebookAuthSetting>(builder.Configuration.GetSection("FacebookAuthSettings").Get<FacebookAuthSetting>());
builder.Services.AddSingleton<EmailConfiguration>(builder.Configuration.GetSection("EmailSettings").Get<EmailConfiguration>());

//Admin
builder.Services.AddScoped<RolesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Master


builder.Services.AddScoped<IPatient, PatientRepository>();

//logg

builder.WebHost.ConfigureLogging((hostingContext, logging) => {

    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging")); //appsettings.json
    logging.AddConsole(); //Adds a console logger named 'Console' to the factory.
    logging.AddDebug(); //Adds a debug logger named 'Debug' to the factory.
    logging.AddEventSourceLogger(); //Adds an event logger named 'EventSource' to the factory.
    logging.AddNLog(); // Enable NLog as one of the Logging Provider

});

builder.Services.AddHttpClient();
builder.Services.AddIdentity<AuthUser, AspNetRole>()
                .AddEntityFrameworkStores<GlobalContext>()
                .AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
    builder => builder.WithOrigins("http://106.51.65.164:8097/swagger").AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddCors();


builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseFailureEvents = true;
})
                .AddExtensionGrantValidator<PhoneNumberTokenGrantValidator>()
                .AddInMemoryPersistedGrants()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
                .AddInMemoryClients(IdentityServerConfig.GetClients_test())//scopes
                .AddAspNetIdentity<AuthUser>();

var applicationUrl = builder.Configuration["ApplicationUrl"].TrimEnd('/');

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
})
//.AddCookie(options =>
//{
//        options.LoginPath = "/connect/token";
//        options.ExpireTimeSpan = TimeSpan.FromDays(1);
//})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; //false
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:aud"],
        ValidIssuer = builder.Configuration["JWT:iss"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),

    };
    options.Authority = applicationUrl;

});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.SlidingExpiration = true;
    //options.CookieName = "MyCookie";
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser().Build());
});

//Auth

//builder.Services.AddMvc(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});

//builder.Services.AddMvc();

//Video confirence
//Start
builder.Services.AddOptions();
builder.Services.Configure<BigBlueButtonAPISettings>(builder.Configuration.GetSection("VgslVCAPISettings"));
builder.Services.AddScoped<BigBlueButtonAPIClient>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<BigBlueButtonAPISettings>>().Value;
    var factory = provider.GetRequiredService<IHttpClientFactory>();
    return new BigBlueButtonAPIClient(settings, factory.CreateClient());
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


//End

builder.Services.AddSwaggerGen(c =>{
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
                            Scopes = new Dictionary<string, string>()
                            {
                                { IdentityServerConfig.ApiName, IdentityServerConfig.ApiFriendlyName }
                            }
                        }
                    }
                });
});
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var result = new ValidationFailedResult(context.ModelState);

        // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
        result.ContentTypes.Add(MediaTypeNames.Application.Json);
        result.ContentTypes.Add(MediaTypeNames.Application.Xml);

        return result;
    };
});
//builder.Services.AddControllers();
var culture = CultureInfo.CreateSpecificCulture("en-US");
var dateformat = new DateTimeFormatInfo
{
    ShortDatePattern = "dd/MM/yyyy",
    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
};
culture.DateTimeFormat = dateformat;

var supportedCultures = new[]
{
    culture
};



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Swagger UI - Global API";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{IdentityServerConfig.ApiFriendlyName} V1");
        c.OAuthClientId(IdentityServerConfig.SwaggerClientID);
        c.OAuthClientSecret("no_password"); //Leaving it blank doesn't work
    });
}
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseStaticFiles();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseIdentityServer();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsApi");

app.UseCookiePolicy();

app.MapControllers();

app.Run();
