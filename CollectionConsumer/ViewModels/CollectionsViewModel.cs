using System;
using System.Collections.ObjectModel;
using System.Linq;
using CollectionConsumer.Models;
using CollectionConsumer.Services;
using CollectionConsumer.Views.Dialogs;

namespace CollectionConsumer.ViewModels
{
    public class CollectionsViewModel : ViewModelBase
    {
        private readonly AppData _appData;
        private readonly IDataService _dataService;
        private readonly IFilePickerService _filePickerService;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ObservableCollection<Collection> Collections { get; }

        public RelayCommand AddCollectionCommand { get; }
        public RelayCommand<Collection> OpenCollectionCommand { get; }
        public RelayCommand<Collection> DeleteCollectionCommand { get; }

        public event Action<Collection>? RequestNavigation;

        public CollectionsViewModel(AppData appData, IDataService dataService, IFilePickerService filePickerService, MainWindowViewModel mainVm)
        {
            _appData = appData;
            _dataService = dataService;
            _filePickerService = filePickerService;
            _mainWindowViewModel = mainVm;
            Collections = new ObservableCollection<Collection>(_appData.Collections);

            AddCollectionCommand = new RelayCommand(AddCollection);
            OpenCollectionCommand = new RelayCommand<Collection>(OpenCollection);
            DeleteCollectionCommand = new RelayCommand<Collection>(DeleteCollection);
        }

        private async void AddCollection()
        {
            var vm = new AddCollectionDialogViewModel(_filePickerService);
            var dialog = new AddCollectionDialog(vm);
            var result = await dialog.ShowDialog<Collection?>(_mainWindowViewModel.OwnerWindow);
            if (result != null)
            {
                Collections.Add(result);
                SaveData();
            }
        }

        private void DeleteCollection(Collection collection)
        {
            if (collection == null) return;
            Collections.Remove(collection);
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