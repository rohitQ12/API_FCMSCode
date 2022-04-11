using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Doctor_ScheduleModule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Doc_schedule_Id { get; set; }

        public int DO_Id_FK { get; set; }

        public string? Do_Scd_day { get; set; }

        public string? Time_from { get; set; }

        public string? Time_to { get; set; }

        public Nullable<System.DateTime> Added_date { get; set; }

        public int? Added_by { get; set; }

        public Nullable<System.DateTime> Modified_date { get; set; }

        public int? Modified_by { get; set; }

        public int Delete_status { get; set; }

        public Nullable<System.DateTime> Deleted_date { get; set; }

        public int? Deleted_by { get; set; }

        public int Is_active { get; set; }
    }

    public class Schedule_historyModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Doc_schedule_history_Id { get; set; }

        public int Doc_schedule_Id { get; set; }

        public int DO_Id_FK { get; set; }

        public string? Do_Scd_day { get; set; }

        public string? Time_from { get; set; }

        public string? Time_to { get; set; }

        public Nullable<System.DateTime> Added_date { get; set; }

        public int? Added_by { get; set; }

        public Nullable<System.DateTime> Modified_date { get; set; }

        public int? Modified_by { get; set; }

        public int Delete_status { get; set; }

        public Nullable<System.DateTime> Deleted_date { get; set; }

        public int? Deleted_by { get; set; }

        public int Is_active { get; set; }

    }
}
