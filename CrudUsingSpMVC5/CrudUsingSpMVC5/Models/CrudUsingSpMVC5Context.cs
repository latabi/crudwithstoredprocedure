using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrudUsingSpMVC5.Models
{
    public class CrudUsingSpMVC5Context : DbContext
    {
        

        public CrudUsingSpMVC5Context() : base("name=MyConnection")
        {
        }
       
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerVM>().Map(e =>
            {
                e.Properties(p => new { p.Name, p.Email });
                e.ToTable("Customers");
            }).Map(e =>
            {
                e.Properties(p => new { p.CurrentAddress, p.PermanantAddress });
                e.ToTable("Address");
            }).Map(e =>
            {
                e.Properties(p => new { p.State });
                e.ToTable("States");
            }).Map(e =>
            {
                e.Properties(p => new { p.City });
                e.ToTable("Cities");
            }).MapToStoredProcedures();
        }

        public System.Data.Entity.DbSet<CrudUsingSpMVC5.Models.CustomerVM> CustomerVMs { get; set; }
    }
}
