using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DAL
{
    public class Category : IAuditEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2-100 characters")]
        public required string Name { get; set; }

        /*--------------------------------------------------*/
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }

        /*--------------------------------------------------*/
        public override string ToString()
        {
            return $"Id={Id}, Name={Name}";
        }
    }
}
