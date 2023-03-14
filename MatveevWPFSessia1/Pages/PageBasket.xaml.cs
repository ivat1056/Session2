using MatveevWPFSessia1.Class;
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

namespace MatveevWPFSessia1.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageBasket.xaml
    /// </summary>
    public partial class PageBasket : Page
    {
        User user;
        List<BasketClass> bascet; 
        public PageBasket(List<BasketClass> bascet, User user)
        {
            InitializeComponent();
            this.bascet = bascet;
            this.user = user;
            lvProduct.ItemsSource = bascet;

            cmbPickupPoint.ItemsSource = Base.ep.Adress.ToList();
            cmbPickupPoint.SelectedIndex = 0;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
