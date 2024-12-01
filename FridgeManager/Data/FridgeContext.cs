using Microsoft.EntityFrameworkCore;
using FridgeManagerAPI.Models;

namespace FridgeManagerAPI.Data
{
    public class FridgeContext : DbContext
    {
        public FridgeContext(DbContextOptions<FridgeContext> options) : base(options) { }

        public DbSet<FridgeItem> FridgeItems { get; set; }
    }
}