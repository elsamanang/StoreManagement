using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoreManagement.App.DTOs;
using StoreManagement.App.Models;
using StoreManagement.App.Services.Product;
using StoreManagement.App.Services.ProductCategory;
using StoreManagement.App.Services.Storage;
using System;
using System.Collections.Frozen;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace StoreManagement.App.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {

        private readonly IStorageService _storageService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        [ObservableProperty]
        private ObservableCollection<Product>? _products;

        [ObservableProperty]
        private ObservableCollection<ProductCategory>? productCategories;

        [ObservableProperty]
        private FrozenSet<Storage>? _storages;

        [ObservableProperty]
        private NewStorage? _storage;

        [ObservableProperty]
        private NewProductCategory? _category;

        [ObservableProperty]
        private NewProduct? _product;

        public MainViewModel()
        {
            Title = "Smart Storage";

            _storageService = new StorageService();
            _productCategoryService = new ProductCategoryService();
            _productService = new ProductService();

            OnInitializeAsync().SafeFireAndForget(exception =>
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            });
        }


        protected override async Task OnInitializeAsync()
        {
            IsBusy = true;
            Storage = new();
            Category = new();
            Product = new();

            var storages = await _storageService.GetActiveAsync();
            var categories = await _productCategoryService.GetProductCategories();
            var products = await _productService.GetAllProducts();

            Storages = storages;
            ProductCategories = new ObservableCollection<ProductCategory>(categories.OrderBy(x => x.Id));
            Products = new ObservableCollection<Product>(products.OrderBy(x => x.Id));

            IsBusy = !IsBusy;
        }


        [RelayCommand]
        public async Task SaveStorage()
        {
            try
            {
                IsBusy = true;

                await _storageService.CreateAsync(Storage).ConfigureAwait(false);

                Storage = new();

                var storages = await _storageService.GetActiveAsync();

                Storages = storages;

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }
        }

        [RelayCommand]
        public async Task SaveProduct()
        {
            try
            {
                IsBusy = true;

                await _productService.CreateProduct(Product).ConfigureAwait(false);

                Product = new();

                await OnInitializeAsync();

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }
        }
        
        [RelayCommand]
        public async Task SaveCategory()
        {
            try
            {
                IsBusy = true;

                await _productCategoryService.CreateProductCategory(Category).ConfigureAwait(false);

                Category = new();

                await OnInitializeAsync();

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }
        }

    }
}
