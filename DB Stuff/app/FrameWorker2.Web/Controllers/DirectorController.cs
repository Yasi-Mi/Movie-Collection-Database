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
    public class DirectorController : Controller
    {
        private readonly IRepository<Director> _DirectorRepository;
        private readonly DirectorCudTasks _DirectorTasks;

        public DirectorController(IRepository<Director> itemRepository, DirectorCudTasks itemTasks, IRepository<Director> aRepository)
        {
            _DirectorRepository = itemRepository;
            _DirectorTasks = itemTasks;
        }

        public ActionResult Index()
        {
            return View(_DirectorRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View("Edit", _DirectorTasks.CreateEditViewModel());
        }

        public ActionResult Edit(int id)
        {
            return View(_DirectorTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Director Director)
        {
            if (ModelState.IsValid)
            {
                ActionConfirmation<Director> confirmation = _DirectorTasks.SaveOrUpdate(Director);
                if (confirmation.WasSuccessful)
                {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index", "Director");
                }

                ViewData["message"] = confirmation.Message;
            }
            return View(_DirectorTasks.CreateEditViewModel(Director));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ActionConfirmation<Director> confirmation = _DirectorTasks.Delete(id);
            TempData["message"] = confirmation.Message;
            return RedirectToAction("Index", "Director");
        }
    }
}
