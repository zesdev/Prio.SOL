using Prio.BL.Database.Models;
using System.Collections.Generic;

namespace Prio.BL.Database.ModelLogic
{
    public interface IAktivitetLogic
    {
        void AddNewAktivitet(AktivitetModel model);
        List<AktivitetModel> GetAllAktiviteter();
        void RemoveAktivitet(int key);
        void UpdateAktivitet(int key, AktivitetModel model);
    }
}