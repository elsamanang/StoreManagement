using CommunityToolkit.Mvvm.ComponentModel;

namespace StoreManagement.App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _title;

        [ObservableProperty]
        private bool _isBusy;


        /// <summary>
        /// Used to initialize viewModel data
        /// </summary>
        /// <returns></returns>
        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
