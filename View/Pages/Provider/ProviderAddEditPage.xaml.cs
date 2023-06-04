using ShopWpfApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopWpfApp.View.Pages.Provider
{
    /// <summary>
    /// Логика взаимодействия для ProviderAddEditPage.xaml
    /// </summary>
    public partial class ProviderAddEditPage : Page
    {
        public ProviderAddEditPage(Model.Provider item)
        {
            InitializeComponent();
            if (item == null) _item = new Model.Provider();
            else _item = item;
            DataContext = _item;
        }
        Model.Provider _item;    
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_item.Id == 0) ShopEntities.GetContext().Provider.Add(_item);
                ShopEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Nav.Back();
            }
            catch
            {
                MessageBox.Show("Все поля необходимы для заполнения");
            }
        }
    }
}
