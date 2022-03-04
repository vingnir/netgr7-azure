using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetWebApi.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

    }
}
