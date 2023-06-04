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
    /// Логика взаимодействия для ProviderAddEditTovarPage.xaml
    /// </summary>
    public partial class ProviderAddEditTovarPage : Page
    {
        public ProviderAddEditTovarPage(Model.Provider prov)
        {
            InitializeComponent();
            _Provider= prov;
            _MaterialList = ShopEntities.GetContext().Material.Where(q => !q.IsDeleted).ToList();
            TovarCBox.ItemsSource = _MaterialList; 

        }
        private List<Material> _MaterialList;
        private Model.Provider _Provider;
        private MaterialProviderList _item;
        private void TovarCBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (TovarCBox.Text == "")
            {

                TovarCBox.ItemsSource = _MaterialList;
                TovarCBox.SelectedItem = null;
                return;
            }
            TovarCBox.IsDropDownOpen = true;
            string Text = TovarCBox.Text.ToLower();


            TovarCBox.ItemsSource = _MaterialList.Where(q => q.Name.ToLower().Contains(Text)).ToList();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_item.Id == 0) ShopEntities.GetContext().MaterialProviderList.Add(_item);

                ShopEntities.GetContext().SaveChanges();
                PageLoaded(null, null);
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так :с");
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            TovarLBox.ItemsSource = ShopEntities.GetContext().MaterialProviderList
                .Where(q=>q.ProviderId == _Provider.Id).ToList();
            MaterialProviderList item = new MaterialProviderList();
            item.Provider = _Provider;
            _item= item;
            DataContext = _item;
        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            if (TovarLBox.SelectedItem == null)
            {
                MessageBox.Show("Нужно выбрать запись для удаления");
                return;
            }
            var item = TovarLBox.SelectedItem as MaterialProviderList;
            ShopEntities.GetContext().MaterialProviderList.Remove(item);
            ShopEntities.GetContext().SaveChanges();
            PageLoaded(null, null);
        }

        private void ReloadClick(object sender, RoutedEventArgs e)
        {
            PageLoaded(null, null); 
        }

       
       

        private void SelectedClick(object sender, SelectionChangedEventArgs e)
        {
            var item = TovarLBox.SelectedItem as MaterialProviderList;
            TovarCBox.SelectedItem = null;
            _item = item;
            DataContext = _item;
        }
    }
}
