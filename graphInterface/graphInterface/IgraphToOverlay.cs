using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphInterface
{
    public interface IgraphToOverlay
    {
        void AddListobj(List<graphglobal.Lineobj> _lineobj);

        void ResetListobj();

    }
}
