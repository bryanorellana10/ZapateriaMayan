using CapaBaseDeDatos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBaseDeDatos.Data
{
    public class UsersRepository
    {
        private Context _context = null;

        public UsersRepository(Context context)
        {
            _context = context;
        }

        public IList<User> GetUsers()
        {
            return _context.Users
             .Include(u => u.Roles)
             .OrderBy(u => u.UserName)
             .ToList();
        }
    }
}
