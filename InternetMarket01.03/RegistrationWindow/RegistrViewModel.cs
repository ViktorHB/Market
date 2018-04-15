using MWVMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace RegistrationWindow
{
    class RegistrViewModel : ViewModelBase
    {

        private String _login;
        private String _email;
        private String _password;
        private String _secureCode;
        private String _generatedCode = "!@#$%^&*(*(&^%$@#$%^()_";
        private String _message;
        private ICommand _signUp;
        private ICommand _sendSms;
        private Visibility _visabilityOfMessage;
        private Visibility _visabilityOfCodeText;
        private Visibility _visabilityOfCodeTextBox;
        private RegistrLogic _logic;
        List<String> _uaOperators;

        public RegistrViewModel()
        {
            _logic = new RegistrLogic();
            _uaOperators = new List<string>
            {
                "66","99","50","95","63","93,","73","97","67","98","39","91","92","94"
            };
            HideAllHiddenThings();
        }


        private async void SendSmsCommand(object obj)
        {
            if (await CheckUserExixtAsync() != true)
            {
                _generatedCode = new Random().Next(1000, 9999).ToString();
                await SendSmsAsync(_generatedCode);
                VisabilityOfCodeText = Visibility.Visible;
                VisabilityOfCodeTextBox = Visibility.Visible;
            }
            else
            {
                LoginExist();
            }
        }
        private void LoginExist()
        {
            ShowMessage();
            Message = "Такой номер телефона уже зарегистрированный!";
            ClearTheInputFields();
        }

        private void ShowMessage()
        {
            VisabilityOfMessage = Visibility.Visible;
        }

        private Task<bool> CheckUserExixtAsync()
        {
            return Task.Run(() =>
            {
                return _logic.CheckUserName(Login);
            });
        }

        private Task<bool> SendSmsAsync(String code)
        {
            return Task.Run(() =>
           {
               new SmsLogic().SendSms(Login, _generatedCode);
               return true;
           });
        }

        private async void SignUpCommand(object obj)
        {
            if (LoginParsing())
            {
                Password = ReceivePassword(obj);
                if (OtheInputsCheck() && SecureCodeCheck())
                {
                    try
                    {
                        await AddUserAsync();
                        RunUserWindow();
                        Application.Current.Windows.OfType<RegistrationWindow.MainWindow>().First().Close();
                        Application.Current.MainWindow.Close();
                    }
                    catch (Exception)
                    {
                        RegistrationFailed();
                    }
                }
            }
        }

        private bool OtheInputsCheck()
        {
            if (String.IsNullOrWhiteSpace(Password) && String.IsNullOrEmpty(Password))
            {
                ShowMessage();
                Message = "Введите пароль";
                return false;
            }
            if (String.IsNullOrWhiteSpace(Email) && String.IsNullOrEmpty(Email))
            {
                ShowMessage();
                Message = "Введите Email";
                return false;

            }
            return true;
        }

        private bool SecureCodeCheck()
        {
            if (_generatedCode == SecureCode)
                return true;
            ShowMessage();
            Message = "Проверочный код не совпадает";
            return false;
        }


        private bool LoginParsing()
        {
            if (Login.Length > 9 && !String.IsNullOrEmpty(Login) && !String.IsNullOrWhiteSpace(Login))
            {
                String tmp = Login.Substring(0, 4);

                if (tmp == "+380" && Login.Length == 13)
                    return true;

                tmp = Login.Substring(1, 2);

                if (Login.Length == 10 && _uaOperators.Contains(tmp))
                {
                    Login = "+38" + Login;
                    return true;
                }
                ShowMessage();
                Message = "Не верный формат телефонного номера";
            }
            else
            {
                ShowMessage();
                Message = "Номер cлишком короткий";
            }
            return false;

        }

        private void RegistrationFailed()
        {
            ShowMessage();
            Message = "Регистрация не удалась. Проверьте правильность введенных данных.";
            ClearTheInputFields();
        }

        private void ClearTheInputFields()
        {
            Login = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
        }

        private void HideAllHiddenThings()
        {
            VisabilityOfMessage = Visibility.Hidden;
            VisabilityOfCodeText = Visibility.Hidden;
            VisabilityOfCodeTextBox = Visibility.Hidden;
        }

        private Task AddUserAsync()
        {
            return Task.Run(() =>
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    _logic.AddUser(Login, Password, Email);
                }
            });
        }


        private void RunUserWindowAsync()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => { new UserWindow.MainWindow(Login).Show(); }));
        }

        private void RunUserWindow()
        {
            UserWindow.MainWindow wUser = new UserWindow.MainWindow(Login);
            wUser.Show();
        }

        #region Prop

        public String Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public Visibility VisabilityOfCodeText
        {
            get { return _visabilityOfCodeText; }
            set
            {
                _visabilityOfCodeText = value;
                OnPropertyChanged(nameof(VisabilityOfCodeText));

            }
        }

        public Visibility VisabilityOfCodeTextBox
        {
            get { return _visabilityOfCodeTextBox; }
            set
            {
                _visabilityOfCodeTextBox = value;
                OnPropertyChanged(nameof(VisabilityOfCodeTextBox));

            }
        }

        public Visibility VisabilityOfMessage
        {
            get { return _visabilityOfMessage; ; }
            set
            {
                _visabilityOfMessage = value;
                OnPropertyChanged(nameof(VisabilityOfMessage));
            }
        }
        public ICommand SendSms
        {
            get
            {
                if (_sendSms == null)
                    _sendSms = new RelayCommand(SendSmsCommand);
                return _sendSms;
            }
            set
            {
                _sendSms = value;
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
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public String SecureCode
        {
            get { return _secureCode; }
            set
            {
                _secureCode = value;
                OnPropertyChanged(nameof(SecureCode));
            }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        #endregion Prop

        #region ForPasswordBox
        private String ReceivePassword(object parameter)
        {
            var passwordContainer = parameter as IPasswordSupplier;

            if (passwordContainer != null)
            {
                var secureString = passwordContainer.GetPassword();
                return ConvertToUnsecureString(secureString);
                //GET PASSWORD!!!
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
    }
}
