using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWpfApp.Model
{
    partial class SaleList
    {
        public string Name
        {
            get
            {
                return $"{Material.Name} - {Amount}";
            }
        }
    }
}
