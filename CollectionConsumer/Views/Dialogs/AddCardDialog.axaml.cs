using Avalonia.Controls;
using CollectionConsumer.ViewModels;
using CollectionConsumer.Models;

namespace CollectionConsumer.Views.Dialogs
{
    public partial class AddCardDialog : Window
    {
        public AddCardDialog()
        {
            InitializeComponent();
        }

        public AddCardDialog(AddCardDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            vm.DialogResult += OnDialogResult;
        }

        private void OnDialogResult(Card? card)
        {
            Close(card);
        }
    }
}