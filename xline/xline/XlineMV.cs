using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using static graphglobal.global;

namespace xline
{
    public class XlineMV : MVbase.MVbse
    {
        private Brush m_stroke;
        private double m_thickness;
        //Line1
        private double x1;
        private double x2;
        private double y1;
        private double y2;

        //Line2
        private double x3;
        private double y3;
        private double x4;
        private double y4;


        
        public XlineMV(byte _r, byte _g, byte _b)
        {
            m_thickness = 1;
            m_stroke = new SolidColorBrush(Color.FromRgb(_r, _g, _b));
            doublexline();
        }

        public XlineMV()
        {
            m_thickness = 1;
            m_stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            doublexline();
        }    


        private void doublexline()
        {
            X1 = 0;
            X2 = 15;
            Y1 = 0;
            Y2 = 15;
            X3 = 15;
            Y3 = 0;
            X4 = 0;
            Y4 = 15;
        }

        public void SetColor(byte _r, byte _g, byte _b)
        {
            m_stroke = new SolidColorBrush(Color.FromRgb(_r, _g, _b));
            OnPropertyChanged("Stroke");
        }

        public Brush Stroke { get => m_stroke; set => m_stroke = value; }
        public double Thickness { get => m_thickness; set => m_thickness = value; }
        public double X1 { get => x1; set => x1 = value; }
        public double X2 { get => x2; set => x2 = value; }
        public double Y1 { get => y1; set => y1 = value; }
        public double Y2 { get => y2; set => y2 = value; }
        public double X3 { get => x3; set => x3 = value; }
        public double Y3 { get => y3; set => y3 = value; }
        public double X4 { get => x4; set => x4 = value; }
        public double Y4 { get => y4; set => y4 = value; }
    }
}
