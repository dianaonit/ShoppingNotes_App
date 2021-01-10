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
    public partial class ProductShopPage : ContentPage
    {
        public ProductShopPage()
        {
            InitializeComponent();
        }

        public ProductShopPage(ShopList shop)
        {
            InitializeComponent();

            BindingContext = shop;
        }
        async void Edit_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ListPage(BindingContext as ShopList));
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (ShopList)BindingContext;

            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }

    }
}