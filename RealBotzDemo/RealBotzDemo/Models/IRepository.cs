using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealBotzDemo.Models
{
    public interface IRepository
    {
        List<UserList> GetAll();
        User GetById(int id);
        string Add_Update(User user);
        string Delete(int id);
        List<Country> GetCountries();
    }
}
