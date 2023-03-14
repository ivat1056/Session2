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
    /// Логика взаимодействия для Correct.xaml
    /// </summary>
    public partial class Correct : Page
    {
        List<BasketClass> basket = new List<BasketClass>();
        User user;
        public Correct(int Role2)
        {
            InitializeComponent();
            lvProduct.ItemsSource = Base.ep.Product.ToList();
            int RoleU = Role2;
            

        }

        private void lvProduct_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new PageBasket(basket, user));
        }

        private void FormAnOrder_Click(object sender, RoutedEventArgs e)
        {
            Product x = (Product)lvProduct.SelectedItem;
            bool stock = false; 
            foreach (BasketClass productBasket in basket)
            {
                if (productBasket.product == x) 
                {
                    productBasket.count = productBasket.count += 1;
                    stock = true;
                }
            }
            if (!stock) 
            {
                BasketClass product = new BasketClass();
                product.product = x;
                product.count = 1;
                basket.Add(product);
            }
        }
    }
}
