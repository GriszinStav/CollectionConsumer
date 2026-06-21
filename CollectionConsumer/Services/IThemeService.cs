namespace CollectionConsumer.Services
{
    public interface IThemeService
    {
        void SetTheme(string themeName);
        string CurrentTheme { get; }
    }
}