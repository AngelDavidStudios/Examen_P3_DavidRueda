using Examen_PF.MVVM.Models;
using Examen_PF.Utility;
using Microsoft.EntityFrameworkCore;

namespace Examen_PF.Data;

public class AppDbContext: DbContext
{
    public DbSet<ModelJoke> Jokes { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string DbPath = $"Filename={PathDb.GetPath("JokesDb.db")}";
        optionsBuilder.UseSqlite(DbPath);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ModelJoke>(entity =>
        {
            entity.HasKey(col => col.IdJoke);
            entity.Property(col => col.IdJoke).IsRequired().ValueGeneratedOnAdd();
        });
    }
}