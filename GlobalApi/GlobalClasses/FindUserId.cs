using GlobalApi.Data;
using GlobalApi.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GlobalApi.GlobalClasses
{
    public class FindUserId
    {

        private readonly GlobalContext db;
        
        public FindUserId()
        {
            db = new GlobalContext();
        }
         
        public async Task<string> FindRole_Id_FKFromUserName(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x=>x.UserName==userName);
            return userDetails.Role_Id_FK;
        }

        public async Task<string> FindRoleNameFromUserName(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            return await FindRoleNameFromRole_Id_FK(userDetails.Role_Id_FK);
        }
        public async Task<string> FindRoleNameFromUserId(string userid)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.Id == userid);
            return await FindRoleNameFromRole_Id_FK(userDetails.Role_Id_FK);
        }

        public async Task<string> FindRoleNameFromRole_Id_FK(string roleId)
        {
            IdentityRole role = await db.Roles.SingleOrDefaultAsync(x=>x.Id == roleId);
            string roleName = role.Name.ToString();
            return roleName;
        }
        public async Task<string> FindUserIdFromUserName(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            return userDetails.Id;  
        }
        public async Task<string> FindUserIdFromUserNames(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);

            return userDetails.Id;
        }
        public async Task<string> FindIs_TestUserFromUserName(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            return userDetails.Id;
        }
        public async Task<int> FindPatientIdFromUserId(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            var PatientId = await db.Patient.SingleOrDefaultAsync(x => x.UserId == userDetails.Id);
            return PatientId.PR_Id;
        }
        public async Task<string> FindUserIdFromPatientId(int PatientId)
        {
            var PatientDetails = await db.Patient.SingleOrDefaultAsync(x => x.PR_Id == PatientId);
            return PatientDetails.UserId;
        }
        public string FindUserIdFromDoctorId(int? DoctorId)
        {
            var DoctorUserId = (db.Doctor.Where(x=>x.DO_Id==DoctorId).Select(x=>x.UserId)).ToString();

            return DoctorUserId;
        }
        public async Task<string> FindPatientIdFromUserEmaiOrNumber(string email,string phonenumber)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == email || x.UserName == phonenumber);
            return userDetails.Id;
        }
        public async Task<string> FindIdFromUserName(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            return userDetails.Id;
        }
        public async Task<List<AuthUser_Details>> FindUser()
        {
            try
            {
                //List<AuthUser> userDetails = await authDb.Users.OrderByDescending(d => d.UserId).ToListAsync();

                var result = (from d in db.Users
                              join e in db.Roles on d.Role_Id_FK equals e.Id
                              //orderby d.UserId descending
                              select new AuthUser_Details
                              {
                                  Id = d.Id,
                                  //UserId = d.UserId,
                                  //RoleIdFk = d.Role_Id_FK,
                                  //Rolename = e.Name,
                                  Inactive = d.Inactive,
                                  FirstName = d.FirstName,
                                  LastName = d.LastName,
                                  //imagename= (System.IO.File.ReadAllBytes("wwwroot/Images/"+ d.imagename)),
                                  IsEnabled = d.IsEnabled,
                                  UserName = d.UserName,
                                  Email = d.Email,
                                  PhoneNumber = d.PhoneNumber
                              }).ToListAsync();


                return await result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<AuthUser> FindUser(string username)
        {
            return await db.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> CheckRoles(string roleId)
        {
            //var result = await gbcontext.AspNetRoles.FirstOrDefaultAsync(d => d.Id == roleId)

            var result = await db.Roles.SingleOrDefaultAsync(d => d.Id == roleId);
            if (result.Inactive != "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
            return true;
        }

    }
}
