using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RealTimeProject.Models
{
    public class ApplicationUser : IdentityUser //IdentityUser is inbuilt model here. Along with the entities  we have added five more entity from our side..
    {
        [Required]
        public string? Name { get; set; }
        public string? StreetAddress    { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }                   
    }
}
