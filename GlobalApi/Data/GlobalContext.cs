using Microsoft.EntityFrameworkCore;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GlobalApi.Models.Authentication;

namespace GlobalApi.Data
{
    public class GlobalContext: IdentityDbContext<AuthUser, AspNetRole, string>
    {
        private readonly IConfigurationRoot configurationRoot = null!;
        public GlobalContext():this(new DbContextOptions<GlobalContext>())
        {
            
        }
        public GlobalContext(DbContextOptions<GlobalContext> options) : base(options)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }
        public DbSet<Menus> Menus { get; set; } = null!;
        public DbSet<SubMenu> SubMenu { get; set; } = null!;
        public DbSet<SubMenuFunctions> SubMenusFunctions { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<RoleClaims> RoleClaims { get; set; } = null!;
        public DbSet<SubRoleClaims> SubRoleClaims { get; set; } = null!;
        public DbSet<Offices> Office { get; set; } = null!;
        public DbSet<OfficeRoles> OfficeRoles { get; set; } = null!;
        public DbSet<SubMenusDetails> SubMenusDetails { get; set; } = null!;
        public DbSet<SubMenusFunctionDetails> SubMenusFunctionDetails { get; set; } = null!;

        //Master
        public DbSet<Notification> Notification { get; set; } = null!;
        public DbSet<AppointmentModel> PatientAppointment { get; set; } = null!;
        public DbSet<Assistant> Assistant { get; set; } = null!;
        public DbSet<Complaint> Complaint { get; set; } = null!;
        public DbSet<Consultation> Consultation { get; set; } = null!;
        public DbSet<Countries> Countries { get; set; } = null!;
        public DbSet<Currency> Currency { get; set; } = null!;
        public DbSet<Department> Department { get; set; } = null!;
        public DbSet<Designation> Designation { get; set; } = null!;
        public DbSet<DiagnosticCenters> DiagnosticCenters { get; set; } = null!;
        public DbSet<DietPlan> DietPlan { get; set; } = null!;
        public DbSet<Discipline> Discipline { get; set; } = null!;
        public DbSet<Diseases> Diseases { get; set; } = null!;
        public DbSet<Districts> Districts { get; set; } = null!;
        public DbSet<Doctor> Doctor { get; set; } = null!;
        public DbSet<Doctor_ScheduleModule> Doctor_Schedules { get; set; } = null!;
        public DbSet<Schedule_historyModel> Doctor_Schedule_history { get; set; } = null!;
        public DbSet<DocumentType> DocumentType { get; set; } = null!;
        public DbSet<Emp_Category> Emp_Category { get; set; } = null!;
        public DbSet<Emp_Type> Emp_Type { get; set; } = null!;
        public DbSet<Hospital> Hospital { get; set; } = null!;
        //public DbSet<Imaging> Imaging { get; set; } = null!;
        public DbSet<ImgTest> ImgTest { get; set; } = null!;
        public DbSet<IMG_INVESTIGATIONS> IMG_INVESTIGATIONS { get; set; } = null!;
        public DbSet<IMG_SUBINVESTIGATIONS> IMG_SUBINVESTIGATIONS { get; set; } = null!;
        public DbSet<IMG_Description> IMG_Description { get; set; } = null!;
        public DbSet<LAB_INVESTIGATIONS> LAB_INVESTIGATIONS { get; set; } = null!;
        public DbSet<LAB_SUBINVESTIGATIONS> LAB_SUBINVESTIGATIONS { get; set; } = null!;
        public DbSet<LAB_Description> LAB_Description { get; set; } = null!;
        public DbSet<LabTesting> LabTesting { get; set; } = null!;
        public DbSet<Network> Network { get; set; } = null!;
        public DbSet<Parameters> Parameters { get; set; } = null!;
        public DbSet<Patient> Patient { get; set; } = null!;
        public DbSet<Drug_Prescription> Drug_Prescription { get; set; } = null!;
        public DbSet<PatientDocument> PatientDocument { get; set; } = null!;
        public DbSet<ImgTestDetails> ImgTestDetails { get; set; } = null!;
        public DbSet<LabTestingDetails> LabTestingDetails { get; set; } = null!;
        public DbSet<Pharmacy> Pharmacy { get; set; } = null!;
        public DbSet<Qualification> Qualification { get; set; } = null!;
        public DbSet<Relation> Relation { get; set; } = null!;
        public DbSet<Section> Section { get; set; } = null!;
        public DbSet<SHReferrals> SHReferrals { get; set; } = null!;
        public DbSet<SkillSets> SkillSets { get; set; } = null!;
        public DbSet<Specialization> Specialization { get; set; } = null!;
        public DbSet<States> States { get; set; } = null!;
        public DbSet<Symptoms> Symptoms { get; set; } = null!;
        public DbSet<UsersLists> UsersLists { get; set; } = null!;
        public DbSet<Vle> Vle { get; set; } = null!;
        public DbSet<DocPkValue> DocPkValue { get; set; } = null!;
        public DbSet<ComplaintMst> ComplaintMst { get; set; } = null!;
        public DbSet<SymptomsMst> SymptomsMst { get; set; } = null!;
        public DbSet<DiseasesDtl> DiseasesDtl { get; set; } = null!;
        public DbSet<Drug_Type> Drug_Type { get; set; } = null!;
        public DbSet<Drug_Frequency> Drug_Frequency { get; set; } = null!;
        public DbSet<Drug_Units> Drug_Units { get; set; } = null!;
        public DbSet<DrugMaster> Drug_Master { get; set; } = null!;
        public DbSet<Drug_Manufacturer> Drug_Manufacturers { get; set; } = null!;
        public DbSet<Language> Language { get; set; } = null!;
        public DbSet<DoctorLanguage> DoctorLanguage { get; set; } = null!;
        public DbSet<DoctorLocation> DoctorLocation { get; set; } = null!;
        public DbSet<Status> Status { get; set; } = null !;
        public DbSet<Taluk> Taluk { get; set; } = null!;
        public DbSet<Gram> Gram { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<Hos_Type> Hos_Type { get; set; } = null!;
        public DbSet<DiagnosticType> DiagnosticType { get; set; } = null!;
        public DbSet<DiagnoCategory> DiagnoCategory { get; set; } = null!;
        public DbSet<PharmacyCategory> PharmacyCategory { get; set; } = null!;
        public DbSet<PharmacyType> PharmacyType { get; set; } = null!;
        public DbSet<Caste_MST> Caste_MST { get; set; } = null!;
        public DbSet<Identity_DOC_MST> Identity_DOC_MST { get; set; } = null!;
        public DbSet<Nationality_MST> Nationality_MST { get; set; } = null!;
        public DbSet<Religion_MST> Religion_MST { get; set; } = null!; 
        public DbSet<Insurer_MST> Insurer_MST { get; set; } = null!;
        public DbSet<Language_MST> Language_MST { get; set; } = null!;
        public DbSet<Occupation_MST> Occupation_MST { get; set; } = null!;

        public DbSet<SuffixPrefix> SuffixPrefix { get; set; } = null!;
        public DbSet<PHC_Appointment> PHC_Appointment { get; set; } = null!;
        public DbSet<AllergySigns> AllergySigns { get; set; } = null!;
        public DbSet<PatientHealthRecords> PatientHealthRecords { get; set; } = null!;
        public DbSet<AllergySigns_DTL> AllergySigns_DTL { get; set; } = null!;
        public DbSet<DoctorDocument> DoctorDocument { get; set; } = null!;
        public DbSet<Consult_Complaint_DTL> Consult_Complaint_DTL { get; set; } = null!;
        public DbSet<Consult_Symptoms_DTL> Consult_Symptoms_DTL { get; set; } = null!;
        public DbSet<Consult_Diseases_DTL> Consult_Diseases_DTL { get; set; } = null!;
        public DbSet<Consult_AllergySigns_DTL> Consult_AllergySigns_DTL { get; set; } = null!;
        public DbSet<Consult_Parameters> Consult_Parameters { get; set; } = null!;
        public DbSet<Diagnostic_Test> Diagnostic_Test { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Caste_MST>()
            .HasIndex(p => new { p.Caste, p.status,p.Nationality_ID_FK,p.Religion_ID_FK })
            .IsUnique(true);
            modelBuilder.Entity<Identity_DOC_MST>()
            .HasIndex(p => new { p.DOC_Name, p.status})
            .IsUnique(true);
            modelBuilder.Entity<Nationality_MST>()
            .HasIndex(p => new { p.Nationality, p.status })
            .IsUnique(true);
            modelBuilder.Entity<Religion_MST>()
            .HasIndex(p => new { p.Religion, p.status, p.Nationality_ID_FK })
            .IsUnique(true);
            modelBuilder.Entity<Language_MST>()
           .HasIndex(p => new { p.Language, p.status})
           .IsUnique(true);
            modelBuilder.Entity<Occupation_MST>()
           .HasIndex(p => new { p.Occupation, p.status})
           .IsUnique(true);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(configurationRoot.GetConnectionString("ConnectionString"));
    }

}
