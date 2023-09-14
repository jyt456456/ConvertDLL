using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cntgraph
{
    internal class CountgraphMV : MVbase.MVbse
    {

        private List<int> m_combolist;
        private int m_curcount;
        private string m_curLot = "";
        public ICommand selchange { get; set; }

        public List<int> Combolist { get => m_combolist; set => m_combolist = value; }
        public int Curcount { get => m_curcount; set => m_curcount = value; }
        public string CurLot { get => m_curLot; set => m_curLot = value; }

        public CountgraphMV()
        {
            init();
        }


        private void init()
        {
            m_combolist = new List<int>();

            for (int i = 1; i < 5; ++i)
            {
                Combolist.Add(i);
            }

            selchange = new RelayCommand<object>(selchanged);

        }

        private void selchanged(object _obj)
        {
            SelectionChangedEventArgs args = _obj as SelectionChangedEventArgs;
            if (args == null)
                return;

            object selecteditem = args.AddedItems[0];
            //graph 변경
            Curcount = Convert.ToInt32(selecteditem);
            OnPropertyChanged("SelChange");
        }


        public void SetLot(string _str)
        {
            m_curLot = _str;
        }

    }
}
