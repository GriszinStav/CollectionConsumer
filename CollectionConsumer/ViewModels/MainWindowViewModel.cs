using System;
using CollectionConsumer.Models;
using CollectionConsumer.Services;

namespace CollectionConsumer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IThemeService _themeService;
        private readonly IFilePickerService _filePickerService;
        private AppData _appData;
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public RelayCommand ShowCollectionsCommand { get; }
        public RelayCommand ShowSettingsCommand { get; }

        public MainWindowViewModel(IDataService dataService, IThemeService themeService, IFilePickerService filePickerService)
        {
            _dataService = dataService;
            _themeService = themeService;
            _filePickerService = filePickerService;
            _appData = new AppData();

            ShowCollectionsCommand = new RelayCommand(() => NavigateToCollections());
            ShowSettingsCommand = new RelayCommand(() => NavigateToSettings());

            LoadData();
        }

        private async void LoadData()
        {
            _appData = await _dataService.LoadDataAsync();
            _themeService.SetTheme(_appData.CurrentTheme);
            NavigateToCollections();
        }

        private void NavigateToCollections()
        {
            var vm = new CollectionsViewModel(_appData, _dataService);
            vm.RequestNavigation += NavigateToDetail;
            CurrentView = vm;
        }

        private void NavigateToSettings()
        {
            CurrentView = new SettingsViewModel(_themeService, _dataService, _appData);
        }

        private void NavigateToDetail(Collection collection)
        {
            CurrentView = new CollectionDetailViewModel(_appData, _dataService, _filePickerService, collection);
        }
    }
}