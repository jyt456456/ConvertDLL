using graphglobal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Overrayview
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class OverView : UserControl ,graphInterface.IgraphToOverlay ,graphInterface.IYSynchro
    {
        private OverViewVM m_vm;

        public OverView()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.DataContext = new OverViewVM();
            m_vm = this.DataContext as OverViewVM;
            m_vm.PropertyChanged += _UCSPropertyChanged;
        }


        private void _UCSPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "AddType":
                  //  m_interactor.AddType(m_graphvm.Addbit);
                    break;
            }
        }

        void graphInterface.IYSynchro.YSynco(double _Y)
        {
            //m_vm
        }

        public void ResetSearch()
        {
            //m_vm.Reset();
        }

        public void AddListobj(List<Lineobj> _lineobj)
        {
            m_vm.AddData(_lineobj);
        }

        public void ResetListobj()
        {
            m_vm.ClearList();
        }
    }
}