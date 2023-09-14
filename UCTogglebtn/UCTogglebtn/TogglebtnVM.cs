using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace UCTogglebtn
{
    internal class TogglebtnVM : MVbase.MVbse
    {

        private bool m_bRealtime;
        private Thickness m_margin;
        private bool m_bToggleEnable = false;
        public ObservableCollection<bool> m_blist { get; set; }

        public Thickness Margin { get => m_margin; set => m_margin = value; }
        public ICommand EditCommand { get { return new RelayCommand<string>(CheckClick); } }
        //public RoutedEvent m_routedclick;

        public bool BRealtime { get => m_bRealtime; set => m_bRealtime = value; }
        public bool BToggleEnable { get => m_bToggleEnable; set => m_bToggleEnable = value; }


        public TogglebtnVM()
        {
            m_blist = new ObservableCollection<bool>
            {
                false, // 0 -> ON/OFF
                false // 1 -> enable
            };
        }


        public void CheckClick(object sender)
        {
            //m_bRealtime = !m_bRealtime;
            OnPropertyChanged("CheckClick");
        }


        public void SetMargin(int Max, int _iCur)
        {
            double Top = 330;
            double bot = 580;
            switch (Max)
            {
                case 1:
                    if (_iCur == 1)
                    {
                        Margin = new Thickness(1100.0, Top, 1300.0, bot);
                    }

                    break;
                case 2:
                    if (_iCur == 1)
                    {
                        Margin = new Thickness(650.0, Top, 800.0, bot);
                    }
                    else
                    {
                        Margin = new Thickness(1250.0, Top, 1400.0, bot);
                    }

                    break;
                case 3:
                    switch (_iCur)
                    {
                        case 1:
                            Margin = new Thickness(450.0, Top, 600.0, bot);
                            break;
                        case 2:
                            Margin = new Thickness(1100.0, Top, 1200.0, bot);
                            break;
                        default:
                            Margin = new Thickness(1650.0, Top, 1800.0, bot);
                            break;
                    }

                    break;
                case 4:
                    switch (_iCur)
                    {
                        case 1:
                            Margin = new Thickness(350.0, Top, 600.0, bot);
                            break;
                        case 2:
                            Margin = new Thickness(800.0, Top, 900.0, bot);
                            break;
                        case 3:
                            Margin = new Thickness(1300.0, Top, 1400.0, bot);
                            break;
                        default:
                            Margin = new Thickness(1700.0, Top, 1800.0, bot);
                            break;
                    }

                    break;
            }

            OnPropertyChanged("margin");

        }


        public void SetEnable(bool _bEnable)
        {
            m_blist[0] = false;
            m_blist[1] = _bEnable;
            m_bRealtime = false;
            OnPropertyChanged("reset");
        }

    }

}
