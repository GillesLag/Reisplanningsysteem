using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_DAL
{
    public class DatabaseOperations
    {
        private readonly ReisplanningssysteemContext _context;

        public DatabaseOperations()
        {
            _context = new ReisplanningssysteemContext();
        }
    }
}
