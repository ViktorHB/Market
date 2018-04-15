using System.Security;
using System.Windows;

namespace RegistrationWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPasswordSupplier
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public SecureString GetPassword()
        {
            return tb_password.SecurePassword;
        }
    }
}
