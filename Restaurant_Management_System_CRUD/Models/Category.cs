using System.ComponentModel.DataAnnotations;

namespace Restaurant_Management_System_CRUD.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

/*        public ICollection<Menu> Menus { get; set; }
*/    }
}
