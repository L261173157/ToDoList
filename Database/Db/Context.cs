using System.Configuration;
using Database.Models.Component;
using Database.Models.DoList;
using Microsoft.EntityFrameworkCore;

namespace Database.Db;

public class Context : DbContext
{
    public DbSet<Thing> Things { get; set; }

    public DbSet<DictDb> DictDbs { get; set; }

    //链接本地sqlite
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["DbSqlite"].ConnectionString);
    //}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(ConfigurationManager.ConnectionStrings["mySql"].ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Thing>(entity =>
        {
            entity.HasKey(e => e.ThingId);
            entity.Property(e => e.Content).IsRequired();
        });

        modelBuilder.Entity<DictDb>(entity => { entity.HasKey(e => e.Id); });
    }
}