using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoreManagement.App.DTOs;
using StoreManagement.App.Models;
using StoreManagement.App.Services.Storage;
using System;
using System.Collections.Frozen;
using System.Windows.Forms;

namespace StoreManagement.App.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {

        private readonly IStorageService _storageService;

        [ObservableProperty]
        private FrozenSet<Product>? _products;

        [ObservableProperty]
        private FrozenSet<Storage>? _storages;

        [ObservableProperty]
        private NewStorage? _storage;

        public MainViewModel()
        {
            Title = "Smart Storage";

            _storageService = new StorageService();

            OnInitializeAsync().SafeFireAndForget(exception =>
            {
                MessageBox.Show(exception.Message + "\n" + exception?.InnerException?.Message + "\n" + exception?.InnerException?.StackTrace, "Smart-Storage", MessageBoxButtons.OK);
            });
        }


        protected override async Task OnInitializeAsync()
        {
            IsBusy = true;
            Storage = new();

            var storages = await _storageService.GetActiveAsync();

            Storages = storages;

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

    }
}
