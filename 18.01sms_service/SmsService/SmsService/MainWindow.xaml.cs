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

namespace SmsService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_SendSms_Click(object sender, RoutedEventArgs e)
        {
            ChannelFactory<IContractMessage> channelFactory =
                new ChannelFactory<IContractMessage>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:8000/Service"));

            IContractMessage proxy = channelFactory.CreateChannel();
            proxy.SendMessage(tb_number.Text, tb_message.Text);
        }
    }
}
