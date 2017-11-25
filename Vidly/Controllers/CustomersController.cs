using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            //jak sama nazwa wskazuje, pozwala nam przekazac do widoku dwa modele
            // dolaczony model musi rowniez wystepowac jako zmienna w modelu Customer
            //--------- Teraz korzystamy z API wiec nie potrzebujemy przesylac modelu

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        public ActionResult New()
        {
            var membersipTypes = _context.MembershipTypes.ToList();
            var vievModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membersipTypes
            };
            return View("CustomerForm",vievModel);
        }
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            //Validacje dzileimy na server i client side
            // 1. dodajemy data annotation w naszym modelu, co jest wymagane i specyfika danego pola
            // 2. dodajemy warunek !Model.State ktory rowniez sprawdza czy model przeszedl walidacje wyslana z widoku
            // 3. dodajemy w widoku walidacje pol, ktore sa wymagane
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToCustomer = customer.IsSubscribedToCustomer;
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }

    }
}