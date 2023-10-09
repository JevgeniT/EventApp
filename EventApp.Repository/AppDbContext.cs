using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Repository;

public class AppDbContext : IdentityDbContext<User>
{
    private readonly IOptions<PreConfiguredUser> _adminUser;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<PreConfiguredUser> adminUser) : base(options)
    {
        _adminUser = adminUser;
    }
    
    public DbSet<Event> Events { get; set; }
    public DbSet<EventUsers> EventUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder  modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var userId = Guid.NewGuid().ToString();
        var adminRoleId = Guid.NewGuid().ToString();
        var adminUser = _adminUser.Value;
        var normalisedEmail = adminUser.Email.ToUpper();
        var email = adminUser.Email;
        
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = userId,
            UserName =  email,
            NormalizedUserName = normalisedEmail,
            Email = email,
            NormalizedEmail = normalisedEmail,
            PasswordHash = new PasswordHasher<User>().HashPassword(null, adminUser.Password)
        });

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole()
        {
            Id = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN"
        });
        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
        {
           RoleId = adminRoleId,
           UserId = userId,
        });
        
        modelBuilder.Entity<User>().Property(p => p.UserName).HasColumnName("FirstName");
    }
}