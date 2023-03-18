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
            FrameClass.frame.Navigate(new Correct(Role, bascet));
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {

            Order order = new Order();
            int countDay = 0; 
            List<Order> orderLast = Base.ep.Order.OrderBy(x => x.OrderNumber).ToList();
            order.OrderNumber = orderLast[orderLast.Count - 1].OrderNumber + 1;
            order.OrderStatusID = Base.ep.OrderStatus.FirstOrDefault(x => x.Name == "Новый").OrderStatus1; // 
            order.OrderDate = DateTime.Now;
            int a = CheckTime();
            if (a==1)
            {
                countDay = 6;
            }
            else
            {
                countDay = 3;
            }
            order.OrderDeliveryDate = order.OrderDate.AddDays(countDay);
            order.PointsOfIssue = (int)((MatveevWPFSessia1.Adress)cmbPickupPoint.SelectedItem).PointsOfIssue;
            if (user != null)
            {
                order.UserID = user.UserID;
            }
            Random rand = new Random();
            string textCode = "";
            for (int i = 0; i < 3; i++)
            {
                textCode = textCode + rand.Next(10).ToString();
            }
            order.OrderCode = Convert.ToInt32(textCode);
            Base.ep.Order.Add(order);
            Base.ep.SaveChanges();
            foreach (BasketClass productBasket in bascet)
            {
                OrderProduct orderProduct = new OrderProduct();
                orderProduct.OrderID = order.OrderID;
                orderProduct.ProductID = productBasket.product.ProductID;
                orderProduct.Count = productBasket.count;
                Base.ep.OrderProduct.Add(orderProduct);
                Base.ep.SaveChanges();
            }
            
            MessageBox.Show("Заказ успешно создан");
            //Ticket ticket = new Ticket(order, bascet, summa, summaDiscount, countDay);
            //ticket.ShowDialog();
            //bascet.Clear();
            //this.Close();

        }

        public int CheckTime()
        {
            foreach (BasketClass productBasket in bascet)
            {
                if ((productBasket.product.ProductQuantityInStock < 3) || (productBasket.product.ProductQuantityInStock < productBasket.count))
                {
                    return 1;
                }
            }
            return 2;
        }
    }
}
