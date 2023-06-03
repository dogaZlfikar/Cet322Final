using Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<Models.Blog>? Blogs { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public IEnumerable<SelectListItem> Category { get; set; }
    public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, DisplayOrder = 1, Name = "Lifestyle" },
            new Category { Id = 2, DisplayOrder = 2, Name = "Sport" },
            new Category { Id = 3, DisplayOrder = 3, Name = "Health" });
    }
}