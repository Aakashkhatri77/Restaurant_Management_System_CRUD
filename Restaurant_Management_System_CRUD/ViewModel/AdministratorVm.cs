using System.ComponentModel.DataAnnotations;

namespace Restaurant_Management_System_CRUD.ViewModel
{
    public class AdministratorVm
    {
        [Key]
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get; set; }
    }
}
