using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWVMLib;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DataBase;

namespace UserWindow
{
    public class UserWindowViewModel : ViewModelBase
    {

        public UserWindowViewModel()
        {
            _dataBase = new MarketModel();
            Products = _dataBase.Product.ToList();
            BasketItems = new ObservableCollection<ProductToOrder>();
            OrderGridItemsCollection = new ObservableCollection<OrderGridItems>();

            RemoveFromBasketCommand = new RelayCommand(RemoveFromBasketCommandExecute, null);
            ChangeDeliveryStateCommand = new RelayCommand(ChangeDeliveryStateCommandExecute, null);
            MakeOrderCommand = new RelayCommand(MakeOrderCommandExecute, null);
            CancelOrderCommand = new RelayCommand(CancelOrderCommandExecute, null);
            FillOrdersCommand = new RelayCommand(FillOrdersCommandExecute, null);

            ClearQuantityPriceCommand = new RelayCommand(ClearQuantityPriceCommandExecute, null);

        }

        private void ClearQuantityPriceCommandExecute(object obj)
        {
            TotalPrice = 0;
            QuantityOfProducts = "";
        }

        private void ChangeDeliveryStateCommandExecute(object obj)
        {

            FillOrdersCommandExecute(new object());
            var ogi = obj as OrderGridItems;
            ogi.ButtonState = false;
            var order = _dataBase.Orders.Single(o => o.id == ogi._order.id);
            order.id_state = 2;
            _dataBase.SaveChanges();

        }

        private void FillOrdersCommandExecute(object obj)
        {
            //if (TabIndexSelected == 1)
            //{
            var userOrders = _dataBase.Orders.Where(o => o.Users.phone == CurrentPhoneNumber);
            OrderGridItemsCollection.Clear();

            foreach (var ogi in userOrders)
            {
                var newogi = new OrderGridItems() { _order = ogi };

                if (ogi.id_state == 2)
                    newogi.ButtonState = false;

                OrderGridItemsCollection.Add(newogi);
            }
            //}
        }

        private void CancelOrderCommandExecute(object obj)
        {
            SelectedCatalog = null;
            //SelectedProduct = null;
            TotalPrice = 0;
            QuantityOfProducts = string.Empty;
        }

        private void MakeOrderCommandExecute(object obj)
        {
            NewGlobalServiceReference.NewGlobalServiceClient gsc = new NewGlobalServiceReference.NewGlobalServiceClient();


            //Orders orderToMake = new Orders();
            //orderToMake.count = int.Parse(QuantityOfProducts);
            //orderToMake.date = DateTime.Now;
            //orderToMake.Product = SelectedProduct;
            //orderToMake.Users = _dataBase.Users.Single(u => u.phone == CurrentPhoneNumber);
            //orderToMake.id_state = 1;

            List<OrderGridItems> orders = new List<OrderGridItems>();

            foreach (var bi in BasketItems)
            {
                orders.Add(new OrderGridItems
                {
                     _order = new DataBase.Orders
                     {
                         count = bi.Count,
                         date = DateTime.Now,
                         Product = bi.Product,
                         Users = _dataBase.Users.Single(u => u.phone == CurrentPhoneNumber),
                         id_state = 1,
                         full_cost = bi.FullPrice
                     }
                });
            }


            foreach (var ogi in orders)
            {
                gsc.MakeOrder(ogi._order.Product.id, CurrentPhoneNumber, DateTime.Now, ogi.TotalPrice.ToString(), ogi.Quantity.ToString());
                SendMilToTheCustomer(ogi._order, ogi.Url);
            }


            FillOrdersCommandExecute(null);


        }

        private void RemoveFromBasketCommandExecute(object obj)
        {
            BasketItems.Remove(SelectedBasketItem);
        }

        private MarketModel _dataBase;

        public List<Orders> Orders => _dataBase.Orders.ToList();
        public List<Catalog> Catalogs => _dataBase.Catalog.ToList();

        private List<Product> _products;
        public List<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }

        }

        private int _tabIndexSelected;

        public int TabIndexSelected
        {
            get { return _tabIndexSelected; }
            set
            {
                _tabIndexSelected = value;
                OnPropertyChanged(nameof(TabIndexSelected));
            }
        }


        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                if (value != null)
                    BasketItems.Add(new ProductToOrder { Product = SelectedProduct });
                OnPropertyChanged(nameof(SelectedProduct));
                QuantityOfProducts = null;
                TotalPrice = null;
            }
        }

        private Catalog _selectedCatalog;

        public Catalog SelectedCatalog
        {
            get { return _selectedCatalog; }
            set
            {
                _selectedCatalog = value;
                if (value != null)
                    Products = _dataBase.Product.Where(p => p.Catalog.title.Equals(SelectedCatalog.title)).ToList();
                OnPropertyChanged(nameof(SelectedCatalog));
            }
        }


        private string _quantityOfProducts;

        public string QuantityOfProducts
        {
            get { return _quantityOfProducts; }
            set
            {
                _quantityOfProducts = value;
                if (value != string.Empty)
                {
                    var bi = BasketItems.Last(p => p.Product == SelectedProduct);
                    bi.Count = int.Parse(QuantityOfProducts);
                    TotalPrice = SelectedProduct.price * double.Parse(QuantityOfProducts);
                    bi.FullPrice = TotalPrice;

                    BasketItems.RemoveAt(BasketItems.Count - 1);
                    BasketItems.Add(bi);

                }
                OnPropertyChanged(nameof(QuantityOfProducts));
            }
        }

        private double? _totalPrice;

        public double? TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private ObservableCollection<ProductToOrder> _BasketItems;

        public ObservableCollection<ProductToOrder> BasketItems
        {
            get { return _BasketItems; }
            set
            {
                _BasketItems = value;
                OnPropertyChanged(nameof(BasketItems));
            }
        }

        private ProductToOrder _selectedBI;

        public ProductToOrder SelectedBasketItem
        {
            get { return _selectedBI; }
            set
            {
                _selectedBI = value;
                OnPropertyChanged(nameof(SelectedBasketItem));
            }
        }

        private OrderGridItems _sogi;

        public OrderGridItems SelectedOrderGridItem
        {
            get { return _sogi; }
            set
            {
                _sogi = value;
                OnPropertyChanged(nameof(SelectedOrderGridItem));
            }
        }


        public ObservableCollection<OrderGridItems> OrderGridItemsCollection { get; set; }

        private string _cPhoneNumb;

        public string CurrentPhoneNumber
        {
            get { return _cPhoneNumb; }
            set
            {
                _cPhoneNumb = value;
                OnPropertyChanged(nameof(CurrentPhoneNumber));
            }
        }

        public ICommand RemoveFromBasketCommand { get; set; }
        public ICommand MakeOrderCommand { get; set; }
        public ICommand CancelOrderCommand { get; set; }
        public ICommand FillOrdersCommand { get; set; }
        public ICommand ChangeDeliveryStateCommand { get; set; }
        public RelayCommand ClearQuantityPriceCommand { get; private set; }

        private void SendMilToTheCustomer(Orders o, String image)
        {

            String eMail = _dataBase.Users.Where(u => u.phone == CurrentPhoneNumber).FirstOrDefault().email;
            SendEmail.Send(eMail, CurrentPhoneNumber, string.Format("Продукт: {0}, Количество: {1}, Дата заказа {2}", o.Product, o.count, o.date), image);
        }
    }

    public class ProductToOrder
    {
        public Product Product { get; set; }
        public int Count { get; set; }
        public double? FullPrice { get; set; }

        public string Title => Product.title;
        public string ImgUrl => Product.url;
    }

    public class OrderGridItems : ViewModelBase
    {
        public Orders _order { get; set; }
        public string UserPhone => _order.Users.phone;
        public string Title => _order.Product.title;
        public string Date => _order.date.ToString();
        public string State => _order.OrderState.title;
        public string TotalPrice => _order.full_cost.ToString();
        public string Quantity => _order.count.ToString();
        public string Url => _order.Product.url;

        private bool _bs;
        public bool ButtonState
        {
            get { return _bs; }
            set
            {
                _bs = value;
                OnPropertyChanged(nameof(ButtonState));
            }
        }

        public OrderGridItems()
        {
            ButtonState = true;
        }
    }




}
