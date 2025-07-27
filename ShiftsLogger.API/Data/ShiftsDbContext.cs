using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ShiftsLogger.API.Models;

namespace ShiftsLogger.API.Data
{
    public class ShiftsDbContext : DbContext 
    {
        public ShiftsDbContext(DbContextOptions<ShiftsDbContext> options) : base(options)
        { }      
        
        public DbSet<Shift> Shifts { get; set; }
    }
}
