using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }


        [Display(Name = "DrugType")]
        public virtual int DType_Id_FK { get; set; }
        [JsonIgnore]
        [ForeignKey("DType_Id_FK")]
        public virtual DrugType? DrugType { get; set; }


        [StringLength(50)]
        public string DrugUnit { get; set; }


        [Required]
        public bool delete_flag { get; set; }

        [Required]
        public int status { get; set; }

    }

}
