using stationary_management_system.Models;
using System.Linq;

namespace stationary.Models
{
    public class SQLAdminRepository : IAdminRepository
    {

        private readonly AppDbContext context;
        public SQLAdminRepository(AppDbContext context)
        {
            this.context = context;
        }
        Admin IAdminRepository.AddAdmin(Admin admin)
        {
                context.Admin.Add(admin);
                context.SaveChanges();
                return admin;
        }
        Admin IAdminRepository.GetAdmin(string name,string pass)
        {
            Admin namepresent = context.Admin.FirstOrDefault(m => m.Name == name);
            if(namepresent != null)
            {
                return context.Admin.FirstOrDefault(m => m.Password == pass);
            }
            return null;
        }



    }
}
