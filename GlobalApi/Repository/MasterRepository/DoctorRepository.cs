using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class DoctorRepository : IDoctor
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public DoctorRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Doctor> InsertDoctor(Doctor_Images lead)
        {
            try
            {
                int id = await primarykeyvalue.primary_key("Doctor");
                string uniqueFilename = ProcessUploadedFile(lead);
                
                Doctor obj = new Doctor()
                {
                    DO_Id = id,
                    DO_Code = lead.DO_Code,
                    DO_FirstName = lead.DO_FirstName,
                    DO_LastName = lead.DO_LastName,
                    DO_DOB = lead.DO_DOB,
                    DO_Gender = lead.DO_Gender,
                    DO_Address = lead.DO_Address,
                    DO_Country_Id_FK = lead.DO_Country_Id_FK,
                    DO_ST_Id_FK = lead.DO_ST_Id_FK,
                    DO_DI_Id_FK = lead.DO_DI_Id_FK,
                    DO_Taluk_Id = lead.DO_Taluk_Id,
                    DO_Gram_Id = lead.DO_Gram_Id,
                    DO_PostalCode = lead.DO_PostalCode,
                    DO_MobileNumber = lead.DO_MobileNumber,
                    DO_OfficialNumber = lead.DO_OfficialNumber,
                    DO_Email = lead.DO_Email,
                    DO_HO_Id_FK = lead.DO_HO_Id_FK,
                    DO_QU_Id_FK = lead.DO_QU_Id_FK,
                    DO_DE_Id_FK = lead.DO_DE_Id_FK,
                    DO_CD_Id_FK = lead.DO_CD_Id_FK,
                    DO_SP_Id_FK = lead.DO_SP_Id_FK,
                    DO_Photo = uniqueFilename,
                    DO_UserId_FK = lead.DO_UserId_FK,
                    DO_Village = lead.DO_Village,
                    DO_Alernative_Numb = lead.DO_Alernative_Numb,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Doctor.AddAsync(obj);
                //var Dlang = await doctorLanguageRepository.InsertDoctorLanguage(lead.DoctorLanguage, id);
                await db.SaveChangesAsync();
                if (lead.DO_Languages != null)
                {
                    List<int> Lang = lead.DO_Languages.Split(',').Select(int.Parse).ToList();
                    foreach (var dl in Lang)
                    {
                        var list1 = (from a in db.Doctor orderby a.DO_Id descending select a.DO_Id).FirstOrDefaultAsync();
                        int _pkid = await primarykeyvalue.primary_key("DoctorLanguage");
                        DoctorLanguage obj1 = new DoctorLanguage();
                        obj1.Id = _pkid;
                        obj1.doc_Id_FK = await list1;
                        obj1.Lang_Id_FK = dl;
                        obj1.created_by = 1;
                        obj1.created_date = DateTime.Now;
                        obj1.delete_flag = false;
                        obj1.status = 1;

                        var result1 = await db.DoctorLanguage.AddAsync(obj1);
                        await db.SaveChangesAsync();

                    }
                    await InsertUsers(obj);
                    return result.Entity;
                }
                else
                {
                    var list1 = (from a in db.Doctor orderby a.DO_Id descending select a.DO_Id).FirstOrDefaultAsync();
                    int _pkid = await primarykeyvalue.primary_key("DoctorLanguage");
                    DoctorLanguage obj1 = new DoctorLanguage();
                    obj1.Id = _pkid;
                    obj1.doc_Id_FK = await list1;
                    obj1.Lang_Id_FK = 2;
                    obj1.created_by = 1;
                    obj1.created_date = DateTime.Now;
                    obj1.delete_flag = false;
                    obj1.status = 1;

                    var result1 = await db.DoctorLanguage.AddAsync(obj1);
                    await db.SaveChangesAsync();

                }
                await InsertUsers(obj);
                return result.Entity;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UsersLists> InsertUsers(Doctor lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Doctor",
                User_ref_id = lead.DO_Id,
                created_by = 1,
                created_date = DateTime.Now,
                delete_flag = false,
                status = 1

            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;
        }
        //Inserting DoctorRegistration Logo
        private string ProcessUploadedFile(Doctor_Images model)
        {
            string? uniqueFileName = null;


            if (model.DO_Photo != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Doctor");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.DO_Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.DO_Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<Doctor> UpdateDoctor(Doctor_Images lead)
        {
            try
            {
                //string result1 = Convert.ToString(lead.DO_Languages.FirstOrDefault());
                var result = await db.Doctor.FirstOrDefaultAsync(x => x.DO_Id == lead.DO_Id);
                if (lead.DO_Photo != null)
                {
                    if (result != null)                             
                    {
                        string filepath = Path.Combine("wwwroot/Doctor", result.DO_Photo);
                        System.IO.File.Delete(filepath);
                    }
                }
                //Update DoctorRegistration logo
                string uniqueFilename = ProcessUploadedFile(lead);

                if (result != null)
                { 
                    
                        result.DO_Id = lead.DO_Id;
                        result.DO_Code = lead.DO_Code;
                        result.DO_FirstName = lead.DO_FirstName;
                        result.DO_LastName = lead.DO_LastName;
                        result.DO_DOB = lead.DO_DOB;
                        result.DO_Gender = lead.DO_Gender;
                        result.DO_Address = lead.DO_Address;
                        result.DO_Country_Id_FK = lead.DO_Country_Id_FK;
                        result.DO_ST_Id_FK = lead.DO_ST_Id_FK;
                        result.DO_DI_Id_FK = lead.DO_DI_Id_FK;
                        result.DO_Taluk_Id = lead.DO_Taluk_Id;
                        result.DO_Gram_Id = lead.DO_Gram_Id;
                        result.DO_PostalCode = lead.DO_PostalCode;
                        result.DO_MobileNumber = lead.DO_MobileNumber;
                        result.DO_OfficialNumber = lead.DO_OfficialNumber;
                        result.DO_Email = lead.DO_Email;
                        result.DO_HO_Id_FK = lead.DO_HO_Id_FK;
                        result.DO_QU_Id_FK = lead.DO_QU_Id_FK;
                        result.DO_DE_Id_FK = lead.DO_DE_Id_FK;
                        result.DO_CD_Id_FK = lead.DO_CD_Id_FK;
                        result.DO_SP_Id_FK = lead.DO_SP_Id_FK;
                        result.DO_Photo = uniqueFilename;
                        result.DO_UserId_FK = lead.DO_UserId_FK;
                        result.DO_Village = lead.DO_Village;
                        result.DO_Alernative_Numb = lead.DO_Alernative_Numb;
                        result.modified_by = 2;
                        result.modified_date = DateTime.Now;
                        result.delete_flag = false;
                        result.status = 2;
                    List<int> Lang = lead.DO_Languages.Split(',').Select(int.Parse).ToList();
                    var Doclanguage = (from d in db.DoctorLanguage where d.doc_Id_FK == lead.DO_Id select d).ToList();
                    foreach (var dl in Lang)
                    {
                        if (!Doclanguage.Any(c => c.Lang_Id_FK == dl))
                        {
                            var list1 = (from a in db.Doctor orderby a.DO_Id descending select a.DO_Id).FirstOrDefaultAsync();
                            int _pkid = await primarykeyvalue.primary_key("DoctorLanguage");
                            DoctorLanguage obj1 = new DoctorLanguage();
                            obj1.Id = _pkid;
                            obj1.doc_Id_FK = await list1;
                            obj1.Lang_Id_FK = dl;
                            obj1.created_by = 1;
                            obj1.created_date = DateTime.Now;
                            obj1.delete_flag = false;
                            obj1.status = 2;

                            var result1 = await db.DoctorLanguage.AddAsync(obj1);
                            await db.SaveChangesAsync();

                        }
                        else { 
                            var delete = await db.DoctorLanguage.FirstOrDefaultAsync(x => x.doc_Id_FK == lead.DO_Id);
                            if (delete != null)
                            {
                                var data = db.DoctorLanguage.Remove(delete);
                                await db.SaveChangesAsync();
                            }
                        }
                    }
                    await db.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllDoctor>> GetAllDoctor()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Doctor
                                 join b in db.States on a.DO_ST_Id_FK equals b.stat_id
                                 join c in db.Districts on a.DO_DI_Id_FK equals c.district_id
                                 join d in db.Hospital on a.DO_HO_Id_FK equals d.Hos_Id
                                 join e in db.Qualification on a.DO_QU_Id_FK equals e.qualification_id
                                 join f in db.Designation on a.DO_DE_Id_FK equals f.designation_id
                                 join g in db.Discipline on a.DO_CD_Id_FK equals g.CD_Id
                                 join h in db.Specialization on a.DO_SP_Id_FK equals h.SP_Id
                                 join i in db.Countries on a.DO_Country_Id_FK equals i.cntry_id
                                 join j in db.Taluk on a.DO_Taluk_Id equals j.Taluk_id into jlist
                                 from j in jlist.DefaultIfEmpty()
                                 join k in db.Gram on a.DO_Gram_Id equals k.Gram_id into klist
                                 from k in klist.DefaultIfEmpty()
                                 orderby a.DO_Id descending
                                 select new GetAllDoctor
                                 {
                                     DO_Id = a.DO_Id,
                                     DO_Code = a.DO_Code,
                                     DO_FirstName = a.DO_FirstName,
                                     DO_LastName = a.DO_LastName,
                                     DO_DOB = a.DO_DOB,
                                     DO_Gender = a.DO_Gender,
                                     DO_Address = a.DO_Address,
                                     DO_Country_Id_FK = a.DO_Country_Id_FK,
                                     DO_Country_name = i.country_name,
                                     DO_ST_Id_FK = a.DO_ST_Id_FK,
                                     DO_StateName = b.state_name,
                                     DO_DI_Id_FK = a.DO_DI_Id_FK,
                                     DO_DistrictName = c.district_name,
                                     DO_Taluk_Id = a.DO_Taluk_Id,
                                     Taluk_name = j.Taluk_name,
                                     DO_Gram_Id = a.DO_Gram_Id,
                                     Gram_name = k.Gram_name,
                                     DO_PostalCode = a.DO_PostalCode,
                                     DO_MobileNumber = a.DO_MobileNumber,
                                     DO_OfficialNumber = a.DO_OfficialNumber,
                                     DO_Email = a.DO_Email,
                                     DO_HO_Id_FK = a.DO_HO_Id_FK,
                                     DO_Hospital = d.Hos_HospitalName,
                                     DO_QU_Id_FK = a.DO_QU_Id_FK,
                                     DO_Qualification = e.qualification_Name,
                                     DO_DE_Id_FK = a.DO_DE_Id_FK,
                                     DO_Designation = f.designation_desc,
                                     DO_CD_Id_FK = a.DO_CD_Id_FK,
                                     DO_ClinicalDiscipline = g.CD_ClinicalDiscipline,
                                     DO_SP_Id_FK = a.DO_SP_Id_FK,
                                     DO_Specialization = h.SP_Specialization,
                                     DO_Photo = a.DO_Photo,
                                     Imagebyte =System.IO.File.ReadAllBytes(("wwwroot/Doctor/" + a.DO_Photo)),
                                     DO_UserId_FK = a.DO_UserId_FK,
                                     DO_Village = a.DO_Village,
                                     DO_Alernative_Numb = a.DO_Alernative_Numb,
                                     delete_flag = a.delete_flag,
                                     status = a.status,

                                 });
                    return await query.ToListAsync();
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Doctor> DeleteDoctor(int DO_Id)
        {
            try
            {
                var result = await db.Doctor.FirstOrDefaultAsync(x => x.DO_Id == DO_Id);
                if (result != null)
                {
                    result.DO_Id = DO_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<DoctorById> GetDoctorById(int DO_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Doctor
                             join b in db.States on a.DO_ST_Id_FK equals b.stat_id
                             join c in db.Districts on a.DO_DI_Id_FK equals c.district_id
                             join d in db.Hospital on a.DO_HO_Id_FK equals d.Hos_Id
                             join e in db.Qualification on a.DO_QU_Id_FK equals e.qualification_id
                             join f in db.Designation on a.DO_DE_Id_FK equals f.designation_id
                             join g in db.Discipline on a.DO_CD_Id_FK equals g.CD_Id
                             join h in db.Specialization on a.DO_SP_Id_FK equals h.SP_Id
                             join i in db.Countries on a.DO_Country_Id_FK equals i.cntry_id
                             join j in db.Taluk on a.DO_Taluk_Id equals j.Taluk_id into jlist
                             from j in jlist.DefaultIfEmpty()
                             join k in db.Gram on a.DO_Gram_Id equals k.Gram_id into klist
                             from k in klist.DefaultIfEmpty()
                             where a.DO_Id == DO_Id
                             select new DoctorById
                             {
                                 DO_Id = a.DO_Id,
                                 DO_Code = a.DO_Code,
                                 DO_FirstName = a.DO_FirstName,
                                 DO_LastName = a.DO_LastName,
                                 DO_DOB = a.DO_DOB,
                                 DO_Gender = a.DO_Gender,
                                 DO_Address = a.DO_Address,
                                 DO_Country_Id_FK = a.DO_Country_Id_FK,
                                 DO_Country_name = i.country_name,
                                 DO_ST_Id_FK = a.DO_ST_Id_FK,
                                 DO_StateName = b.state_name,
                                 DO_DI_Id_FK = a.DO_DI_Id_FK,
                                 DO_DistrictName = c.district_name,
                                 DO_Taluk_Id = a.DO_Taluk_Id,
                                 Taluk_name = j.Taluk_name,
                                 DO_Gram_Id = a.DO_Gram_Id,
                                 Gram_name = k.Gram_name,
                                 DO_PostalCode = a.DO_PostalCode,
                                 DO_MobileNumber = a.DO_MobileNumber,
                                 DO_OfficialNumber = a.DO_OfficialNumber,
                                 DO_Email = a.DO_Email,
                                 DO_HO_Id_FK = a.DO_HO_Id_FK,
                                 DO_Hospital = d.Hos_HospitalName,
                                 DO_QU_Id_FK = a.DO_QU_Id_FK,
                                 DO_Qualification = e.qualification_Name,
                                 DO_DE_Id_FK = a.DO_DE_Id_FK,
                                 DO_Designation = f.designation_desc,
                                 DO_CD_Id_FK = a.DO_CD_Id_FK,
                                 DO_ClinicalDiscipline = g.CD_ClinicalDiscipline,
                                 DO_SP_Id_FK = a.DO_SP_Id_FK,
                                 DO_Specialization = h.SP_Specialization,
                                 DO_Photo = a.DO_Photo,
                                 Imagebyte = System.IO.File.ReadAllBytes(("wwwroot/Doctor" + a.DO_Photo)),
                                 DO_UserId_FK = a.DO_UserId_FK,
                                 DO_Village = a.DO_Village,
                                 DO_Alernative_Numb = a.DO_Alernative_Numb,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<List<Doctor_DD>> Doctor_DD(int SP_Id)
        {
            if (db != null)
            {
                var query = (from a in db.Doctor
                             join b in db.Specialization on a.DO_SP_Id_FK equals b.SP_Id
                             join c in db.Hospital on a.DO_HO_Id_FK equals c.Hos_Id
                             join d in db.Districts on a.DO_DI_Id_FK equals d.district_id
                             where a.DO_SP_Id_FK == SP_Id && 
                             a.delete_flag == false && a.status == 1
                             select new Doctor_DD
                             {
                                 DO_Id = a.DO_Id,
                                 DO_Name = string.Concat(a.DO_FirstName,a.DO_LastName),
                                 DO_Photo = a.DO_Photo,
                                 Sp_Name = b.SP_Specialization,
                                 Hos_Name = c.Hos_HospitalName,
                                 district = d.district_name,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
    }
}
