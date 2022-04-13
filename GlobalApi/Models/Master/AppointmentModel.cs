using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class AppointmentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Appt_Id { get; set; }

        [Display(Name = "Patient")]
        public virtual int? Appt_PatientId_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Appt_PatientId_FK")]
        public virtual Patient? Patient { get; set; }

        [Display(Name = "Discipline")]
        public virtual int? Appt_CD_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Appt_CD_Id_FK")]
        public virtual Discipline? Discipline { get; set; }


        [Display(Name = "Doctor")]
        public virtual int? Appt_DO_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Appt_DO_Id_FK")]
        public virtual Doctor? Doctor { get; set; }

        //imp
        //[Required]
        //public int Select_doctor { get; set; }
        [Required]
        [StringLength(50)]
        public DateTime? Appt_DateTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Select_day { get; set; } 

        //[Required]
        //[StringLength(50)]
        //public string Select_Time { get; set; }

        //[Required]
        [StringLength(50)]
        public string? Select_FrmTime { get; set; }

        //[Required]
        [StringLength(50)]
        public string? Select_toTime { get; set; }

        //status record
        public int? Doctor_approval_status { get; set; }
        public int? Appt_Is_active { get; set; }

        [StringLength(50)]
        public string? Appt_Type { get; set; }

        [Display(Name = "Assistant")]
        public int? Assi_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Assi_Id_FK")]
        public virtual Assistant? Assistant { get; set; }


        [Display(Name = "SHReferrals")]
        public int? Ref_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("Ref_Id_FK")]
        public virtual SHReferrals? SHReferrals { get; set; }

        //public int? Dis_id { get; set; }


        //public int? Hos_id { get; set; }
        //public int? spe_id { get; set; }

        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public int? deleted_by { get; set; }
        public Nullable<System.DateTime> deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }
    }
    public class GetAllAppointmentModel
    {
        public int Appt_Id { get; set; }
        public int? Appt_PatientId_FK { get; set; }
        public string? Appt_P_Code { get; set; }
        public string? Appt_P_Name { get; set; }
        public int? Appt_CPT_Id_FK { get; set; }
        public string? Appt_CPT_Name { get; set; }
        public int? Appt_SYM_Id_FK { get; set; }
        public string? Appt_SYM_Name { get; set; }
        public int? Appt_Dis_Id_Fk { get; set; }
        public string? Appt_Dis_Name { get; set; }
        public int? Appt_PA_Id_FK { get; set; }
        public string? Appt_PA_Height { get; set; }
        public string? Appt_PA_Weight { get; set; }
        public string? Appt_PA_TempInFahrenheit { get; set; }
        public string? Appt_PA_TempInCelsius { get; set; }
        public string? Appt_PA_BloodPressure { get; set; }
        public string? Appt_PA_Sugar { get; set; }
        public string? Appt_PA_RespiratoryRate { get; set; }
        public string? Appt_PA_PulseRate { get; set; }
        public string Appt_PA_ECG { get; set; }
        public string Appt_PA_OxygenSaturation { get; set; }
        //public string? Other_symptoms { get; set; }
        public int? Appt_CD_Id_FK { get; set; }
        public string? Appt_CD_Name { get; set; }
        public int? Appt_DO_Id_FK { get; set; }
        public string? Appt_DO_Name { get; set; }
        public DateTime? Appt_DateTime { get; set; }
        public string Select_day { get; set; }
        //public string Select_Time { get; set; }
        public string? Select_FrmTime { get; set; }
        public string? Select_toTime { get; set; }
        public int? Doctor_approval_status { get; set; }
        public int? Appt_Is_active { get; set; }
        public string? Appt_Type { get; set; }
        public int? Assi_Id_FK { get; set; }
        public string? Appt_Assi_Name { get; set; }
        public int? Ref_Id_FK { get; set; }
        //public int? Dis_id { get; set; }
        //public int? Hos_id { get; set; }
        //public int? spe_id { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }


    }
    public class AppointmentModelById
    {
        public int Appt_Id { get; set; }
        public int? Appt_PatientId_FK { get; set; }
        public string? Appt_P_Code { get; set; }
        public string? Appt_P_Name { get; set; }
        public int? Appt_CPT_Id_FK { get; set; }
        public string? Appt_CPT_Name { get; set; }
        public int? Appt_SYM_Id_FK { get; set; }
        public string? Appt_SYM_Name { get; set; }
        public int? Appt_Dis_Id_Fk { get; set; }
        public string? Appt_Dis_Name { get; set; }
        public int? Appt_PA_Id_FK { get; set; }
        public string? Appt_PA_Height { get; set; }
        public string? Appt_PA_Weight { get; set; }
        public string? Appt_PA_TempInFahrenheit { get; set; }
        public string? Appt_PA_TempInCelsius { get; set; }
        public string? Appt_PA_BloodPressure { get; set; }
        public string? Appt_PA_Sugar { get; set; }
        public string? Appt_PA_RespiratoryRate { get; set; }
        public string? Appt_PA_PulseRate { get; set; }
        public string Appt_PA_ECG { get; set; }
        public string Appt_PA_OxygenSaturation { get; set; }
        //public string? Other_symptoms { get; set; }
        public int? Appt_CD_Id_FK { get; set; }
        public string? Appt_CD_Name { get; set; }
        public int? Appt_DO_Id_FK { get; set; }
        public string? Appt_DO_Name { get; set; }
        public DateTime? Appt_DateTime { get; set; }
        public string Select_day { get; set; }
        //public string Select_Time { get; set; }
        public string? Select_FrmTime { get; set; }
        public string? Select_toTime { get; set; }
        public int? Doctor_approval_status { get; set; }
        public int? Appt_Is_active { get; set; }
        public string? Appt_Type { get; set; }
        public int? Assi_Id_FK { get; set; }
        public string? Appt_Assi_Name { get; set; }
        public int? Ref_Id_FK { get; set; }
        //public int? Dis_id { get; set; }
        //public int? Hos_id { get; set; }
        //public int? spe_id { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

    public class InsertDetails
    {
        public int Appt_Id { get; set; }
        public int? Appt_PatientId_FK { get; set; }
        public int? Appt_CD_Id_FK { get; set; }
        public int? Appt_DO_Id_FK { get; set; }
        public DateTime? Appt_DateTime { get; set; }
        public string Select_day { get; set; }
        //public string Select_Time { get; set; }

        public string? Select_FrmTime { get; set; }
        public string? Select_toTime { get; set; }
        public int? Doctor_approval_status { get; set; }
        public int? Appt_Is_active { get; set; }
        public string? Appt_Type { get; set; }
        public int? Assi_Id_FK { get; set; }
        //public int? Ref_Id_FK { get; set; }
        //public int? Dis_id { get; set; }
        //public int? Hos_id { get; set; }
        //public int? spe_id { get; set; }
        public List<Complaint> Complaint { get; set; } = null!;
        public List<Symptoms> Symptoms { get; set; } = null!;
        public List<DiseasesDtl> DiseasesDtl { get; set; } = null!;
        public List<Parameters> Parameters { get; set; } = null!;
        public int? created_by { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetDocDD
    {
        public int Doc_Id { get; set; }
        public string Doc_Name { get; set; }

    }



}
