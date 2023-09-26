using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Overrayview.EventArgsConverters
{
    class MouseDragDeltaEventArgConverter : EventArgsConverterExtension<MouseDragDeltaEventArgConverter>
    {
        public override object Convert(object value, object parameter)
        {
            var e = value as DragDeltaEventArgs;
            if (e == null)
            {
                return null;
            }
            Point pt = new Point(e.HorizontalChange,e.VerticalChange);

            return pt;
        }

    }
}
