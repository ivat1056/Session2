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
        List<BasketClass> basket12;
        User user;
        public Correct(int Role2, List<BasketClass> basket)
        {
            InitializeComponent();
            lvProduct.ItemsSource = Base.ep.Product.ToList(); // 
            int RoleU = Role2;
            basket12 = basket;
            cbSort.SelectedIndex = 0;
            cbFilt.SelectedIndex = 0;

        }
        public Correct(int Role2)
        {
            InitializeComponent();
            lvProduct.ItemsSource = Base.ep.Product.ToList(); // 
            int RoleU = Role2;
            cbSort.SelectedIndex = 0;
            cbFilt.SelectedIndex = 0;
            basket12 = new List<BasketClass>();
        }

        private void lvProduct_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new PageBasket(basket12, user));
        }

        private void FormAnOrder_Click(object sender, RoutedEventArgs e)
        {
            Product x = (Product)lvProduct.SelectedItem;
            bool stock = false; 
            foreach (BasketClass productBasket in basket12)
            {
                if (productBasket.product == x) 
                {
                    productBasket.count = productBasket.count += 1;
                    stock = true;
                }
            }
            if (!stock) 
            {
                Button btn = (Button)sender;
                int index = Convert.ToInt32(btn.Uid);
                Product basket = Base.ep.Product.FirstOrDefault(z => z.ProductID == index);
                BasketClass product = new BasketClass();
                product.product = basket;
                product.count = 1;
                basket12.Add(product);
                MessageBox.Show("Заказ добавлен в корзину");
            }
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new OrdersPage());
        }
        public void Filter()
        {
            List<Product> products = Base.ep.Product.ToList();
            if (tbSearch.Text != "")
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
                List<Product> products1 = Base.ep.Product.Where(x => x.ProductDescription.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
                foreach (Product material in products1)
                {
                    products.Add(material);
                }
                products = products.Distinct().ToList();
            }
            if (cbFilt.SelectedIndex > 0) 
            {
                switch (cbFilt.SelectedIndex)
                {
                    case 1:
                        products = products.Where(x => x.ProductDiscountAmount > 0 && x.ProductDiscountAmount < 9.99).ToList();
                        break;
                    case 2:
                        products = products.Where(x => x.ProductDiscountAmount > 10 && x.ProductDiscountAmount < 14.99).ToList();
                        break;
                    case 3:
                        products = products.Where(x => x.ProductDiscountAmount > 15).ToList();
                        break;
                }
            }
            switch (cbSort.SelectedIndex)
            {
                case 1:
                    products = products.OrderBy(x => x.ProductCost).ToList();
                    break;
                case 2:
                    products = products.OrderByDescending(x => x.ProductCost).ToList();
                    break;
            }
            lvProduct.ItemsSource = products;
            if (products.Count == 0)
            {
                MessageBox.Show("Результат не найден");
            }
            CountRecords.Text = products.Count + " из " + Base.ep.Product.ToList().Count;
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
