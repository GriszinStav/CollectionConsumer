using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace CollectionConsumer.Services
{
    public class ThemeService : IThemeService
    {
        private readonly Application _app;
        private ResourceDictionary? _currentThemeDict;
        public string CurrentTheme { get; private set; } = "Light";

        public ThemeService(Application app)
        {
            _app = app;
        }

        public void SetTheme(string themeName)
        {
            var uri = new Uri($"avares://CollectionConsumer/Resources/Themes/Theme{themeName}.axaml");
            var newDict = AvaloniaXamlLoader.Load(uri) as ResourceDictionary;
            if (newDict == null)
                throw new InvalidOperationException($"Не удалось загрузить тему '{themeName}'");

            if (_currentThemeDict != null)
                _app.Resources.MergedDictionaries.Remove(_currentThemeDict);

            _app.Resources.MergedDictionaries.Add(newDict);
            _currentThemeDict = newDict;
            CurrentTheme = themeName;
        }
    }
}