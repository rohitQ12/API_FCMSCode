using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models.Master
{
    public class Student_Enquiry
    {
        [Key]
        public int Id { get; set; }
        public int Stu_id_fk { get; set; }
        public string? Stu_Name { get; set; }
        public string? Ind_Name { get; set; }
        public string Stu_phoneNumber { get; set; }
        public string? Stu_Email { get; set; }
        public string? Stu_Que { get; set; }
        public string? Stu_Ans { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

    public class StuqueInsert 
    {
        public string? Stu_Name { get; set; }
        public string? Ind_Name { get; set; }
        public string? Stu_Email { get; set; }

        public string? Stu_phoneNumber { get; set; }
        public string? Stu_Que { get; set; }

    }

    public class StuqueUpdate
    {
        public int Id { get; set; }
        //public int Stu_id_fk { get; set; }
        public string? Stu_Name { get; set; }
        public string? Ind_Name { get; set; }
        public string? Stu_Email { get; set; }

        public string? Stu_phoneNumber { get; set; }
        public string? Stu_Que { get; set; }
       // public string? Stu_Ans { get; set; }



    }

    public class StuqueGetAll 
    {
        public int Id { get; set; }
        public string? Stu_Name { get; set; }
        public string? Ind_Name { get; set; }
        public string? Stu_Email { get; set; }

        public string Stu_phoneNumber { get; set; }
        public string? Stu_Que { get; set; }
        public string? Sts_Details { get; set; }

    }

    public class StuQueries : StuqueInsert
    {
        public string? Stu_Ans { get; set; }
        public int Id { get; set; }

    }
    public class UpdateNote
    {
        public int Id { get; set; }
        public string? Stu_Email { get; set; }
        public string? Stu_Ans { get; set; }

    }

    public class stuAllNote
    {
        public int Id { get; set; }
        public string? SecName { get; set; }


    }


}
