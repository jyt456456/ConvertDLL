using graphInterface;
using Microsoft.VisualBasic;
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


namespace UCSearch
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class IUCSearch : UserControl, ICountgraph
    {

        //Frame inter
        public List<graphInterface.IUcSearch> interactor { set; get; }
        public IUcSearch CntChart { get => m_CntChart; set => m_CntChart = value; }

        private graphInterface.IUcSearch m_CntChart;
        private UCSearchViewM _vm;
        
        public IUCSearch()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.DataContext = new UCSearchViewM();
            _vm = this.DataContext as UCSearchViewM;

            _vm.PropertyChanged += _UCSPropertyChanged;

            interactor = new List<graphInterface.IUcSearch>();
        }

        private void _UCSPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SearchLOT":
                    for (int i = 0; i < interactor.Count; ++i)
                    {
                        interactor[i].LOTSearch(_vm.StrLot);
                        CntChart.LOTSearch(_vm.StrLot);
                    }
                    break;
            }
        }

        void ICountgraph.GetCountGraph(int _icount)
        {
            _vm.Curcount = _icount;
        }

        void ICountgraph.GetLotCount(string _lot, int _icount)
        {
            _vm.Curcount = _icount;

            for (int i = 0; i < interactor.Count; ++i)
            {
                interactor[i].LOTSearch(_vm.StrLot);
            }
            _vm.testfun();
        }



    }
}