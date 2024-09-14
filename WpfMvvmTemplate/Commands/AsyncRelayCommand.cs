using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfMvvmTemplate.Commands
{
    public class AsyncRelayCommand<T> : ICommand
    {
        private readonly Func<T, CancellationToken, Task> _execute;
        private readonly Predicate<T> _canExecute;
        private CancellationTokenSource _cts;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<T, CancellationToken, Task> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if(_isExecuting)
                return false;

            if(_canExecute == null)
                return true;

            return parameter == null && typeof(T).IsValueType
                ? _canExecute(default(T))
                : _canExecute((T)parameter);
        }

        public async void Execute(object parameter)
        {
            _isExecuting = true;
            _cts = new CancellationTokenSource();
            RaiseCanExecuteChanged();

            try
            {
                await _execute((T)parameter, _cts.Token);
            }
            finally
            {
                _isExecuting = false;
                _cts = null;
                RaiseCanExecuteChanged();
            }
        }

        public void Cancel()
        {
            if(_cts != null && !_cts.IsCancellationRequested)
                _cts.Cancel();
        }

        private void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
