using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stationary_management_system.Models
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        public SQLProductRepository(AppDbContext context)
        {
            this.context = context;
        }
        Product IProductRepository.Add(Product student)
        {
            context.Products.Add(student);
            context.SaveChanges();
            return student;
        }
        Product IProductRepository.Delete(int Id)
        {
            Product student = context.Products.Find(Id);
            if (student != null)
            {
                context.Products.Remove(student);
                context.SaveChanges();
            }
            return student;
        }

        IEnumerable<Product> IProductRepository.GetAllProducts()
        {
            return context.Products;
        }

        Product IProductRepository.GetProduct(int id)
        {
            return context.Products.FirstOrDefault(m => m.Id == id);
        }

        Product IProductRepository.Update(Product studentChanges)
        {
            var student = context.Products.Attach(studentChanges);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return studentChanges;
        }
    }
    
}
