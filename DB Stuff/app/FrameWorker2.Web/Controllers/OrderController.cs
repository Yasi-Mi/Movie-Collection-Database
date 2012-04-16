using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using FrameWorker2.Tasks;
using FrameWorker2.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;
using FrameWorker2.Domain;

namespace FrameWorker2.Web.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        private readonly IRepository<Order> _orderRepository;
        //private readonly OrderCudTasks _orderTasks;

        public ActionResult Index()
        {
            return View(_orderRepository.GetAll());
        }
        
        //public OrderController(IRepository<Edition> orderRepository, OrderCudTasks orderTasks)
        //{
        //    _itemRepository = itemRepository;
        //    _itemTasks = itemTasks;
        //}


    }
}
