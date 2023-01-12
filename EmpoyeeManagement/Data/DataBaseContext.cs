using EmpoyeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpoyeeManagement.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Employee> employees { get; set; }
    }
}
