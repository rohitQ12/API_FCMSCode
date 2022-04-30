using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.AdminIRepository;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
using GlobalApi.Models.AdminClaims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GlobalApi.Repository.AdminRepository
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<AuthUser> userManager =null!;
        private readonly RoleManager<AspNetRole> roleManager ;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private FindUserId findUserId;
        //public UserRepository():this(new UserManager<AuthUser>(new UserStore<AuthUser>(new GlobalContext()),new Options,
        //    new PasswordHasher<AuthUser>(),Logger<>
        //    ), RoleManager<AspNetRole>(),new GlobalContext())
        //{
        //}
        public UserRepository(UserManager<AuthUser> userManager, RoleManager<AspNetRole> roleManager,
               GlobalContext globalContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = globalContext;
            primarykeyvalue = new Primarykeyvalue();
            this.findUserId = new FindUserId();
        }
        public async Task<List<AuthUser_Details>> GetUser()
        {
            try
            {
                var result = (from d in db.Users
                              join e in db.Roles on d.Role_Id_FK equals e.Id
                              select new AuthUser_Details
                              {
                                  Id = d.Id,
                                  Role_Id_FK = d.Role_Id_FK,
                                  Rolename = e.Name,
                                  Inactive = d.Inactive,
                                  FirstName = d.FirstName,
                                  LastName = d.LastName,
                                  DOB=d.DOB,
                                  Gender=d.Gender,
                                  IsEnabled = d.IsEnabled,
                                  UserName = d.UserName,
                                  Email = d.Email,
                                  PhoneNumber = d.PhoneNumber
                              }).ToListAsync();


                return await result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<AuthUser> UpdateUserProfile(string Id, IFormFile Image,
            string Email,string PhoneNumber, string FirstName, string LastName, string Gender,DateTime? DOB)
        {
            try
            {
                string Username=null;
                var user = await db.Users.FirstOrDefaultAsync(x=>x.Id== Id);
                if (Image != null)
                {
                    if (user.Imagename != null && user.Imagename!= "user-1633249__340 (1).png")
                    {
                        string filepath = Path.Combine("wwwroot/Images", user.Imagename);
                        System.IO.File.Delete(filepath);
                    }

                }
                
                string image = Image==null ? user.Imagename: ProcessUploadedFile(Image);
                string[] EmailSeparators = user.UserName.Split("@");
                for(int i= 0; i < EmailSeparators.Length; i++)
                {
                    if (EmailSeparators[i].ToLower() == "gmail.com")
                    {
                        Username = Email;
                    }
                }

                if (user != null)
                {
                    user.UserName = Username == "gmail.com" ? Email: PhoneNumber;
                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.PhoneNumber = PhoneNumber;
                    user.Email = Email;
                    user.Gender = Gender;
                    user.DOB = DOB;
                    user.Imagename = image;
                    await db.SaveChangesAsync();
                    return user;
                }
                else
                    return null;
               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string ProcessUploadedFile(IFormFile image)
        {
            string uniqueFileName=null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public async Task<Profile> InsertUserProfile(string Email,string Firstname,string Lastname,string PhoneNumber)
        {
            try
            {
                var id = await primarykeyvalue.primary_key("UserProfile");
                Profile obj = new Profile()
                {
                    Id = id,
                    EmailID = Email,
                    UserName = Firstname+""+ Lastname,
                    Firstname= Firstname,
                    Lastname= Lastname,
                    Phonenumber = PhoneNumber,
                    Image = "user-1633249__340 (1).png"

                };
                var result = await db.Profiles.AddAsync(obj);
                await db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AuthUser_Details> GetUserByname(string username)
        {
            try 
            {
                var profile = await db.Users.FirstOrDefaultAsync(b => b.Email == username || b.PhoneNumber == username);
                AuthUser_Details obj = new AuthUser_Details();
                obj.Id = profile.Id;
                obj.UserName = profile.UserName;
                obj.FirstName = profile.FirstName;
                obj.LastName = profile.LastName;
                obj.Email = profile.Email;
                obj.Gender = profile.Gender;
                obj.PhoneNumber = profile.PhoneNumber;
                obj.DOB = profile.DOB;
                obj.Imagebyte = System.IO.File.ReadAllBytes(("wwwroot/Images/" + profile.Imagename));
                obj.Imagename = profile.Imagename;
                return obj;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

    }
}
