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

namespace mainview
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCMainView : UserControl, ICountgraph
    {
        private MainViewModel _vm;

        public UCMainView()
        {
            InitializeComponent();
            init();
        }
        
        private void init()
        {
            this.DataContext = new MainViewModel();
            _vm = this.DataContext as MainViewModel;
            cb_cont.maininteractor = this;
            cb_cont.searchinteractor = _vm.m_ucsarch[0];
        }


        void ICountgraph.GetCountGraph(int _icount)
        {
            //_vm.ChangeCount = _icount;
            _vm.CountChangeEvent(_icount);
            //
        }

        void ICountgraph.GetLotCount(string _lot, int _icount)
        {
            _vm.CountChangeEvent(_icount);
        }
    }
}