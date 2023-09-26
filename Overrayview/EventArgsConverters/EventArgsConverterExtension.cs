using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Overrayview.EventArgsConverters
{


    public abstract class EventArgsConverterExtension<T> :MarkupExtension, Command.IEventArgsConverter

    where T : class, new()

    {

        private static Lazy<T> _converter = new Lazy<T>(() => new T());



        public override object ProvideValue(IServiceProvider serviceProvider)

        {

            return _converter.Value;

        }



        public abstract object Convert(object value, object parameter);

    }
}
