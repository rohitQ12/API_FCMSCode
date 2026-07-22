using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using static GlobalApi.Models.Master.vedio_Documents;


namespace GlobalApi.Repository.MasterRepository
{
    public class UploadVideosRepository : UploadVideosIRepository
    {
        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private ADO_Configrations ado_Configurations;
        private readonly IConfigurationRoot configurationRoot = null!;
        private readonly HttpClient _httpClient;
        private readonly FileUpload fileUpload;

        public UploadVideosRepository()
        {
            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            fileUpload = new FileUpload();
            _httpClient = new HttpClient();
        }

       
        public async Task<string> InsertCourseVideos(vedio_Documents fileupload)
        {
            try
            {
                
                var duplicate = await db.Upload_Videos.FirstOrDefaultAsync(x => x.vi_name == fileupload.vi_name);
                if (duplicate == null)
                {

                    
                    int id = await primarykeyvalue.primary_key("Upload_Videos");
                    string vedioPath = this.fileUpload.ProcessUploadedFile("wwwroot/Upload_vedioes", fileupload.vi_vedio);
                    string filePath = this.fileUpload.ProcessUploadedFile("wwwroot/Upload_vedioes", fileupload.vi_file);



                    Upload_Videos obj = new Upload_Videos()
                    {
                        vi_id = id,
                        vi_name = fileupload.vi_name,
                        vi_url = vedioPath,
                        vi_file = filePath,
                        vi_time = fileupload.vi_time,
                        vi_amount = fileupload.vi_amount,
                        created_by = 1,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 1
                    };
                    var result = await db.Upload_Videos.AddAsync(obj);
                    await db.SaveChangesAsync();
                    return "File uploaded successfully";
                }
                else
                    return "Video name already present";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllVedio_Documents>> GetAllVideo_Documents()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Upload_Videos
                                 where a.status != 6
                                 orderby a.vi_id descending
                                 select new GetAllVedio_Documents
                                 {
                                     vi_id = a.vi_id,
                                     vi_name = a.vi_name,
                                     vi_time = a.vi_time,
                                     vi_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + a.vi_url,
                                     vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + a.vi_file,
                                     status = a.status,
                                     vi_amount = a.vi_amount,
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

        public async Task<GetAllVedio_DocumentsById> GetAllVideo_DocumentsById(int vi_id)
        {
            
                var query = (from a in db.Upload_Videos
                             where a.status != 6 && a.vi_id == vi_id
                             orderby a.vi_id descending
                             select new GetAllVedio_DocumentsById
                             {
                                 vi_id = a.vi_id,
                                 vi_name = a.vi_name,
                                 vi_time = a.vi_time,
                                 vi_url = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + a.vi_url,
                                 vi_file = configurationRoot.GetSection("AppUrl").Value + "Upload_vedioes/" + a.vi_file,
                                 status = a.status,
                                 vi_amount = a.vi_amount,
                             }).FirstOrDefaultAsync();
                return await query;
        }


        public async Task<string> DeleteVideo_DocumentsById(int vi_id)
        {
            try
            {
                var result = await db.Upload_Videos.FirstOrDefaultAsync(x => x.vi_id == vi_id);
                if (result != null)
                {
                    result.vi_id = vi_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Video Deleted Successfully";
                }
                return "Video Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public async Task<string> UpdateCourse_Section(UpdateCourse_Section updateCourse_Section)
        {
            try
            {
                var result = await db.Course_Section.FirstOrDefaultAsync(x => x.sc_id == updateCourse_Section.sc_id);

                if (result != null)
                {
                    result.sc_id = updateCourse_Section.sc_id;
                    result.sc_name = updateCourse_Section.sc_name;
                    result.sc_ch_Fk = updateCourse_Section.sc_ch_Fk;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return "Section Updated Successfully";
                }
                return "Section Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> UpdateVideo_DocumentsById(UpdateVedio_DocumentsById lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {
                    string Filename = string.Empty;
                    string DocumentFilename = string.Empty;
                    string DoctorqualificationDoc = string.Empty;
                    var objname = await globalContext.Upload_Videos.Where(x => x.vi_name == lead.vi_name).FirstOrDefaultAsync();
                    var objuser = await globalContext.Upload_Videos.Where(x => x.vi_id == lead.vi_id).FirstOrDefaultAsync();

                    if (objname != null)
                    {
                        return "Video name already exists.";
                    }

                    if (lead.vi_url != null)
                    {
                        if (objuser.vi_url != null)
                        {
                            string filepath = Path.Combine("wwwroot/Upload_vedioes", objuser.vi_url);
                            System.IO.File.Delete(filepath);
                        }
                        Filename = this.fileUpload.ProcessUploadedFile("wwwroot/Upload_vedioes", lead.vi_url);
                    }
                    else
                    {
                        Filename = objuser.vi_url;
                    }


                    if (lead.vi_file != null)
                    {
                        if (objuser.vi_url != null)
                        {
                            string filepath = Path.Combine("wwwroot/Upload_vedioes", objuser.vi_file);
                            System.IO.File.Delete(filepath);
                        }
                        DocumentFilename = this.fileUpload.ProcessUploadedFile("wwwroot/Upload_vedioes", lead.vi_file);
                    }
                    else
                    {
                        DocumentFilename = objuser.vi_file;
                    }

                    if (objuser != null)
                    {
                        objuser.vi_id = lead.vi_id;
                        objuser.vi_name = lead.vi_name;
                        objuser.vi_time = lead.vi_time;
                        objuser.modified_by = 1;
                        objuser.modified_date = DateTime.Now;
                        objuser.delete_flag = false;
                        objuser.status = 2;
                        await globalContext.SaveChangesAsync();
                        return "Video file successfully Updated";
                    }

                    return "Video Didn't Exists";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> ApproveVideo(Approvevedio_Documents approvevedio_Documents)
        {
            try
            {
                var result = await db.Upload_Videos.Where(x => x.vi_id == approvevedio_Documents.vi_id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.status = 3;
                    await db.SaveChangesAsync();
                    return "VideoUpload Approved Successfully";
                }
                return "VideoUpload Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }













    }
}
