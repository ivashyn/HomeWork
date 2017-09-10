using System;

namespace MatOrderingService.Domain.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public string OrderDetails { get; set; }
        public string OrderCode { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
    }
}
