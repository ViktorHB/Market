using System.Windows;

namespace AdminWindow
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
        public MainWindow(string login)
        {
            InitializeComponent();
            this.Title = login;    
        }
    }
}
