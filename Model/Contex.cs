using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWpfApp.Model
{
    partial class ShopEntities
    {
        private static ShopEntities _context;
        public static ShopEntities GetContext()
        {
            if (_context == null) _context = new ShopEntities();
            return _context;
        }
    }
}
