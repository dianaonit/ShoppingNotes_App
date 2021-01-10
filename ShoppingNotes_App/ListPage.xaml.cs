using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingNotes_App.Models;

namespace ShoppingNotes_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        ListProduct _listProduct;

       

        public ListPage()
        {
            _listProduct = new ListProduct();
            BindingContext = new ShopList();
            InitializeComponent();
        }

        public ListPage(ShopList shopList)
        {
            _listProduct = new ListProduct();
            BindingContext = shopList;
            InitializeComponent();
        }


      

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            slist.Date = DateTime.UtcNow;
            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            await App.Database.DeleteListProductAsync(_listProduct);

            await Navigation.PopAsync();
        }



        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((ShopList)BindingContext)
            {
                BindingContext = new Product()
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (ShopList)BindingContext;

            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }


        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product p;
            if (e.SelectedItem != null)
            {
                var shopList = (ShopList)BindingContext;

                p = e.SelectedItem as Product;

                var listProducts = await App.Database.GetListProductAsyncTest(shopList.ID);
                foreach (var listProduct in listProducts)
                {
                    if (listProduct.ShopListID == shopList.ID && listProduct.ProductID == p.ID)
                        _listProduct = listProduct;
                }

            }
        }




    }
}