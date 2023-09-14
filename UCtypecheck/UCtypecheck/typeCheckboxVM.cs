///using graphmodule.MVModel;
using GalaSoft.MvvmLight.Command;
using graphglobal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using static graphglobal.global;


namespace graphmodule.typeCheckbox
{
    class typeCheckboxVM : MVbase.MVbse
    {

        
//        public RelayCommand
        

        public ObservableCollection<Checkbool> m_listcheckbox { get; set; } // 

        public ICommand EditCommand { get { return new RelayCommand<string>(CheckClick); } }

        public List<string> m_liststr { get; set; }
        public Thickness Margin { get => m_margin; set => m_margin = value; }

        private Thickness m_margin;
        private string m_curType;
        private bool m_curCheck; //체크여부
        

        private int m_bitCurCheck = 0; // 종합정보
        public string CurType { get => m_curType; set => m_curType = value; }
        public bool CurCheck { get => m_curCheck; set => m_curCheck = value; }
        public int BitCurCheck { get => m_bitCurCheck; set => m_bitCurCheck = value; }


        //private string m_changeType;



        public typeCheckboxVM() 
        {
            m_listcheckbox = new ObservableCollection<Checkbool>();
            Checkbool cb = new Checkbool();
            cb.Enable = false;
            cb.Check = false;
            cb.Defect_type = "one";
            m_listcheckbox.Add(cb);
            cb.Defect_type = "two";

            m_listcheckbox.Add(cb);
            cb.Defect_type = "three";

            m_listcheckbox.Add(cb);
            cb.Defect_type = "four";

            m_listcheckbox.Add(cb);
            cb.Defect_type = "five";

            m_listcheckbox.Add(cb);
            cb.Defect_type = "six";

            m_listcheckbox.Add(cb);
            //
            
            m_liststr = new List<string>();

            for (DefetType j = 0; j < DefetType.END; ++j)
            {
                m_liststr.Add(j.ToString());
            }
            ///
        }

        public void CheckClick(object _str)
        {

            string test = _str.ToString();
            m_curType = test.Substring(0, test.Length);

            for (int i = 0; i < m_listcheckbox.Count; ++i)
            {
                if (m_curType.Equals(m_listcheckbox[i].Defect_type))
                {
                    Checkbool cb = m_listcheckbox[i];
                    int bit = global.GetTypeToBit(m_curType);

                    if (cb.Check)
                    {
                        //지우기
                        //m_bitCurCheck - bit
                        if((m_bitCurCheck & bit) == bit)
                        {
                            m_bitCurCheck -= bit;
                        }
                        cb.Check = false;
                        m_curCheck = false;
                        
                    }

                    else
                    {
                        //추가
                        //m_bitCurCheck + bit
                        if ((m_bitCurCheck & bit) != bit)
                        {
                            m_bitCurCheck += bit;
                        }
                        cb.Check = true;
                        m_curCheck = true;
                        
                    }

                    
                    m_listcheckbox[i] = cb;
                    OnPropertyChanged("CheckClick");
                    break;

                }


            }

        }

        public void ChagneFrame(string _Iot, int _iFrame)
        {
            postdb.DB db = new postdb.DB();
            List<string> temp = new List<string>();
            temp =  db.ChagneFrameCheck(_Iot, _iFrame);
            m_bitCurCheck = 0;

            for (int t = 0; t < m_listcheckbox.Count; ++t)
            {
                Checkbool cb;
                cb = m_listcheckbox[t];
                cb.Check = false;
                cb.Enable = false;
                m_listcheckbox[t] = cb;
            }


            for (int i = 0; i < temp.Count; ++i)
            {
                for (int j = 0; j < m_listcheckbox.Count; ++j)
                {
                    if (m_listcheckbox[j].Defect_type.Equals(temp[i]))
                    {
                        //m_bitCurCheck + bit 안전장치 &연산 false일때만
                        int bit = global.GetTypeToBit(m_listcheckbox[j].Defect_type);
                        if((m_bitCurCheck & bit) != bit)
                        {
                            m_bitCurCheck += bit;
                        }
                        Checkbool cb;
                        cb = m_listcheckbox[j];
                        cb.Check = true;
                        cb.Enable = true;
                        m_listcheckbox[j] = cb;
                        break;
                    }

                }
            }
            OnPropertyChanged("changecheck");

        }




        public void SetMargin(int Max, int _iCur)
        {
            double top = 250;
            double bot = 500;

            switch (Max)
            {
                case 1:
                    if (_iCur == 1)
                    {
                        Margin = new Thickness(800.0, top, 1000.0, bot);
                    }

                    break;
                case 2:
                    if (_iCur == 1)
                    {
                        Margin = new Thickness(350.0, top, 800.0, bot);
                    }
                    else
                    {
                        Margin = new Thickness(950.0, top, 1200.0, bot);
                    }

                    break;
                case 3:
                    switch (_iCur)
                    {
                        case 1:
                            Margin = new Thickness(150.0, top, 600.0, bot);
                            break;
                        case 2:
                            Margin = new Thickness(800.0, top, 1200.0, bot);
                            break;
                        default:
                            Margin = new Thickness(1350.0, top, 1700.0, bot);
                            break;
                    }

                    break;
                case 4:
                    switch (_iCur)
                    {
                        case 1:
                            Margin = new Thickness(30.0, top, 300.0, bot);
                            break;
                        case 2:
                            Margin = new Thickness(520.0, top, 700.0, bot);
                            break;
                        case 3:
                            Margin = new Thickness(1000.0, top, 1200.0, bot);
                            break;
                        default:
                            Margin = new Thickness(1450.0, top, 1600.0, bot);
                            break;
                    }

                    break;
            }

            OnPropertyChanged("margin");

        }



        public void Reset()
        {
            Checkbool cb = new Checkbool();
            cb.Enable = false;
            cb.Check = false;

            for(int i=0; i< m_listcheckbox.Count; ++i)
            {
                cb.defect_type = m_listcheckbox[i].defect_type;
                m_listcheckbox[i] = cb;
            }
            OnPropertyChanged("reset");
        }


        public void AddType(int _typebit)
        {
            int temp = 1;
            //Int32 bit = 8;
            //bool check = 0x01 && ;
            if ((1 & _typebit) == 1)
            {
                //one
               
                DispatcherService.Invoke((System.Action)(() =>
                {
                    Checkbool cb;
                    cb = m_listcheckbox[0];
                    cb.Check = true;
                    cb.Enable = true;
                    m_listcheckbox[0] = cb;

                    // your logic
                }));
            }
            else if ((2 & _typebit) == 2)
            {
                
                DispatcherService.Invoke((System.Action)(() =>
                {
                    Checkbool cb;
                    cb = m_listcheckbox[1];
                    cb.Check = true;
                    cb.Enable = true;
                    m_listcheckbox[1] = cb;
                    // your logic
                }));
            }
            else if ((4 & _typebit) == 4)
            {
               
                DispatcherService.Invoke((System.Action)(() =>
                {
                    Checkbool cb;
                    cb = m_listcheckbox[2];
                    cb.Check = true;
                    cb.Enable = true;
                    m_listcheckbox[2] = cb;

                    // your logic
                }));
            }
            else if ((8 & _typebit) == 8)
            {
                
                DispatcherService.Invoke((System.Action)(() =>
                {
                    Checkbool cb;
                    cb = m_listcheckbox[3];
                    cb.Check = true;
                    cb.Enable = true;
                    m_listcheckbox[3] = cb;

                    // your logic
                }));

            }
            else if ((16 & _typebit) == 16)
            {
               
                DispatcherService.Invoke((System.Action)(() =>
                {
                    Checkbool cb;
                    cb = m_listcheckbox[4];
                    cb.Check = true;
                    cb.Enable = true;
                    m_listcheckbox[4] = cb;

                    // your logic
                }));
            }
            else if ((32 & _typebit) == 32)
            {
                
                DispatcherService.Invoke((System.Action)(() =>
                {
                    Checkbool cb;
                    cb = m_listcheckbox[5];
                    cb.Check = true;
                    cb.Enable = true;
                    m_listcheckbox[5] = cb;

                    // your logic
                }));
            }

            OnPropertyChanged("chang");
        }
        public static class DispatcherService
        {
            public static void Invoke(Action action)
            {
                Dispatcher dispatchObject = Application.Current != null ? Application.Current.Dispatcher : null;
                if (dispatchObject == null || dispatchObject.CheckAccess())
                    action();
                else
                    dispatchObject.Invoke(action);
            }
        }

    }


}
