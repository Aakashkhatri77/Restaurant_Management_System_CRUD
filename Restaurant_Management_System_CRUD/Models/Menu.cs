using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Management_System_CRUD.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float  Price { get; set; }
        public string  Description { get; set; }
         public int CategoryId { get; set; }
        public virtual Category Category { get; set;}
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Order> Orders { get; set; }


        // Foreign key 
        /*  [Display(Name = "Category")]
          public int CategoryId { get; set; }

          [ForeignKey("CategoryId")]
          public virtual Category Categories { get; set; }*/
    }
}
