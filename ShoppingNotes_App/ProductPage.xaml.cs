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
    public partial class ProductPage : ContentPage
    {
        ShopList sl;
        ListProduct _listProduct;
        public ProductPage(ShopList slist)
        {
            InitializeComponent();
            sl = slist;
            _listProduct = new ListProduct();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.SaveProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
          

            var lp = new ListProduct()
            {
                ShopListID = sl.ID,
                ProductID = product.ID
            };

            var listProducts = await App.Database.GetListProductAsyncTest(sl.ID);
            foreach (var listProduct in listProducts)
            {
                if (listProduct.ProductID == product.ID)
                    return;
            }

            _listProduct = lp;
            product.ListProducts = new List<ListProduct> { lp };

            await App.Database.SaveListProductAsync(_listProduct);
            ((Product)BindingContext).ID = 0;

            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.DeleteProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }


        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Product product;
            if (e.SelectedItem != null)
            {
                product = e.SelectedItem as Product;

                BindingContext = product;
                //var lp = new ListProduct()
                //{
                //    ShopListID = sl.ID,
                //    ProductID = p.ID
                //};

                //var listProducts = await App.Database.GetListProductAsyncTest(sl.ID);
                //foreach (var listProduct in listProducts){
                //    if (listProduct.ProductID == p.ID)
                //        return;
                //}

                //_listProduct = lp;
                //p.ListProducts = new List<ListProduct> { lp };
            }
        }


    }
}