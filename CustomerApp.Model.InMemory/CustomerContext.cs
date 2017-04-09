using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerApp.Model;


namespace CustomerApp.Model.InMemory
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(bool addExampleCustomers=true)
        {
            if (addExampleCustomers)
            {
                AddExampleCustomers();
            }
        }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
            base.OnConfiguring(optionsBuilder);            
        }

        public void AddExampleCustomers()
        {
            Customers.Add(new Customer() { Name = "Adam", Surname = "White", TelephoneNumber = "+48 1234", Address = "Katowice" });
            Customers.Add(new Customer() { Name = "John", Surname = "Black", TelephoneNumber = "+44 223234", Address = "London" });
            Customers.Add(new Customer() { Name = "Jane", Surname = "Smith", TelephoneNumber = "+1 12211234", Address = "Chicago" });

            SaveChanges();
        }
    }
}
