using System.Threading.Tasks;
using CollectionConsumer.Models;

namespace CollectionConsumer.Services
{
    public interface IDataService
    {
        Task<AppData> LoadDataAsync();
        Task SaveDataAsync(AppData data);
    }
}