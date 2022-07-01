using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class ReVisit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
		public int RV_Id { get; set; }
		public int? CON_Id { get; set; }
		public string? Date { get; set; }
		public int? Doctor_Id { get; set; }
		public string? RV_Date { get; set; }
		
		[StringLength(50)]
		public string? RV_Time { get; set; }
		
		[StringLength(250)]
		public string? Remarks { get; set; }
		public int Created_by { get; set; }
		public DateTime Created_date { get; set; }
		public int? Modified_by { get; set; }
		public DateTime? Modified_date { get; set; }
		public int? Deleted_by { get; set; }
		public DateTime? Deleted_date { get; set; }

		[Required]
		public bool Delete_flag { get; set; }

		[Required]
		public int Status { get; set; }

	}
	public class GetAllReVisit
	{
		public int RV_Id { get; set; }
		public int? CON_Id { get; set; }
		public string? Date { get; set; }
		public int? Doctor_Id { get; set; }
		public string? Doctor_Name { get; set; }
		public string? RV_Date { get; set; }
		public string? RV_Time { get; set; }
		public string? Remarks { get; set; }
		public bool Delete_flag { get; set; }
		public int Status { get; set; }
		public string? sts_name { get; set; }
	}
	public class ApprvReVisit
	{
		public int RV_Id { get; set; }
		public int Status { get; set; }
		public string? Select_FrmTime { get; set; }
		public string? Select_toTime { get; set; }

	}

}
