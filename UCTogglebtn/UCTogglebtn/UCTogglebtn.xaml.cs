using Microsoft.VisualBasic;
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

namespace UCTogglebtn
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCTogglebtn : UserControl , graphInterface.IFrame
    {

        private TogglebtnVM m_vm;


        //to graph
        public graphInterface.ITrigger Triter { set; private get; }

        public UCTogglebtn()
        {
            
            InitializeComponent();

            init();
        }


        private void init()
        {
             
            this.DataContext = new TogglebtnVM();
            m_vm = this.DataContext as TogglebtnVM;
            m_vm.PropertyChanged += _UCtypePropertyChanged;
            

        }

        void graphInterface.IFrame.ResetSearch()
        {
            //frame √ ±‚»≠
            //enable =false
            m_vm.SetEnable(false);
            //KillThread
            //OnPropertyChanged("reset");
        }

        void graphInterface.IFrame.getFrame(string _lot, int _iframe)
        {
            //enable =true
            m_vm.SetEnable(true);

            //KillThread
            
        }
        private void _UCtypePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CheckClick":
                    Triter.RealTime(m_vm.m_blist[0]);
                    break;

            }
            //temp();
        }

        public void SetMargin(int max, int _icur)
        {
            m_vm.SetMargin(max, _icur);
        }
    }

}