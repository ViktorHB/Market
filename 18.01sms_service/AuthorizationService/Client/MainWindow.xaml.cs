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
using System.ServiceModel;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChannelFactory<IContract> channelFactory;
        IContract proxy;

        public MainWindow()
        {
            InitializeComponent();
            channelFactory =
               new ChannelFactory<IContract>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8000/MyService"));

            proxy = channelFactory.CreateChannel();
        }

        private void button_sign_in_Click(object sender, RoutedEventArgs e)
        {
            
            var _doesexist = proxy.doesExist(tb_login.Text);

            if (_doesexist)
            {
                proxy.SignIn(tb_login.Text, tb_password.Text);
                MessageBox.Show("Authorization is successful.");
            }

            else
                MessageBox.Show("You should sign up to enter the System.");
            
        }

        private void button_sign_up_Click(object sender, RoutedEventArgs e)
        {
            if (proxy.SignUp(tb_login.Text, tb_password.Text))
                MessageBox.Show("SignUp is successful.");
        }
    }
}
