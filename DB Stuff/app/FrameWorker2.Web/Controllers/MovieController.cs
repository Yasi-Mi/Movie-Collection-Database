using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieDatabase.Domain;
using MovieDatabase.Tasks;
using MovieDatabase.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace MovieDatabase.Web.Areas.ItemMgmt.Controllers
{
    public class MovieController : Controller
    {
        private readonly IRepository<Movie> _MovieRepository;
        private readonly MovieCudTasks _MovieTasks;

        public MovieController(IRepository<Movie> itemRepository, MovieCudTasks itemTasks, IRepository<Movie> aRepository)
        {
            _MovieRepository = itemRepository;
            _MovieTasks = itemTasks;
        }

        public ActionResult Index()
        {
            return View(_MovieRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View("Edit", _MovieTasks.CreateEditViewModel());
        }

        public ActionResult Edit(int id)
        {
            return View(_MovieTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie Movie)
        {
            if (ModelState.IsValid)
            {
                ActionConfirmation<Movie> confirmation = _MovieTasks.SaveOrUpdate(Movie);
                if (confirmation.WasSuccessful)
                {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index", "Movie");
                }

                ViewData["message"] = confirmation.Message;
            }
            return View(_MovieTasks.CreateEditViewModel(Movie));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ActionConfirmation<Movie> confirmation = _MovieTasks.Delete(id);
            TempData["message"] = confirmation.Message;
            return RedirectToAction("Index", "Movie");
        }
    }
}
