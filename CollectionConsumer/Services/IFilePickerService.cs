using System.Threading.Tasks;

namespace CollectionConsumer.Services
{
    public interface IFilePickerService
    {
        Task<string?> PickImageFileAsync();
    }
}