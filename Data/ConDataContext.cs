using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EmployeesApp.Models.ConData;

namespace EmployeesApp.Data
{
    public partial class ConDataContext : DbContext
    {
        public ConDataContext()
        {
        }

        public ConDataContext(DbContextOptions<ConDataContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EmployeesApp.Models.ConData.Employee>()
              .HasOne(i => i.Department)
              .WithMany(i => i.Employees)
              .HasForeignKey(i => i.DepartmentID)
              .HasPrincipalKey(i => i.DepartmentID);

            builder.Entity<EmployeesApp.Models.ConData.Employee>()
              .HasOne(i => i.Gender)
              .WithMany(i => i.Employees)
              .HasForeignKey(i => i.GenderID)
              .HasPrincipalKey(i => i.GenderID);

            builder.Entity<EmployeesApp.Models.ConData.Employee>()
              .HasOne(i => i.Employee1)
              .WithMany(i => i.Employees1)
              .HasForeignKey(i => i.ManagerID)
              .HasPrincipalKey(i => i.EmployeeID);
            this.OnModelBuilding(builder);
        }

        public DbSet<EmployeesApp.Models.ConData.Department> Departments { get; set; }

        public DbSet<EmployeesApp.Models.ConData.Employee> Employees { get; set; }

        public DbSet<EmployeesApp.Models.ConData.Gender> Genders { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}