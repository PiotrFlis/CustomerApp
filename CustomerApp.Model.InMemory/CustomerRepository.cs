using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApp.Model;

namespace CustomerApp.Model.InMemory
{
   
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(CustomerContext context)
        {
            this.context = context;
        }

        CustomerContext context;
        public async Task<bool> Create(Customer customer)
        {
            if (GetCustomerByName(customer.Name, customer.Surname) == null)
            {
                context.Customers.Add(customer);
                return (await SaveChanges());
            }
            return true;
        }

        public async Task<bool> Remove(string name, string surname)
        {
            Customer customerByName = GetCustomerByName(name, surname);
            if (customerByName != null)
            {
                context.Customers.Remove(customerByName);
                return (await SaveChanges());
            }
            return true;
        }

        public async Task<bool> Remove(int id)
        {
            Customer customerByName = GetCustomerById(id);
            if (customerByName != null)
            {
                context.Customers.Remove(customerByName);
                return (await SaveChanges());
            }
            return true;
        }

        private Customer GetCustomerByName(string name, string lastName)
        {
            return context.Customers.Where(c => c.Name == name && c.Surname == lastName).FirstOrDefault();
        }
        public Customer GetCustomerById(int id)
        {
            return context.Customers.Where(c => c.Id == id).FirstOrDefault();
        }

        public async Task<bool> Update(Customer customer)
        {
            Customer customerByName = GetCustomerById(customer.Id);
            if (customerByName != null)
            {
                customerByName.Name = customer.Name;
                customerByName.Surname= customer.Surname;
                customerByName.TelephoneNumber = customer.TelephoneNumber;
                customerByName.Address = customer.Address;

                context.Customers.Update(customerByName);
                return (await SaveChanges());
            }
            return true;
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return from cust in context.Customers select cust;
        }
        private async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >=0);
        }
    }
    
}
