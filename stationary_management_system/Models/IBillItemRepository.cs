using System.Collections.Generic;

namespace stationary_management_system.Models
{
    public interface IBillItemRepository
    {
        int Add(BillItem item);
        IEnumerable<BillItem> GetBillItemsByBillId(Bill bill);
    }
}
