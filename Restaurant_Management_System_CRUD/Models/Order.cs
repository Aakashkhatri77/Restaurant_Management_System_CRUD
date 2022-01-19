namespace Restaurant_Management_System_CRUD.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public Nullable<int> Unit { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }

        public virtual Menu Menu { get; set; }

    }
}
