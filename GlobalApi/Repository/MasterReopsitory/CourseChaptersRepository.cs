
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace GlobalApi.Repository.MasterRepository
{
    public class CourseChaptersRepository : CourseChaptersIRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        public CourseChaptersRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }

        public async Task<string> InsertCourse_Chapters(InsertCourse_Chapters insertCourse_Chapters)
        {
            try
            {
               
                if (insertCourse_Chapters.ch_name == null)
                {
                    return "chapter name Already Exists";
                }
               
                int id = await primarykeyvalue.primary_key("Course_Chapters");
                Course_Chapters obj = new Course_Chapters()
                {
                    ch_id = id,
                    ch_name = insertCourse_Chapters.ch_name,
                    ch_vi_FK = insertCourse_Chapters.ch_vi_FK,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Course_Chapters.AddAsync(obj);
                await db.SaveChangesAsync();
                return "Chapter name Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<string> UpdateCourse_Chapters(UpdateCourse_Chapters updateCourse_Chapters)
        {
            try
            {
                var result = await db.Course_Chapters.FirstOrDefaultAsync(x => x.ch_id == updateCourse_Chapters.ch_id);

                if (result != null)
                {
                    result.ch_id = updateCourse_Chapters.ch_id;
                    result.ch_name = updateCourse_Chapters.ch_name;
                    result.ch_vi_FK = updateCourse_Chapters.ch_vi_FK;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return "Chapter Updated Successfully";
                }
                return "Chapter Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllCourse_Chapters>> GetAllCourse_Chapters()
        {
            try
            {
                var query = (from a in db.Course_Chapters
                             join b in db.Upload_Videos on a.ch_vi_FK equals b.vi_id
                             join c in db.Status on a.status equals c.sts_id
                             where a.ch_id != 0
                             select new GetAllCourse_Chapters
                            {
                                 ch_id = a.ch_id,
                                 ch_name = a.ch_name,
                                 ch_vi_FK=a.ch_vi_FK,
                                 vi_name=b.vi_name,
                                 vi_amount = b.vi_amount,
                                 vi_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + b.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + b.vi_file,
                                 status = a.status,
                                 sts_name = c.sts_name,
                             });
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        public async Task<List<GetCourseChaptersById>> GetCourseChaptersByName(string ch_name)
        {
            try
            {
                var query = (from a in db.Course_Chapters
                             join b in db.Upload_Videos on a.ch_vi_FK equals b.vi_id
                             join c in db.Status on a.status equals c.sts_id
                             where a.ch_id != 0 && a.ch_name == ch_name
                             select new GetCourseChaptersById
                             {
                                 ch_id = a.ch_id,
                                 ch_name = a.ch_name,
                                 ch_vi_FK = a.ch_vi_FK,
                                 vi_name = b.vi_name,
                                 vi_amount = b.vi_amount,
                                 vi_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + b.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + b.vi_file,
                                 status = a.status,
                                 sts_name = c.sts_name,
                             });
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        public async Task<GetCourseChaptersById> GetCourseChaptersById(int ch_id)
        {
            try
            {
                var query = (from a in db.Course_Chapters
                             join b in db.Upload_Videos on a.ch_vi_FK equals b.vi_id
                             join c in db.Status on a.status equals c.sts_id
                             where a.ch_id != 0 && a.ch_id == ch_id
                             select new GetCourseChaptersById
                             {
                                 ch_id = a.ch_id,
                                 ch_name = a.ch_name,
                                 vi_amount = b.vi_amount,
                                 vi_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + b.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + b.vi_file,
                                 status = a.status,
                                 sts_name = c.sts_name,
                             }).FirstOrDefaultAsync();
                  return await query;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetCourse_Chapters_DD>> GetCourse_Chapters_DD()
        {
            var query = (from a in db.Course_Chapters
                         join b in db.Upload_Videos on a.ch_vi_FK equals b.vi_id
                         join c in db.Status on a.status equals c.sts_id
                         where a.delete_flag == false && a.status == 3
                         && a.ch_id != 0
                         orderby a.ch_id
                         select new GetCourse_Chapters_DD
                         {
                             ch_id = a.ch_id,
                             ch_name = a.ch_name +"("+ b.vi_name +")" 
                         }).ToListAsync();
            return await query;
        }

        public async Task<string> DeleteCourse_ChaptersById(int ch_id)
        {
            try
            {
                var result = await db.Course_Chapters.FirstOrDefaultAsync(x => x.ch_id == ch_id);
                if (result != null)
                {
                    result.ch_id = ch_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Chapter Deleted Successfully";
                }
                return "Chapter Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> ApproveCourse_ChaptersById(ApproveCourse_Chapters approveCourse_Chapters)
        {
            try
            {
                var result = await db.Course_Chapters.Where(x => x.ch_id == approveCourse_Chapters.ch_id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.status = 3;
                    await db.SaveChangesAsync();
                    return "Chapter Approved Successfully";
                }
                return "Chapter Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
