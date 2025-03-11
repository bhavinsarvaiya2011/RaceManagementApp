using CommunityToolkit.Mvvm.ComponentModel;

namespace RaceManagementApp.UI.Services
{
    public class LoaderService : ObservableObject
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }
    }
}
