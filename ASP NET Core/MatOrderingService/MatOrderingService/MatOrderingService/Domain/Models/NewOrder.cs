using System.ComponentModel.DataAnnotations;

namespace MatOrderingService.Domain.Models
{
    public class NewOrder
    {
        [StringLength(200)]
        public string OrderDetails { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatorId { get; set; }
    }
}
