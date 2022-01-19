using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant_Management_System_CRUD.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NoOfChair { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
