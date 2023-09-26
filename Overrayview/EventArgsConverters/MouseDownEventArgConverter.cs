using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Reflection.Metadata;

namespace Overrayview.EventArgsConverters
{
    class MouseDownEventArgConverter : EventArgsConverterExtension<MouseDownEventArgConverter>
    {
        public override object Convert(object value, object parameter)
    {
      //  var element = (FrameworkElement)value;
        var e = value as MouseButtonEventArgs;
            var el = (FrameworkElement)e.Source;
        if (e == null)
        {
                return null;
        }

            Point pt = e.GetPosition(el);

        return pt;
    }

}
}
