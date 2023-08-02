using System.ComponentModel.DataAnnotations;

namespace EProductUI.Models
{
    public class CategoryModel
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public static implicit operator HttpContent(CategoryModel v)
        {
            throw new NotImplementedException();
        }
    }
}
