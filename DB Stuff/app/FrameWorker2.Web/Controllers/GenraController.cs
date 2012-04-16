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
    public class GenreController : Controller
    {
        private readonly IRepository<Genre> _GenreRepository;
        private readonly GenreCudTasks _GenreTasks;

        public GenreController(IRepository<Genre> itemRepository, GenreCudTasks itemTasks, IRepository<Genre> aRepository)
        {
            _GenreRepository = itemRepository;
            _GenreTasks = itemTasks;
        }

        public ActionResult Index()
        {
            return View(_GenreRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View("Edit", _GenreTasks.CreateEditViewModel());
        }

        public ActionResult Edit(int id)
        {
            return View(_GenreTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre Genre)
        {
            if (ModelState.IsValid)
            {
                ActionConfirmation<Genre> confirmation = _GenreTasks.SaveOrUpdate(Genre);
                if (confirmation.WasSuccessful)
                {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index", "Genre");
                }

                ViewData["message"] = confirmation.Message;
            }
            return View(_GenreTasks.CreateEditViewModel(Genre));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ActionConfirmation<Genre> confirmation = _GenreTasks.Delete(id);
            TempData["message"] = confirmation.Message;
            return RedirectToAction("Index", "Genre");
        }
    }
}
