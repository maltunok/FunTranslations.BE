using FunTranslations.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FunTranslations.Infrastructure.Data;
public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext()
    {

    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
       
    }

    public DbSet<RequestResponseRecord> RequestResponseRecords { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

