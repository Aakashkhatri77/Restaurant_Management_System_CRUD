namespace Restaurant_Management_System_CRUD.Models
{
    public class ViewCustomer
    {
        public int Id { get; set; }

        public int CustomerId { get; set; } 
        public Customer Customer { get; set; }
    }
}
