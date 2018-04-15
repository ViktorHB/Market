using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWVMLib;
using System.Windows.Input;
using System.Threading;
using System.Collections.ObjectModel;
using DataBase;

namespace AdminWindow
{
    public class AdminWindowViewModel : ViewModelBase
    {


        public AdminWindowViewModel()
        {
            string status_waiting = "Waiting...";

            _dataBase = new MarketModel();

            //DataBaseSaveOrdersCommand = new RelayCommand(DataBaseSaveOrdersCommandExecute, null);
            AddProductAddCommand = new RelayCommand(AddProductAddCommandExecute, null);
            AddProductCancelCommand = new RelayCommand(AddProductCancelCommandExecute, null);
            AddCatalogAddCommand = new RelayCommand(AddCatalogAddCommandExecute, null);
            AddCatalogCancelCommand = new RelayCommand(AddCatalogCancelCommandExecute, null);

            AddUserAddCommand = new RelayCommand(AddUserAddCommandExecute, null);
            AddUserCancelCommand = new RelayCommand(AddUserCancelCommandExecute, null);

            Orders = new ObservableCollection<DataBase.Orders>(_dataBase.Orders);//_dataBase.Orders.Local;     
            Products = new ObservableCollection<Product>(_dataBase.Product);
            Users = new ObservableCollection<DataBase.Users>(_dataBase.Users);

            SelectedProductToDelete = Products.First();
            SelectedCatalogToDelete = Catalogs.First();
            SelectedUserToDelete = Users.First();

            DeleteProductCommand = new RelayCommand(DeleteProductCommmandExecute, null);
            CancelDeletingProductCommand = new RelayCommand(CancelDeletingProductCommandExecute, null);

            DeleteCatalogCommand = new RelayCommand(DeleteCatalogCommandExecute, null);
            CancelDeletingCatalogCommand = new RelayCommand(CancelDeletingCatalogCommandExecute, null);

            DeleteUserCommand = new RelayCommand(DeleteUserCommandExecute, null);
            CancelDeletingUserCommand = new RelayCommand(CancelDeletingUserCommandExecute, null);

            DeletingCatalogStatusText = status_waiting;
            DeletingProductStatusText = status_waiting;
            DeletingUserStatusText = status_waiting;

            AddingCatalogStatusText = status_waiting;
            AddingProductStatusText = status_waiting;
            AddingUserStatusText = status_waiting;
        }

        private void CancelDeletingUserCommandExecute(object obj) => SelectedUserToDelete = Users.First();

        private void DeleteUserCommandExecute(object obj)
        {
            _dataBase.Users.Remove(SelectedUserToDelete);

            Users.Remove(SelectedUserToDelete);
            _dataBase.SaveChanges();

            DeletingUserStatusText = "Deleted!";
        }

        private void CancelDeletingCatalogCommandExecute(object obj) => SelectedCatalogToDelete = Catalogs.First();

        private void DeleteCatalogCommandExecute(object obj)
        {
            _dataBase.Catalog.Remove(SelectedCatalogToDelete);
            Catalogs.Remove(SelectedCatalogToDelete);

            _dataBase.SaveChanges();

            DeletingCatalogStatusText = "Deleted!";
        }

        private void CancelDeletingProductCommandExecute(object obj) => SelectedProductToDelete = _dataBase.Product.First();

        private void DeleteProductCommmandExecute(object obj)
        {

            _dataBase.Product.Remove(SelectedProductToDelete);
            Products.Remove(SelectedProductToDelete);
            _dataBase.SaveChanges();

            DeletingProductStatusText = "Deleted !";
        }

        private string _deletingprodstatus;

        public string DeletingProductStatusText
        {
            get { return _deletingprodstatus; }
            set { _deletingprodstatus = value;
                OnPropertyChanged(nameof(DeletingProductStatusText));
            }
        }

        private string _addingcatalogstatustext ;

        public string AddingCatalogStatusText
        {
            get { return _addingcatalogstatustext ; }
            set { _addingcatalogstatustext  = value;
                OnPropertyChanged(nameof(AddingCatalogStatusText));}
        }

        private string _addingprodstatustext;

        public string AddingProductStatusText
        {
            get { return _addingprodstatustext; }
            set { _addingprodstatustext = value;
                OnPropertyChanged(nameof(AddingProductStatusText));}
        }



        private string _deletingCatalogStatusText;

        public string DeletingCatalogStatusText
        {
            get { return _deletingCatalogStatusText; }
            set { _deletingCatalogStatusText = value;
                OnPropertyChanged(nameof(DeletingCatalogStatusText));
            }
        }

        private string _addinguserstatustext;

        public string AddingUserStatusText
        {
            get { return _addinguserstatustext; }
            set { _addinguserstatustext = value;
                OnPropertyChanged(nameof(AddingUserStatusText));
            }
        }

        private string _deluserstatus;

        public string DeletingUserStatusText
        {
            get { return _deluserstatus; }
            set { _deluserstatus = value;
                OnPropertyChanged(nameof(DeletingUserStatusText)); }
        }

        private Users _selusertodel;

        public Users SelectedUserToDelete
        {
            get { return _selusertodel; }
            set { _selusertodel = value;
                OnPropertyChanged(nameof(SelectedUserToDelete));
            }
        }


        private void AddUserCancelCommandExecute(object obj)
        {
            AddUserEmail = string.Empty;
            AddUserPassword = string.Empty;
            AddUserPhone = string.Empty;
        }

        private void AddUserAddCommandExecute(object obj)
        {
            Users newUser = new Users
            {
                id_accessstatud = 2,
                email = AddUserEmail,
                phone = AddUserPhone,
                password = AddUserPassword
            };
            _dataBase.Users.Add(newUser);

            Users.Add(newUser);

            _dataBase.SaveChanges();

            AddingUserStatusText = "Added!";
            //_dataBase.SaveChangesAsync();

            //AddUserEmail = null;
            //AddUserPassword = null;
            //AddUserPhone = null;

            AddUserCancelCommandExecute(null);
        }

        private void AddCatalogCancelCommandExecute(object obj)
        {
            Catalog newCatalog = new Catalog()
            {
                title = AddCatalogTitle
            };

            _dataBase.Catalog.Add(newCatalog);
            //DataBaseSaveCommandExecute(obj);
        }

        private void AddCatalogAddCommandExecute(object obj)
        {
            Catalog newCatalog = new Catalog
            {
                title = AddCatalogTitle
            };

            _dataBase.Catalog.Add(newCatalog);
            Catalogs.Add(newCatalog);
            _dataBase.SaveChanges();

            AddingCatalogStatusText = "Added!";

            AddCatalogTitle = string.Empty;
        }

        private void AddProductAddCommandExecute(object obj)
        {
            Product newProduct = new Product()
            {
                id_catalog = AddProductCatalog.id,
                title = AddProductTitle,
                url = AddProductUrl,
                price = double.Parse(AddProductPrice)
            };


            _dataBase.Product.Add(newProduct);
            _dataBase.SaveChanges();

            Products.Add(newProduct);

            AddProductCancelCommandExecute(null);

            AddingProductStatusText = "Added!";

            //DataBaseSaveCommandExecute(obj);
        }

        private void AddProductCancelCommandExecute(object obj)
        {
            AddProductCatalog = null;
            AddProductPrice = string.Empty;
            AddProductTitle = string.Empty;
            AddProductUrl = string.Empty;
        }

        // ERROR
        private void DataBaseSaveOrdersCommandExecute() {

            var xxx = _dataBase.Orders.First(o => o.id == SelectedOrder.id);
            xxx = SelectedOrder;
            _dataBase.SaveChanges();
        }


        //private MarketModel _dataBase;
        public MarketModel _dataBase { get; set; }


        private ObservableCollection<Orders> _orders;

        public ObservableCollection<Orders> Orders
        {
            get { return _orders; }
            set { _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private ObservableCollection<Users> _users;
        public ObservableCollection<Users> Users
        {
            get { return _users; }
            set
            {
                _users = value;
            }
        }

        private Users _su;

        public Users SelectedUser
        {
            get { return _su; }
            set { if (_su != value)
                {
                    _su = value;
                    OnPropertyChanged(nameof(SelectedUser));
                    DatabaseSaveUser();
                }
                OnPropertyChanged(nameof(SelectedUser));
            }
        }


        private void DatabaseSaveUser()
        {
            var xxx = _dataBase.Users.First(x => x.id == SelectedUser.id);
            xxx = SelectedUser;
            _dataBase.SaveChanges();
        }

        private ObservableCollection<Product> _prods;
        public ObservableCollection<Product> Products
        {
            get { return _prods; }
            set { _prods = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private Product _sp;

        public Product SelectedProduct
        {
            get { return _sp; }
            set
            {
                if (_sp != value)
                {
                    _sp = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                    DatabaseSaveProduct();
                }
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private void DatabaseSaveProduct()
        {
            var dbchange = _dataBase.Product.First(x => x.id == SelectedProduct.id);
            dbchange = SelectedProduct;
            _dataBase.SaveChanges();
        }


        private ObservableCollection<Catalog> _catalogs;
        public ObservableCollection<Catalog> Catalogs
        {
            get
            {
                if(_catalogs == null)
                {
                    _catalogs = new ObservableCollection<Catalog>(GetCatalogs());
                }
                return _catalogs;
            }
            set
            {
                _catalogs = value;
            }
        }

        public ICommand DataBaseSaveOrdersCommand { get; set; }
        public ICommand AddProductAddCommand { get; set; }
        public ICommand AddProductCancelCommand { get; set; }

        private ICommand _addCatalog;
        public ICommand AddCatalogCommand
        {
            get
            {
                if (_addCatalog == null)
                    _addCatalog = new RelayCommand(AddCatalogToDataBase);
                return _addCatalog;
            }
            set
            {
                _addCatalog = value;
            }
        }

        private void AddCatalogToDataBase(object obj)
        {
            _dataBase.Catalog.Add(new Catalog { title = AddCatalogTitle });
            _dataBase.SaveChangesAsync();
            AddCatalogTitle = null;
        }

        public RelayCommand AddCatalogAddCommand { get; set; }

        public RelayCommand AddCatalogCancelCommand { get; set; }
        public RelayCommand AddUserAddCommand { get; private set; }
        public RelayCommand AddUserCancelCommand { get; private set; }

        private string _apt;

        public string AddProductTitle
        {
            get { return _apt; }
            set
            {
                _apt = value;
                OnPropertyChanged(nameof(AddProductTitle));
            }
        }

        private Catalog _apc;

        public Catalog AddProductCatalog
        {
            get { return _apc; }
            set { _apc = value; }
        }

        private string _app;

        public string AddProductPrice
        {
            get { return _app; }
            set
            {
                _app = value;
                OnPropertyChanged(nameof(AddProductPrice));
            }
        }

        private string _apu;

        public string AddProductUrl
        {
            get { return _apu; }
            set
            {
                _apu = value;
                OnPropertyChanged(nameof(AddProductUrl));
            }
        }

        private string _act;

        public string AddCatalogTitle
        {
            get { return _act; }
            set
            {
                _act = value;
                OnPropertyChanged(nameof(AddCatalogTitle));
            }
        }

        private string _auph;

        public string AddUserPhone
        {
            get { return _auph; }
            set { _auph = value; OnPropertyChanged(nameof(AddUserPhone)); }
        }

        private string _aups;

        public string AddUserPassword
        {
            get { return _aups; }
            set { _aups = value; OnPropertyChanged(nameof(AddUserPassword)); }
        }

        private string _aue;

        public string AddUserEmail
        {
            get { return _aue; }
            set { _aue = value; OnPropertyChanged(nameof(AddUserEmail)); }
        }


        private int _selectetTabIndex;
        public int SelectetTabIndex
        {
            get { return _selectetTabIndex; }
            set
            {
                _selectetTabIndex = value;
                OnPropertyChanged(nameof(SelectetTabIndex));
                if (SelectetTabIndex == 1)
                {
                    //Update cattalogs
                    PrintCatalogs();
                }

            }
        }

        private Orders _so;

        public Orders SelectedOrder
        {
            get { return _so; }
            set {
                if (_so != value)
                {
                    _so = value;
                    OnPropertyChanged(nameof(SelectedOrder));
                    DataBaseSaveOrdersCommandExecute();
                }
                OnPropertyChanged(nameof(SelectedOrder));

            }
        }

        private Product _sptodel;

        public Product SelectedProductToDelete
        {
            get { return _sptodel; }
            set { _sptodel = value;
                OnPropertyChanged(nameof(SelectedProductToDelete));
            }
        }

        private Catalog _sctodel;

        public Catalog SelectedCatalogToDelete
        {
            get { return _sctodel; }
            set
            {
                _sctodel = value;
                OnPropertyChanged(nameof(SelectedCatalogToDelete));
            }
        }

        public RelayCommand DeleteProductCommand { get; }
        public RelayCommand CancelDeletingProductCommand { get; }
        public RelayCommand DeleteCatalogCommand { get; }
        public RelayCommand CancelDeletingCatalogCommand { get; }
        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand CancelDeletingUserCommand { get; }

        private void PrintCatalogs()
        {
            Catalogs.Clear();
            foreach (var item in GetCatalogs())
                Catalogs.Add(item);
        }

        private List<Catalog> GetCatalogs()
        {
            return  _dataBase.Catalog.OrderByDescending(x=>x.title).ToList();
        }
    }
}
