using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data;
public class StoreContext(DbContextOptions options) : IdentityDbContext<User>(options)
{

    public required DbSet<Product> Products{ get; set; }

    public required DbSet<Basket> Baskets{ get; set; }

    public required DbSet<Address> Addresses { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>()
        .HasData(
            new IdentityRole {Id = "93f69460-c5ee-49c9-a096-e1f4309a8253", Name = "Member", NormalizedName = "MEMBER"},
            new IdentityRole {Id = "f7e5202b-82b6-4e27-ad00-973daa0e9148", Name = "Admin", NormalizedName = "ADMIN"}
            
        );

       
    }

    
}