using stationary_management_system.Models;

namespace stationary.Models
{
    public interface IAdminRepository
    {
        Admin AddAdmin(Admin admin);
        Admin GetAdmin(string name,string pass);
    }
}
