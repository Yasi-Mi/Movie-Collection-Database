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
    public class WriterController : Controller
    {
        private readonly IRepository<Writer> _WriterRepository;
        private readonly WriterCudTasks _WriterTasks;

        public WriterController(IRepository<Writer> itemRepository, WriterCudTasks itemTasks, IRepository<Writer> aRepository)
        {
            _WriterRepository = itemRepository;
            _WriterTasks = itemTasks;
        }

        public ActionResult Index()
        {
            return View(_WriterRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View("Edit", _WriterTasks.CreateEditViewModel());
        }

        public ActionResult Edit(int id)
        {
            return View(_WriterTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Writer Writer)
        {
            if (ModelState.IsValid)
            {
                ActionConfirmation<Writer> confirmation = _WriterTasks.SaveOrUpdate(Writer);
                if (confirmation.WasSuccessful)
                {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index", "Writer");
                }

                ViewData["message"] = confirmation.Message;
            }
            return View(_WriterTasks.CreateEditViewModel(Writer));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ActionConfirmation<Writer> confirmation = _WriterTasks.Delete(id);
            TempData["message"] = confirmation.Message;
            return RedirectToAction("Index", "Writer");
        }
    }
}
