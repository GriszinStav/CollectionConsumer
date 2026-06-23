using System;
using System.Collections.Generic;
using CollectionConsumer.Models;
using CollectionConsumer.Services;

namespace CollectionConsumer.ViewModels
{
    public class AddCardDialogViewModel : ViewModelBase
    {
        private readonly IFilePickerService _filePickerService;

        public List<string> Rarities { get; } = new()
        {
            "common", "uncommon", "rare", "special",
            "epic", "extraordinary", "mythic", "legendary"
        };

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string? _imagePath;
        public string? ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        private string _rarity = "common";
        public string Rarity
        {
            get => _rarity;
            set => SetProperty(ref _rarity, value);
        }

        private string _priceText = string.Empty;
        public string PriceText
        {
            get => _priceText;
            set => SetProperty(ref _priceText, value);
        }

        private DateTime? _acquisitionDate;
        public DateTime? AcquisitionDate
        {
            get => _acquisitionDate;
            set => SetProperty(ref _acquisitionDate, value);
        }

        public RelayCommand ChooseImageCommand { get; }
        public RelayCommand OkCommand { get; }
        public RelayCommand CancelCommand { get; }

        public event Action<Card?>? DialogResult;

        public AddCardDialogViewModel(IFilePickerService filePickerService)
        {
            _filePickerService = filePickerService;
            ChooseImageCommand = new RelayCommand(ChooseImage);
            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        private async void ChooseImage()
        {
            var path = await _filePickerService.PickImageFileAsync();
            if (path != null)
                ImagePath = path;
        }

        private void Ok()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                // Можно показать сообщение, но для простоты просто не даём закрыть
                return;
            }

            decimal? price = null;
            if (!string.IsNullOrWhiteSpace(PriceText) && decimal.TryParse(PriceText, out var parsed))
                price = parsed;

            var card = new Card
            {
                Name = Name,
                ImagePath = ImagePath,
                Rarity = Rarity,
                Price = price,
                AcquisitionDate = AcquisitionDate
            };
            DialogResult?.Invoke(card);
        }

        private void Cancel()
        {
            DialogResult?.Invoke(null);
        }
    }
}