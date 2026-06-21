using System;
using System.Collections.ObjectModel;
using System.Linq;
using CollectionConsumer.Models;
using CollectionConsumer.Services;

namespace CollectionConsumer.ViewModels
{
    public class CollectionDetailViewModel : ViewModelBase
    {
        private readonly AppData _appData;
        private readonly IDataService _dataService;
        private readonly IFilePickerService _filePickerService;

        private Card _selectedCard = new();
        public Card SelectedCard
        {
            get => _selectedCard;
            set => SetProperty(ref _selectedCard, value);
        }

        public Collection Collection { get; }
        public ObservableCollection<Card> Cards { get; }

        public RelayCommand AddCardCommand { get; }
        public RelayCommand<Card> SelectCardCommand { get; }
        public RelayCommand RemoveCardCommand { get; }
        public RelayCommand PickImageForCardCommand { get; }

        public CollectionDetailViewModel(AppData appData, IDataService dataService, IFilePickerService filePickerService, Collection collection)
        {
            _appData = appData;
            _dataService = dataService;
            _filePickerService = filePickerService;
            Collection = collection;
            Cards = new ObservableCollection<Card>(collection.Cards);

            AddCardCommand = new RelayCommand(AddCard);
            SelectCardCommand = new RelayCommand<Card>(SelectCard);
            RemoveCardCommand = new RelayCommand(RemoveCard, () => SelectedCard != null);
            PickImageForCardCommand = new RelayCommand(PickImageForCard, () => SelectedCard != null);

            if (Cards.Count > 0)
                SelectedCard = Cards[0];
        }

        private void AddCard()
        {
            var card = new Card { Name = "\u041D\u043E\u0432\u0430\u044F \u043A\u0430\u0440\u0442\u0430" };
            Cards.Add(card);
            Collection.Cards = Cards.ToList();
            SaveData();
            SelectedCard = card;
        }

        private void SelectCard(Card card)
        {
            SelectedCard = card;
        }

        private void RemoveCard()
        {
            if (SelectedCard != null)
            {
                Cards.Remove(SelectedCard);
                Collection.Cards = Cards.ToList();
                SaveData();
                SelectedCard = Cards.FirstOrDefault();
            }
        }

        private async void PickImageForCard()
        {
            if (SelectedCard == null) return;
            var path = await _filePickerService.PickImageFileAsync();
            if (path != null)
            {
                SelectedCard.ImagePath = path;
                SaveData();
            }
        }

        private async void SaveData()
        {
            await _dataService.SaveDataAsync(_appData);
        }
    }
}