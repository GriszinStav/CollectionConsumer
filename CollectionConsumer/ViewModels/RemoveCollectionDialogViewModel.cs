using System;

namespace CollectionConsumer.ViewModels
{
    public class RemoveCollectionDialogViewModel : ViewModelBase
    {
        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public RelayCommand DeleteCommand { get; }
        public RelayCommand CancelCommand { get; }

        public event Action<bool?>? DialogResult;

        public RemoveCollectionDialogViewModel(string collectionName)
        {
            Message = $"\u0412\u044B \u0443\u0432\u0435\u0440\u0435\u043D\u044B, \u0447\u0442\u043E \u0445\u043E\u0442\u0438\u0442\u0435 \u0443\u0434\u0430\u043B\u0438\u0442\u044C \u043A\u043E\u043B\u043B\u0435\u043A\u0446\u0438\u044E \"{collectionName}\"?";
            DeleteCommand = new RelayCommand(Delete);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Delete()
        {
            DialogResult?.Invoke(true);
        }

        private void Cancel()
        {
            DialogResult?.Invoke(false);
        }
    }
}