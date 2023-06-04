using ShopWpfApp.View.Pages.Provider;
using ShopWpfApp.View.Pages.Sale;
using ShopWpfApp.View.Pages.Tovar;
using ShopWpfApp.View.WIndows;
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

namespace ShopWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(bool b)
        {
            InitializeComponent();
            Nav.frame = MainFrame;
            IsAdmin = b;
        }
        public static bool IsAdmin;
        private void Go(Page p)
        {
            Nav.frame.Navigate(p);
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Nav.Back();
        }

        private void TovarClick(object sender, RoutedEventArgs e)
        {
            Go(new TovarMainPage());
        }

        private void ProviderClick(object sender, RoutedEventArgs e)
        {
            Go(new ProviderMainPage());
        }

        private void CheckClick(object sender, RoutedEventArgs e)
        {
            Go(new SaleMainPage());
        }
    }
}
