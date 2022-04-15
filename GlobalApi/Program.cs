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
builder.Services.AddScoped<IPrimarykeyvalue, Primarykeyvalue>();
builder.Services.AddScoped<ClaimsHandle>();
builder.Services.AddScoped<FindUserId>();
builder.Services.AddTransient<IEMailService, EmailService>();
builder.Services.AddSingleton<FacebookAuthSetting>(builder.Configuration.GetSection("FacebookAuthSettings").Get<FacebookAuthSetting>());
builder.Services.AddSingleton<EmailConfiguration>(builder.Configuration.GetSection("EmailSettings").Get<EmailConfiguration>());

//Admin
builder.Services.AddScoped<RolesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Master
builder.Services.AddScoped<IMenu, MenuRepository>();
builder.Services.AddScoped<ISubMenu, SubMenuRepository>();
builder.Services.AddScoped<ISubMenuFunctionsRepository, SubMenuFunctionsRepository>();
builder.Services.AddScoped<IOfficesRepository, OfficesRepository>();
builder.Services.AddScoped<IOfficesRepository, OfficesRepository>();
builder.Services.AddScoped<IAppointment, AppointmenttRepository>();
builder.Services.AddScoped<IAssistant, AssistantRepository>();
builder.Services.AddScoped<IComplaint, ComplaintRepository>();
builder.Services.AddScoped<IConsultation, ConsultationRepository>();
builder.Services.AddScoped<ICountry, CountryRepository>();
builder.Services.AddScoped<ICurrency, CurrencyRepository>();
builder.Services.AddScoped<IDepartment, DepartmentRepository>();
builder.Services.AddScoped<IDesignation, DesignationRepository>();
builder.Services.AddScoped<IDiagnosticCenters, DiagnosticCentersRepository>();
builder.Services.AddScoped<IDietPlan, DietPlanRepository>();
builder.Services.AddScoped<IDiscipline, DisciplineRepository>();
builder.Services.AddScoped<IDiseases, DiseasesRepository>();
builder.Services.AddScoped<IDistrict, DistrictRepository>();
builder.Services.AddScoped<IDoctor, DoctorRepository>();
builder.Services.AddScoped<IDoctor_ScheduleInterface, Doctor_ScheduleRepository>();
builder.Services.AddScoped<IDocumentType, DocumentTypeRepository>();
builder.Services.AddScoped<IHospital, HospitalRepository>();
builder.Services.AddScoped<ILAB_INVESTIGATIONS, LAB_INVESTIGATIONSRepository>();
builder.Services.AddScoped<ILAB_SUBINVESTIGATIONS, LAB_SUBINVESTIGATIONSRepository>();
builder.Services.AddScoped<IIMG_INVESTIGATIONS, IMG_INVESTIGATIONSRepository>();
builder.Services.AddScoped<IIMG_SUBINVESTIGATIONS, IMG_SUBINVESTIGATIONSRepository>();
//builder.Services.AddScoped<IImaging, ImagingRepository>();
//builder.Services.AddScoped<ILabTest, LabTestRepository>();
builder.Services.AddScoped<INetwork, NetworkRepository>();
builder.Services.AddScoped<IParameters, ParametersRepository>();
builder.Services.AddScoped<IPatient, PatientRepository>();
builder.Services.AddScoped<IPatient_Prescription_DTL, Patient_Prescription_DTLRepository>();
builder.Services.AddScoped<ILabTestingDetails, LabTestingDetailsRepository>();
builder.Services.AddScoped<IImgTestDetails, ImgTestDetailsRepository>();
//builder.Services.AddScoped<IPatientDxImgDetails, PatientDxImgDetailsRepository>();
//builder.Services.AddScoped<IPatientDxLabDetails, PatientDxLabDetailsRepository>();
builder.Services.AddScoped<IPatientRxDetails, PatientRxDetailsRepository>();
builder.Services.AddScoped<IPharmacy, PharmacyRepository>();
builder.Services.AddScoped<IQualification, QualificationRepository>();
builder.Services.AddScoped<IRelation, RelationRepository>();
builder.Services.AddScoped<ISection, SectionRepository>();
//builder.Services.AddScoped<ISHReferrals, SHReferralsRepository>();
builder.Services.AddScoped<ISkillSet, SkillSetRepository>();
builder.Services.AddScoped<ISpecialization, SpecializationRepository>();
builder.Services.AddScoped<Istate, StateRepository>();
builder.Services.AddScoped<ISymptoms, SymptomsRepository>();
builder.Services.AddScoped<IVle, VleRepository>();
builder.Services.AddScoped<IPrimarykeyvalue, Primarykeyvalue>();
builder.Services.AddScoped<IAllowedMenusRepository, AllowedMenusRepository>();
builder.Services.AddScoped<IDoctor_Schedulehistory, Doctor_SchedulehistoryRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IComplaintMst, ComplaintMstRepository>();
builder.Services.AddScoped<ISymptomsMst, SymptomsMstRepository>();
builder.Services.AddScoped<IDrugMaster, DrugMasterRepository>();
builder.Services.AddScoped<IDoctorLanguage, DoctorLanguageRepository>();
builder.Services.AddScoped<IDoctorLocation, DoctorLocationRepository>();

builder.Services.AddHttpClient();
builder.Services.AddIdentity<AuthUser, AspNetRole>()
                .AddEntityFrameworkStores<GlobalContext>()
                .AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
    builder => builder.WithOrigins("http://localhost:32973//swagger").AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddCors();

//builder.Services.AddIdentityServer()
//                .AddDeveloperSigningCredential()
//                .AddInMemoryPersistedGrants()
//                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
//                .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
//                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
//                .AddInMemoryClients(IdentityServerConfig.GetClients())
//                .AddAspNetIdentity<AuthUser>()
//                .AddProfileService<ProfileService>();

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

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser().Build());
});

builder.Services.AddMvc(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});


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

//app.UseCors(x => x.AllowAnyOrigin()
//                  .AllowAnyMethod()
//                  .AllowAnyHeader()
//                  .AllowCredentials());
app.UseCors("CorsApi");

app.MapControllers();

app.Run();
