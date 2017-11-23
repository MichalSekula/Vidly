using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;
using System.Data.Entity;
using System;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
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
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(m=>m.Genre).SingleOrDefault(c => c.Id == id);

            if (movies == null)
                return HttpNotFound();

            return View(movies);
        }
        //Koncepcja CRUD polega na wykorzystaniu jednego widoku do Add/Edit
        //Jesli chcemy dodac nowy movie, przesylamy jedynie liste do dropdownListy,
        public ActionResult MovieForm()
        {
            var genresList = _context.Genres.ToList();
            var viewModel = new MoviesFormViewModel
            {
                Genres = genresList,
                Movies = new Movies()
            };
            return View(viewModel);
        }
        //W tej klasie dokonujemy dodania, edycji oraz zapisu naszej bazy 
        //przesylamy odpowiedni model z naszego widoku i w zaleznosci czy posiada Id czy nie 
        // Dokonujemy odpowiedniej operacji Add/Edit 
        [HttpPost]
        public ActionResult Save(Movies movies)
        {
            //Validacje dzileimy na server i client side
            // 1. dodajemy data annotation w naszym modelu, co jest wymagane i specyfika danego pola
            // 2. dodajemy warunek !Model.State ktory rowniez sprawdza czy model przeszedl walidacje wyslana z widoku
            // 3. dodajemy w widoku walidacje pol, ktore sa wymagane
            if (!ModelState.IsValid)
            {
                var viewModel = new MoviesFormViewModel
                {
                    Movies = movies,
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if (movies.Id == 0)
            {
                movies.DateAdded = DateTime.Now;
                _context.Movies.Add(movies);
                
            }
            else
            {
                var MoviesInDb = _context.Movies.SingleOrDefault(m => m.Id == movies.Id);
                MoviesInDb.Name = movies.Name;
                MoviesInDb.ReleaseDate = movies.ReleaseDate;
                MoviesInDb.GenreId = movies.GenreId;
                MoviesInDb.NumberInStock = movies.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Movies");
        }
        //Przesylamy do tego samego widoku New Movie film o danym Id 
        //Roznica polega na tym, iz oprocz listy Generes przesylamy caly movies
        // Poniewaz musimy zapelnic pola edytora danymi filmu do edycji
        public ActionResult Edit(int id)
        {
            var mov = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (mov == null)
                return HttpNotFound();
            var moviesViewModel = new MoviesFormViewModel
            {
                Movies = mov,
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", moviesViewModel);
        }
    }
}