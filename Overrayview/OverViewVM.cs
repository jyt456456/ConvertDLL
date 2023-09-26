using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using graphglobal;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Shapes;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using Command;
using System.Runtime.InteropServices;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Overrayview
{
    public class OverViewVM : MVbase.MVBase, Command.IEventArgsConverter
    {
        private double x = 0;
        private double y = 0;
        private double width = 0;
        private double height = 0;
        private System.Drawing.Color color;
        private bool isSel = false;
        private double contentOffsetX = 0;
        private double contentOffsetY = 0;
        private double contentWidth = 450;
        private double contentHeight = 450;
        private double contentViewportWidth = 120;
        private double contentViewportHeight = 100;


        private double scaleX;
        private double scaleY;

        
        private RelayCommand<System.Windows.Point> _mouseDragCommand;
        private RelayCommand<System.Windows.Point> _mouseDownCommand;
        private ObservableCollection<Lineobj> overobjitem;
        private ObservableCollection<Ractdata> rectangles = new ObservableCollection<Ractdata>();
        private ObservableCollection<Lineobj> m_objitems;

        //private static OverViewVM instance = new OverViewVM();
        
        public ObservableCollection<Lineobj> Objitems { get => m_objitems; set => m_objitems = value; }
        

        public ICommand MouseDragCommand
        {
            get
            {
                return this._mouseDragCommand ??

                     (this._mouseDragCommand = new RelayCommand<System.Windows.Point>(this.ExecuteMouseDrag));
            }
        }

        public ICommand MouseDownCommand
        {

            get
            {
                return this._mouseDownCommand ??

                     (this._mouseDownCommand = new RelayCommand<System.Windows.Point>(this.ExecuteMouseDown));
            }
        }

        public double ScaleY { get => scaleY; set => scaleY = value; }
        public double ScaleX { get => scaleX; set => scaleX = value; }

        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        public double Y 
        { get => y; 
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }



        public double Width 
        { 
            get => width;
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
                
        }
        public double Height 
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged("Height");
            } 
        }
        public Color Color { get => color; set => color = value; }
        public double ContentOffsetX
        {
            get => contentOffsetX; 
            set
            {
                contentOffsetX = value;
                OnPropertyChanged("ContentOffsetX");
            } 
        }
        public double ContentOffsetY 
        {
            get => contentOffsetY; 
            set
            {
                contentOffsetY = value;
                OnPropertyChanged("ContentOffsetY");
            }
            
        }
        public double ContentWidth
        {
            get => contentWidth;
            set
            {
                contentWidth = value;
                OnPropertyChanged("ContentWidth");
            }

        }
        public double ContentHeight 
        {
            get => contentHeight;
            set
            {
                contentHeight = value;
                OnPropertyChanged("ContentHeight");
            }
            
        }
        public double ContentViewportWidth 
        {
            get => contentViewportWidth;
            set
            {
                contentViewportWidth = value;
                OnPropertyChanged("ContentViewportWidth");
            }
            
        }
        public double ContentViewportHeight 
        {
            get => contentViewportHeight;
            set
            {
                contentViewportHeight = value;
                OnPropertyChanged("ContentViewportHeight");
            }
            
        }
        public ObservableCollection<Lineobj> Overobjitem 
        {
            get => overobjitem; 
            set
            {
                overobjitem = value;
                OnPropertyChanged("Overobjitem");
            }
            
        }

        

        public OverViewVM()
        {
            m_objitems = new ObservableCollection<Lineobj>();
            //scalex = 0.5;
            scaleX = 1;
            scaleY = 0.1;
        }
        /*public static OverViewVM Instance
        {
            get
            {
                return instance;
            }
        }*/
        public OverViewVM(double _x, double _y, double _width, double _height, Color _color)
        {
            X = _x;
            Y = _y;
            Width = _width;
            Height = _height;
            Color = _color;
        }


        private void ExecuteMouseDrag(System.Windows.Point delta)
        {
            this.MouseDrag(delta);
        }

        private void MouseDrag(System.Windows.Point delta)
        {
            double newContentOffsetX = Math.Min(Math.Max(0.0, ContentOffsetX + delta.X), ContentWidth - ContentViewportWidth);
            
                ContentOffsetX = newContentOffsetX;
            double newContentOffsetY = Math.Min(Math.Max(0.0, ContentOffsetY + delta.Y), ContentHeight - ContentViewportHeight);
            ContentOffsetY = newContentOffsetY;            
        }


        private void ExecuteMouseDown(System.Windows.Point pt)
        {
            double newX = pt.X - (ContentViewportWidth / 2);
            double newY = pt.Y - (ContentViewportHeight / 2);
            ContentOffsetX = newX;
            ContentOffsetY = newY;

        }


        public object Convert(object value, object parameter)
        {
            var args = (MouseEventArgs)value;
            var element = (FrameworkElement)parameter;
            var point = args.GetPosition(element);
            
            return point;
        }


        public void AddData(List<graphglobal.Lineobj> _list)
        {

            for(int i=0; i< _list.Count; ++i)
            {
                // 스케일 최소값 Setting
                graphglobal.Lineobj tempobj = new graphglobal.Lineobj();
                tempobj = _list[i].GetCopy();

                double tempx = _list[i].Xmv.X2 * scaleX;
                double tempy = _list[i].Xmv.Y2 * scaleY;

                if (tempx < 5)
                {
                    tempobj.Xmv.LineScaleX = 1 / scaleX;
                }

                if (tempy < 5)
                {
                    tempobj.Xmv.LineScaleY = 1 / scaleY;
                }

                m_objitems.Add(_list[i]);
            }
        }

        public void ClearList()
        {
            m_objitems.Clear();
        }
    }
}
