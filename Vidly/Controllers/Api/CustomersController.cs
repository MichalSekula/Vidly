using System.Collections.Generic;
using System.Linq;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Vidly.Models;
using AutoMapper;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    //Jest to odpowiednik starego webservice
    //dzieki tej klasie mozemy testowac, dodawac, edytowac, pobierac, usuwac nasze dane z bazy danych
    //Opiera się to na koncepcji REST, zawiera akcje Http, ktore obsluguja dane rzadania
    //GET, POST, PUT, DELETE 
    //Dane z tabeli mozemy zobaczyc za pomoca kodu XML, po uruchomieniu aplikacji i w linku dopisac api/customers
    //Polecane jest zainstalowac rozszerzenie do google chrome - POSTMAN, ktory bardziej zrozumiale pokazuje dane oraz latwiej mozna zarzadzac danymi
    // w RESTfull web Api, nie wolno poslugiwac sie parametrami bezposrednio z naszego modelu, dlatego poslugujemy sie klasa pomocna CustomerDto
    // ktora reprezentuje nasz model

    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET api/customers/
        //Tak sie uzywa automappera do inicjalizowania zmiennych z Dtos bez wypisywania wszystkich pol inicjalizujacych
        public IEnumerable<CustomerDto> GetCustomer()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }
        //GET api/customers/1
        //A tak sie uzywa gdy chcemy zwrocic jednego customer'a 
        public CustomerDto customer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer, CustomerDto>(customer);
        }
        //POST api/customers
        [System.Web.Http.HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
        }
        //PUT api/customer/1
        [System.Web.Http.HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            // Z racji tego iz w mapperze poslugujemy sie zmiennymi reprezentujacymi mapowane klasy,
            // czyli reprezentant modelu customerInDb oraz reprezentant klasy Dto customerDto
            // nie musimy podwojnie mapowac nasze klasy i wystarczy uproszczony zapis,
            // zamiast -- Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb); -- 
            // uzyjemy...

            Mapper.Map(customerDto, customerInDb);

            //automapper sluzy do tego aby pozbyc sie takiej inicjalizacji jaka jest ponizej i zastapic ja jedna linia 
            //customerInDb.Name = customer.Name;
            //customerInDb.Birthdate = customer.Birthdate;
            //customerInDb.IsSubscribedToCustomer = customer.IsSubscribedToCustomer;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;

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