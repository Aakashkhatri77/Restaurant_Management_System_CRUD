using System.ComponentModel.DataAnnotations;

namespace Restaurant_Management_System_CRUD.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [RegularExpression(@"^[\w+\._-]{5,25}@[a-z]{2,12}.(com|org)")]
        public string Email { get; set; }
        public string Contact { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string RoleType { get; set; }
    }
}
