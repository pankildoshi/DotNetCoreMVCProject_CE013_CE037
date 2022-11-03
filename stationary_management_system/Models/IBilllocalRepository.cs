using CollegeApp.Models;
using System.Collections.Generic;

namespace CollegeApp.Models
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
