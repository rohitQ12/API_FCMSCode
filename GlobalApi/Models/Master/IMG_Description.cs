namespace GlobalApi.Models.Master
{
    public class IMG_Description
    {
        public int Img_DescId { get; set; }
        public int? Img_Invt_Id { get; set; }
	    public int? Img_SubInvt_Id { get; set; }
	    public string? Img_Description { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetAllIMG_Desc
    {
        public int Img_DescId { get; set; }
        public int? Img_Invt_Id { get; set; }
        public string? Category { get; set; }
        public int? Img_SubInvt_Id { get; set; }
        public string? Sub_Category { get; set; }
        public string? Img_Description { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class GetImgDesc_ById
    {
        public int Img_DescId { get; set; }
        public int? Img_Invt_Id { get; set; }
        public string? Category { get; set; }
        public int? Img_SubInvt_Id { get; set; }
        public string? Sub_Category { get; set; }
        public string? Img_Description { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }

    }
    public class Img_Desc_DD
    {
        public int Img_DescId { get; set; }
        //public int? Img_Invt_Id { get; set; }
        //public int? Img_SubInvt_Id { get; set; }
        public string? Img_Description { get; set; }

    }
}
