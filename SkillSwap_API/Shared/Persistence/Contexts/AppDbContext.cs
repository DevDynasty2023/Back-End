using Microsoft.EntityFrameworkCore;
using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Security.Domain.Models;

namespace SkillSwap_API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Leccion>Lecciones { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.SkillCoins).IsRequired();
        builder.Entity<User>().Property(p => p.profilePhoto).IsRequired();
        
        
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<Role>().HasKey(r => r.Id);
        builder.Entity<Role>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(30);
        
        builder.Entity<Role>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);
        
        builder.Entity<Curso>().ToTable("Cursos");
        builder.Entity<Curso>().HasKey(p => p.Id);
        builder.Entity<Curso>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Curso>().Property(p => p.Name).IsRequired();
        builder.Entity<Curso>().Property(p => p.Description).IsRequired();
        
        builder.Entity<Leccion>().ToTable("Lecciones");
        builder.Entity<Leccion>().HasKey(p => p.Id);
        builder.Entity<Leccion>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Leccion>().Property(p => p.titulo).IsRequired();
        builder.Entity<Leccion>().Property(p => p.contenido).IsRequired();
        
        
        builder.Entity<Curso>()
            .HasMany(p => p.Lecciones)
            .WithOne(p => p.Curso)
            .HasForeignKey(p => p.CursoId);
    }
}