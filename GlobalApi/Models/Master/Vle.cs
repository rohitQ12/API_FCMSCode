using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GlobalApi.Models.Master
{
	public class Vle
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int VL_Id { get; set; }

		[StringLength(50)]
		public string? VLE_Center { get; set; }

		[StringLength(3)]
		public string? VLE_Code { get; set; }

		[StringLength(50)]
		public string? VL_ContactPerson { get; set; }
		public DateTime VL_DOB { get; set; }

		[StringLength(20)]
		public string? VL_Gender { get; set; }

		//max
		public string? VL_Address { get; set; }

		[Display(Name = "Countries")]
		public virtual int VL_Country_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("VL_Country_Id_FK")]
		public virtual Countries? Countries { get; set; }


		[Display(Name = "States")]
		public virtual int VL_ST_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("VL_ST_Id_FK")]
		public virtual States? States { get; set; }


		[Display(Name = "Districts")]
		public virtual int VL_DI_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("VL_DI_Id_FK")]
		public virtual Districts? Districts { get; set; }

		[Display(Name = "Taluk")]
		public virtual int Taluk_id { get; set; }
		[JsonIgnore]
		[ForeignKey("Taluk_id")]
		public virtual Taluk? Taluk { get; set; }

		[Display(Name = "Gram")]
		public virtual int Gram_id { get; set; }
		[JsonIgnore]
		[ForeignKey("Gram_id")]
		public virtual Gram? Gram { get; set; }

		public long VL_MobileNumber { get; set; }
		public long? VL_AlterNumber { get; set; }

		[StringLength(50)]
		public string? VL_Email { get; set; }


		[Display(Name = "Qualification")]
		public virtual int VL_QU_Id_FK { get; set; }
		[JsonIgnore]
		[ForeignKey("VL_QU_Id_FK")]
		public virtual Qualification? Qualification { get; set; }


		public int VL_PostalCode { get; set; }

		//max
		public string? VL_Photo { get; set; }

		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }

		[Required]
		public bool delete_flag { get; set; }

		[Required]
		public int status { get; set; }
		
		[StringLength(250)]
		public string? Remarks { get; set; }

	}
	public class GetAllVle
	{
		public int VL_Id { get; set; }
		public string? VLE_Center { get; set; }
		public string? VLE_Code { get; set; }
		public string? VL_ContactPerson { get; set; }
		public DateTime VL_DOB { get; set; }
		public string? VL_Gender { get; set; }
		public string? VL_Address { get; set; }
		public int VL_Country_Id_FK { get; set; }
		public string? VL_Country { get; set; }
		public int VL_ST_Id_FK { get; set; }
		public string? VL_state_name { get; set; }
		public int VL_DI_Id_FK { get; set; }
		public string? VL_district_name { get; set; }
		public int Taluk_id { get; set; }
		public string Taluk_name { get; set; }
		public int Gram_id { get; set; }
		public string? Gram_name { get; set; }
		public long VL_MobileNumber { get; set; }
		public long? VL_AlterNumber { get; set; }
		public string? VL_Email { get; set; }
		public int VL_QU_Id_FK { get; set; }
		public string? VL_qualification { get; set; }
		public int VL_PostalCode { get; set; }
		public string? VL_Photo { get; set; }
		public byte[]? Imagebyte { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }

	}

	public class VleBy_Id
	{
		public int VL_Id { get; set; }
		public string? VLE_Center { get; set; }
		public string? VLE_Code { get; set; }
		public string? VL_ContactPerson { get; set; }
		public DateTime VL_DOB { get; set; }
		public string? VL_Gender { get; set; }
		public string? VL_Address { get; set; }
		public int VL_Country_Id_FK { get; set; }
		public string? VL_Country { get; set; }
		public int VL_ST_Id_FK { get; set; }
		public string? VL_state_name { get; set; }
		public int VL_DI_Id_FK { get; set; }
		public string? VL_district_name { get; set; }
		public int Taluk_id { get; set; }
		public string Taluk_name { get; set; }
		public int Gram_id { get; set; }
		public string? Gram_name { get; set; }
		public long VL_MobileNumber { get; set; }
		public long? VL_AlterNumber { get; set; }
		public string? VL_Email { get; set; }
		public int VL_QU_Id_FK { get; set; }
		public string? VL_qualification { get; set; }
		public int VL_PostalCode { get; set; }
		public string? VL_Photo { get; set; }
		public byte[]? Imagebyte { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? sts_name { get; set; }
		public string? Remarks { get; set; }


	}
	public class VleModel_Image
	{

		public int VL_Id { get; set; }
		public string? VLE_Center { get; set; }
		public string? VLE_Code { get; set; }
		public string? VL_ContactPerson { get; set; }
		public DateTime VL_DOB { get; set; }
		public string? VL_Gender { get; set; }
		public string? VL_Address { get; set; }
		public int VL_Country_Id_FK { get; set; }
		public string? VL_Country { get; set; }
		public int VL_ST_Id_FK { get; set; }
		//public string? VL_state_name { get; set; }
		public int VL_DI_Id_FK { get; set; }
		//public string? VL_district_name { get; set; }
		public int Taluk_id { get; set; }
		public int Gram_id { get; set; }
		public long VL_MobileNumber { get; set; }
		public long? VL_AlterNumber { get; set; }
		public string? VL_Email { get; set; }
		public int VL_QU_Id_FK { get; set; }
		//public string? VL_qualification { get; set; }
		public int VL_PostalCode { get; set; }
		public IFormFile VL_Photo { get; set; }
		public int? created_by { get; set; }
		public Nullable<System.DateTime> created_date { get; set; }
		public int? modified_by { get; set; }
		public Nullable<System.DateTime> modified_date { get; set; }
		public int? deleted_by { get; set; }
		public Nullable<System.DateTime> deleted_date { get; set; }
		public bool delete_flag { get; set; }
		public int status { get; set; }
		public string? Remarks { get; set; }


	}
	public class EditImageModel
	{
		public string? VL_Photo { get; set; }

	}
	public class Vle_DD
    {
		public int VL_Id { get; set; }
		public string? VLE_Center { get; set; }
		public string? VLE_Code { get; set; }

	}
}
