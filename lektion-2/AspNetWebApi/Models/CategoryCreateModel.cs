using System.ComponentModel.DataAnnotations;

namespace AspNetWebApi.Models
{
    public class CategoryCreateModel
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
