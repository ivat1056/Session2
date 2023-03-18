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
using System.Windows.Threading;

namespace MatveevWPFSessia1.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static int countNumber;
        public static bool correct;
        DispatcherTimer disTimer = new DispatcherTimer();
        int countTime;
        int Role = 0;
        public MainPage(bool c, int k)
        {
            InitializeComponent();
            disTimer.Interval = new TimeSpan(0, 0, 1);
            disTimer.Tick += new EventHandler(DisTimer_Tick);
            correct = c;
            countNumber = k;
        }

        private void DisTimer_Tick(object sender, EventArgs e)
        {
            if (countTime == 0) 
            {
                disTimer.Stop();
                tbNewCode.Visibility = Visibility.Collapsed;
                Login.IsEnabled = true;
                Password.IsEnabled = true;
                btn_In.IsEnabled = true;
                btn_In.Visibility = Visibility.Visible;
            }
            else
            {
                tbNewCode.Text = "Получить новый код можно будет через " + countTime + " секунд";
            }
            countTime--;      
        }

        private void BtnGetCode_Click(object sender, RoutedEventArgs e)
        {
            btn_In_Click(sender, e);
        }

        private void btn_In_Click(object sender, RoutedEventArgs e)
        {
            User user1 = Base.ep.User.FirstOrDefault(z => z.UserLogin == Login.Text);

            if (user1 != null)
            {
                if (countNumber > 0)
                {
                    btn_In.Visibility = Visibility.Collapsed;
                    Login.IsEnabled = false;
                    Password.IsEnabled = false;
                    btn_In.IsEnabled = false;
                    countTime = 10;
                    tbNewCode.Text = "Повторно можно будет войти через" + countTime + " секунд";
                    tbNewCode.Visibility = Visibility.Visible;
                    disTimer.Start(); 
                }
                else
                {
                    User user2 = Base.ep.User.FirstOrDefault(z => z.UserPassword == Password.Password);
                    if(user2.UserRole == 1)
                    {
                        Role = 1;
                    }
                    if (user2.UserRole == 2)
                    {
                        Role = 2;
                    }
                    if (user2.UserRole == 3)
                    {
                        Role = 3;
                    }
                    if (user2 != null)
                    {
                        correct = true;
                    }

                    else
                    {
                        correct = false;
                    }

                    if (correct == true)
                    {
                        FrameClass.frame.Navigate(new Correct(Role));
                        Authorization.Visibility = Visibility.Collapsed;
                    }
                    else
                    {

                        if (correct == true)
                        {
                            FrameClass.frame.Navigate(new CAPTCHA());
                            Authorization.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            MessageBox.Show("Вход не удачен");
                            Login.Text = "";
                            Password.Password = "";
                            Login.IsEnabled = true;
                            Password.IsEnabled = true;
                            btn_In.Visibility = Visibility.Visible;
                            FrameClass.frame.Navigate(new CAPTCHA());
                            Authorization.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void guest_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new Correct(Role));
        }
    }
}

