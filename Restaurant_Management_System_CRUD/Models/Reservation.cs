namespace Restaurant_Management_System_CRUD.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public virtual Table Table { get; set; }
        public DateTime CheckIn { get; set; }
        public int NoOfPersons { get; set; }
    }
}
