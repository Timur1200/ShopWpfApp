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
    /// Логика взаимодействия для ProviderMainPage.xaml
    /// </summary>
    public partial class ProviderMainPage : Page
    {
        public ProviderMainPage()
        {
            InitializeComponent();
            if (!MainWindow.IsAdmin) adminPanel.Visibility = Visibility.Collapsed;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new ProviderAddEditPage(null));
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new ProviderAddEditPage(DGrid.SelectedItem as Model.Provider));
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            _list = ShopEntities.GetContext().Provider.ToList();
            DGrid.ItemsSource = _list;
        }
        List<Model.Provider> _list;
        private void TovarClick(object sender, RoutedEventArgs e)
        {
            var item = DGrid.SelectedItem as Model.Provider;
            Nav.frame.Navigate(new ProviderAddEditTovarPage(item));
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text.Length == 0)
            {
                DGrid.ItemsSource = _list;
                return;
            }
            string text = SearchTextBox.Text.ToLower();
            DGrid.ItemsSource = _list.Where(q => q.Name.ToLower().Contains(text) || q.Phone.Contains(text)).ToList();

        }
    }
}
