using Prio.BL.Database.Models;
using System.Collections.Generic;

namespace Prio.BL.Database.ModelLogic
{
    public interface IHandlingslistaLogic
    {
        void AddNewHandlingItem(HandlingItemModel model);
        List<HandlingItemModel> GetItems();
        void RemoveItem(int key);
        void UpdateItem(HandlingItemModel model);
    }
}