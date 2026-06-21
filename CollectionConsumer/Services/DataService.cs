using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CollectionConsumer.Models;

namespace CollectionConsumer.Services
{
    public class DataService : IDataService
    {
        private readonly string _dataPath;

        public DataService()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appData, "CollectionConsumer");
            Directory.CreateDirectory(dir);
            _dataPath = Path.Combine(dir, "data.json");
        }

        public async Task<AppData> LoadDataAsync()
        {
            if (!File.Exists(_dataPath))
                return new AppData();
            var json = await File.ReadAllTextAsync(_dataPath);
            return JsonSerializer.Deserialize<AppData>(json) ?? new AppData();
        }

        public async Task SaveDataAsync(AppData data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_dataPath, json);
        }
    }
}