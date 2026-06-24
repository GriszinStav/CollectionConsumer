using Avalonia.Controls;
using CollectionConsumer.ViewModels;

namespace CollectionConsumer.Views.Dialogs
{
    public partial class RemoveCollectionDialog : Window
    {
        public RemoveCollectionDialog()
        {
            InitializeComponent();
        }

        public RemoveCollectionDialog(RemoveCollectionDialogViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            vm.DialogResult += OnDialogResult;
        }

        private void OnDialogResult(bool? result)
        {
            Close(result);
        }
    }
}