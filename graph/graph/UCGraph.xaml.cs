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

namespace graphDLL
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCGraph : UserControl,  IFrame , IChecktoGraph, ITrigger
    {
        private GraphVM m_graphvm;

        public IGraphtoCheck m_interactor { set; private get; }
        //internal GraphVM Vm { get => m_vm; set => m_vm = value; }

        public UCGraph()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.DataContext = new GraphVM();
            m_graphvm = this.DataContext as GraphVM;
            m_graphvm.PropertyChanged += _UCSPropertyChanged;

        }

        void IFrame.getFrame(string _lot, int _iframe)
        {
            m_graphvm.Str = _lot;
            m_graphvm.Curframe = _iframe;
            m_graphvm.FrameChange();
        }

        void IFrame.ResetSearch()
        {
            m_graphvm.Reset();
        }

        void IChecktoGraph.ChecktoGraph(int _check,bool _bcheck)
        {
            //  m_vm.Check = _check;
            m_graphvm.TypeChange(_check, _bcheck);
        }

        void ITrigger.RealTime(bool _breal)
        {
            m_graphvm.SetRealTime(_breal);
        }

        public void SetMargin(int max, int _iCur)
        {
            m_graphvm.SetMargin(max, _iCur);
            //Ellipse
        }


        private void _UCSPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "AddType":
                    m_interactor.AddType(m_graphvm.Addbit);
                    break;
            }
        }



    }
}