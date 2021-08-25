using DoList.Models;
using Microsoft.EntityFrameworkCore;

namespace DoList.Db
{
    public class ThingsContext : DbContext
    {
        public DbSet<Thing> Things { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=todolist.db");
    }
}