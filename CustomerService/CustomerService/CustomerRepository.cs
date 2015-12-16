using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerService
{
    public class CustomerRepository
    {
        private static readonly Dictionary<int, Customer> s_Customers = new Dictionary<int, Customer>();

        static CustomerRepository()
        {
            s_Customers.Add(
                1,
                new Customer { Id = 1, Name = "Hans", LastContact = new DateTime(1999, 12, 30) });
            s_Customers.Add(
                2,
                new Customer { Id = 2, Name = "Peter", LastContact = new DateTime(2013, 2, 12) });
            s_Customers.Add(
                3,
                new Customer { Id = 3, Name = "Bärbel", LastContact = new DateTime(1900, 10, 6) });
            s_Customers.Add(
                4,
                new Customer { Id = 4, Name = "Dörthe", LastContact = new DateTime(2003, 5, 23) });
            s_Customers.Add(
                5,
                new Customer { Id = 5, Name = "Gabi", LastContact = new DateTime(2001, 12, 12) });
            s_Customers.Add(
                6,
                new Customer { Id = 6, Name = "Ruolf", LastContact = new DateTime(2015, 12, 11) });
            s_Customers.Add(
                7,
                new Customer { Id = 7, Name = "Egon" });
        }

        public static IEnumerable<Customer> GetAll()
        {
            return new List<Customer>(s_Customers.Values);
        }

        public static Customer Get(int id)
        {
            Customer customer;
            s_Customers.TryGetValue(id, out customer);
            return customer;
        }

        public static void Add(Customer c)
        {
            s_Customers.Add(c.Id, c);
        }

        public static void Update(Customer c)
        {
            s_Customers[c.Id] = c;
        }

        public static void Delete(int id)
        {
            s_Customers.Remove(id);
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastContact { get; set; }
    }
}
