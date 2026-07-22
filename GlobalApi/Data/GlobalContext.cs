using GlobalApi.Controllers.MasterController;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models;
using GlobalApi.Models.AdminClaims;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Models.Payment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using static System.Collections.Specialized.BitVector32;

namespace GlobalApi.Data
{
    public class GlobalContext : IdentityDbContext<AuthUser, AspNetRole, string>
    {
        private readonly IConfigurationRoot configurationRoot = null!;
        public GlobalContext() : this(new DbContextOptions<GlobalContext>())
        { 

        }
        public GlobalContext(DbContextOptions<GlobalContext> options) : base(options)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }
      
        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<RoleClaims> RoleClaims { get; set; } = null!;
        public DbSet<SubRoleClaims> SubRoleClaims { get; set; } = null!;
        //public DbSet<Offices> Office { get; set; } = null!;
        //public DbSet<OfficeRoles> OfficeRoles { get; set; } = null!;
        public DbSet<Menus> Menus { get; set; } = null!;
        public DbSet<SubMenu> SubMenu { get; set; } = null!;
        public DbSet<SubMenusFunctions> SubMenusFunctions { get; set; } = null!;
        public DbSet<SubMenusDetails> SubMenusDetails { get; set; } = null!;
        public DbSet<SubMenusFunctionDetails> SubMenusFunctionDetails { get; set; } = null!;
        public DbSet<Discipline> Discipline { get; set; } = null!;
        public DbSet<Discipline_Online> Discipline_Online { get; set; } = null!; 

        //Master

        public DbSet<DocPkValue> DocPkValue { get; set; } = null!;

        public DbSet<UserVerfication> UserVerfication { get; set; } = null!;
        public DbSet<UserVerfication_Online> UserVerfication_Online { get; set; } = null!;
       
        public DbSet<OfficeRoles> OfficeRoles { get; set; } = null!;
        public DbSet<Department> Department { get; set; } = null!;
        public DbSet<Designation> Designation { get; set; } = null!;
        public DbSet<Designation_Online> Designation_Online { get; set; } = null!;

        public DbSet<Customer_ContactUs> Customer_ContactUs { get; set; } = null!;
        public DbSet<Status> Status { get; set; } = null!;
        public DbSet<States> States { get; set; } = null!;
        public DbSet<Countries> Countries { get; set; } = null!;
        public DbSet<Districts> Districts { get; set; } = null!;
        public DbSet<Taluk> Taluk { get; set; } = null!;
          public DbSet<Qualification> Qualification { get; set; } = null!;
          public DbSet<Qualification_Online> Qualification_Online { get; set; } = null!;
          public DbSet<Upload_Videos> Upload_Videos { get; set; } = null!;
          public DbSet<Course_Chapters> Course_Chapters { get; set; } = null!;
          public DbSet<Course_Package> Course_Package { get; set; } = null!;
          public DbSet<Course_Section> Course_Section { get; set; } = null!;
          public DbSet<Course_Master> Course_Master { get; set; } = null!;
          public DbSet<Institution> Institution { get; set; } = null!;
          public DbSet<Corporate> Corporate { get; set; } = null!;
          public DbSet<Student> Student { get; set; } = null!;
          public DbSet<Student_Enquiry> Student_Enquiry { get; set; } = null!;

          public DbSet<Network> Network { get; set; } = null!;
          public DbSet<UsersLists> UsersLists { get; set; } = null!;
          public DbSet<student_courses> student_courses { get; set; } = null!;
          public DbSet<Models.Master.Section> Section { get; set; } = null!;

        public DbSet<Individual> Individual { get; set; } = null!;

        public DbSet<Customer_Enquiry> Customer_Enquiry { get; set; } = null!;
        public DbSet<Enquire_Type> Enquire_Type { get; set; } = null!;       
        public DbSet<Customer_Callback> Customer_Callback { get; set; } = null!;
        public DbSet<feedback> feedback { get; set; } = null!;
        public DbSet<Users_Payment> Users_Payment { get; set; } = null!;








        // This Code Is For The Courier Project






       

        //public DbSet<CustomerDto> Customer { get; set; } = null!;
       public DbSet<UserCustomer> UserCustomer { get; set; } = null!;

       public DbSet<Partner> Partner { get; set; } = null!;
        //public object AspNetUsers { get; internal set; }
        public DbSet<AuthUser> AspNetUsers { get; set; }
        //public DbSet<IdentityRole> AspNetRoles { get; set; }


        //public DbSet<Test> Tests { get; set; }
        //public DbSet<Questions> Questions { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Caste_MST>()
            // .HasIndex(p => new { p.Caste, p.status, p.Nationality_ID_FK, p.Religion_ID_FK })
            // .IsUnique(true);
            // modelBuilder.Entity<Identity_DOC_MST>()
            // .HasIndex(p => new { p.DOC_Name, p.status })
            // .IsUnique(true);
            // modelBuilder.Entity<Nationality_MST>()
            // .HasIndex(p => new { p.Nationality, p.status })
            // .IsUnique(true);
            // modelBuilder.Entity<Religion_MST>()
            // .HasIndex(p => new { p.Religion, p.status, p.Nationality_ID_FK })
            // .IsUnique(true);
            // modelBuilder.Entity<Language_MST>()
            //.HasIndex(p => new { p.Language, p.status })
            //.IsUnique(true);
            // modelBuilder.Entity<Occupation_MST>()
            //.HasIndex(p => new { p.Occupation, p.status })
            //.IsUnique(true);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocPkValue>(entity =>
            {
                entity.HasKey(e => e.PkId);
                entity.HasIndex(e => new { e.PkPresentValue, e.PkPreviousValue }).IsUnique();
            });




            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(configurationRoot.GetConnectionString("ConnectionString"));
    }

}
