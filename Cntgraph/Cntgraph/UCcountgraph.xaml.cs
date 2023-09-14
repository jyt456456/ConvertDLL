using graphInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cntgraph
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCcountgraph : UserControl , graphInterface.IUcSearch
    {
        private CountgraphMV _mv;
        public graphInterface.ICountgraph maininteractor { set; private get; }
        public graphInterface.ICountgraph searchinteractor { set; private get; }
        public UCcountgraph()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.DataContext = new CountgraphMV();
            _mv = this.DataContext as CountgraphMV;
            _mv.PropertyChanged += _cntgraph_PropertyChanged;
            //maininteractor = new 
        }


        private void _cntgraph_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //interface 알림
            switch (e.PropertyName)
            {
                case "SelChange":

                    if (_mv.CurLot.Equals(""))
                    {
                        maininteractor.GetCountGraph(_mv.Curcount);
                        searchinteractor.GetCountGraph(_mv.Curcount);
                        
                    }
                    else
                    {
                        maininteractor.GetLotCount(_mv.CurLot, _mv.Curcount);
                        searchinteractor.GetLotCount(_mv.CurLot,_mv.Curcount);
                       
                    }
                    //   curframe 동기화
                    break;



            }
        }

        void IUcSearch.LOTSearch(string str)
        {
            //FVM.StrLot = str;
            _mv.SetLot(str);
        }


    }
}