using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Overrayview.EventArgsConverters
{
    internal class FilterEventArgConverter : EventArgsConverterExtension<FilterEventArgConverter>
    {

        public override object Convert(object value, object parameter)
        {
            var e = value as FilterEventArgs;
            if (e == null)
            {
                return null;
            }

            return new EventAccArgs.AcceptedFilterEventArgs(e);
        }

    }
}
