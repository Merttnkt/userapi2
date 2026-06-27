    using Microsoft.EntityFrameworkCore;
    using UserapiEfCore.Models; 

    namespace UserapiEfCore.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }