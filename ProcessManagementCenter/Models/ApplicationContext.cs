using Microsoft.EntityFrameworkCore;
using ProcessManagementCenter.Domain;

namespace ProcessManagementCenter.Models
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Miner> Miners { get; set; }
        public DbSet<Shift> Shifts { get; set; }
    }
}
