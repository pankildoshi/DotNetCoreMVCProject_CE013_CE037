using stationary_management_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace stationary_management_system.Models
{
    public class SQLBilllocalRepository: IBilllocalRepository
    {

        private readonly AppDbContext context;

        public SQLBilllocalRepository(AppDbContext context)
        {
            this.context = context;
        }

        Billlocal IBilllocalRepository.Delete(Billlocal billlocal)
        {
            string name = billlocal.Name;
            Product fetchdata = context.Products.FirstOrDefault(m => m.Name == name);
            fetchdata.Quantity = fetchdata.Quantity + billlocal.Quantity;
            Billlocal productdelete = context.Billlocal.Find(name);
            if (productdelete != null)
            {
                var product = context.Products.Attach(fetchdata);
                product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Billlocal.Remove(productdelete);
                context.SaveChanges();
            }
            return billlocal;
        }
        Billlocal IBilllocalRepository.GetProduct(string name)
        {
            return context.Billlocal.FirstOrDefault(m => m.Name == name);
        }

        Billlocal IBilllocalRepository.Add(Billlocal billlocal)
        {
            string name = billlocal.Name;
            Product fetchdata= context.Products.FirstOrDefault(m => m.Name == name);
            if (fetchdata != null)
            {
                if (fetchdata.Quantity > 0 && fetchdata.Quantity >= billlocal.Quantity)
                {
                    billlocal.Price = fetchdata.Price;
                    billlocal.Totalprice=fetchdata.Price*billlocal.Quantity;
                    fetchdata.Quantity =fetchdata.Quantity-billlocal.Quantity;
                    var student = context.Products.Attach(fetchdata);
                    student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.Billlocal.Add(billlocal);
                    context.SaveChanges();
                    return billlocal;
                }
                return null;
            }
            return null;
        }

        IEnumerable<Billlocal> IBilllocalRepository.GetAllbill()
        {
            return context.Billlocal;
        }

        void IBilllocalRepository.DeleteAll()
        {
            context.RemoveRange(context.Billlocal);
            context.SaveChanges();
        }
    }
}
