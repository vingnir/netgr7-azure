using System.ComponentModel.DataAnnotations;

namespace AspNetWebApi.Models.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
