using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GlobalApi.Repository.MasterRepository
{
    public class DoctorRepository : IDoctor
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private DoctorLanguageRepository doctorLanguageRepository;
        private IPrimarykeyvalue primarykeyvalue;
        public DoctorRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            this.doctorLanguageRepository = new DoctorLanguageRepository();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Doctor> InsertDoctor(Doctor_Images lead)
        {
            try
            {
                var getdocpkId = (from a in db.DocPkValue where a.PkName == "Doctor" select a.PkId).FirstOrDefault();
                var getpresentval = (from a in db.DocPkValue where a.PkName == "Doctor" select a.PkPresentValue).FirstOrDefault();
                var strvoucherno = await PkIdAutomaicGeneration_test(getdocpkId, "Doctor", getpresentval);
                var deptno = strvoucherno.automaticgen_patid;
                var strinvoiceno = await GetSuffixPrefixDetails(getdocpkId);
                var strprefix = strinvoiceno.Prefix;
                var year = Convert.ToString(DateTime.Now.Year);

                int id = await primarykeyvalue.primary_key("Doctor");
                string uniqueFilename = lead.DO_Photo != null ? ProcessUploadedFile(lead) : "user-1633249__340 (1).png";
                
                Doctor obj = new Doctor()
                {
                    DO_Id = id,
                    DO_RegNo = year + strprefix + deptno,
                    DO_Code = lead.DO_Code,
                    DO_FirstName = lead.DO_FirstName,
                    DO_LastName = lead.DO_LastName,
                    DO_DOB = lead.DO_DOB,
                    DO_Gender = lead.DO_Gender,
                    DO_MotherTongue = lead.DO_MotherTongue,
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
                    PANno = lead.PANno,
                    GSTno = lead.GSTno,
                    Regno = lead.Regno,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Doctor.AddAsync(obj);
                await db.SaveChangesAsync();
                //var dlang = await doctorLanguageRepository.InsertDoctorLanguage(lead.DoctorLanguage,id);
                await InsertUsers(obj);
                return result.Entity;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<get_Patidautomatic> PkIdAutomaicGeneration_test(int PkId, string tab_name, decimal txtBox)
        {
            try
            {
                Microsoft.Data.SqlClient.SqlConnection sql;
                Microsoft.Data.SqlClient.SqlCommand cmd;
                using (sql = ado_Configurations.connection())
                {
                    cmd = new Microsoft.Data.SqlClient.SqlCommand("PkIdAutomaicGeneration", sql);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@PkId", PkId));
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@tab_name", tab_name));
                    cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@txtBox", txtBox));
                    await sql.OpenAsync();
                    var rdr = await cmd.ExecuteScalarAsync();
                    get_Patidautomatic automicpatid = new get_Patidautomatic();
                    automicpatid.automaticgen_patid = Convert.ToString(rdr);
                    return automicpatid;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<viewdetail_suffixprefix> GetSuffixPrefixDetails(int DocPkTblId)
        {
            DataSet ds = new DataSet();
            //SqlConnection sql = new SqlConnection(ado_Configurations.connection());
            var sql = ado_Configurations.connection();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetSuffixPrefixDetails", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@DocPkTblId", DocPkTblId));
            await sql.OpenAsync();
            da.SelectCommand = cmd;
            await cmd.ExecuteNonQueryAsync();
            da.Fill(ds);

            viewdetail_suffixprefix viewdetailsufpref = new viewdetail_suffixprefix();
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    viewdetailsufpref.SuffixprefixId = Convert.ToInt32(dr["SuffixprefixId"]);
                    viewdetailsufpref.DocPkTblId = Convert.ToInt32(dr["DocPkTblId"]);
                    viewdetailsufpref.StartIndex = Convert.ToDecimal(dr["StartIndex"]);
                    viewdetailsufpref.Prefix = Convert.ToString(dr["Prefix"]);
                    viewdetailsufpref.Suffix = Convert.ToString(dr["Suffix"]);
                    viewdetailsufpref.WidthOfNumericalPart = Convert.ToInt32(dr["WidthOfNumericalPart"]);
                    viewdetailsufpref.PrefillWithZero = Convert.ToBoolean(dr["PrefillWithZero"]);
                }
            }
            return viewdetailsufpref;
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
        private string ProcessUploadedFileUP(Doctor_ImagesUP model)
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


        public async Task<Doctor> UpdateDoctor(Doctor_ImagesUP lead)
        {
            try
            {
                var result = await db.Doctor.FirstOrDefaultAsync(x => x.DO_Id == lead.DO_Id);
                
                //Update DoctorRegistration logo
                string uniqueFilename = lead.DO_Photo != null ? ProcessUploadedFileUP(lead): result.DO_Photo;

                if (result != null)
                { 
                    
                    result.DO_Id = lead.DO_Id;
                    result.DO_Code = lead.DO_Code;
                    result.DO_FirstName = lead.DO_FirstName;
                    result.DO_LastName = lead.DO_LastName;
                    result.DO_DOB = lead.DO_DOB;
                    result.DO_Gender = lead.DO_Gender;
                    result.DO_MotherTongue = lead.DO_MotherTongue;
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
                    result.PANno = lead.PANno;
                    result.GSTno = lead.GSTno;
                    result.Regno = lead.Regno;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    //var dlang = await doctorLanguageRepository.UpdateDoctorLanguage(lead.DoctorLanguage, lead.DO_Id);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllDoctor>> GetAllDoctor(int? DO_HO_Id_FK, string roleaction)
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Doctor
                                 join b in db.States on a.DO_ST_Id_FK equals b.stat_id into blist
                                 from b in blist.DefaultIfEmpty()
                                 join c in db.Districts on a.DO_DI_Id_FK equals c.district_id into clist
                                 from c in clist.DefaultIfEmpty()
                                 join d in db.Hospital on a.DO_HO_Id_FK equals d.Hos_Id into dlist
                                 from d in dlist.DefaultIfEmpty()
                                 join e in db.Qualification on a.DO_QU_Id_FK equals e.qualification_id into elist
                                 from e in elist.DefaultIfEmpty()
                                 join f in db.Designation on a.DO_DE_Id_FK equals f.designation_id into flist
                                 from f in flist.DefaultIfEmpty()
                                 join g in db.Discipline on a.DO_CD_Id_FK equals g.CD_Id into glist
                                 from g in glist.DefaultIfEmpty()
                                 join h in db.Specialization on a.DO_SP_Id_FK equals h.SP_Id into hlist
                                 from h in hlist.DefaultIfEmpty()
                                 join i in db.Countries on a.DO_Country_Id_FK equals i.cntry_id into ilist
                                 from i in ilist.DefaultIfEmpty()
                                 join j in db.Taluk on a.DO_Taluk_Id equals j.Taluk_id into jlist 
                                 from j in jlist.DefaultIfEmpty()
                                 join k in db.Gram on a.DO_Gram_Id equals k.Gram_id into klist
                                 from k in klist.DefaultIfEmpty()
                                 where roleaction == "Hospital" ? a.DO_HO_Id_FK == DO_HO_Id_FK : a.DO_Id > 0
                                 join l in db.Language_MST on a.DO_MotherTongue equals l.Id into llist
                                 from l in llist.DefaultIfEmpty()
                                 join m in db.Status on a.status equals m.sts_id
                                 where a.DO_Id != 0
                                 orderby a.DO_Id descending
                                 select new GetAllDoctor
                                 {
                                     DO_Id = a.DO_Id,
                                     DO_Code = a.DO_Code,
                                     DO_RegNo = a.DO_RegNo,
                                     DO_FirstName = a.DO_FirstName,
                                     DO_LastName = a.DO_LastName,
                                     DO_DOB = a.DO_DOB,
                                     DO_Gender = a.DO_Gender,
                                     DO_MotherTongue = a.DO_MotherTongue,
                                     Language = l.Language,
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
                                     Imagebyte =File.Exists("wwwroot/Doctor/" + a.DO_Photo) == true ?
                                                System.IO.File.ReadAllBytes("wwwroot/Doctor/" + a.DO_Photo) :
                                                System.IO.File.ReadAllBytes(("wwwroot/Doctor/" + "user-1633249__340 (1).png")),
                                     DO_UserId_FK = a.DO_UserId_FK,
                                     DO_Village = a.DO_Village,
                                     DO_Alernative_Numb = a.DO_Alernative_Numb,
                                     PANno = a.PANno,
                                     GSTno = a.GSTno,
                                     Regno = a.Regno,
                                     delete_flag = a.delete_flag,
                                     status = a.status,
                                     sts_name = m.sts_name,
                                     Remarks = a.Remarks,

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
                             join b in db.States on a.DO_ST_Id_FK equals b.stat_id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Districts on a.DO_DI_Id_FK equals c.district_id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Hospital on a.DO_HO_Id_FK equals d.Hos_Id into dlist
                             from d in dlist.DefaultIfEmpty()
                             join e in db.Qualification on a.DO_QU_Id_FK equals e.qualification_id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.Designation on a.DO_DE_Id_FK equals f.designation_id into flist
                             from f in flist.DefaultIfEmpty()
                             join g in db.Discipline on a.DO_CD_Id_FK equals g.CD_Id into glist
                             from g in glist.DefaultIfEmpty()
                             join h in db.Specialization on a.DO_SP_Id_FK equals h.SP_Id into hlist
                             from h in hlist.DefaultIfEmpty()
                             join i in db.Countries on a.DO_Country_Id_FK equals i.cntry_id into ilist
                             from i in ilist.DefaultIfEmpty()
                             join j in db.Taluk on a.DO_Taluk_Id equals j.Taluk_id into jlist
                             from j in jlist.DefaultIfEmpty()
                             join k in db.Gram on a.DO_Gram_Id equals k.Gram_id into klist
                             from k in klist.DefaultIfEmpty()
                             join l in db.Language_MST on a.DO_MotherTongue equals l.Id into llist
                             from l in llist.DefaultIfEmpty()
                             join m in db.Status on a.status equals m.sts_id
                             where a.DO_Id == DO_Id && a.DO_Id != 0
                             select new DoctorById
                             {
                                 DO_Id = a.DO_Id,
                                 DO_Code = a.DO_Code,
                                 DO_RegNo = a.DO_RegNo,
                                 DO_FirstName = a.DO_FirstName,
                                 DO_LastName = a.DO_LastName,
                                 DO_DOB = a.DO_DOB,
                                 DO_Gender = a.DO_Gender,
                                 DO_MotherTongue = a.DO_MotherTongue,
                                 Language = l.Language,
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
                                 Imagebyte = File.Exists("wwwroot/Doctor/" + a.DO_Photo) == true ?
                                                System.IO.File.ReadAllBytes("wwwroot/Doctor/" + a.DO_Photo) :
                                                System.IO.File.ReadAllBytes(("wwwroot/Doctor/" + "user-1633249__340 (1).png")),
                                 DO_UserId_FK = a.DO_UserId_FK,
                                 DO_Village = a.DO_Village,
                                 DO_Alernative_Numb = a.DO_Alernative_Numb,
                                 PANno = a.PANno,
                                 GSTno = a.GSTno,
                                 Regno = a.Regno,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = m.sts_name,
                                 Remarks = a.Remarks,
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
                             a.delete_flag == false && a.status != 6 && a.DO_Id != 0
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
        public async Task<string> ApproveDoctor(ApproveDoctor lead)
        {
            try
            {
                if(lead.DO_Id != 0)
                {
                    var result = await db.Doctor.Where(x => x.DO_Id == lead.DO_Id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.cntry_id = lead.cntry_id;
                        result.status = 3;
                        if (lead.Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = lead.Remarks;
                        await db.SaveChangesAsync();
                        return "Doctor is Approved";
                    }
                    else
                        return "Already Active";
                }
                else
                    return "Cannot Approve Default Doctor";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
