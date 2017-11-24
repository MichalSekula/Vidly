using System.Collections.Generic;
using System.Linq;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    //Jest to odpowiednik starego webservice
    //dzieki tej klasie mozemy testowac, dodawac, edytowac, pobierac, usuwac nasze dane z bazy danych
    //Opiera się to na koncepcji REST, zawiera akcje Http, ktore obsluguja dane rzadania
    //GET, POST, PUT, DELETE 
    //Dane z tabeli mozemy zobaczyc za pomoca kodu XML, po uruchomieniu aplikacji i w linku dopisac api/customers
    //Polecane jest zainstalowac rozszerzenie do google chrome - POSTMAN, ktory bardziej zrozumiale pokazuje dane oraz latwiej mozna zarzadzac danymi

    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET api/customers/
        public IEnumerable<Customer> GetCustomer()
        {
            return _context.Customers.ToList();
        }
        //GET api/customers/1
        public Customer customer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return customer;
        }
        //POST api/customers
        [System.Web.Http.HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }
        //PUT api/customer/1
        [System.Web.Http.HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToCustomer = customer.IsSubscribedToCustomer;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }
        //DELETE api/customer/1
        [System.Web.Http.HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}