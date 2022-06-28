using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GlobalApi.Repository.MasterRepository
{
    public class AssistantRepository : IAssistant
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        public AssistantRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
        }
        public async Task<Assistant> InsertAssistant(Assistant_Images lead,string UserId)
        {
            try
            {
                var getdocpkId = (from a in db.DocPkValue where a.PkName == "Assistant" select a.PkId).FirstOrDefault();
                var getpresentval = (from a in db.DocPkValue where a.PkName == "Assistant" select a.PkPresentValue).FirstOrDefault();
                //var strvoucherno = await PkIdAutomaicGeneration_test(1,"Branch",1);
                var strvoucherno = await PkIdAutomaicGeneration_test(getdocpkId, "Assistant", getpresentval);
                var deptno = strvoucherno.automaticgen_patid;
                //invoiceno with suffix and prefix//
                var strinvoiceno = await GetSuffixPrefixDetails(getdocpkId);
                var strprefix = strinvoiceno.Prefix;
                var year = Convert.ToString(DateTime.Now.Year);


                int id = await primarykeyvalue.primary_key("Assistant");
                string uniqueFilename = lead.Assi_Photo != null ? ProcessUploadedFile(lead) : "user-1633249__340 (1).png";
                Assistant obj = new Assistant()
                {
                    Assi_Id = id,
                    Asssi_UserID=UserId,
                    //Assi_code = "AS" + Convert.ToString(id),
                    Assi_code = "ASS"+Convert.ToString(id),
                    Assi_FirstName = lead.Assi_FirstName,
                    Assi_LastName = lead.Assi_LastName,
                    Assi_DOB = lead.Assi_DOB,
                    Assi_Gender = lead.Assi_Gender,
                    Assi_MotherTongue = lead.Assi_MotherTongue,
                    Assi_Hos_Id_FK = lead.Assi_Hos_Id_FK,
                    Assi_Qua_Id_FK = lead.Assi_Qua_Id_FK,
                    Assi_Des_Id_FK = lead.Assi_Des_Id_FK,
                    Assi_skill_id = lead.Assi_skill_id,
                    Assi_Photo = uniqueFilename,
                    Assi_Address = lead.Assi_Address,
                    Assi_Country_Id_FK = lead.Assi_Country_Id_FK,
                    Assi_ST_Id_FK = lead.Assi_ST_Id_FK,
                    Assi_DI_Id_FK = lead.Assi_DI_Id_FK,
                    taluk_Id_Fk = lead.taluk_Id_Fk,
                    gram_Id_Fk = lead.gram_Id_Fk,
                    //Assi_Village = lead.Assi_Village,
                    Assi_PostalCode = lead.Assi_PostalCode,
                    Assi_MobileNumber = lead.Assi_MobileNumber,
                    Assi_LandLineNumber = lead.Assi_LandLineNumber,
                    Assi_AlternativeNumber = lead.Assi_AlternativeNumber,
                    Assi_Email = lead.Assi_Email,
                    ASISfxPrfxId = year + strprefix + deptno,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Assistant.AddAsync(obj);
                await InsertUsers(obj);
                await db.SaveChangesAsync();
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
        public async Task<UsersLists> InsertUsers(Assistant lead)
        {
            int _id = await primarykeyvalue.primary_key("UsersLists");
            UsersLists insert = new UsersLists()
            {
                Id = _id,
                User_cat = "Assistant",
                User_ref_id = lead.Assi_Id,
                created_by = 1,
                created_date = DateTime.Now,
                delete_flag = false,
                status = 1,

            };
            var _new = await db.UsersLists.AddAsync(insert);
            await db.SaveChangesAsync();
            return _new.Entity;

        }
        private string ProcessUploadedFile(Assistant_Images model)
        {
            string uniqueFileName = null;


            if (model.Assi_Photo != null)
            {
                string uploadsFolder = Path.Combine("wwwroot/Assistant");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Assi_Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Assi_Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public async Task<Assistant> UpdateAssistant(Assistant_Images lead)
        {
            try
            {
                var result = await db.Assistant.FirstOrDefaultAsync(x => x.Assi_Id == lead.Assi_Id);
                if (lead.Assi_Photo != null)
                {
                    if (result.Assi_Photo != null && result.Assi_Photo != "user-1633249__340 (1).png")
                    {
                        string filepath = Path.Combine("wwwroot/Assistant", result.Assi_Photo);
                        System.IO.File.Delete(filepath);
                    }

                }

                string uniqueFilename = lead.Assi_Photo!=null?ProcessUploadedFile(lead): result.Assi_Photo;

                if (result != null)
                {
                    result.Assi_Id = lead.Assi_Id;
                    result.Assi_code = lead.Assi_code;
                    result.Assi_FirstName = lead.Assi_FirstName;
                    result.Assi_LastName = lead.Assi_LastName;
                    result.Assi_DOB = lead.Assi_DOB;
                    result.Assi_Gender = lead.Assi_Gender;
                    result.Assi_MotherTongue = lead.Assi_MotherTongue;
                    result.Assi_Hos_Id_FK = lead.Assi_Hos_Id_FK;
                    result.Assi_Qua_Id_FK = lead.Assi_Qua_Id_FK;
                    result.Assi_Des_Id_FK = lead.Assi_Des_Id_FK;
                    result.Assi_skill_id = lead.Assi_skill_id;
                    result.Assi_Photo = uniqueFilename;
                    result.Assi_Address = lead.Assi_Address;
                    result.Assi_Country_Id_FK = lead.Assi_Country_Id_FK;
                    result.Assi_ST_Id_FK = lead.Assi_ST_Id_FK;
                    result.Assi_DI_Id_FK = lead.Assi_DI_Id_FK;
                    result.taluk_Id_Fk = lead.taluk_Id_Fk;
                    result.gram_Id_Fk = lead.gram_Id_Fk;
                    //result.Assi_Village = lead.Assi_Village;
                    result.Assi_PostalCode = lead.Assi_PostalCode;
                    result.Assi_MobileNumber = lead.Assi_MobileNumber;
                    result.Assi_LandLineNumber = lead.Assi_LandLineNumber;
                    result.Assi_AlternativeNumber = lead.Assi_AlternativeNumber;
                    result.Assi_Email = lead.Assi_Email;
                    result.modified_by = 2;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
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
        public async Task<List<GetAllAssistant>> GetAllAssistant(int? Assi_Hos_Id_FK, string roleaction)
        {
            if (db != null)
            {
                var query = (from a in db.Assistant
                             join b in db.Hospital on a.Assi_Hos_Id_FK equals b.Hos_Id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Qualification on a.Assi_Qua_Id_FK equals c.qualification_id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Designation on a.Assi_Des_Id_FK equals d.designation_id into dlist
                             from d in dlist.DefaultIfEmpty()
                             join e in db.SkillSets on a.Assi_skill_id equals e.Skillset_id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.States on a.Assi_ST_Id_FK equals f.stat_id into flist
                             from f in flist.DefaultIfEmpty()
                             join g in db.Districts on a.Assi_DI_Id_FK equals g.district_id into glist
                             from g in glist.DefaultIfEmpty()
                             join h in db.Countries on a.Assi_Country_Id_FK equals h.cntry_id into hlist
                             from h in hlist.DefaultIfEmpty()
                             join i in db.Taluk on a.taluk_Id_Fk equals i.Taluk_id into ilist
                             from i in ilist.DefaultIfEmpty()
                             join j in db.Gram on a.gram_Id_Fk equals j.Gram_id into jlist
                             from j in jlist.DefaultIfEmpty()
                             join k in db.Language_MST on a.Assi_MotherTongue equals k.Id into klist
                             from k in klist.DefaultIfEmpty()
                             join l in db.Status on a.status equals l.sts_id
                             where a.Assi_Id != 0
                             where roleaction == "Hospital" ? a.Assi_Hos_Id_FK == Assi_Hos_Id_FK : a.Assi_Id > 0
                             orderby a.Assi_Id descending
                             select new GetAllAssistant
                             {
                                 Assi_Id = a.Assi_Id,
                                 Assi_code = a.Assi_code,
                                 Assi_FirstName = a.Assi_FirstName,
                                 Assi_LastName = a.Assi_LastName,
                                 Assi_DOB = a.Assi_DOB,
                                 Assi_Gender = a.Assi_Gender,
                                 Assi_MotherTongue = a.Assi_MotherTongue,
                                 Language = k.Language,
                                 Assi_Hos_Id_FK = a.Assi_Hos_Id_FK,
                                 Assi_Hos_HospitalName = b.Hos_HospitalName,
                                 Assi_Qua_Id_FK = a.Assi_Qua_Id_FK,
                                 Assi_qualification = c.qualification_Name,
                                 Assi_Des_Id_FK = a.Assi_Des_Id_FK,
                                 Assi_Designation = d.designation_desc,
                                 Assi_skill_id = a.Assi_skill_id,
                                 Assi_Skill = e.Skillset_name,
                                 Assi_Photo = a.Assi_Photo,
                                 Imagebyte = File.Exists("wwwroot/Assistant/" + a.Assi_Photo) == true ?
                                             System.IO.File.ReadAllBytes("wwwroot/Assistant/" + a.Assi_Photo) :
                                             System.IO.File.ReadAllBytes(("wwwroot/Assistant/" + "user-1633249__340 (1).png")),
                                 Assi_Address = a.Assi_Address,
                                 Assi_Country_Id_FK = a.Assi_Country_Id_FK,
                                 Assi_Country_name = h.country_name,
                                 Assi_ST_Id_FK = a.Assi_ST_Id_FK,
                                 state_name = f.state_name,
                                 Assi_DI_Id_FK = a.Assi_DI_Id_FK,
                                 district_name = g.district_name,
                                 taluk_Id_Fk = a.taluk_Id_Fk,
                                 taluk_name = i.Taluk_name,
                                 gram_Id_Fk = a.gram_Id_Fk,
                                 gram_name = j.Gram_name,
                                 //Assi_Village = a.Assi_Village,
                                 Assi_PostalCode = a.Assi_PostalCode,
                                 Assi_MobileNumber = a.Assi_MobileNumber,
                                 Assi_LandLineNumber = a.Assi_LandLineNumber,
                                 Assi_AlternativeNumber = a.Assi_AlternativeNumber,
                                 Assi_Email = a.Assi_Email,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = l.sts_name,
                             });
                return await query.ToListAsync();

            }
            return null;

        }

        public async Task<List<Assistant_DD>> GetAssistant_DD(int? Assi_Hos_Id_FK, string roleaction)
        {
            if (db != null)
            {
                var query = (from a in db.Assistant
                             where a.delete_flag == false && a.status == 1 && (roleaction == "Hospital" ? a.Assi_Hos_Id_FK == Assi_Hos_Id_FK : a.Assi_Id > 0)
                             select new Assistant_DD
                             {
                                 Assi_Id = a.Assi_Id,
                                 Assi_code = a.Assi_code,
                                 Assi_FirstName = a.Assi_FirstName,
                                 Assi_LastName = a.Assi_LastName,
                             }).ToListAsync();
                return await query;
            }
            return null;
        }
        public async Task<Assistant> DeleteAssistant(int Assi_Id)
        {
            try
            {
                var result = await db.Assistant.FirstOrDefaultAsync(x => x.Assi_Id == Assi_Id);
                if (result != null)
                {
                    result.Assi_Id = Assi_Id;
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
        public async Task<AssistantById> GetAssistantById(int Assi_Id,string roleaction)
        {
            if (db != null)
            {
                var query = (from a in db.Assistant
                             join b in db.Hospital on a.Assi_Hos_Id_FK equals b.Hos_Id into blist
                             from b in blist.DefaultIfEmpty()
                             join c in db.Qualification on a.Assi_Qua_Id_FK equals c.qualification_id into clist
                             from c in clist.DefaultIfEmpty()
                             join d in db.Designation on a.Assi_Des_Id_FK equals d.designation_id into dlist
                             from d in dlist.DefaultIfEmpty()
                             join e in db.SkillSets on a.Assi_skill_id equals e.Skillset_id into elist
                             from e in elist.DefaultIfEmpty()
                             join f in db.States on a.Assi_ST_Id_FK equals f.stat_id into flist
                             from f in flist.DefaultIfEmpty()
                             join g in db.Districts on a.Assi_DI_Id_FK equals g.district_id into glist
                             from g in glist.DefaultIfEmpty()
                             join h in db.Countries on a.Assi_Country_Id_FK equals h.cntry_id into hlist
                             from h in hlist.DefaultIfEmpty()
                             join i in db.Taluk on a.taluk_Id_Fk equals i.Taluk_id into ilist
                             from i in ilist.DefaultIfEmpty()
                             join j in db.Gram on a.gram_Id_Fk equals j.Gram_id into jlist
                             from j in jlist.DefaultIfEmpty()
                             join k in db.Language_MST on a.Assi_MotherTongue equals k.Id into klist
                             from k in klist.DefaultIfEmpty()
                             join l in db.Status on a.status equals l.sts_id
                             where a.Assi_Id == Assi_Id && a.Assi_Id != 0
                             where roleaction == "Hospital" ? a.Assi_Hos_Id_FK == Assi_Id : a.Assi_Id > 0
                             select new AssistantById
                             {
                                 Assi_Id = a.Assi_Id,
                                 Assi_code = a.Assi_code,
                                 Assi_FirstName = a.Assi_FirstName,
                                 Assi_LastName = a.Assi_LastName,
                                 Assi_DOB = a.Assi_DOB,
                                 Assi_Gender = a.Assi_Gender,
                                 Assi_MotherTongue = a.Assi_MotherTongue,
                                 Language = k.Language,
                                 Assi_Hos_Id_FK = a.Assi_Hos_Id_FK,
                                 Assi_Hos_HospitalName = b.Hos_HospitalName,
                                 Assi_Qua_Id_FK = a.Assi_Qua_Id_FK,
                                 Assi_qualification = c.qualification_Name,
                                 Assi_Des_Id_FK = a.Assi_Des_Id_FK,
                                 Assi_Designation = d.designation_desc,
                                 Assi_skill_id = a.Assi_skill_id,
                                 Assi_Skill = e.Skillset_name,
                                 Assi_Photo = a.Assi_Photo,
                                 Imagebyte = File.Exists("wwwroot/Assistant/" + a.Assi_Photo) == true ?
                                             System.IO.File.ReadAllBytes("wwwroot/Assistant/" + a.Assi_Photo) :
                                             System.IO.File.ReadAllBytes(("wwwroot/Assistant/" + "user-1633249__340 (1).png")),
                                 Assi_Address = a.Assi_Address,
                                 Assi_Country_Id_FK = a.Assi_Country_Id_FK,
                                 Assi_Country_name = h.country_name,
                                 Assi_ST_Id_FK = a.Assi_ST_Id_FK,
                                 state_name = f.state_name,
                                 Assi_DI_Id_FK = a.Assi_DI_Id_FK,
                                 district_name = g.district_name,
                                 taluk_Id_Fk = a.taluk_Id_Fk,
                                 taluk_name = i.Taluk_name,
                                 gram_Id_Fk = a.gram_Id_Fk,
                                 gram_name = j.Gram_name,
                                 //Assi_Village = a.Assi_Village,
                                 Assi_PostalCode = a.Assi_PostalCode,
                                 Assi_MobileNumber = a.Assi_MobileNumber,
                                 Assi_LandLineNumber = a.Assi_LandLineNumber,
                                 Assi_AlternativeNumber = a.Assi_AlternativeNumber,
                                 Assi_Email = a.Assi_Email,
                                 delete_flag = a.delete_flag,
                                 status = a.status,
                                 sts_name = l.sts_name
                             }).FirstOrDefaultAsync();
                return await query;
            }
            return null;
        }
        public async Task<string> ApproveAssistant(ApproveAssistant approveAssistant)
        {
            try
            {
                if(approveAssistant.Assi_Id != 0)
                {
                    var result = await db.Assistant.Where(x => x.Assi_Id == approveAssistant.Assi_Id).FirstOrDefaultAsync();
                    if (result.status != 3)
                    {
                        //result.Assi_Id = Assi_Id;
                        result.status = 3;
                        if (approveAssistant.Remarks == null)
                        {
                            result.Remarks = "OK";
                        }
                        else
                            result.Remarks = approveAssistant.Remarks;
                        await db.SaveChangesAsync();
                        return "Assistant is Approved";
                    }
                    else
                        return "Already Active";
                }
                else
                    return "Cannot Approve Default Assistant";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
