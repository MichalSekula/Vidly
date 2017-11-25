using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;
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
        public IHttpActionResult GetCustomer()
        {
            var customerDtos = _context.Customers.Include(c=>c.MembershipType).ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }
        //GET api/customers/1
        //A tak sie uzywa gdy chcemy zwrocic jednego customer'a 
        public IHttpActionResult customer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        //POST api/customers
        //W web api poslugujemy sie bardzo podobnym interfejsem jak w standardowym kodzie mvc
        // czyli zamiast ActionResult dajemy IHttpActionResult ktory zwraca nam zmieniona forme return a zwraca nam dokaldnie cos takiego
        // api/customers/10 - w zaleznosci jakie mamy id,
        // w prawidlowej konwencji RESTfull heders powinno byc 201 Created w dodawaniu customer'a
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDto.Id);
        }
        //PUT api/customer/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();
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

            return Ok();
        }
        //DELETE api/customer/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}