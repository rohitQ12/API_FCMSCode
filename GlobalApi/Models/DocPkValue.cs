using System.ComponentModel.DataAnnotations;

namespace GlobalApi.Models
{
    public class DocPkValue
    {
        [Key]
        public int PkId { get; set; }
        public string? PkName { get; set; }
        public int PkStartValue { get; set; }
        public int PkEndValue { get; set; }
        public int PkPresentValue { get; set; }
        public int PkPreviousValue { get; set; }
		public int? CreatedBy { get; set; }
		public Nullable<System.DateTime> CreatedDate { get; set; }
		public int? ModifiedBy { get; set; }
		public Nullable<System.DateTime> ModifiedDate { get; set; }
		public int? DeletedBy { get; set; }
		public Nullable<System.DateTime> DeletedDate { get; set; }
		public bool DeleteFlag { get; set; }
		public int Status { get; set; }
    }
}
