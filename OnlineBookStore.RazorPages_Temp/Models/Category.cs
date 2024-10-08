using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineBookStore.RazorPages_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 255, ErrorMessage = "The Display order must be between 1-255")]
        public int DisplayOrder { get; set; }
    }
}
