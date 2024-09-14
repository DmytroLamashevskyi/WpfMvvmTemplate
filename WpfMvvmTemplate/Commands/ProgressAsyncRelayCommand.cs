using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfMvvmTemplate.Commands
{
    public class ProgressAsyncRelayCommand : ICommand
    {
        private readonly Func<IProgress<int>, Task> _execute;
        private bool _isExecuting;

        public ProgressAsyncRelayCommand(Func<IProgress<int>, Task> execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => !_isExecuting;

        public async void Execute(object parameter)
        {
            _isExecuting = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            var progress = new Progress<int>(percent =>
            {
                ProgressValue = percent;
                OnPropertyChanged(nameof(ProgressValue));
            });

            await _execute(progress);

            _isExecuting = false;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public int ProgressValue { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
