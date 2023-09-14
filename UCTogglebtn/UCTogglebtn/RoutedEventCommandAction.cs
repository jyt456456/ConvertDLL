using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows;

namespace UCTogglebtn
{
    public sealed class RoutedEventCommandAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(RoutedEventArgs), typeof(RoutedEventCommandAction), null);

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(RoutedEventCommandAction), null);

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        protected override void Invoke(object arg)
        {
            if (this.AssociatedObject == null)
                return;

            var vCommand = this.Command;
            if (vCommand == null)
                return;

            if (this.CommandParameter != null)
            {
                if (vCommand.CanExecute(this.CommandParameter))
                    vCommand.Execute(this.CommandParameter);
            }
            else
            {
                if (vCommand.CanExecute(arg))
                    vCommand.Execute(arg);
                // "object sender"를 함께 보내려면, 위의 코드를 아래로 바꿉니다.
                // vCommand.Execute(new Tuple(this.AssociatedObject, arg));
            }
        }


    }
}
