using stationary_management_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace stationary_management_system.Models
{
    public class SQLBillItemRepository : IBillItemRepository
    {
        private readonly AppDbContext context;

        public SQLBillItemRepository(AppDbContext context)
        {
            this.context = context;
        }

        int IBillItemRepository.Add(BillItem item)
        {
            context.BillItem.Add(item);
            context.SaveChanges();
            return item.Id;
        }

        IEnumerable<BillItem> IBillItemRepository.GetBillItemsByBillId(Bill bill)
        {
            var items = context.BillItem.Where(item => item.Bill == bill).AsEnumerable();
            return items;
        }
    }
}
