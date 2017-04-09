using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using CustomerApp.Model;
using CustomerApp.Model.InMemory;

namespace CustomerApp.WebApp.Tests
{
    [TestClass]
    public class CustomersControllerTests
    {
        CustomerContext customersContext;
        CustomerRepository repo;

        [TestInitialize]
        public void SetupTest()
        {
            customersContext = new CustomerContext(false);
            repo = new CustomerRepository(customersContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            customersContext.Dispose();
        }
        [TestMethod]
        public async Task ShouldCreateCustomer()
        {
            //given
            Customer customerToAdd = new Customer() { Name = "Jim", Surname = "Tester", TelephoneNumber = "123", Address = "New York" };

            //when            
            bool isOk = await repo.Create(customerToAdd);
            Assert.IsTrue(isOk);
         
            //then            
            List<Customer> custContextList = new List<Customer>(customersContext.Customers.Local);
            Assert.AreEqual(1, custContextList.Count);
            Customer customerAdded = custContextList[0];

            Assert.AreEqual(customerToAdd, customerAdded);
        }

        [TestMethod]
        public async Task ShouldRemoveCustomerByName()
        {
            //given
            Customer customerToRemove = new Customer() { Name = "Steve", Surname = "QualityAssurance", TelephoneNumber = "123", Address = "New York" };
            AddTestCustomer(customerToRemove);
                        
            //when
            bool isOk = await repo.Remove(customerToRemove.Name, customerToRemove.Surname);
            Assert.IsTrue(isOk);
            
            //then          
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(0, customersContext.Customers.Local.Count);
        }
        [TestMethod]
        public async Task ShouldRemoveCustomerById()
        {
            //given
            Customer customerToRemove = new Customer() { Name = "John", Surname = "Tester", TelephoneNumber = "123", Address = "New York" };
            AddTestCustomer(customerToRemove);
            int customerId = customerToRemove.Id;
            //when          
            bool isOk = await repo.Remove(customerId);
            Assert.IsTrue(isOk);                
            
            //then
            Assert.AreEqual(0, customersContext.Customers.Local.Count);
        }

        [TestMethod]
        public void ShouldGetCustomers()
        {
            //given

            customersContext.AddExampleCustomers();
            //when
            IEnumerable<Customer> cust = repo.GetCustomers();
            List<Customer> custList = new List<Customer>(cust);
            //then
            Assert.AreEqual(customersContext.Customers.Local.Count, custList.Count);            

        }
        [TestMethod]
        public async Task ShouldUpdateCustomer()
        {
            //given

            Customer customerToUpdate = new Customer() { Name = "Andrew", Surname = "Tester", TelephoneNumber = "123", Address = "New York" };
            AddTestCustomer(customerToUpdate);
            
            Customer customerUpdated = customerToUpdate;
            customerUpdated.Address = "newAddress";
            //when
            bool isOk = await repo.Update(customerUpdated);
            //then
            Assert.IsTrue(isOk);
            List<Customer> custList = new List<Customer>(customersContext.Customers.Local);
            
            Assert.AreEqual(1, customersContext.Customers.Local.Count);
            Assert.AreEqual(customerUpdated, custList[0]);

        }
        [TestMethod]
        public void ShouldGetCustomerById()
        {
            //given
            
            Customer customerToGet = new Customer() { Name = "Adam", Surname = "Tester", TelephoneNumber = "123", Address = "New York" };
            AddTestCustomer(customerToGet);
            int customerId = customerToGet.Id;
            //when
            Customer customer = repo.GetCustomerById(customerId);
            //then
            List<Customer> custContextList = new List<Customer>(customersContext.Customers.Local);

            Assert.AreEqual(customerToGet, customer);

        }
        [TestMethod]
        public void ShouldNotGetByIdNotExistingCustomer()
        {
            //given

            Customer customer = repo.GetCustomerById(14234);
            //then
            Assert.IsNull(customer);

        }

        private void AddTestCustomer(Customer customerToGet)
        {
            customersContext.Customers.Add(customerToGet);
            customersContext.SaveChanges();
        }
    }
}
