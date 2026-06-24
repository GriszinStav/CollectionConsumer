using Avalonia.Controls;
using CollectionConsumer.ViewModels;

namespace CollectionConsumer.Views.Dialogs
{
    public partial class RemoveCardDialog : Window
    {
        public RemoveCardDialog()
        {
            InitializeComponent();
        }

        public RemoveCardDialog(RemoveCardDialogViewModel vm)
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