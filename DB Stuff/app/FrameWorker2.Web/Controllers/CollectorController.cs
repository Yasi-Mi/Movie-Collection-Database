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
    public class CollectorController : Controller
    {
        private readonly IRepository<Collector> _CollectorRepository;
        private readonly CollectorCudTasks _CollectorTasks;

        public CollectorController(IRepository<Collector> itemRepository, CollectorCudTasks itemTasks, IRepository<Collector> aRepository)
        {
            _CollectorRepository = itemRepository;
            _CollectorTasks = itemTasks;
        }

        public ActionResult Index()
        {
            return View(_CollectorRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View("Edit", _CollectorTasks.CreateEditViewModel());
        }

        public ActionResult Edit(int id)
        {
            return View(_CollectorTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Collector Collector)
        {
            if (ModelState.IsValid)
            {
                ActionConfirmation<Collector> confirmation = _CollectorTasks.SaveOrUpdate(Collector);
                if (confirmation.WasSuccessful)
                {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index", "Collector");
                }

                ViewData["message"] = confirmation.Message;
            }
            return View(_CollectorTasks.CreateEditViewModel(Collector));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ActionConfirmation<Collector> confirmation = _CollectorTasks.Delete(id);
            TempData["message"] = confirmation.Message;
            return RedirectToAction("Index", "Collector");
        }
    }
}
