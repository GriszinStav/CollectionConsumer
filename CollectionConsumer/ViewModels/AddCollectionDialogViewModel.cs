using System;
using CollectionConsumer.Models;
using CollectionConsumer.Services;

namespace CollectionConsumer.ViewModels
{
    public class AddCollectionDialogViewModel : ViewModelBase
    {
        private readonly IFilePickerService _filePickerService;

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string? _iconPath;
        public string? IconPath
        {
            get => _iconPath;
            set => SetProperty(ref _iconPath, value);
        }

        public RelayCommand ChooseIconCommand { get; }
        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        public event Action<Collection?>? DialogResult;

        public AddCollectionDialogViewModel(IFilePickerService filePickerService)
        {
            _filePickerService = filePickerService;

            ChooseIconCommand = new RelayCommand(ChooseIcon);
            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        private async void ChooseIcon()
        {
            var path = await _filePickerService.PickImageFileAsync();
            if (path != null)
                IconPath = path;
        }

        private void Ok()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return;
            var collection = new Collection
            {
                Name = Name,
                IconPath = IconPath
            };
            DialogResult?.Invoke(collection);
        }

        private void Cancel()
        {
            DialogResult?.Invoke(null);
        }
    }
}