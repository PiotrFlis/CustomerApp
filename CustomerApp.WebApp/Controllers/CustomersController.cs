using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerApp.Model;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerApp.WebApp
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        ICustomerRepository repository;
        public CustomersController(ICustomerRepository repository)
        {
            this.repository = repository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Ok(repository.GetCustomers());
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (ModelState.IsValid)
            {
                Customer customer = repository.GetCustomerById(id);
                return Ok(customer);
            }
            return BadRequest();
        }

        // POST api/customers/5
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (await repository.Create(customer))
                {
                    return Ok(customer);
                }
            }
            return BadRequest();          
        }

        // PUT api/customers
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (await repository.Update(customer))
                {
                    return Ok(customer);
                }                
            }
            return BadRequest();
        }

        // DELETE api/customers
        [HttpDelete()]
        public async Task<IActionResult> Delete([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (await repository.Remove(customer.Name, customer.Surname))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {                
                if (await repository.Remove(id))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
