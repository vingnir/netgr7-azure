using System.ComponentModel.DataAnnotations;

namespace AspNetWebApi.Models
{
    public class CategoryUpdateModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
