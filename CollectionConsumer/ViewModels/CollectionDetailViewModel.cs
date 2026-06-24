using System;
using System.Collections.ObjectModel;
using System.Linq;
using CollectionConsumer.Models;
using CollectionConsumer.Services;
using CollectionConsumer.Views.Dialogs;

namespace CollectionConsumer.ViewModels
{
    public class CollectionDetailViewModel : ViewModelBase
    {
        private readonly AppData _appData;
        private readonly IDataService _dataService;
        private readonly IFilePickerService _filePickerService;
        private readonly MainWindowViewModel _mainWindowViewModel;

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

        public CollectionDetailViewModel(AppData appData, IDataService dataService, IFilePickerService filePickerService, MainWindowViewModel mainVm, Collection collection)
        {
            _appData = appData;
            _dataService = dataService;
            _filePickerService = filePickerService;
            _mainWindowViewModel = mainVm;
            Collection = collection;
            Cards = new ObservableCollection<Card>(collection.Cards);

            AddCardCommand = new RelayCommand(AddCard);
            SelectCardCommand = new RelayCommand<Card>(SelectCard);
            RemoveCardCommand = new RelayCommand(RemoveCard, () => SelectedCard != null);

            if (Cards.Count > 0)
                SelectedCard = Cards[0];
        }

        private async void AddCard()
        {
            var vm = new AddCardDialogViewModel(_filePickerService);
            var dialog = new AddCardDialog(vm);
            var result = await dialog.ShowDialog<Card?>(_mainWindowViewModel.OwnerWindow);
            if (result != null)
            {
                Cards.Add(result);
                Collection.Cards = Cards.ToList();
                SaveData();
                SelectedCard = result;
            }
        }

        private void SelectCard(Card card)
        {
            SelectedCard = card;
        }

        private async void RemoveCard()
        {
            if (SelectedCard == null) return;
            if (string.IsNullOrEmpty(SelectedCard.Name))
                return;

            var vm = new RemoveCardDialogViewModel(SelectedCard.Name);
            var dialog = new RemoveCardDialog(vm);
            var result = await dialog.ShowDialog<bool?>(_mainWindowViewModel.OwnerWindow);
            if (result == true)
            {
                Cards.Remove(SelectedCard);
                Collection.Cards = Cards.ToList();
                SaveData();
                SelectedCard = Cards.FirstOrDefault();
            }
        }

        private async void SaveData()
        {
            await _dataService.SaveDataAsync(_appData);
        }
    }
}