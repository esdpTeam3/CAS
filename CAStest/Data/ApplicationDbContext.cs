using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CAStest.Models;
using CAStest.Models.ViewModels;

namespace CAStest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<IndividualEntrepreneur> IndividualEntrepreneurs { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<ContractProperties> ContractPropertieses { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProperties> CategoryPropertieses { get; set; }
        public DbSet<CounterpartyContracts> CounterpartyContractses { get; set; }
        public DbSet<ContractCounterparty> ContractCounterparties { get; set; }
        public DbSet<Act> Acts { get; set; }
        public DbSet<Supplementary> Supplementaries { get; set; }

        public DbSet<Permissions> Permissionses { get; set; }
        public DbSet<ConnectionUserGroup> GroupsUserGroups { get; set; }
        public DbSet<PermissionsGroup> PermissionsGroups { get; set; }
        public DbSet<CASFile> CASFiles { get; set; }
        public DbSet<ContractFile> ContractFiles { get; set; }
        public DbSet<ActFile> ActFiles { get; set; }
        public DbSet<SupplementaryFile> SupplementaryFiles { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<Notification> Notifications { get; set; }




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       





        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Individual>()
                .HasOne(o => o.Company)
                .WithMany(s => s.Individuals)
                .HasForeignKey(s => s.CompanyId);

            builder.Entity<Company>()
                .HasMany(s => s.Individuals)
                .WithOne(o => o.Company)
                .HasPrincipalKey(s => s.Id)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
