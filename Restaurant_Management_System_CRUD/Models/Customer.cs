using System.ComponentModel.DataAnnotations;

namespace Restaurant_Management_System_CRUD.Models
{
    public class Customer
    { 
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }
}
