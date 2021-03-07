using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prio.BL.Database.ModelLogic;
using Prio.BL.Database.Models;
using Prio.BL.MainLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prio.MVC.Controllers
{
    [Authorize]
    public class AktivitetController : Controller
    {
        private IDatLogic mainLogic;
        public AktivitetController(IDatLogic _mainLogic)
        {
            mainLogic = _mainLogic;
        }
        public IActionResult Index()
        {
            var datmodels = mainLogic.GetAllAktiviteter();
            datmodels = datmodels.OrderBy(x => x.Prioritet).ToList();
            return View(datmodels);
        }
        public IActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNew(AktivitetModel model)
        {
            try
            {
                mainLogic.AddNew(model);
            }
            catch
            { 
            }
            return RedirectToAction("index");
        }
        public IActionResult Remove(int key)
        {
            if(key != 0)
            {
                var aktivitet = mainLogic.GetAllAktiviteter().FirstOrDefault(x => x.Key == key);
                return View(aktivitet);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public IActionResult Remove(AktivitetModel model)
        {
            if (model.Key != 0)
            {
                mainLogic.RemoveAktivitet(model.Key);
            }
            return RedirectToAction("index");
        }

        public IActionResult Edit(int key)
        {
            if (key != 0)
            {
                var model = mainLogic.GetAllAktiviteter().FirstOrDefault(x => x.Key == key);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(AktivitetModel model)
        {
            if (model.Key != 0)
            {
                mainLogic.UpdateAktivitet(model.Key, model);
            }
            return RedirectToAction("Index");
        }
    }
}
