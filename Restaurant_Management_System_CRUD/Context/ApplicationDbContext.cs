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
        public DbSet<Administrator> Admin { get; set; }
        public DbSet<Menu> Menu { get; set; }
        
    }
}
