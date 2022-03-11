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

namespace PR3._1
{
    /// <summary>
    /// Interaction logic for AvtorizationWindow.xaml
    /// </summary>
    public partial class AvtorizationWindow : Page
    {
        string Login = "employee";
        string Pass = "employee";
        public AvtorizationWindow()
        {
            InitializeComponent();
        }

        int counter = 0;
        DispatcherTimer timer = new DispatcherTimer();
        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {

            if (tbLogin.Text == Login && pbPass.Password == Pass)
            {
                MessageBox.Show("Вы вошли");
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                counter++;
                MessageBox.Show("Такого пользователя нет!");
            }
            if (counter >= 3)
            {
                MessageBox.Show("Вы ввели 3 раза неправильно данные! \nПодождите 1 минуту.");
                timer = new System.Windows.Threading.DispatcherTimer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = new TimeSpan(0, 0, 60);
                btnEnter.Visibility = Visibility.Hidden;
                timer.Start();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            btnEnter.Visibility = Visibility.Visible;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
