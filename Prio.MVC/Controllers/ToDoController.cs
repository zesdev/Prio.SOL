using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prio.BL.MainLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prio.MVC.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        IDatLogic mainLogic;
        public ToDoController(IDatLogic _mainLogic)
        {
            mainLogic = _mainLogic;
        }
        public IActionResult Index()
        {
            var aktiviteterCountOfDay = mainLogic.GetTotalActivitiesForDay();
            var models = mainLogic.GetDailyToDos();
            var filteredmodels = models.Where(x => x.toDoModel.IsDone == false).OrderBy(x => x.aktivitetModel.Prioritet).ToList();
            ViewBag.isDone = (aktiviteterCountOfDay-filteredmodels.Count).ToString() + "/" + aktiviteterCountOfDay.ToString();
            return View(filteredmodels);
        }
        public IActionResult Done(int key)
        {
            if(key != 0)
            {
                mainLogic.UpdateToDo(key);
            }
            return RedirectToAction("index");
        }
    }
}
