using DevFreela.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Persistance;

public class DevFreelaDbContext : DbContext
{
    public readonly DbContextOptions<DevFreelaDbContext> _dbContextOptions;
    public DevFreelaDbContext(DbContextOptions <DevFreelaDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TechEntity> Techs { get; set; }
    public DbSet<UserTechEntity> UserTechs { get; set; }
    public DbSet<CommentsEntity> Comments { get; set; }      
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserEntity>(e =>
        {
            e.HasKey(u => u.Id);
            
            e.HasMany(u => u.Technologies)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
               
        });
        
        builder.Entity<ProjectEntity>(e =>
        {
            e.HasKey(p => p.Id);
            
            e.HasOne(p => p.Developer)
                .WithMany(u => u.ProjectsAsDeveloper)
                .HasForeignKey(p => p.DeveloperId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(p => p.Client)
                .WithMany(u => u.ProjectsAsClient)
                .HasForeignKey(p => p.ClientID)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<TechEntity>(e =>
        {
            e.HasKey(p => p.Id);
        });
        
        builder.Entity<UserTechEntity>(e =>
        {
            e.HasKey(p => p.Id);

            e.HasOne(p => p.Technology).WithMany(p => p.UserTech)
                .HasForeignKey(fk => fk.TechId)
                .OnDelete(DeleteBehavior.Restrict);
                
        });
        
        builder.Entity<CommentsEntity>(e =>
        {
            e.HasKey(p => p.Id);

            e.HasOne(p => p.Project).WithMany(p => p.Comments)
                .HasForeignKey(fk => fk.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);;
        });
        
        base.OnModelCreating(builder);
    }
}   