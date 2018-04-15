using MWVMLib;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AuthorizationWindow
{
    class AuthViewModel : ViewModelBase
    {
        private String _login;
        private String _password;
        private ICommand _signIn;
        private ICommand _signUp;
        private Visibility _isVisibleMainZone;
        private Visibility _isVisibleConnectingString;

        private AuthLogic _logic;
        public AuthViewModel()
        {
            _logic = new AuthLogic();
            DefaultVisabilities();
        }



        private void SignUpCommand(object obj)
        {
            RunRegistrationWindowAsync();
        }

        private Task RunRegistrationWindowAsync()
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    new RegistrationWindow.MainWindow().ShowDialog();
                }));
            });
        }

        private void SignInCommand(object parameter)
        {
            Password = ReceivePassword(parameter);
            ChangeVisabilities();
            CheckLoginAngPasswordAsync();

            #region notAsync

            //ReceivePassword(parameter);

            //if (_logic.Check(Login, Password))
            //{
            //    switch (_logic.GetStatus(Login))
            //    {
            //        case "admin":
            //            // await RunAdminWndAsync();
            //            // new Thread(RunAdminWnd);
            //            RunAdminWnd();
            //            Application.Current.MainWindow.Close();
            //            break;
            //        case "user":
            //            //new Thread(RunUserWnd);
            //            // Thread.Sleep(3000);
            //            RunUserWnd();
            //            Application.Current.MainWindow.Close();
            //            break;
            //        default: break;
            //    }
            //}
            //else
            //{
            //    Login = null;
            //    Password = null;
            //    MessageBox.Show("Такого пользователя не сущаствует! Проверьте правильность ввода данных!");
            //}

            #endregion notAsync
        }

        private async void CheckLoginAngPasswordAsync()
        {
            if (await EnterWindowsAsync() == true)
            {
                Application.Current.MainWindow.Close();
            }
            else
            {
                Login = null;
                Password = null;
                MessageBox.Show("Такого пользователя не сущаствует! Проверьте правильность ввода данных!");
                DefaultVisabilities();
            }
        }

        private Task<bool> EnterWindowsAsync()
        {
            return Task.Run(() =>
            {
                if (_logic.Check(Login, Password))
                {
                    switch (_logic.GetStatus(Login))
                    {
                        case "admin":
                            RunAdminWndAsync();
                            Thread.Sleep(3000);
                            break;
                        case "user":
                            RunUserWndAsync();
                            Thread.Sleep(3000);
                            break;
                        default: break;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        private void RunAdminWndAsync()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => { new AdminWindow.MainWindow(Login).Show(); }));
        }
        private void RunUserWndAsync()
        {
            Action action = () =>
            {
                new UserWindow.MainWindow(Login).Show();
            };
            Application.Current.Dispatcher.BeginInvoke(action);
        }
        private void RunAdminWnd()
        {
            AdminWindow.MainWindow wAdmin = new AdminWindow.MainWindow(Login);
            wAdmin.Show();
        }
        private void RunUserWnd()
        {
            UserWindow.MainWindow wUser = new UserWindow.MainWindow(Login);
            wUser.Show();
        }
        private void DefaultVisabilities()
        {
            IsVisibleConnectingString = Visibility.Hidden;
            IsVisibleMainZone = Visibility.Visible;
        }
        private void ChangeVisabilities()
        {
            IsVisibleMainZone = Visibility.Hidden;
            IsVisibleConnectingString = Visibility.Visible;
        }


        #region ForPasswordBox

        private String ReceivePassword(object parameter)
        {
            var passwordContainer = parameter as IPasswordSupplier;

            if (passwordContainer != null)
            {
                var secureString = passwordContainer.GetPassword();
                //GET PASSWORD!!!
                return ConvertToUnsecureString(secureString);
            }
            return null;
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        #endregion ForPasswordBox

        #region Prop

        public Visibility IsVisibleConnectingString
        {
            get
            {
                return _isVisibleConnectingString;
            }
            set
            {
                _isVisibleConnectingString = value;
                OnPropertyChanged(nameof(IsVisibleConnectingString));
            }
        }

        public Visibility IsVisibleMainZone
        {
            get
            {
                return _isVisibleMainZone;
            }
            set
            {
                _isVisibleMainZone = value;
                OnPropertyChanged(nameof(IsVisibleMainZone));

            }
        }
        public ICommand SignUp
        {
            get
            {
                if (_signUp == null)
                    _signUp = new RelayCommand(SignUpCommand);
                return _signUp;
            }
            set
            {
                _signUp = value;
            }
        }
        public ICommand SignIn
        {
            get
            {
                if (_signIn == null)
                    _signIn = new RelayCommand(SignInCommand);
                return _signIn;
            }
            set
            {
                _signIn = value;
            }
        }

        public String Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public String Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        #endregion Prop
    }
}
