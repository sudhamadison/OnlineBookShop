using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RealTimeProject.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [NotNull]
        [StringLength(100)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
