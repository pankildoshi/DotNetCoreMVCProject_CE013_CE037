using CollegeApp.Models;
using System.Collections.Generic;

namespace CollegeApp.Models
{
    public interface IBillRepository
    {
        IEnumerable<Bill> GetAllBills();

        int Add(Bill Bill);

        Bill GetBill(int id);
    }
}
