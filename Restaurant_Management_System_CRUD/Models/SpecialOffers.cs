using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Management_System_CRUD.Models
{
    public class SpecialOffers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
