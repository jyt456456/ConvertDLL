using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace graphDLL
{
    internal class ScrollViewerBehavior : MVbase.MVBase
    {
        
        public static double GetAutoScrollToTop(DependencyObject obj)
        {
            return (double)obj.GetValue(AutoScrollToTopProperty);
        }

        public static void SetAutoScrollToTop(DependencyObject obj, double value)
        {
            obj.SetValue(AutoScrollToTopProperty, value);
        }

        public static readonly DependencyProperty AutoScrollToTopProperty =
            DependencyProperty.RegisterAttached("AutoScrollToTop", typeof(bool), typeof(ScrollViewerBehavior), new PropertyMetadata(false, (o, e) =>
            {
                var scrollViewer = o as ScrollViewer;
                if (scrollViewer == null)
                {
                    return;
                }
                    double st = scrollViewer.ContentHorizontalOffset;
                    SetAutoScrollToTop(o, st);
            }));

        private double scorlly;

        public double Scorlly 
        { 
            get => scorlly; 
            set
            {
                scorlly = value;
                OnPropertyChanged("Reset");
            }
                 
        }

        
    }
}
