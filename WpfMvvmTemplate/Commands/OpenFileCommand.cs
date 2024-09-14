using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfMvvmTemplate.Commands
{
    public class OpenFileCommand : ICommand
    {
        private readonly Action<string> _execute;

        public OpenFileCommand(Action<string> execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? result = openFileDialog.ShowDialog();

            if(result == true)
            {
                string filename = openFileDialog.FileName;
                _execute(filename);
            }
        }
    }
}
