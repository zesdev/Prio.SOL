using Prio.BL.Database.Models;
using Prio.BL.ViewModels;
using System.Collections.Generic;

namespace Prio.BL.MainLogic
{
    public interface IDatLogic
    {
        List<AktivitetModel> GetAllAktiviteter();
        List<ToDoViewModel> GetDailyToDos();
        void RemoveAktivitet(int key);
        void UpdateAktivitet(int key, AktivitetModel model);
        void UpdateToDo(int aktivitetsKey);
        void AddNew(AktivitetModel model);
    }
}