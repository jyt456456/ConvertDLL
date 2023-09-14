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

namespace FrameSel
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UCFrameSel : UserControl, graphInterface.IUcSearch
    {
        public IFrame grpahinteractor { set; private get; }
        public IFrame checkinteractor { set; private get; }

        public IFrame Toggleinter { set; private get; }

        private FrameViewM FVM;

        public UCFrameSel()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.DataContext = new FrameViewM();
            FVM = this.DataContext as FrameViewM;
            FVM.PropertyChanged += _fm_PropertyChanged;

        }

        private void _fm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //interface 알림
            switch (e.PropertyName)
            {
                case "SelectChange":
                    grpahinteractor.getFrame(FVM.StrLot, FVM.Curframe);
                    checkinteractor.getFrame(FVM.StrLot, FVM.Curframe);
                    Toggleinter.getFrame(FVM.StrLot, FVM.Curframe);
                    //   curframe 동기화
                    break;
                case "ResetSearch":
                    grpahinteractor.ResetSearch();
                    checkinteractor.ResetSearch();
                    Toggleinter.ResetSearch();
                    break;

                default: break;
            }
        }


        void IUcSearch.LOTSearch(string str)
        {
            //FVM.StrLot = str;
            FVM.SetFrameList(str);
        }
        public void SetMargin(int max, int _icur)
        {
            FVM.SetMargin(max, _icur);
        }
    }
}