using System.Collections.Generic;

namespace CollegeApp.Models
{
    public interface IBillItemRepository
    {
        int Add(BillItem item);
        IEnumerable<BillItem> GetBillItemsByBillId(Bill bill);
    }
}
