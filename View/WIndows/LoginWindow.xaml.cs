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
using System.Windows.Shapes;

namespace ShopWpfApp.View.WIndows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Login(bool b)
        {
            MainWindow main= new MainWindow(b);
            main.Show();
            this.Close();
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (LoginTBox.Text == "1" && PassBox.Password == "1")
            {
                Login(true);
                return;
            }
            else if (LoginTBox.Text == "2" && PassBox.Password == "2")
            {
                Login(false);
                return;
            }
            MessageBox.Show("Неверно введен логин или пароль");
        }
    }
}
