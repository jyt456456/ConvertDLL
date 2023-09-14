using graphglobal;
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

namespace xline
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Xline : UserControl
    {

        private XlineMV m_mv;
        private global.rgb m_rgb;

        /*

        public static readonly DependencyProperty rgbValueProperty = DependencyProperty.Register(
"Properrgb", typeof(global.rgb), typeof(Xline), new PropertyMetadata(null));

        public global.rgb Properrgb
        {
            get { return (global.rgb)GetValue(rgbValueProperty); }
            set { 
                SetValue(rgbValueProperty, value);
                m_mv.SetColor(Properrgb.R, Properrgb.G, Properrgb.B);
            }
        }
        */
        public Xline()
        {
            InitializeComponent();
            init();
        }
      
        private void init()
        {
           // m_rgb = Properrgb;
           // this.DataContext = new XlineMV(m_rgb.R, m_rgb.G, m_rgb.B);
            m_mv = this.DataContext as XlineMV;
            //   m_vm.PropertyChanged += _UCSPropertyChanged;
        }
    }
}