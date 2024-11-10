using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineBookStore.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required (ErrorMessage = "This field is required")]
        public string ISBN { get; set; }
        [Required (ErrorMessage = "This field is required")]
        public string Author { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(1,1000, ErrorMessage = "Please enter a value between 1 to 1000")]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1,1000, ErrorMessage = "Please enter a value between 1 to 1000")] 
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 to 1000")]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 to 1000")]
        public double Price100 { get; set; }
        [ValidateNever]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }
    }
}
