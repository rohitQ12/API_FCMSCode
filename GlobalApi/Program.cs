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

//builder.Services.AddMvc(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});

//builder.Services.AddMvc();


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
builder.Services.AddControllers();

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

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsApi");

app.UseCookiePolicy();

app.MapControllers();

app.Run();
