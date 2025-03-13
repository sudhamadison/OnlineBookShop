using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealTimeProject.Models
{
    public class OrderHeader
    {
        public int OrderHeaderId {  get; set; }
        public int ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string? OrderStatus   { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Carrier {  get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Name { get; set; }
    }
}
