using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphInterface
{
    public interface ICountgraph
    {
        void GetCountGraph(int _icount);
        void GetLotCount(string _lot, int _icount);
    }
}
