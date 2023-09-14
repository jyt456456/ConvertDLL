using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Command
{
    public class intCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<uint> _execute;


        public intCommand(Action<uint> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(Convert.ToUInt32(parameter));
        }
    }
}
