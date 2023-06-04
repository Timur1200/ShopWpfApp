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
using Word = Microsoft.Office.Interop.Word;

namespace ShopWpfApp.View.Pages.Sale
{
    /// <summary>
    /// Логика взаимодействия для SaleMainPage.xaml
    /// </summary>
    public partial class SaleMainPage : Page
    {
        public SaleMainPage()
        {
            InitializeComponent();
            if (!MainWindow.IsAdmin) adminPanel.Visibility = Visibility.Collapsed;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            _list = ShopEntities.GetContext().Check.ToList();
            DGrid.ItemsSource = _list;
        }
        List<Check> _list;
        private void AddClick(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new SaleAddEditPage(null));
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem == null)
            {
                MessageBox.Show("Нужно выбрать запись!");
                return;
            }
            var item = DGrid.SelectedItem as Check;
            Nav.frame.Navigate(new SaleAddEditPage(item));
        }
        private readonly string TemplateFileName = System.IO.Path.GetFullPath(@"Word/file.docx");
        void ReplaceWordStub(String stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }
        private void WordClick(object sender, RoutedEventArgs e)
        {
            var item = DGrid.SelectedItem as Check;
            var wordApp = new Word.Application();

            wordApp.Visible = false;
            var wordDocument = wordApp.Documents.Open(TemplateFileName);
            ReplaceWordStub("(код)", $"{item.Id}", wordDocument);
            ReplaceWordStub("(дата)", $"{item.Date}", wordDocument);
            ReplaceWordStub("(сумма)", $"{item.Sum}", wordDocument);
            StringBuilder text = new StringBuilder();
            var tovars = ShopEntities.GetContext().SaleList.Where(q => q.CheckId == item.Id).ToList();
            foreach(var i in tovars)
            {
                string str = $"{i.Material.Name} {i.Material.Price}руб x {i.Amount}шт.";
                text.AppendLine(str);
            }
            ReplaceWordStub("(товар)", text.ToString() , wordDocument);
            wordDocument.SaveAs2(System.IO.Path.GetFullPath($@"Word/{Guid.NewGuid()}.docx"));

            wordApp.Visible = true;

        }

        private void Date1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Date1.SelectedDate == null) return;
                DateTime dateTime = Date1.SelectedDate.Value;
                DGrid.ItemsSource = _list.Where(q=>q.Date.Value.ToString("d") == dateTime.ToString("d")).ToList();
            }
            catch
            {

            }
        }

        private void ReloadClick(object sender, RoutedEventArgs e)
        {
            DGrid.ItemsSource = _list;
        }
    }
}
