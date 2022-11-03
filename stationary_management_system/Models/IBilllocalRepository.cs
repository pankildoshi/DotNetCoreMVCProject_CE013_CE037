using stationary_management_system.Models;
using System.Collections.Generic;

namespace stationary_management_system.Models
{
    public interface IBilllocalRepository
    {

        Billlocal GetProduct(string Name);
        IEnumerable<Billlocal> GetAllbill();
        Billlocal Add(Billlocal billlocal);
        Billlocal Delete(Billlocal billlocal);
        void DeleteAll();
        
    }
}
