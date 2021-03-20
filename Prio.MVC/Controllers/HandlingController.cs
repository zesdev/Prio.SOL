using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prio.BL.Database.Models;
using Prio.BL.MainLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prio.MVC.Controllers
{
    [Authorize]
    public class HandlingController : Controller
    {
        IDatLogic mainLogic;
        public HandlingController(IDatLogic _mainLogic)
        {
            mainLogic = _mainLogic;
        }
        public IActionResult Index()
        {
            return View(mainLogic.GetHandlingItems());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(HandlingItemModel model)
        {
            mainLogic.AddHandlingItem(model);
            return RedirectToAction("index");
        }
        public IActionResult Remove(int key)
        {
            mainLogic.RemoveHandlingItem(key);
            return RedirectToAction("index");
        }
        public IActionResult Edit(int key)
        {
            var model = mainLogic.GetHandlingItems().First(x => x.Key == key);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(HandlingItemModel model)
        {
            mainLogic.UpdateHandlingItem(model);
            return RedirectToAction("index");
        }
    }
}
