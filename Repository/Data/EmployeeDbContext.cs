using ENTITYAPP.Repository.Models;
using Microsoft.EntityFrameworkCore;


namespace ENTITYAPP.Repository.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmpDetails> EmpDetails { get; set; }
        public DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                        .HasOne(c => c.EmpDetails)  // Check this line
                        .WithOne(c => c.Employee)
                        .HasForeignKey<EmpDetails>(e => e.EmpId)
                        .IsRequired();


            modelBuilder.Entity<EmpDetails>()
                        .HasKey(ed => ed.EmpId);

          
        }

    }
}

