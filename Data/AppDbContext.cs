using Itroots_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Itroots_Task.Data
{
    public class AppDbContext :DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserRole>().HasKey(ur=> new {ur.UserId , ur.RoleId});
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Itroots_Task.Models.Invoice>? Invoice { get; set; }
    }
}
