using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using xline;

namespace graphglobal
{
    public class global
    {

        public struct Defect_info
        {
            private double x;
            private double y;
            private string defect_type;
            private uint defect_xindex;

            public double X { get => x; set => x = value; }
            public double Y { get => y; set => y = value; }
            public string Defect_type { get => defect_type; set => defect_type = value; }
            public uint Defect_xindex { get => defect_xindex; set => defect_xindex = value; }
        }


        public struct rgb
        {
            private byte r;
            private byte g;
            private byte b;

            public rgb(byte _r, byte _g, byte _b)
            {
                r = _r;
                g = _g;
                b = _b;
            }

            public byte R { get => r; set => r = value; }
            public byte G { get => g; set => g = value; }
            public byte B { get => b; set => b = value; }
        }

        public struct Checkbool
        {
            public string defect_type;
            public bool enable;
            public bool check;

            public bool Enable { get => enable; set => enable = value; }
            public bool Check { get => check; set => check = value; }
            public string Defect_type { get => defect_type; set => defect_type = value; }

        }

        static public int GetTypeToBit(string _Type)
        {
            int bit = 1;

            switch (_Type)
            {
                case "one":
                    bit = 1;
                    break;
                case "two":
                    bit = bit << 1;
                    break;
                case "three":
                    bit = bit << 2;
                    break;
                case "four":
                    bit = bit << 3;
                    break;
                case "five":
                    bit = bit << 4;
                    break;
                case "six":
                    bit = bit << 5;
                    break;
                default:
                    bit = 0;
                    break;
            }
            return bit;

        }

        //Defect_line

        public class Notifier : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChenaged(string property)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        

        public struct im_defect
        {
            private string lot_name;
            private string model_name;
            private int frame_index;
            private int defect_index;
            private string defect_type;
            private bool skip;
            private double pos_x;
            private double pos_y;
            private int model;

            public string Lot_name { get => lot_name; set => lot_name = value; }
            public int Frame_index { get => frame_index; set => frame_index = value; }
            public int Defect_index { get => defect_index; set => defect_index = value; }
            public string Defect_type { get => defect_type; set => defect_type = value; }
            public bool Skip { get => skip; set => skip = value; }
            public double Pos_x { get => pos_x; set => pos_x = value; }
            public double Pos_y { get => pos_y; set => pos_y = value; }
            public int Model { get => model; set => model = value; }
            public string Model_name { get => model_name; set => model_name = value; }
        }

        public enum DefetType
        {
            one = 0,
            two,
            three,
            four,
            five,
            six,
            END
        }


        public struct DefectAllinfo
        {
            private string lot_name;
            private int frame_index;
            private int module_index;
            private string defect_type;
            private bool skip;
            private double pos_x;
            private double pos_y;
            
            
            public int Frame_index { get => frame_index; set => frame_index = value; }
            public int Module_index { get => module_index; set => module_index = value; }
            public string Defect_type { get => defect_type; set => defect_type = value; }
            public bool Skip { get => skip; set => skip = value; }
            public double Pos_x { get => pos_x; set => pos_x = value; }
            public double Pos_y { get => pos_y; set => pos_y = value; }
            public string Lot_name { get => lot_name; set => lot_name = value; }
            
        }


    }
    public class LineVM
    {
        private double linex1;
        private double linex2;
        private double liney1;
        private double liney2;
        private string defect_type;

        public double LineWidth = 100;
        public double LineHeight = 100;

        private Brush Stroke;
        private double thickness;

        public double Linex1 { get => linex1; set => linex1 = value; }
        public double Linex2 { get => linex2; set => linex2 = value; }

        public double Liney2 { get => liney2; set => liney2 = value; }
        public Brush Stroke1 { get => Stroke; set => Stroke = value; }
        public double Thickness { get => thickness; set => thickness = value; }
        public double Liney1 { get => liney1; set => liney1 = value; }
        public string Defect_type
        {
            get => defect_type; set => defect_type = value;
        }
    }
    
    public class TextboxVM
    {
        private string text;
        private Thickness pos;

        public string Text { get => text; set => text = value; }
        public Thickness Pos { get => pos; set => pos = value; }
    }


    public class Lineobj
    {

        private string defect_type;
        private uint index;
        private Thickness m_lineposs;
        private Command.intCommand editcommand;
        public double LineWidth = 100;
        public double LineHeight = 100;
        private xline.XlineMV xmv;
        private Brush Stroke;
        private double thickness;

        private global.rgb linergb;
        private bool btnEnable;

        public string Defect_type { get => Defect_type1; set => Defect_type1 = value; }
        public Brush Stroke1 { get => Stroke; set => Stroke = value; }
        public double Thickness { get => thickness; set => thickness = value; }
        public string Defect_type1 { get => defect_type; set => defect_type = value; }
        public uint Index { get => index; set => index = value; }
        public Thickness Lineposs { get => m_lineposs; set => m_lineposs = value; }
        public Command.intCommand Editcommand { get => editcommand; set => editcommand = value; }
        public global.rgb Linergb { get => linergb; set => linergb = value; }
        public bool BtnEnable { get => btnEnable; set => btnEnable = value; }
        public XlineMV Xmv { get => xmv; set => xmv = value; }
    }





}
