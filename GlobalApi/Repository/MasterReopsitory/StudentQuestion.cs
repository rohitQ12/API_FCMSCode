using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using static Slapper.AutoMapper;

namespace GlobalApi.Repository.MasterReopsitory
{
    public class StudentQuestion : IStudentQuestion
    {
        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;

        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private IEMailService _EMailService;
        private readonly FileUpload fileUpload;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentQuestion(IEMailService EMailService)
        {

            ado_Configurations = new ADO_Configrations();
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
            fileUpload = new FileUpload();
            this._EMailService = EMailService;
        }
        public async Task<string> DeleteStuQue(int id)
        {
            try
            {
                var result = await db.Student_Enquiry.FirstOrDefaultAsync(x => x.Id == id);

                if (result != null)
                {
                    result.Id = id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Queries Deleted Successfully";
                }
                return "Queries Details Does Not Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<StuqueGetAll>> GetAll_StuQueById(string cus_eq_phoneNo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StuqueGetAll>> GetAll_StuQue()
        {
            var query = (from a in db.Student_Enquiry
                         join b in db.Status on a.status equals b.sts_id
                         where a.Id != 0 && a.status != 6
                         orderby a.Id descending
                         select new StuqueGetAll
                         {
                             Id = a.Id,
                             Ind_Name = a.Ind_Name,
                             Stu_Name = a.Stu_Name,
                             Stu_Email = a.Stu_Email,
                             Stu_phoneNumber = a.Stu_phoneNumber,
                             Stu_Que = a.Stu_Que,
                             Sts_Details=b.sts_name

                         });
            return await query.ToListAsync();
        }

        public async Task<string> InsertStuQue(StuqueInsert lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {


                    Student_Enquiry obj = new Student_Enquiry()
                    {
                        Stu_Name = lead.Stu_Name,
                        Ind_Name = lead.Ind_Name,
                        Stu_phoneNumber = lead.Stu_phoneNumber,
                        Stu_Email = lead.Stu_Email,
                        Stu_Que = lead.Stu_Que,
                        created_date = DateTime.Now,
                        delete_flag = false,
                        status = 12
                    };
                    var result = await db.Student_Enquiry.AddAsync(obj);
                    if (result != null)
                    {
                        await db.SaveChangesAsync();

                        return "Question Added Successfully";
                    }
                //    var emailResult = await SendStuQue(new StuQueries
                //    {
                //        Stu_Name = obj.Stu_Name,
                //        Stu_Email = obj.Stu_Email,
                //        Stu_Que = obj.Stu_Que,
                //        Stu_Ans = obj.Stu_Ans
                //    // Include any additional details required for the email
                //});
                //    if (emailResult == "Mail sent successfully")
                  
                }
                return "Question Added Failed";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> SendStuQue(StuQueries lead)
        {

            try
            {
                var StuQue = await db.Student_Enquiry.FirstOrDefaultAsync(x => x.Id == lead.Id);
                if (StuQue != null)
                {
                    StuQue.Id = lead.Id;
                    //StuQue.Stu_id_fk = StuQue.Stu_id_fk;
                    //StuQue.Stu_Name = lead.Stu_Name;
                    //StuQue.Ind_Name = lead.Ind_Name;
                    //StuQue.Stu_phoneNumber = lead.Stu_phoneNumber;
                    //StuQue.Stu_Email = lead.Stu_Email;
                    //StuQue.Stu_Que = lead.Stu_Que;
                    StuQue.Stu_Ans = lead.Stu_Ans;
                    StuQue.modified_by = 1;
                    StuQue.modified_date = DateTime.Now;
                    StuQue.delete_flag = false;
                    StuQue.status = 11;

                    await db.SaveChangesAsync();
                    //return "Queries Updated Successfully";
                }

                //here mail sending
                await _EMailService.SendStuEmailAsync(lead.Stu_Email, "enquiry@neurospineoptica.com", "Confirm your email", $"<h4>Dear {lead.Stu_Name}, you've successfully submitted details to our LMS</h4>" +
               $"<p1>Student Questions: {lead.Stu_Que} </p1>");

                await _EMailService.SendStuEmailAsync("enquiry@neurospineoptica.com", lead.Stu_Email,  "Confirm your email", $"<h4>Dear {lead.Stu_Name}, Thank you for reaching our LMS. I hope this email finds you well.</h4>" +
             $"<p1>Student Questions: {lead.Stu_Que} <br>  Answers: {lead.Stu_Ans}</p1>");


                return "Mail sent successfully";
            }
            catch (Exception ex)
            {

                return "Mail sending failed:" +ex.ToString();
            }
            //student question update
           

        }


        public async Task<string> UpdateStuQue(StuqueUpdate lead)
        {
            try
            {
                var StuQue = await db.Student_Enquiry.FirstOrDefaultAsync(x => x.Id == lead.Id);


                if (StuQue != null)
                {
                    StuQue.Id = lead.Id;
                    StuQue.Stu_id_fk = StuQue.Stu_id_fk;
                    StuQue.Stu_Name = lead.Stu_Name;
                    StuQue.Ind_Name = lead.Ind_Name;
                    StuQue.Stu_phoneNumber = lead.Stu_phoneNumber;
                    StuQue.Stu_Email = lead.Stu_Email;
                    StuQue.Stu_Que = lead.Stu_Que;
                    //StuQue.Stu_Ans = lead.Stu_Ans;
                    StuQue.modified_by = 1;
                    StuQue.modified_date = DateTime.Now;
                    StuQue.delete_flag = false;
                    StuQue.status = 2;

                    await db.SaveChangesAsync();
                    return "Queries Updated Successfully";
                }
                return "Queries Didn't Exists";

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
