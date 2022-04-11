using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GlobalApi.Models.Master
{
    public class Notification
    {
        [Key]
        public int EventId { get; set; }
        [StringLength(50)]
        public string UserId { get; set; } = null!;
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(250)]
        public string Description { get; set; } = null!;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool IsFullDay { get; set; }
        public bool ReadNotifcation { get; set; }

    }
}
