using graphInterface;
using graphmodule.typeCheckbox;
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


namespace UCtypecheck
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCtypecheckbox : UserControl , IFrame, IGraphtoCheck
    {

        private typeCheckboxVM _vm;
        public graphInterface.ITypeCheckbox interactor { set; private get; }
        public IChecktoGraph graphinter { set; private get; }
        public UCtypecheckbox()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.DataContext = new typeCheckboxVM();
            _vm = this.DataContext as typeCheckboxVM;

            //m.PropertyChanged += _UCSPropertyChanged;
            _vm.PropertyChanged += _UCtypePropertyChanged;
        }
        
        private void _UCtypePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SearchLOT":
                    //interactor.DefectType(_vm.CurType, _vm.CurCheck);
                    break;
                case "CheckClick":
                    //interactor.DefectType(_vm.CurType, _vm.CurCheck);
                    graphinter.ChecktoGraph(_vm.BitCurCheck,_vm.CurCheck);
                    break;
                
            }
            //temp();
        }


        public void SetMargin(int max, int _iCur)
        {
            _vm.SetMargin(max, _iCur);
        }


        void IFrame.getFrame(string _Iot, int _iframe)
        {
            _vm.ChagneFrame(_Iot, _iframe);
        }

        void IFrame.ResetSearch()
        {
            _vm.Reset();
        }

        void IGraphtoCheck.AddType(int _typebit)
        {
            _vm.AddType(_typebit);
        }

    }
}