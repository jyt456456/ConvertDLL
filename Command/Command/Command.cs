using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Command
{
    public class Command : ICommand
    {

            private Action ExecuteMethod;
            private Action<object> _action;
            //Func<object, bool> CanexecuteMethod;

            public Command(Action executeMethod)
            {
                this.ExecuteMethod = executeMethod;
                //this.CanexecuteMethod = canexecuteM_ethodction;
            }

            public event EventHandler CanExecuteChanged;

            //조건검사
            public bool CanExecute(object parameter)
            {

                if (ExecuteMethod != null)
                {
                    //      return CanexecuteMethod(parameter);
                    return true;
                }
                else
                    return false;





            }

        

            //실행
            public void Execute(object parameter)
            {
                //ExecuteMethod(parameter);
                ExecuteMethod.Invoke();

            }
    }
    

}
