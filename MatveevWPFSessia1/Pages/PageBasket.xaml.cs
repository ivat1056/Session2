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
        int Role = 0;
        BasketClass bas;
        public PageBasket(List<BasketClass> bascet, User user)
        {
            InitializeComponent();
            this.bascet = bascet;
            this.user = user;
            lvProduct.ItemsSource = bascet;
            cmbPickupPoint.ItemsSource = Base.ep.Adress.ToList();
            cmbPickupPoint.SelectedValuePath = "PointsOfIssue";
            cmbPickupPoint.DisplayMemberPath = "Address";
            cmbPickupPoint.SelectedIndex = 0;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) // удаление из корзины 
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            BasketClass productBasket = bascet.FirstOrDefault(x => x.product.ProductID == index);
            bascet.Remove(productBasket);
            lvProduct.Items.Refresh();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new Correct(Role, bas));
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {



        }
    }
}
