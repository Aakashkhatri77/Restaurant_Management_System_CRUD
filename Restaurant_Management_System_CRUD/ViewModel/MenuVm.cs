using Restaurant_Management_System_CRUD.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Management_System_CRUD.ViewModel
{
    public class MenuVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
       /* public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }*/

        // Foreign key 
        /*  [Display(Name = "Category")]
          public int CategoryId { get; set; }

          [ForeignKey("CategoryId")]
          public virtual Category Categories { get; set; }*/
    }
    public class CartViewModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total
        {
            get { return Price * Quantity; }
        }
    }
}
