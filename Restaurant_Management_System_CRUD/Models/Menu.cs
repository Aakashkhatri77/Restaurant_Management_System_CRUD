using System.ComponentModel.DataAnnotations;

namespace Restaurant_Management_System_CRUD.Models
{
    public class Menu
    {
        [Key]
        public int Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public float  Price { get; set; }
        public string  Description { get; set; }
    }
}
