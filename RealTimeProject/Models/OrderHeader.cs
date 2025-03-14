using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealTimeProject.Models
{
    public class OrderHeader
    {
        [Key]
        public int OrderHeaderId {  get; set; }
        public string ApplicationUserId { get; set; } //we are using string datatype here.Because this entity is a foreign key which is a primary key in the ApplicationUser [we have inherited inbuilt IdentityUser table which has string as a Primary key here. That's y]
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
        public string? PhoneNumber { get; set; }
    }
}
