using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerService.Controllers
{
    public class CustomersController : ApiController
    {
        // GET api/values
        public IEnumerable<Customer> Get()
        {
            return CustomerRepository.GetAll();
        }

        // GET api/values/5
        public Customer Get(int id)
        {
            return CustomerRepository.Get(id);
        }

        // POST api/values
        public void Post([FromBody]Customer customer)
        {
            CustomerRepository.Add(customer);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Customer customer)
        {
            CustomerRepository.Update(customer);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            CustomerRepository.Delete(id);
        }
    }
}
