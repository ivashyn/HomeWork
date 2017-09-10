using System.ComponentModel.DataAnnotations;

namespace MatOrderingService.Domain.Models
{
    public class EditOrder
    {
        [StringLength(200)]
        public string OrderDetails { get; set; }
    }
}
