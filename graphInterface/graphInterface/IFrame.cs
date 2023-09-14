using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphInterface
{
    public interface IFrame
    {
        void getFrame(string _lot, int _iframe);

        void ResetSearch();
    }
}
