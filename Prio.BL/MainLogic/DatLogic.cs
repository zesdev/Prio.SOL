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
    }
}
