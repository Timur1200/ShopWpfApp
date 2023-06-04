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

namespace ShopWpfApp.View.Pages.Tovar
{
    /// <summary>
    /// Логика взаимодействия для TovarAddEditPage.xaml
    /// </summary>
    public partial class TovarAddEditPage : Page
    {
        public TovarAddEditPage(Material item)
        {
            InitializeComponent();
            if (item == null) _material = new Material();
            else _material = item;
            _material.IsDeleted = false;
            DataContext = _material;
        }
        private Material _material;
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_material.Id == 0) ShopEntities.GetContext().Material.Add(_material);
                ShopEntities.GetContext().SaveChanges();
                Nav.Back();
                MessageBox.Show("Информация сохранена");
            }
            catch
            {
                MessageBox.Show("Все поля необходимы для заполнения");
            }
        }
    }
}
