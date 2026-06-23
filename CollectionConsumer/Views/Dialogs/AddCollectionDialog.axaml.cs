using Avalonia.Controls;
using CollectionConsumer.ViewModels;
using CollectionConsumer.Models;

namespace CollectionConsumer.Views.Dialogs
{
    public partial class AddCollectionDialog : Window
    {
        public AddCollectionDialog()
        {
            InitializeComponent();
        }

        public AddCollectionDialog(AddCollectionDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            vm.DialogResult += OnDialogResult;
        }

        private void OnDialogResult(Collection? collection)
        {
            Close(collection);
        }
    }
}