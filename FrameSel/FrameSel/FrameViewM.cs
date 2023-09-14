//using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight;
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
using System.Windows.Media.Converters;

namespace FrameSel
{
    internal class FrameViewM : MVbase.MVbse
    {


        //LOT name -> frame list get
        public ObservableCollection<string> m_frameList { get; set; }
        public string StrLot { get => m_strLot; set => m_strLot = value; }
        private Thickness m_margin;
        private int curframe = 0;
        private string m_strLot;
        private string strcurframe = "";
           //   get { return selectedIndex; }
          //  set { selectedIndex = value; OnPropertyChanged("SelectedItem"); }

        public ICommand SelectChagne { get; set; }
        public int Curframe
        {
            get
            {
                return curframe;
            }
            set
            {
                curframe = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public Thickness Margin { get => m_margin; set => m_margin = value; }
        public string Strcurframe 
        {
            get
            {
                return strcurframe; 
            }
            set
            {
               strcurframe = value;
                OnPropertyChanged("SelectedItem");
            }
        }
            

        public FrameViewM()
        {
            init();
        }



        //Selection Change

        private void init()
        {
            m_frameList = new ObservableCollection<string>();
            SelectChagne = new RelayCommand<object>(SeletChanged);
        }

        public void SetFrameList(string _str)
        {
            if (_str == null)
                return;

            if (m_strLot == _str)
                return;

            m_strLot = _str;
            m_strLot = m_strLot.Replace(" ", string.Empty);
            if (m_strLot.Equals(""))
                return;

          
        
            postdb.DB pdb = new postdb.DB();
            
            List<int> framelist = new List<int>();


            framelist = pdb.SeachLot(m_strLot);
            if (framelist.Count == 0)
            {
                MessageBox.Show("일치하는 LOT번호가 없습니다.");
                
                return;
            }

            m_frameList.Clear();

            List<int> frmaelist = pdb.SeachLot(m_strLot);

            for (int i = 0; i < frmaelist.Count; ++i)
            {
                m_frameList.Add(frmaelist[i].ToString());
            }

            //Curframe = 1;
            //-> 인터페이스 Reset명령
            //ResetSearch
            curframe = 0;
            OnPropertyChanged("ResetSearch");
        }


        private void SeletChanged(object obj)
        {

            if(m_frameList.Count <1)
            {
                return;
            }

            SelectionChangedEventArgs args = obj as SelectionChangedEventArgs;
            if (args == null)
                return;
            
            object selecteditem = args.AddedItems[0];
            //graph 변경
            curframe = Convert.ToInt32(selecteditem);

            OnPropertyChanged("SelectChange");

        }

        public void SetMargin(int _Max,int _iCur)
        {
            
            switch(_Max)
            {
                case 1:
                    // 정중앙
                    if(_iCur ==1)
                    {
                        Margin = new Thickness(900, 50, 1200, 400);
                    }
                    break;
                case 2:
                    if(_iCur ==1)
                    {
                        Margin = new Thickness(450, 50, 800, 400);
                    }
                    else
                    {
                        Margin = new Thickness(1050, 50, 1600, 400);
                    }

                    break;
                case 3:
                    if (_iCur == 1)
                    {
                        Margin = new Thickness(250, 50, 600, 400);
                    }
                    else if(_iCur ==2)
                    {
                        Margin = new Thickness(900, 50, 1200, 400);
                    }
                    else
                    {
                        Margin = new Thickness(1450, 50, 1700, 400);
                       // Margin = new Thickness(800, 100, 1200, 400);
                    }
                    break;
                case 4:
                    if (_iCur == 1)
                    {
                        Margin = new Thickness(130, 50, 300, 400);
                    }
                    else if (_iCur == 2)
                    {
                        Margin = new Thickness(600, 50, 700, 400);
                    }
                    else if(_iCur == 3)
                    {
                       Margin = new Thickness(1100, 50, 1200, 400);
                    }
                    else
                    {
                       Margin = new Thickness(1550, 50, 1600, 400);
                    }
                    break;
            }

            OnPropertyChanged("margin");

        }






    }
}
