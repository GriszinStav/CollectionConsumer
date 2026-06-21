using Avalonia.Platform.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CollectionConsumer.Services
{
    public class FilePickerService : IFilePickerService
    {
        private readonly IStorageProvider _storageProvider;

        public FilePickerService(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public async Task<string?> PickImageFileAsync()
        {
            var options = new FilePickerOpenOptions
            {
                Title = "Выберите изображение",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Images")
                    {
                        Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.bmp" }
                    }
                }
            };
            var files = await _storageProvider.OpenFilePickerAsync(options);
            if (files.Count == 0) return null;

            var sourcePath = files[0].Path.LocalPath;
            var destDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                        "CollectionConsumer", "UserImages");
            Directory.CreateDirectory(destDir);
            var destName = Guid.NewGuid().ToString() + Path.GetExtension(sourcePath);
            var destPath = Path.Combine(destDir, destName);
            File.Copy(sourcePath, destPath, true);
            return destPath;
        }
    }
}