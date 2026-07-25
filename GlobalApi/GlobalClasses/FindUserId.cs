using GlobalApi.Data;
using GlobalApi.Models.Authentication;
using GlobalApi.Models.Master;
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

        //public async Task<string> FindRole_Id_FKFromUserName(string userName)
        //{
        //    AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        //    if (userDetails == null)
        //    {
        //        return "Role id not found for the requested user";
        //    }
        //    return userDetails.Role_Id_FK;
        //}

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
            IdentityRole role = await db.Roles.SingleOrDefaultAsync(x => x.Id == roleId);
            string roleName = role.Name.ToString();
            return roleName;
        }
        //public async Task<string> FindUserIdFromUserName(string userName)
        //{
        //    AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        //    return userDetails.Id;
        //}
        public async Task<string> FindUserIdFromUserNames(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);

            return userDetails.Id;
        }


        //public async Task<int> FindOfficeRoleIdFromUserNames(string userName)
        //{
        //    string UserId = await FindUserIdFromUserName(userName);
        //    var OfficeRoleId = await db.OfficeRoles.Where(x => x.UserId == UserId).Select(x => x.Id).FirstOrDefaultAsync();

        //    return OfficeRoleId;
        //}

        public async Task<int?> FindOfficeIdFromUserNames(string userName)
        {
            string UserId = await FindUserIdFromUserName(userName);
            var OfficeId = await db.OfficeRoles.Where(x => x.UserId == UserId).Select(x => x.OfficeId).FirstOrDefaultAsync();

            return OfficeId;
        }
        public async Task<string> FindIs_TestUserFromUserName(string userName)
        {
            AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            return userDetails.Id;
        }
        #region Forget password (Institution,Corporate,Individual)
        public async Task<int> FindInstitutionIdFromUserId(string userName)
        {
            AuthUser userDetails = await db.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            Institution insDetails = await db.Institution.FirstOrDefaultAsync(x => x.Ins_UserID == userDetails.Id);
            if (insDetails != null)
            {
                return insDetails.Ins_Id;
            }
            else
                return 0;

        }
        public async Task<int> FindCorporateIdFromUserId(string userName)
        {
            AuthUser userDetails = await db.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            Corporate coDetails = await db.Corporate.FirstOrDefaultAsync(x => x.CO_UserId == userDetails.Id);
            if (coDetails != null)
            {
                return coDetails.CO_Id;
            }
            else
                return 0;

        }

        public async Task<int> FindStudentIdFromUserId(string userName)
        {
            AuthUser userDetails = await db.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            Student coStuDetails = await db.Student.FirstOrDefaultAsync(x => x.Stu_UserID == userDetails.Id);
            if (coStuDetails != null)
            {
                return coStuDetails.Stu_Id;
            }
            else
                return 0;

        }


        public async Task<int> FindIndividualIdFromUserId(string userName)
        {
            AuthUser userDetails = await db.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            Individual coDetails = await db.Individual.FirstOrDefaultAsync(x => x.Ind_UserID == userDetails.Id);
            if (coDetails != null)
            {
                return coDetails.Ind_Id;
            }
            else
                return 0;

        }
        #endregion
        //public async Task<long> FindPatientIdFromUserId_Online(string userName)
        //{
        //    AuthUser userDetails = await db.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        //    Patient_Online PatientDetails = await db.Patient_Online.FirstOrDefaultAsync(x => x.pr_user_id == userDetails.Id);
        //    if (PatientDetails != null)
        //    {
        //        return PatientDetails.pr_id;
        //    }
        //    else
        //        return 0;



        //public async Task<string> FindUserIdFromPatientId(int PatientId)
        //{
        //    var PatientDetails = await db.Patient.SingleOrDefaultAsync(x => x.PR_Id == PatientId);
        //    return PatientDetails.PR_UserId;
        //}

        //public async Task<string> FindUserIdFromPatientId_Online(long PatientId)
        //{
        //    var PatientDetails = await db.Patient_Online.SingleOrDefaultAsync(x => x.pr_id == PatientId);
        //    return PatientDetails.pr_user_id;
        //}
        //public async Task<int> FindDoctorIdFromUserId(string userName)
        //{
        //    AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        //    var DoctorId = await db.Doctor.SingleOrDefaultAsync(x => x.DO_UserId == userDetails.Id);
        //    return DoctorId.DO_Id;
        //}
        //public string FindUserIdFromDoctorId(int? DoctorId)
        //{
        //    var DoctorUserId = (db.Doctor.Where(x => x.DO_Id == DoctorId).Select(x => x.DO_UserId)).ToString();

        //    return DoctorUserId;

        //public async Task<string> FindPatientIdFromUserEmaiOrNumber(string email, string phonenumber)
        //{
        //    AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == email || x.UserName == phonenumber);
        //    return userDetails.Id;
        //}
        //public async Task<string> FindIdFromUserName(string userName)
        //{
        //    AuthUser userDetails = await db.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        //    return userDetails.Id;
        //}
        //public async Task<string> FindAssistantUserIdFromAssistantId(int AssistantId)
        //{
        //    var Assistant = await db.Assistant.SingleOrDefaultAsync(x => x.Assi_Id == AssistantId);
        //    return Assistant.Asssi_UserID;
        //}
        //public async Task<string> FindDoctorUserIdFromDoctorId(int DoctorId)
        //{
        //    var Doctor = await db.Doctor.SingleOrDefaultAsync(x => x.DO_Id == DoctorId);
        //    return Doctor.DO_UserId;
        //}
        //public async Task<string> FindPatientUserIdFromPatientId(int? PatientId)
        //{
        //    var Patient = await db.Patient.SingleOrDefaultAsync(x => x.PR_Id == PatientId);
        //    return Patient.PR_UserId;
        //}
        //public async Task<int?> FindHospitalIdFromHospitalOfficeUsername(string userName)
        //{
        //    try
        //    {
        //        var OfficeId = await (from a in db.Users
        //                              join b in db.OfficeRoles on a.Id equals b.UserId
        //                              join c in db.Roles on a.Role_Id_FK equals c.Id
        //                              where c.Rolecategory == "Hospital" && a.UserName == userName
        //                              select b.OfficeId).FirstOrDefaultAsync();
        //        return OfficeId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<int?> FindAssistantIdFromHospitalOfficeUsername(string userName)
        //{
        //    try
        //    {
        //        var OfficeId = await (from a in db.Users
        //                              join b in db.OfficeRoles on a.Id equals b.UserId
        //                              join c in db.Roles on a.Role_Id_FK equals c.Id
        //                              where c.Rolecategory == "Hospital" && c.Name == "Medical Assistant"
        //                                                       && a.UserName == userName
        //                              select b.OfficeId).FirstOrDefaultAsync();
        //        return OfficeId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //public async Task<int?> FindPharmacyIdFromPharmacyOfficeUsername(string userName)
        //{
        //    try
        //    {
        //        var OfficeId = await (from a in db.Users
        //                              join b in db.OfficeRoles on a.Id equals b.UserId
        //                              join c in db.Roles on a.Role_Id_FK equals c.Id
        //                              where c.Rolecategory == "Pharmacy" && a.UserName == userName
        //                              select b.OfficeId).FirstOrDefaultAsync();
        //        return OfficeId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<int?> FindDCIdFromDCOfficeUsername(string userName)
        //{
        //    try
        //    {
        //        var OfficeId = await (from a in db.Users
        //                              join b in db.OfficeRoles on a.Id equals b.UserId
        //                              join c in db.Roles on a.Role_Id_FK equals c.Id
        //                              where c.Rolecategory == "Diag.Center" && a.UserName == userName
        //                              select b.OfficeId).FirstOrDefaultAsync();
        //        return OfficeId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<int?> FindDoctorIdFromHospitalOfficeUsername(string userName)
        //{
        //    try
        //    {
        //        var DoctorId = await (from a in db.Users
        //                              join b in db.OfficeRoles on a.Id equals b.UserId
        //                              join c in db.Roles on a.Role_Id_FK equals c.Id
        //                              join d in db.Hospital on b.OfficeId equals d.Hos_Id
        //                              join e in db.Doctor on d.Hos_Id equals e.DO_HO_Id_FK
        //                              where c.Rolecategory == "Hospital" && a.UserName == userName
        //                              select e.DO_Id).FirstOrDefaultAsync();
        //        return DoctorId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<int> FindDoctorIdFromUsername(string userName)
        //{
        //    try
        //    {
        //        var DoctorId = await (from a in db.Users
        //                              join b in db.OfficeRoles on a.Id equals b.UserId into blist
        //                              from b in blist.DefaultIfEmpty()
        //                              join c in db.Roles on a.Role_Id_FK equals c.Id
        //                              join e in db.Doctor on a.Id equals e.DO_UserId
        //                              where a.UserName == userName
        //                              select e.DO_Id).FirstOrDefaultAsync();
        //        return DoctorId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<long> FindDoctorIdFromUsername_Online(string userName)
        //{
        //    try
        //    {
        //        var DoctorId = await (from a in db.Users
        //                              join b in db.OfficeRoles on a.Id equals b.UserId into blist
        //                              from b in blist.DefaultIfEmpty()
        //                              join c in db.Roles on a.Role_Id_FK equals c.Id
        //                              join e in db.Doctor_Online on a.Id equals e.do_user_id
        //                              where a.UserName == userName
        //                              select e.do_id).FirstOrDefaultAsync();
        //        return DoctorId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<int> FindAssistantIdFromUsername(string userName)
        //{
        //    try
        //    {
        //        var AsstId = await (from a in db.Users
        //                            join b in db.OfficeRoles on a.Id equals b.UserId into blist
        //                            from b in blist.DefaultIfEmpty()
        //                            join c in db.Roles on a.Role_Id_FK equals c.Id
        //                            join e in db.Assistant on a.Id equals e.Asssi_UserID
        //                            where a.UserName == userName
        //                            select e.Assi_Id).FirstOrDefaultAsync();
        //        return AsstId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<int> FindHospitalIdFromUsername(string userName)
        //{
        //    try
        //    {
        //        var HospitalId = await (from a in db.Users
        //                                join b in db.OfficeRoles on a.Id equals b.UserId
        //                                join c in db.Roles on a.Role_Id_FK equals c.Id
        //                                join e in db.Hospital on b.OfficeId equals e.Hos_Id
        //                                where c.Rolecategory == "Hospital" && c.Name != "Doctor" && a.UserName == userName
        //                                select e.Hos_Id).FirstOrDefaultAsync();
        //        return HospitalId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        //public async Task<int> FindHosBranchIdFromUsername(string userName)
        //{
        //    try
        //    {
        //        var HospitalId = await (from a in db.Assistant
        //                                join h in db.Hospital on a.Assi_Hos_Id_FK equals h.Hos_Id
        //                                join u in db.Users on a.Asssi_UserID equals u.Id
        //                                where u.UserName == userName
        //                                select h.Hos_Id).FirstOrDefaultAsync();
        //        return HospitalId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<int> FindHospitalIdFromUsername_Online(string userName)
        //{
        //    try
        //    {
        //        var HospitalId = await (from a in db.Users
        //                                join b in db.OfficeRoles on a.Id equals b.UserId
        //                                join c in db.Roles on a.Role_Id_FK equals c.Id
        //                                join e in db.Hospital_Online on b.OfficeId equals e.hos_id
        //                                where c.Rolecategory == "Hospital" && c.Name != "Doctor" && a.UserName == userName
        //                                select e.hos_id).FirstOrDefaultAsync();
        //        return HospitalId;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        public async Task<string> FindRolecategoryFromUserName(string userName)
        {
            try
            {
                var RoleId = await FindRole_Id_FKFromUserName(userName);
                if (RoleId == "Role id not found for the requested user")
                {
                    return null;
                }
                var Rolecategoryname = await (from d in db.Roles where d.Id == RoleId select d.Rolecategory).FirstOrDefaultAsync();
                return Rolecategoryname;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public async Task<List<AuthUser_Details>> FindUser()
        //{
        //    try
        //    {
        //        //List<AuthUser> userDetails = await authDb.Users.OrderByDescending(d => d.UserId).ToListAsync();

        //        var result = (from d in db.Users
        //                      join e in db.Roles on d.Role_Id_FK equals e.Id
        //                      //orderby d.UserId descending
        //                      select new AuthUser_Details
        //                      {
        //                          Id = d.Id,
        //                          //UserId = d.UserId,
        //                          //RoleIdFk = d.Role_Id_FK,
        //                          //Rolename = e.Name,
        //                          Inactive = d.Inactive,
        //                          FirstName = d.FirstName,
        //                          LastName = d.LastName,
        //                          //imagename= (System.IO.File.ReadAllBytes("wwwroot/Images/"+ d.imagename)),
        //                          IsEnabled = d.IsEnabled,
        //                          UserName = d.UserName,
        //                          Email = d.Email,
        //                          PhoneNumber = d.PhoneNumber
        //                      }).ToListAsync();


        //        return await result;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        //public async Task<AuthUser> FindUser(string username)
        //{
        //    return await db.Users.SingleOrDefaultAsync(x => x.UserName == username);
        //}

        //public async Task<bool> CheckRoles(string roleId)
        //{
        //    //var result = await gbcontext.AspNetRoles.FirstOrDefaultAsync(d => d.Id == roleId)

        //    var result = await db.Roles.SingleOrDefaultAsync(d => d.Id == roleId);
        //    if (result.Inactive != "Y")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public async Task<bool> Deleteuser(string Userid)
        //{
        //    var result = await db.Users.FirstOrDefaultAsync(x => x.Id == Userid);
        //    if (result != null)
        //    {
        //        db.Users.Remove(result);
        //        db.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        //public async Task<int> GetCons_Id_ByAppointmentId(int appointment_id)
        //{
        //    var result = await db.Consultation.SingleOrDefaultAsync(x => x.CON_APPT_Id_FK == appointment_id);
        //    return result.CON_Id;
        //}


        //public async Task<string> FindUserIdFromUserName(string userName)
        //{
        //    // UserName column = "Admin", NormalizedUserName = "8778650328" in your table
        //    // so search by PhoneNumber if userName is a phone number
        //    var userDetails = await db.AspNetUsers
        //        .SingleOrDefaultAsync(x => x.PhoneNumber == userName || x.UserName == userName);

        //    if (userDetails == null)
        //        return null;

        //    return userDetails.Id;
        //}


        public async Task<string> FindUserIdFromUserName(string userName)
        {
            AuthUser userDetails = await db.Users
                .SingleOrDefaultAsync(x => x.PhoneNumber == userName || x.UserName == userName);

            if (userDetails == null)
                return null;

            return userDetails.Id;
        }

        //public async Task<string> FindRole_Id_FKFromUserName(string userName)
        //{
        //    var userDetails = await db.AspNetUsers
        //        .SingleOrDefaultAsync(x => x.PhoneNumber == userName || x.UserName == userName);

        //    if (userDetails == null)
        //        return "Role id not found for the requested user";

        //    return userDetails.Role_Id_FK;
        //}

        public async Task<string> FindRole_Id_FKFromUserName(string userName)
        {
            AuthUser userDetails = await db.Users
                .SingleOrDefaultAsync(x => x.PhoneNumber == userName || x.UserName == userName);

            if (userDetails == null)
                return "Role id not found for the requested user";

            return userDetails.Role_Id_FK;
        }



        //public async Task<bool> CheckRoles(string roleId)
        //{
        //    var result = await db.AspNetRoles.SingleOrDefaultAsync(d => d.Id == roleId);

        //    if (result == null)
        //        return false;

        //    return result.Inactive != "Y"; // true = active, false = inactive
        //}


        public async Task<bool> CheckRoles(string roleId)
        {
            var result = await db.Roles.SingleOrDefaultAsync(d => d.Id == roleId);

            if (result == null)
                return false;

            return result.Inactive != "Y";
        }

    }
}
