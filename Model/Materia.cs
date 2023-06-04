using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWpfApp.Model
{
    partial class Material
    {
        public string FullName
        {
            get
            {
                return $"{Name} - {Price} руб - {Amount} шт.";
            }
        }
    }
}
