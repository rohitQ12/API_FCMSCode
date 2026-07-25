using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using static GlobalApi.Models.Master.GetAllCourse_Section;

namespace GlobalApi.Repository.MasterReopsitory
{
    public class CourseMasterRepository : Course_MasterIRepository
    {
        private readonly GlobalContext db;
        private ADO_Configrations ado_Configurations;
        private IPrimarykeyvalue primarykeyvalue;

        private readonly IConfigurationRoot configurationRoot = null!;
        public CourseMasterRepository()
        {
            db = new GlobalContext();
            ado_Configurations = new ADO_Configrations();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }

        public async Task<string> InsertCourse_Master(InsertCourse_Master insertCourse_Master)
        {
            try
            {

                //if (insertCourse_Section.sc_name == null)
                //{
                //    return "Section name Already Exists";
                //}

                int id = await primarykeyvalue.primary_key("Course_Master");
                Course_Master obj = new Course_Master()
                {
                    cu_id = id,
                    cu_name = insertCourse_Master.cu_name,
                    cu_sc_id_fk = insertCourse_Master.cu_sc_id_fk,
                    cu_author = insertCourse_Master.cu_author,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Course_Master.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Course name Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> UpdateCourse_Master(UpdateCourse_Master updateCourse_Master)
        {
            try
            {
                var result = await db.Course_Master.FirstOrDefaultAsync(x => x.cu_id == updateCourse_Master.cu_id);

                if (result != null)
                {
                    result.cu_id = updateCourse_Master.cu_id;
                    result.cu_name = updateCourse_Master.cu_name;
                    result.cu_author = updateCourse_Master.cu_author;
                    result.cu_sc_id_fk = updateCourse_Master.cu_sc_id_fk;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return "Course Updated Successfully";
                }
                return "Course Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllCourse_Master>> GetAllCourse_Master()
        {
            try
            {
                var query = (from a in db.Course_Master
                             join b in db.Course_Section on a.cu_sc_id_fk equals b.sc_id
                             join c in db.Course_Chapters on b.sc_ch_Fk equals c.ch_id
                             join d in db.Upload_Videos on c.ch_vi_FK equals d.vi_id
                             join e in db.Status on a.status equals e.sts_id
                             where a.cu_id != 0
                             select new GetAllCourse_Master
                             {
                                 cu_id = a.cu_id,
                                 cu_name = a.cu_name,
                                 ch_id = c.ch_id,
                                 ch_name = c.ch_name,
                                 sc_id = b.sc_id,
                                 sc_name = b.sc_name,
                                 cu_author = a.cu_author,
                                 vi_id = d.vi_id,
                                 vi_name = d.vi_name,
                                 vi_amount = d.vi_amount,
                                 ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                 vi_time = d.vi_time,
                                 status = a.status,
                                 sts_name = e.sts_name,

                             });
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public async Task<List<GetCourse_MasterById>> GetCourse_MasterById(string cu_name)
        {
            try
            {

                var query = (from a in db.Course_Master
                             join b in db.Course_Section on a.cu_sc_id_fk equals b.sc_id
                             join c in db.Course_Chapters on b.sc_ch_Fk equals c.ch_id
                             join d in db.Upload_Videos on c.ch_vi_FK equals d.vi_id
                             join e in db.Status on a.status equals e.sts_id
                             where a.cu_id != 0 && a.cu_name == cu_name && a.status != 6
                             select new GetCourse_MasterById
                             {
                                 cu_id = a.cu_id,
                                 cu_name = a.cu_name,
                                 ch_id = c.ch_id,
                                 ch_name = c.ch_name,
                                 sc_id = b.sc_id,
                                 sc_name = b.sc_name,
                                 cu_author = a.cu_author,
                                 vi_id = d.vi_id,
                                 vi_name = d.vi_name,
                                 vi_amount = d.vi_amount,
                                 vi_time = d.vi_time,
                                 ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
                                 status = a.status,
                                 sts_name = e.sts_name,
                             });


                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }













        //public async Task<List<GetCourse_MasterById> GetCourse_MasterById(string cu_name)

        //{
        //    try
        //    {
        //        var query = (from a in db.Course_Master
        //                     join b in db.Course_Section on a.cu_sc_id_fk equals b.sc_id
        //                     join c in db.Course_Chapters on b.sc_ch_Fk equals c.ch_id
        //                     join d in db.Upload_Videos on c.ch_vi_FK equals d.vi_id
        //                     join e in db.Status on a.status equals e.sts_id
        //                     where a.cu_id != 0 && a.cu_name == cu_name
        //                     select new GetCourse_MasterById
        //                     {
        //                         cu_id = a.cu_id,
        //                         cu_name = a.cu_name,
        //                         ch_id = c.ch_id,
        //                         ch_name = c.ch_name,
        //                         sc_id = b.sc_id,
        //                         sc_name = b.sc_name,
        //                         vi_id = d.vi_id,
        //                         vi_name = d.vi_name,
        //                         vi_amount = d.vi_amount,
        //                         ch_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_url,
        //                         vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + d.vi_file,
        //                         status = a.status,
        //                         sts_name = e.sts_name,
        //                     });
        //        return await query.ToListAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public async Task<List<GetCourse_Master_DD>> GetCourse_Master_DD()
        {
            var query = (from a in db.Course_Master
                         where a.delete_flag == false && a.status == 3
                         && a.cu_id != 0
                         orderby a.cu_id
                         select new GetCourse_Master_DD
                         {
                             cu_id = a.cu_id,
                             sc_name = a.cu_name,
                         }).ToListAsync();
            return await query;
        }

        public async Task<string> DeleteCourse_MasterById(int cu_Id)
        {
            try
            {
                var result = await db.Course_Master.FirstOrDefaultAsync(x => x.cu_id == cu_Id);
                if (result != null)
                {
                    result.cu_id = cu_Id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Course Deleted Successfully";
                }
                return "Course Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> ApproveCourse_MasterById(ApproveCourse_Master ApproveCourse_Master)
        {
            try
            {
                var result = await db.Course_Master.Where(x => x.cu_id == ApproveCourse_Master.cu_id).FirstOrDefaultAsync();

                if (result != null)
                {
                    result.status = 3; 
                    await db.SaveChangesAsync();
                    return "Course Approved Successfully";
                }
                return "Course Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }





        public async Task<List<GetCourDD>> GetCoursesDD()
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
                {
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetCoursesDD", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        var response = new List<GetCourDD>();
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(GetAllCouDD(reader));
                            }
                        }
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GetCourDD GetAllCouDD(Microsoft.Data.SqlClient.SqlDataReader reader)
        {
            return new GetCourDD()
            {
                cu_name = Convert.ToString(reader["cu_name"]),
                tot_vi_amount = Convert.ToDecimal(reader["tot_vi_amount"]),
                total_time = Convert.ToString(reader["total_time"]),
                cu_author = Convert.ToString(reader["cp_Author"]),


            };
        }

        public async Task<List<GetSecDD>> GetSecDD()
        {
            try
            {

                var query = (from a in db.Course_Section
                             join b in db.Course_Chapters on a.sc_ch_Fk equals b.ch_id
                             join c in db.Upload_Videos on b.ch_vi_FK equals c.vi_id
                             join d in db.Course_Master on a.sc_id equals d.cu_sc_id_fk
                             join e in db.Course_Package on d.cu_name equals e.cu_name
                             where a.sc_id != 0 && a.status != 6
                             group new { a, c,e } by new {  a.sc_name } into grouped

                             select new GetSecDD
                             {
                                 cs_id = grouped.Min(g => g.a.sc_id),
                                 sc_name = grouped.Min(g => g.a.sc_name),
                                 cu_author = grouped.Min(g => g.e.cp_Author),
                                 total_time = grouped.Min(g => g.c.vi_time),
                                 tot_vi_amount = grouped.Max(g => g.c.vi_amount),

                             }).ToListAsync();
                return await query;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
        }

        public async Task<List<GetSecDD>> GetSecById(int sec_Id)
        {
            try
            {
                var query = (from a in db.Course_Section
                             join b in db.Course_Chapters on a.sc_ch_Fk equals b.ch_id
                             join c in db.Upload_Videos on b.ch_vi_FK equals c.vi_id
                             join d in db.Course_Master on a.sc_id equals d.cu_sc_id_fk
                             join e in db.Course_Package on d.cu_name equals e.cu_name
                             where a.sc_id == sec_Id && a.status !=6
                             group new { a, c, e } by new { a.sc_name } into grouped
                             select new GetSecDD
                             {
                                 cs_id = grouped.Min(g => g.a.sc_id),
                                 sc_name = grouped.Min(g => g.a.sc_name),
                                 cu_author = grouped.Min(g => g.e.cp_Author),
                                 total_time = grouped.Min(g => g.c.vi_time),
                                 tot_vi_amount = grouped.Max(g => g.c.vi_amount),
                             }).ToListAsync();
                return await query;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<GetBothDD>> GetAllPackDD()
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection sql = ado_Configurations.connection())
                {
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand("GetAllPackDD", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        var response = new List<GetBothDD>();
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(GetAllPackDD(reader));
                            }
                        }
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GetBothDD GetAllPackDD(Microsoft.Data.SqlClient.SqlDataReader reader)
        {
            return new GetBothDD()
            {
                //cu_name = Convert.ToString(reader["cu_name"]),
                //Amount = Convert.ToDecimal(reader["TotalAmount"]),
                //Duration = Convert.ToString(reader["Duration"]),
                //cp_id = Convert.ToInt32(reader["cp_id"]),

                cu_name = reader["cu_name"] != DBNull.Value ? Convert.ToString(reader["cu_name"]) : string.Empty,
                Amount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"]) : 0,
                Duration = reader["Duration"] != DBNull.Value ? Convert.ToString(reader["Duration"]) : string.Empty,
                //cp_id = reader["cp_id"] != DBNull.Value ? Convert.ToInt32(reader["cp_id"]) : 0  // cp_id is now string to match STRING_AGG result


            };
        }
    }
}
