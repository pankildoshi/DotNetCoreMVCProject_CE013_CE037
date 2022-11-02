using CollegeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CollegeApp.Models
{
    public class SQLBillRepository : IBillRepository
    {
        private readonly AppDbContext context;

        public SQLBillRepository(AppDbContext context)
        {
            this.context = context;
        }

        int IBillRepository.Add(Bill Bill)
        {
            context.Bill.Add(Bill);
            context.SaveChanges();
            return Bill.Id;
        }

        IEnumerable<Bill> IBillRepository.GetAllBills()
        {
            return context.Bill;
        }

        Bill IBillRepository.GetBill(int id)
        {
            return context.Bill.FirstOrDefault(bill => bill.Id == id);
        }
    }
}
