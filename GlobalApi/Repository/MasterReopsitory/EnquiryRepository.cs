using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using GlobalApi.Data;
using Microsoft.EntityFrameworkCore;
using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using Microsoft.Data.SqlClient;
using GlobalApi.Models.Authentication;
using Dapper;
using System.Linq;
using static Slapper.AutoMapper;
using System.Runtime.Intrinsics.Arm;

namespace GlobalApi.Repository.MasterRepository
{
    public class EnquiryRepository : EnquiryIRepository
    {

        private ADO_Configrations ado_Configurations;
        private readonly GlobalContext db;

        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        private IEMailService _EMailService;
        private readonly FileUpload fileUpload;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EnquiryRepository(IEMailService EMailService)
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


        public async Task<string> InsertEnquiry(Enquiry_details lead)
        {
            try
            {
                using (GlobalContext globalContext = new GlobalContext())
                {

                    if (lead.cus_eq_phoneNo == null)
                    {
                        return "MobileNumber cannot be Empty";
                    }

                    else if (lead.cus_eq_email == null)
                    {
                        return "Email cannot be Empty";
                    }

                     int cus_eq_id = await primarykeyvalue.primary_key("Customer_Enquiry");

                         Customer_Enquiry obj = new Customer_Enquiry()
                    {
                        cus_eq_id = cus_eq_id,
                        cus_eq_name = lead.cus_eq_name,
                        cus_eq_phoneNo = lead.cus_eq_phoneNo,
                        cus_eq_email = lead.cus_eq_email,
                        cus_eq_DOB = lead.cus_eq_DOB,
                        cus_eq_Address = lead.cus_eq_Address,
                        cus_eq_Postalcode = lead.cus_eq_Postalcode,
                        cus_eq_State = lead.cus_eq_State,
                        cus_eq_gender = lead.cus_eq_gender,
                        cus_eq_city = lead.cus_eq_city,
                        cus_Cm_Fk = lead.cus_Cm_Fk,
                        cus_eq_desc = lead.cus_eq_desc,
                        // cus_eq_type = lead.cus_eq_type,
                        created_date = DateTime.Now,
                        cus_eq_status = 1
                    };
                    var result = await globalContext.Customer_Enquiry.AddAsync(obj);
                    await globalContext.SaveChangesAsync();
                    foreach (StudentCourse cpt in lead.Student_Course)
                    {
                        int scId = await primarykeyvalue.primary_key("student_courses");
                        student_courses objCer = new student_courses()
                        {
                            Cou_Id = scId,
                            Stu_id_fk = cus_eq_id,
                            cp_id = cpt.cp_id,
                            created_by = 1,
                            created_date = DateTime.Now,
                            delete_flag = false,
                        };
                        var ComplaintResult = await globalContext.student_courses.AddAsync(objCer);
                        await globalContext.SaveChangesAsync();
                    }
                    await _EMailService.SendRegistrationEmailAsync_New("enquiry@neurospineoptica.com", obj.cus_eq_email, "Confirm your email", $"<h1>Dear {lead.cus_eq_name}, you've successfully submitted details to your LMS</h1>" +
                       $"<p>If you have any query Contact to  9886027182,8594033651</p>");
                   
                    return "Enquiry sended Successfully";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<GetAllEnquire_Type>> GetAllEnquire_Type()
        {
            var query = (from a in db.Enquire_Type
                         where a.enq_id != 0
                         orderby a.enq_id descending
                         select new GetAllEnquire_Type
                         {
                             enq_id = a.enq_id,
                             enq_name = a.enq_name,

                         });
            return await query.ToListAsync();
        }


        public async Task<List<GetAllEnquiry_details>> GetAllEnquiry_details()
        {
            var query = (from a in db.Customer_Enquiry
                         join n in db.Status on a.cus_eq_status equals n.sts_id into nlist
                         from n in nlist.DefaultIfEmpty()
                         join l in db.Course_Package on a.cus_Cm_Fk equals l.cp_id into llist
                         from l in llist.DefaultIfEmpty()
                         join o in db.States on a.cus_eq_State equals o.stat_id into olist
                         from o in olist.DefaultIfEmpty()
                             //wherea.cus_eq_id
                         orderby a.cus_eq_id ascending
                         select new GetAllEnquiry_details
                         {
                             cus_eq_id = a.cus_eq_id,
                             cus_eq_name = a.cus_eq_name,
                             cus_eq_phoneNo = a.cus_eq_phoneNo,
                             cus_eq_email = a.cus_eq_email,
                             cus_eq_DOB = a.cus_eq_DOB,
                             cus_eq_gender = a.cus_eq_gender,
                             cus_eq_Address = a.cus_eq_Address,
                             cus_eq_Postalcode = a.cus_eq_Postalcode,
                             cus_eq_State = a.cus_eq_State,
                             GetAllStudentCourse = (from k in db.student_courses
                                                    join v in db.Course_Package on k.cp_id equals v.cp_id
                                                    where k.Stu_id_fk == a.cus_eq_id
                                                    select new GetAllStudentCourse
                                                    {
                                                        Cou_Id = k.Cou_Id,
                                                        cp_id = k.cp_id,
                                                        cu_name = v.cu_name,
                                                        cp_amount = v.cp_amount

                                                    }).ToList(),
                             cus_eq_city = a.cus_eq_city,
                             //cus_eq_inv = a.cus_eq_inv,
                             St_Cm_Name=l.cu_name,
                             cus_eq_desc = a.cus_eq_desc,
                             cus_eq_status = a.cus_eq_status,
                             St_Cm_Fk = a.cus_Cm_Fk,
                             state_name = o.state_name,
                             sts_name = n.sts_name,
                         });
            return await query.ToListAsync();
        }


        public async Task<List<GetAllEnquiry_detailsById>> GetAllEnquiry_detailsById(string cus_eq_phoneNo)
        {
            var query = (from a in db.Customer_Enquiry
                         join n in db.Status on a.cus_eq_status equals n.sts_id into nlist
                         from n in nlist.DefaultIfEmpty()
                         join l in db.Course_Package on a.cus_Cm_Fk equals l.cp_id into llist
                         from l in llist.DefaultIfEmpty()
                         join o in db.States on a.cus_eq_State equals o.stat_id into olist
                         from o in olist.DefaultIfEmpty()
                         where a.cus_eq_phoneNo == cus_eq_phoneNo
                         select new GetAllEnquiry_detailsById
                         {
                             cus_eq_name = a.cus_eq_name,
                             cus_eq_phoneNo = a.cus_eq_phoneNo,
                             cus_eq_email = a.cus_eq_email,
                             cus_eq_DOB = a.cus_eq_DOB,
                             cus_eq_gender = a.cus_eq_gender,
                             cus_eq_Address = a.cus_eq_Address,
                             cus_eq_Postalcode = a.cus_eq_Postalcode,
                             cus_eq_State = a.cus_eq_State,
                             GetAllStudentCourse = (from k in db.student_courses
                                                    join v in db.Course_Package on k.cp_id equals v.cp_id
                                                    where k.Stu_id_fk == a.cus_eq_id
                                                    select new GetAllStudentCourse
                                                    {
                                                        Cou_Id = k.Cou_Id,
                                                        cp_id = k.cp_id,
                                                        cu_name = v.cu_name,
                                                        cp_amount = v.cp_amount
                                                    }).ToList(),
                             cus_eq_city = a.cus_eq_city,
                             St_Cm_Name = l.cu_name,
                             //cus_eq_inv = a.cus_eq_inv,
                             cus_eq_desc = a.cus_eq_desc,
                             cus_eq_status = a.cus_eq_status,
                             St_Cm_Fk = a.cus_Cm_Fk,
                             state_name = o.state_name,
                             sts_name = n.sts_name,
                         });
            return await query.ToListAsync();
        }


        public async Task<List<GetAllEnquiry_detailsById>> GetAllEnquiryInformation()
        {
            var query = (from a in db.Customer_Enquiry
                         where a.cus_eq_id != 0
                         orderby a.cus_eq_id ascending
                         select new GetAllEnquiry_detailsById
                         {
                             cus_eq_name = a.cus_eq_name,
                             cus_eq_phoneNo = a.cus_eq_phoneNo,
                             cus_eq_email = a.cus_eq_email,
                             cus_eq_DOB = a.cus_eq_DOB,
                             cus_eq_gender = a.cus_eq_gender,
                             cus_eq_Address = a.cus_eq_Address,
                             cus_eq_Postalcode = a.cus_eq_Postalcode,
                             cus_eq_State = a.cus_eq_State,
                             cus_eq_city = a.cus_eq_city,
                             //cus_eq_inv = a.cus_eq_inv,
                             cus_eq_desc = a.cus_eq_desc,

                         });
            return await query.ToListAsync();
        }

        public async Task<string> DeleteEnquiry(int id)
        {
            try
            {
                var result = await db.Customer_Enquiry.FirstOrDefaultAsync(x => x.cus_eq_id == id);
                if (result != null)
                {
                    result.cus_eq_id = id;
                    result.cus_eq_status = 6;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "Successfully Deleted";
                }
                return "Not Successfull";
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}
