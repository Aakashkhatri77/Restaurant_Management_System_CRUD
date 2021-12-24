using System.ComponentModel.DataAnnotations;

namespace Restaurant_Management_System_CRUD.ViewModel
{
    public class CustomerVm
    {
        [Key]
        public int CustomerId { get; set; }
        //[Required ]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Maximum length: 15 Minimum length : 4 ")]
        public string CustomerName { get; set; }
        //[Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        //[Required]
        [StringLength(20)]
        public string Address { get; set; }
        //[Required]
        [RegularExpression(@"\+977[0-9]{10}", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
    }
}
