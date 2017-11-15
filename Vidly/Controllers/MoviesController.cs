using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movie
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            return View(movie);
        }
        //jest to lepsza forma definiowania "ścieżki" nie musimy przełączać się między route.config a kontrolerem
        //jeśli potrzebujemy wyrażeńinnych regex to sprawdzajmy ASP.Net mvc attribute route constraints
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content(year + "/" +month);
        }
    }
}