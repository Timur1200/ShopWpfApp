using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWpfApp.Model
{
    partial class MaterialProviderList
    {
        public string Name
        {
            get
            {
                return $"{Material.Name} - {Price} руб";
            }
        }
    }
}
