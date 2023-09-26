using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Overrayview.EventAccArgs
{
    class AcceptedFilterEventArgs : IFilterEventArgs
    {
        private FilterEventArgs _filterEventArgs;

        public AcceptedFilterEventArgs(FilterEventArgs e)
        {
            this._filterEventArgs = e;
        }

        public bool Accepted
        {
            get
            {
                if (this._filterEventArgs == null)
                {
                    return false;
                }

                return this._filterEventArgs.Accepted;
            }
            set
            {
                if (this._filterEventArgs == null)
                {
                    return;
                }

                this._filterEventArgs.Accepted = value;
            }
        }

        public object Item
        {
            get
            {
                if (this._filterEventArgs == null)
                {
                    return null;
                }

                return this._filterEventArgs.Item;
            }
        }

        public void Dispose()
        {
            this._filterEventArgs = null;
        }
    }
}
