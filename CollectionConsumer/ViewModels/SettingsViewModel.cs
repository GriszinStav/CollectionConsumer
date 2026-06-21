using System.Collections.Generic;
using CollectionConsumer.Models;
using CollectionConsumer.Services;

namespace CollectionConsumer.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IThemeService _themeService;
        private readonly IDataService _dataService;
        private readonly AppData _appData;

        public List<string> AvailableThemes { get; } = new()
        {
            "Windows95", "Windows11", "Dark", "Light", "Draft", "Minecraft"
        };

        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    _themeService.SetTheme(value);
                    _appData.CurrentTheme = value;
                    SaveData();
                }
            }
        }

        public SettingsViewModel(IThemeService themeService, IDataService dataService, AppData appData)
        {
            _themeService = themeService;
            _dataService = dataService;
            _appData = appData;
            _selectedTheme = _themeService.CurrentTheme;
        }

        private async void SaveData()
        {
            await _dataService.SaveDataAsync(_appData);
        }
    }
}