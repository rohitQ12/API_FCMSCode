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

namespace GlobalApi.Repository.AdminRepository
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<AuthUser> userManager =null!;
        private readonly RoleManager<AspNetRole> roleManager ;
        private readonly GlobalContext globalContext;
        private IPrimarykeyvalue primarykeyvalue;
        private FindUserId findUserId;
        public UserRepository(UserManager<AuthUser> userManager, RoleManager<AspNetRole> roleManager,
               GlobalContext globalContext,FindUserId findUserId)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.globalContext= globalContext;
            primarykeyvalue = new Primarykeyvalue(globalContext);
            this.findUserId = findUserId;
        }
        public async Task<List<AuthUser_Details>> GetUser()
        {
            try
            {
                return await findUserId.FindUser();
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        private string ProcessUploadedFile(IFormFile image)
        {
            string uniqueFileName = null;


            if (image != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public async Task<Profile> UpdateUserProfile(Profile_Image userProfile)
        {
            try
            {
                var result = await globalContext.Profiles.FirstOrDefaultAsync(x => x.Id==userProfile.Id);
                var user = await globalContext.Users.FirstOrDefaultAsync(y => y.UserName == result.EmailID || y.UserName == result.Phonenumber);
                if (userProfile.Image != null)
                {
                    if (result.Image != null && result.Image!= "user-1633249__340 (1).png")
                    {
                        string filepath = Path.Combine("wwwroot/Images", result.Image);
                        System.IO.File.Delete(filepath);
                    }

                }
                string image = userProfile.Image==null? result.Image: ProcessUploadedFile(userProfile.Image);

                if (result != null)
                {
                    if(user != null)
                    {
                        user.PhoneNumber = userProfile.Phonenumber;
                        user.UserName = userProfile.Phonenumber;
                        user.FirstName = userProfile.Firstname;
                        user.LastName = userProfile.Lastname;
                        user.Email = userProfile.EmailID;
                    }
                    result.UserName = userProfile.Firstname + userProfile.Lastname;
                    result.Firstname = userProfile.Firstname;
                    result.Lastname = userProfile.Lastname;
                    result.EmailID = userProfile.EmailID;
                    result.Phonenumber = userProfile.Phonenumber;
                    result.Gender = userProfile.Gender;
                    result.DOB = userProfile.DOB;
                    result.Image = image;
                    await globalContext.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
                var result = await globalContext.Profiles.AddAsync(obj);
                await globalContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Profile_Details> GetUserByname(string username)
        {
            var profile = await globalContext.Profiles.FirstOrDefaultAsync(b => b.EmailID == username || b.Phonenumber == username);
            Profile_Details obj = new Profile_Details();
            obj.Id = profile.Id;
            obj.UserName = profile.UserName;
            obj.Firstname = profile.Firstname;
            obj.Lastname = profile.Lastname;
            obj.EmailID = profile.EmailID;
            obj.Gender = profile.Gender;
            obj.Phonenumber = profile.Phonenumber;
            obj.DOB = profile.DOB;
            obj.Image = System.IO.File.ReadAllBytes(("wwwroot/Images/" + profile.Image));

            return obj;
        }

    }
}
