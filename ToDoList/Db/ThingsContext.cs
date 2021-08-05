using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ToDoList.Models;

namespace ToDoList.Db
{
    public class ThingsContext : DbContext
    {
        public DbSet<Thing> Things { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=todolist.db");
    }
}