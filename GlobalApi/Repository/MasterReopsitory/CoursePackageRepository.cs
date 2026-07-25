using GlobalApi.Data;
using GlobalApi.GlobalClasses;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Slapper.AutoMapper;

namespace GlobalApi.Repository.MasterRepository
{
    public class CoursePackageRepository : CoursePackageIRepository
    {

        private readonly GlobalContext db;
        private IPrimarykeyvalue primarykeyvalue;
        private readonly IConfigurationRoot configurationRoot = null!;
        public CoursePackageRepository()
        {
            db = new GlobalContext();
            primarykeyvalue = new Primarykeyvalue();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }

        public async Task<string> InsertCourse_Pack(Course_Pack course_Pack)
        {
            try
            {

                //if (course_Pack.cu_name == null)
                //{
                //    return "CoursePack name cannot be null";
                //}

                int id = await primarykeyvalue.primary_key("Course_Package");
                Course_Package obj = new Course_Package()
                {
                    cp_id = id,
                    cu_name = course_Pack.cu_name,
                    //cp_se_id = course_Pack.cp_se_id,
                    //cp_ch_id=course_Pack.cp_ch_id,
                    cp_amount = course_Pack.cp_amount,
                    cp_Author = course_Pack.cp_Author,
                    // sec_name=course_Pack.sec_name,
                    cu_sc_id_fk = course_Pack.cu_sc_id_fk,
                    package_name = course_Pack.package_name,
                    Discount = course_Pack.Discount,
                    created_by = 1,
                    created_date = DateTime.Now,
                    delete_flag = false,
                    status = 1
                };
                var result = await db.Course_Package.AddAsync(obj);
                await db.SaveChangesAsync();
                return "CoursePack name Added Successfully";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> UpdateCourse_Pack(Course_Pack updateCourse_Pack)
        {
            try
            {
                var result = await db.Course_Package.FirstOrDefaultAsync(x => x.cp_id == updateCourse_Pack.cp_id);

                if (result != null)
                {
                    result.cp_id = updateCourse_Pack.cp_id;
                    result.cu_name = updateCourse_Pack.cu_name;
                    result.cu_sc_id_fk = updateCourse_Pack.cu_sc_id_fk;
                    //result.cp_ch_id = updateCourse_Pack.cp_ch_id;
                    result.cp_Author = updateCourse_Pack.cp_Author;
                    //result.sec_name = updateCourse_Pack.sec_name;
                    result.package_name = updateCourse_Pack.package_name;
                    result.Discount = updateCourse_Pack.Discount;
                    result.cp_amount = updateCourse_Pack.cp_amount;
                    result.modified_by = 1;
                    result.modified_date = DateTime.Now;
                    result.delete_flag = false;
                    result.status = 2;
                    await db.SaveChangesAsync();
                    return "CoursePack Updated Successfully";
                }
                return "CoursePack Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public async Task<List<GetAllCourse_Pack>> GetAllCourse_Pack()
        //{
        //    try
        //    {
        //        var query = (from a in db.Course_Package
        //                     join b in db.Course_Master on a.cu_name equals b.cu_name 
        //                     join c in db.Section on a.sec_name equals c.sc_name

        //                     // join d in db.Course_Chapters on c.sc_ch_Fk equals d.ch_id
        //                     join e in db.Status on a.status equals e.sts_id
        //                     where a.cp_id != 0 && a.status!=6  
        //                     group new { a, e ,b} by new { a.cp_Author, b.cu_name,a.cp_id}  into grouped
        //                     select new GetAllCourse_Pack
        //                     {
        //                         cu_name = grouped.Key.cu_name,
        //                         cp_Author = grouped.Key.cp_Author,
        //                         cp_id = grouped.Min(g => g.a.cp_id),
        //                         //cu_id = grouped.Min(g => g.b.cu_id),
        //                         cp_amount = grouped.Min(g => g.a.cp_amount),
        //                         sec_name = grouped.Min(g => g.a.sec_name),
        //                        // sc_name = grouped.Min(g => g.c.sc_name),
        //                        // cu_sc_id_fk = grouped.Min(g => g.a.cu_sc_id_fk),
        //                         package_name = grouped.Min(g => g.a.package_name),
        //                         Discount = grouped.Min(g => g.a.Discount),
        //                         status = grouped.Min(g => g.a.status),
        //                         sts_name = grouped.Min(g => g.e.sts_name),
        //                     });
        //        return await query.ToListAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public async Task<List<GetAllCourse_Pack>> GetAllCourse_Pack()
        {
            try
            {
                var query = (from a in db.Course_Package
                             join b in db.Course_Master on a.cu_name equals b.cu_name into courseMasterJoin
                             from b in courseMasterJoin.DefaultIfEmpty()
                             join c in db.Course_Section on a.cu_sc_id_fk equals c.sc_id into sectionJoin
                             from c in sectionJoin.DefaultIfEmpty()
                             join e in db.Status on a.status equals e.sts_id
                             where a.cp_id != 0 && a.status != 6 
                             group new { a, e, b, c } by new { a.cp_Author, b.cu_name, a.cp_id, c.sc_name } into grouped
                             select new GetAllCourse_Pack
                             {
                                 cu_name = grouped.Key.cu_name,
                                 cp_Author = grouped.Key.cp_Author,
                                 cp_id = grouped.Min(g => g.a.cp_id),
                                 cp_amount = grouped.Min(g => g.a.cp_amount),
                                 cu_sc_id_fk = grouped.Min(g => g.a.cu_sc_id_fk),
                                 sec_name = grouped.Min(g => g.c.sc_name),
                                 package_name = grouped.Min(g => g.a.package_name),
                                 Discount = grouped.Min(g => g.a.Discount),
                                 status = grouped.Min(g => g.a.status),
                                 sts_name = grouped.Min(g => g.e.sts_name),
                             });
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }





        //public async Task<List<GetAllCoursePackage>> GetAllCoursePackage()
        //{
        //    try
        //    {
        //        var query = (from a in db.Course_Package
        //                     join c in db.Section on a.cu_name equals c.sc_name
        //                     // join d in db.Course_Chapters on c.sc_ch_Fk equals d.ch_id
        //                     join e in db.Status on a.status equals e.sts_id
        //                     where a.cp_id != 0
        //                     group new { a, e, c } by new { a.cp_Author, c.sc_name, a.cp_id } into grouped
        //                     select new GetAllCoursePackage
        //                     {
        //                         cp_Author = grouped.Key.cp_Author,
        //                         cp_id = grouped.Min(g => g.a.cp_id),
        //                         cp_amount = grouped.Min(g => g.a.cp_amount),
        //                         sc_name = grouped.Min(g => g.c.sc_name),
        //                         package_name = grouped.Min(g => g.a.package_name),
        //                         Discount = grouped.Min(g => g.a.Discount),
        //                         status = grouped.Min(g => g.a.status),
        //                         sts_name = grouped.Min(g => g.e.sts_name),
        //                     });
        //        return await query.ToListAsync();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public async Task<GetCourse_PackById> GetCourse_PackById(int cp_id)
        {
            try
            {

                var query = (from a in db.Course_Package
                             join b in db.Course_Master on a.cu_name equals b.cu_name into courseMasterJoin
                             from b in courseMasterJoin.DefaultIfEmpty()
                             join c in db.Course_Section on a.cu_sc_id_fk equals c.sc_id into sectionJoin
                             from c in sectionJoin.DefaultIfEmpty()
                             join e in db.Status on a.status equals e.sts_id
                             where a.cp_id != 0 && a.cp_id == cp_id && a.status != 6
                             group new { a, e, b, c } by new { a.cp_Author, b.cu_name, a.cp_id, c.sc_name } into grouped
                             select new GetCourse_PackById
                             {
                                 cp_id = grouped.Key.cp_id,
                                 cu_name = grouped.Key.cu_name,
                                 cp_Author = grouped.Key.cp_Author,
                                 cu_sc_id_fk = grouped.Min(g => g.a.cu_sc_id_fk),
                                 //sec_name = grouped.Min(g => g.c.sc_name),
                                 package_name = grouped.Min(g => g.a.package_name),
                                 Discount = grouped.Min(g => g.a.Discount),
                                 cp_amount = grouped.Min(g => g.a.cp_amount),
                                 status = grouped.Min(g => g.a.status),
                                 sts_name = grouped.Min(g => g.e.sts_name),
                             }).FirstOrDefaultAsync();

                return await query;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> DeleteCourse_PackById(int cp_id)
        {
            try
            {
                var result = await db.Course_Package.FirstOrDefaultAsync(x => x.cp_id == cp_id);
                if (result != null)
                {
                    result.cp_id = cp_id;
                    result.delete_flag = true;
                    result.status = 6;
                    result.deleted_by = 1;
                    result.deleted_date = DateTime.Now;
                    await db.SaveChangesAsync();
                    return "CoursePack Deleted Successfully";
                }
                return "CoursePack Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> ApproveCourse_PackById(ApproveCourse_Pack approveCourse_Pack)
        {
            try
            {
                var result = await db.Course_Package.Where(x => x.cp_id == approveCourse_Pack.cp_id).FirstOrDefaultAsync();

                if (result != null)
                {
                    result.status = 3;
                    await db.SaveChangesAsync();
                    return "CoursePack Approved Successfully";
                }
                return "CoursePack Doesn't Exists";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public async Task<List<GetSectionDD>> GetSectionDD()
        {
            try
            {

                var query = (from a in db.Course_Section
                             where a.sc_id != 0 && a.status != 6
                             group new { a } by new { a.sc_name } into grouped

                             select new GetSectionDD
                             {
                                 cs_id = grouped.Min(g => g.a.sc_id),
                                 sc_name = grouped.Min(g => g.a.sc_name),

                             }).ToListAsync();
                return await query;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }

        }

        public async Task<List<GetPackageDD>> GetPackageDD()
        {
            try
            {
                var query = (from a in db.Course_Master
                             where a.cu_id != 0 && a.status != 6 
                             group new { a } by new { a.cu_name } into grouped
                             select new GetPackageDD
                             {
                                 cp_id = grouped.Min(g => g.a.cu_id),
                                 cu_name = grouped.Min(g => g.a.cu_name)
                             }).ToListAsync();
                return await query;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<GetPackageDD>> GetHide()
        {
            try
            {
                var query = (from a in db.Course_Master
                             where a.cu_id != 0 && a.cu_name!="UG" && a.status != 6
                             group new { a } by new { a.cu_name } into grouped
                             select new GetPackageDD
                             {
                                 cp_id = grouped.Min(g => g.a.cu_id),
                                 cu_name = grouped.Min(g => g.a.cu_name)
                             }).ToListAsync();
                return await query;
            }
            catch (Exception)
            {

                throw;
            }
        }



        //public async Task<List<GetCranialDD>> GetCardDD(int id)
        //{
        //    try
        //    {


        //        var query = (from a in db.Course_Package
        //                     where a.cp_id == id && a.status != 6
        //                     select new GetCranialDD
        //                     {
        //                         cp_id = a.cp_id,
        //                         cu_name = a.cu_name,
        //                         Amount = a.cp_amount,
        //                         Duration = a.Duration
        //                     }).ToListAsync();

        //        return await query;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public  async Task<List<GetCranialDD>> GetCardDD(int id)
        {
            try
            {
                var query = (from a in db.Course_Package
                             where a.cp_id==id && a.status != 6
                             select new GetCranialDD
                             {
                                 cp_id = a.cp_id,
                                 cu_name = a.cu_name,
                                 Amount = a.cp_amount,
                                 Duration = a.Duration
                             }).ToListAsync();
                return await query;

            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}



