using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CollectionConsumer.Services;
using CollectionConsumer.ViewModels;
using CollectionConsumer.Views;

namespace CollectionConsumer
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var dataService = new DataService();
                var themeService = new ThemeService(this);
                var mainWindow = new MainWindow();
                var filePickerService = new FilePickerService(mainWindow.StorageProvider);
                var mainVm = new MainWindowViewModel(dataService, themeService, filePickerService);
                mainWindow.DataContext = mainVm;
                desktop.MainWindow = mainWindow;
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}