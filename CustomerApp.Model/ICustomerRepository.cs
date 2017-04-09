using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApp.Model
{
    public interface ICustomerRepository
    {
        Task<bool> Create(Customer customer);
        Task<bool> Remove(String name, String surname);
        Task<bool> Remove(int id);
        Task<bool> Update(Customer customer);
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(int id);
    }
}
