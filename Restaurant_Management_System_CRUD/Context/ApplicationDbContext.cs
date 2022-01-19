using Microsoft.EntityFrameworkCore;
using Restaurant_Management_System_CRUD.Models;

namespace Restaurant_Management_System_CRUD.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> RestuarantCustomer { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ViewCustomer> ViewCustomer { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


/*        public IEnumerable<Category> Get()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            return context.Categories;
        }*/

    }

    
}
