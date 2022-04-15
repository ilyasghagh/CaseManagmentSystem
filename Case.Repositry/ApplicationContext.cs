using Case.Data.Domains;
using Case.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Case.Data.Domains.Builder;

namespace Case.Repositry
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserBuilder(modelBuilder.Entity<User>());
            new UserRolesBuilder(modelBuilder.Entity<UserRoles>());
            new RoleBuilder(modelBuilder.Entity<Roles>());
            new CasesBuilder(modelBuilder.Entity<Cases>());
            new CaseCategoryBuilder(modelBuilder.Entity<CaseCategory>());
            new PoliceStationBuilder(modelBuilder.Entity<PoliceStation>());
            new CaseDocumentsBuilder(modelBuilder.Entity<CaseDocuments>());
            new DistirictBuilder(modelBuilder.Entity<Distirict>());
            new CourtBuilder(modelBuilder.Entity<Court>());
            new CarouselBuilder(modelBuilder.Entity<Carousel>());
            new TermConditionBuilder(modelBuilder.Entity<TermCondition>());
            new CitiesBuilder(modelBuilder.Entity<Cities>());
            new ProvincesBuilder(modelBuilder.Entity<Provinces>())
            {

            };
        }
    }
}
