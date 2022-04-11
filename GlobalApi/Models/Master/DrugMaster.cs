using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
    public class DrugMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        public string DrugName { get; set; }

        [Display(Name = "DrugType")]
        public virtual int DT_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DT_Id_FK")]
        public virtual DrugType? DrugType { get; set; }

        [StringLength(10)]
        public string Strength { get; set; }

        [Display(Name = "Unit")]
        public virtual int UT_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("UT_Id_FK")]
        public virtual Unit? Unit { get; set; } 

        [StringLength(50)]
        public string Description { get; set; } 

        //max
        public string? Instruction { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }

        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }

    }
    public class GetAllDrugMaster
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int DT_Id_FK { get; set; }
        public string Drugtype { get; set; }
        public string Strength { get; set; }
        public int UT_Id_FK { get; set; }
        public string Drugunit { get; set; }
        public string Description { get; set; }
        public string? Instruction { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetDrugById
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int DT_Id_FK { get; set; }
        public string Drugtype { get; set; }
        public string Strength { get; set; }
        public int UT_Id_FK { get; set; }
        public string Drugunit { get; set; }
        public string Description { get; set; }
        public string? Instruction { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }

    public class DrugTypeDD
    {
        public int Id { get; set; }
        public string Type { get; set; }

    }
    public class UnitDD
    {
        public int Id { get; set; }
        public int DType_Id_FK { get; set; }
        public string DrugUnit { get; set; }

    }
}
