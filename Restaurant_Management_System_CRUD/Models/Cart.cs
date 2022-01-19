namespace Restaurant_Management_System_CRUD.Models
{
    public class Cart
    {
        public int menuId { get; set; }
        public string menuName { get; set; }
        public int qty { get; set; }
        public int price { get; set; }
        public int bill { get; set; }
    }
}
