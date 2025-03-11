using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RaceManagementApp.UI.ViewModels.Common
{
    public class SearchViewModel<T> : ObservableObject
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                PerformSearch();
            }
        }
        private ObservableCollection<T> _originalItems = new();
        public ObservableCollection<T> OriginalItems
        {
            get => _originalItems;
            set => SetProperty(ref _originalItems, value);
        }

        private ObservableCollection<T> _filteredItems = new();
        public ObservableCollection<T> FilteredItems
        {
            get => _filteredItems;
            set => SetProperty(ref _filteredItems, value);
        }

        private T _selectedItem;
        public T SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public IEnumerable<string> SearchableColumns { get; set; } = Enumerable.Empty<string>();
        public IRelayCommand PerformSearchCommand { get; }

        public SearchViewModel()
        {
            PerformSearchCommand = new RelayCommand(PerformSearch);
        }

        private void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText) || SearchableColumns == null || !SearchableColumns.Any())
            {
                FilteredItems = new ObservableCollection<T>(OriginalItems);
                return;
            }

            var searchLower = SearchText.ToLower();
            var results = OriginalItems.Where(item =>
            {
                foreach (var column in SearchableColumns)
                {
                    var propertyValue = item.GetType().GetProperty(column)?.GetValue(item, null)?.ToString();
                    if (!string.IsNullOrEmpty(propertyValue) && propertyValue.ToLower().Contains(searchLower))
                    {
                        return true;
                    }
                }
                return false;
            }).ToList();

            FilteredItems = new ObservableCollection<T>(results);
        }
    }
}
