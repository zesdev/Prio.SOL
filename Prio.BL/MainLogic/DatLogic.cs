using Prio.BL.Database.ModelLogic;
using Prio.BL.Database.Models;
using Prio.BL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prio.BL.MainLogic
{
    public class DatLogic : IDatLogic
    {
        AktivitetLogic aktivitetsLogic;
        TodoLogic toDoLogic;

        public DatLogic(AktivitetLogic _aktivitetsLogic, TodoLogic _toDoLogic)
        {
            aktivitetsLogic = _aktivitetsLogic;
            toDoLogic = _toDoLogic;
        }

        public List<ToDoViewModel> GetDailyToDos()
        {
            var aktiviteter = aktivitetsLogic.GetAllAktiviteter();
            var date = DateTime.Now;
            var dailyToDos = toDoLogic.GetDailyToDos();
            foreach (var aktivitet in aktiviteter)
            {
                if (dailyToDos.Where(x => x.AktivitetKey == aktivitet.Key).ToList().Count == 0)
                {
                    var model = new ToDoModel { AktivitetKey = aktivitet.Key, IsDone = false };
                    toDoLogic.AddNewToDo(model);
                }
            }
            var toDoModels = toDoLogic.GetDailyToDos();
            var toDoViewModels = new List<ToDoViewModel>();

            foreach (var toDoModel in toDoModels)
            {
                var viewModel = new ToDoViewModel { toDoModel = toDoModel, aktivitetModel = aktiviteter.First(x => x.Key == toDoModel.AktivitetKey) };
                toDoViewModels.Add(viewModel);
            }
            if(DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                toDoViewModels = toDoViewModels.Where(x => x.aktivitetModel.Måndag == true).ToList();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                toDoViewModels = toDoViewModels.Where(x => x.aktivitetModel.Tisdag == true).ToList();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                toDoViewModels = toDoViewModels.Where(x => x.aktivitetModel.Onsdag == true).ToList();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                toDoViewModels = toDoViewModels.Where(x => x.aktivitetModel.Torsdag == true).ToList();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                toDoViewModels = toDoViewModels.Where(x => x.aktivitetModel.Fredag == true).ToList();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                toDoViewModels = toDoViewModels.Where(x => x.aktivitetModel.Lördag == true).ToList();
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                toDoViewModels = toDoViewModels.Where(x => x.aktivitetModel.Söndag == true).ToList();
            }
            return toDoViewModels;
        }
        public List<AktivitetModel> GetAllAktiviteter()
        {
            return aktivitetsLogic.GetAllAktiviteter();
        }
        public void RemoveAktivitet(int key)
        {
            aktivitetsLogic.RemoveAktivitet(key);
        }
        public void UpdateAktivitet(int key, AktivitetModel model)
        {
            aktivitetsLogic.UpdateAktivitet(key, model);
        }
        public void UpdateToDo(int aktivitetsKey)
        {
            toDoLogic.UpdateToDo(aktivitetsKey);
        }

        public void AddNew(AktivitetModel model)
        {
            aktivitetsLogic.AddNewAktivitet(model);
        }

        public int GetTotalActivitiesForDay()
        {
            var aktiviteter = GetAllAktiviteter();
            if(DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                return aktiviteter.Where(x => x.Måndag == true).ToList().Count;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                return aktiviteter.Where(x => x.Tisdag == true).ToList().Count;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                return aktiviteter.Where(x => x.Onsdag == true).ToList().Count;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                return aktiviteter.Where(x => x.Torsdag == true).ToList().Count;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                return aktiviteter.Where(x => x.Fredag == true).ToList().Count;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                return aktiviteter.Where(x => x.Lördag == true).ToList().Count;
            }
            else
            {
                return aktiviteter.Where(x => x.Söndag == true).ToList().Count;
            }
        }
    }
}
