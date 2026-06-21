using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CollectionConsumer.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string prop = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(prop);
            return true;
        }

        public class RelayCommand : System.Windows.Input.ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool>? _canExecute;
            public RelayCommand(Action execute, Func<bool>? canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }
            public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
            public void Execute(object? parameter) => _execute();
            public event EventHandler? CanExecuteChanged;
            public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public class RelayCommand<T> : System.Windows.Input.ICommand
        {
            private readonly Action<T> _execute;
            private readonly Func<T, bool>? _canExecute;
            public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }
            public bool CanExecute(object? parameter) => _canExecute?.Invoke((T)parameter!) ?? true;
            public void Execute(object? parameter) => _execute((T)parameter!);
            public event EventHandler? CanExecuteChanged;
            public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}