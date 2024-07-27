using MehndiAppDotNerCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MehndiAppDotNerCoreWebAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        // Define other DbSets for your entities...
    }
}
