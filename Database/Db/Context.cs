using Microsoft.EntityFrameworkCore;
using Database.Models.DoList;
using Database.Models.Component;


namespace Database.Db
{
    public class Context : DbContext
    {
        public DbSet<Thing> Things { get; set; }

        public DbSet<DictDb> DictDbs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=todolist.db");

    }
}