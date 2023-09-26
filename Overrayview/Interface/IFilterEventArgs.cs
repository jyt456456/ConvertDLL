using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overrayview
{
    interface IFilterEventArgs
    {
        public interface IFilterEventArgs : IDisposable
        {
            bool Accepted { get; set; }

            object Item { get; }
        }
    }
}
