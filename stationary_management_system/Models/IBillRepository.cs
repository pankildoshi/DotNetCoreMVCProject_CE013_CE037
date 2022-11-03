using stationary_management_system.Models;
using System.Collections.Generic;

namespace stationary_management_system.Models
{
    public interface IBillRepository
    {
        IEnumerable<Bill> GetAllBills();

        int Add(Bill Bill);

        Bill GetBill(int id);
    }
}
