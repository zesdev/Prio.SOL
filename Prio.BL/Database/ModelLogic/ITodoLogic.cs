using Prio.BL.Database.Models;
using System.Collections.Generic;

namespace Prio.BL.Database.ModelLogic
{
    public interface ITodoLogic
    {
        void AddNewToDo(ToDoModel model);
        List<ToDoModel> GetDailyToDos();
        void RemoveAllToDosForAktivitet(int key);
        void UpdateToDo(int aktivitetKey);
    }
}