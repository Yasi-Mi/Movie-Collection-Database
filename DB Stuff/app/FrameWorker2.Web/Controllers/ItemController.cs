using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FrameWorker2.Tasks;
using FrameWorker2.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;
using FrameWorker2.Domain;

namespace FrameWorker2.Web.Controllers
{
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        private readonly IRepository<Artwork> _artworkRepository;
        private readonly ArtworkCudTasks _artworkTasks;
        private readonly EditionCudTasks _editionTasks;

        public ItemController(IRepository<Artwork> itemRepository, ArtworkCudTasks itemTasks, EditionCudTasks editionTasks)
        {
            _artworkRepository = itemRepository;
            _artworkTasks = itemTasks;
            _editionTasks = editionTasks;
        }

        public ActionResult Index()
        {
            return View(_artworkRepository.GetAll());
        }

        public ActionResult Edit(int id)
        {
            return View(_artworkTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        public ActionResult Edit(Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                ActionConfirmation<Artwork> confirmation = _artworkTasks.SaveOrUpdate(artwork);
                if (confirmation.WasSuccessful)
                {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Index");
                }

                ViewData["message"] = confirmation.Message;
            }
            return View(_artworkTasks.CreateEditViewModel(artwork));
        }

        public ActionResult EditEdition(int id)
        {
            return View(_editionTasks.CreateEditViewModel(id));
        }

        [HttpPost]
        public ActionResult EditEdition(Edition edition)
        {
            if (ModelState.IsValid)
            {
                int artworkId = edition.Artwork.Id;
                edition.Artwork = _artworkRepository.Get(artworkId);
                ActionConfirmation<Edition> confirmation = _editionTasks.SaveOrUpdate(edition);
                if (confirmation.WasSuccessful)
                {
                    TempData["message"] = confirmation.Message;
                    return RedirectToAction("Edit", new{id = artworkId});
                }

                ViewData["message"] = confirmation.Message;
            }
            return View(_editionTasks.CreateEditViewModel(edition));
        }
    }
}
