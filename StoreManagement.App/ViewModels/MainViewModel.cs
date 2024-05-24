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
        private ProductCategory? _selectedCategory;

        [ObservableProperty]
        private Product? _selectedProduct;

        [ObservableProperty]
        private Storage? _selectedStorage;

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
            SelectedCategory = new() { Name=""};
            SelectedProduct = new() { Label="", ExpiryDate = DateTime.Now};
            SelectedStorage = new();

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

        [RelayCommand]
        public async Task DeleteCategory(ProductCategory item)
        {
            try
            {
                IsBusy = true;

                var isDelete = await _productCategoryService.DeleteProductCategory(item.Id);
                if (isDelete)
                {
                    MessageBox.Show($"Deleted category {item.Id} \n\n Name : {item.Name}");
                }

                await OnInitializeAsync();

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }

        }

        [RelayCommand]
        public async Task DeleteProduct(Product item)
        {
            try
            {
                IsBusy = true;

                var isDeleted = await _productService.DeleteProduct(item.Id);
                if (isDeleted)
                {
                    MessageBox.Show($"Delete product {item.Id} \n\n Name : {item.Label} \n Category : {item.Category?.Name} \n Expiry date : {item.ExpiryDate}");
                }

                await OnInitializeAsync();

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }

        }

        [RelayCommand]
        public async Task DeleteStorage(Storage item)
        {
            try
            {
                IsBusy = true;

                var isDeleted = await _storageService.DeleteAsync(item.Id);
                if (isDeleted)
                {
                    MessageBox.Show($"Delete storage {item.Id} \n\n Quantity : {item.Quantity} \n Product : {item.Product?.Label} \n Create at {item.Created}");
                }

                await OnInitializeAsync();

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }

        }

        [RelayCommand]
        public async Task DetailCategory(ProductCategory item)
        {
            try
            {
                IsBusy = true;

                MessageBox.Show($"Detail category {item.Id} \n\n Name : {item.Name}");

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }

        }

        [RelayCommand]
        public async Task DetailProduct(Product item)
        {
            try
            {
                IsBusy = true;

                MessageBox.Show($"Detail product {item.Id} \n\n Name : {item.Label} \n Category : {item.Category.Name} \n Expiry date : {item.ExpiryDate}");

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }

        }

        [RelayCommand]
        public async Task DelatilStorage(Storage item)
        {
            try
            {
                IsBusy = true;

                MessageBox.Show($"Detail storage {item.Id} \n\n Quantity : {item.Quantity} \n Product : {item.Product?.Label} \n Create at {item.Created}");

                IsBusy = !IsBusy;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            }

        }
    }
}
