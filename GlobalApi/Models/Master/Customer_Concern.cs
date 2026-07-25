using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalApi.Models.Master
{
    public class Customer_Concern
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Cust_Concern_Id { get; set; }
        [StringLength(30)]
        public string? Cust_Concern { get; set; }
    }
    public class Concern_Online
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int concern_id { get; set; }
        public string? concern_type { get; set; }
        public string? concern_name { get; set; }
    }

}
