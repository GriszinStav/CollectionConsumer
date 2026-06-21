using System;
using System.Collections.ObjectModel;
using System.Linq;
using CollectionConsumer.Models;
using CollectionConsumer.Services;

namespace CollectionConsumer.ViewModels
{
    public class CollectionsViewModel : ViewModelBase
    {
        private readonly AppData _appData;
        private readonly IDataService _dataService;

        public ObservableCollection<Collection> Collections { get; }

        public RelayCommand AddCollectionCommand { get; }
        public RelayCommand<Collection> OpenCollectionCommand { get; }

        public event Action<Collection>? RequestNavigation;

        public CollectionsViewModel(AppData appData, IDataService dataService)
        {
            _appData = appData;
            _dataService = dataService;
            Collections = new ObservableCollection<Collection>(_appData.Collections);

            AddCollectionCommand = new RelayCommand(AddCollection);
            OpenCollectionCommand = new RelayCommand<Collection>(OpenCollection);
        }

        private void AddCollection()
        {
            var newColl = new Collection { Name = "\u041D\u043E\u0432\u0430\u044F \u043A\u043E\u043B\u043B\u0435\u043A\u0446\u0438\u044F" };
            Collections.Add(newColl);
            SaveData();
        }

        private void OpenCollection(Collection collection)
        {
            RequestNavigation?.Invoke(collection);
        }

        private async void SaveData()
        {
            _appData.Collections = Collections.ToList();
            await _dataService.SaveDataAsync(_appData);
        }
    }
}