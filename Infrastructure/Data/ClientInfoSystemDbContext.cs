using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ClientInfoSystemDbContext : DbContext
    {
        public ClientInfoSystemDbContext(DbContextOptions<ClientInfoSystemDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Interaction> Interactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(ConfigureClient);
            modelBuilder.Entity<Employee>(ConfigureEmployee);
            modelBuilder.Entity<Interaction>(ConfigureInteraction);
        }
        private void ConfigureClient(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c => c.Email).HasMaxLength(50);
            builder.Property(c => c.Phones).HasMaxLength(30);
            builder.Property(c => c.Address).HasMaxLength(100);
        }
        private void ConfigureEmployee(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Password).HasMaxLength(10);
            builder.Property(e => e.Designation).HasMaxLength(50);
        }
        private void ConfigureInteraction(EntityTypeBuilder<Interaction> builder)
        {
            builder.ToTable("Interaction");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Remarks).HasMaxLength(500);
            builder.HasOne(i => i.Client).WithMany(c => c.Interactions).HasForeignKey(i => i.ClientId);
            builder.HasOne(i => i.Employee).WithMany(e => e.Interactions).HasForeignKey(i => i.EmpId);
        }

    }

}
