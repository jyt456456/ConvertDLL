using Accessibility;
using Command;
using graphglobal;
using Overrayview;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Linq.Expressions;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static graphglobal.global;

namespace graphDLL
{
    public class GraphVM : MVbase.MVBase , Command.IEventArgsConverter
    {
        
        public ObservableCollection<LineVM> m_canvasitems { get; set; }
        public ObservableCollection<TextboxVM> textoneItems { get; set; }

        public ObservableCollection<Lineobj> m_objitems { get; set; }

       

        public string Str { get => m_str; set => m_str = value; }
        public int Curframe { get => m_curframe; set => m_curframe = value; }
        public bool BRealTime { get => m_bRealTime; set => m_bRealTime = value; }
        public Thickness Margin { get => m_margin; set => m_margin = value; }
        public int Addbit { get => m_Addbit; set => m_Addbit = value; }
        public int Check { get => m_check; set => m_check = value; }


        public xline.Xline m_xline { get; set; }
        public List<Lineobj> Overlaylist { get => m_Overlaylist; set => m_Overlaylist = value; }
        public bool Yscroll { get => m_yscroll; set => m_yscroll = value; }
        public bool RealStop { get => m_RealStop; set => m_RealStop = value; }

        private Thickness m_margin;
        private string m_str;
        private int m_curframe;
        private bool m_bRealTime = false;
        private Thread m_threaltime = null;
        private int m_resultbit = 0; // Enable Check -> 불일치 -> NEW Type
        private int m_Addbit = 0;
        private int m_precnt;
        private int m_check; // cur Check정보 
        private bool m_RealStop = false;
        private List<graphglobal.Lineobj> m_Overlaylist;
        private bool m_yscroll;


        public GraphVM()
        {
            init();
        }

        private void init()
        {
            m_canvasitems = new ObservableCollection<LineVM>();
            textoneItems = new ObservableCollection<TextboxVM>();
            m_objitems = new ObservableCollection<Lineobj>();
            m_Overlaylist = new List<Lineobj>();
            LineVM xline = new LineVM();
            LineVM yline = new LineVM();
            //m_overView = new OverView();
           // m_overlayitems = new ObservableCollection<OverView> { m_overView };

            xline = CreateLine(0, 0, 440, 0, Brushes.Black, 1, "zero");
            yline = CreateLine(0, 0, 0, 440, Brushes.Black, 1, "zero");

            m_canvasitems.Add(yline);
            m_canvasitems.Add(xline);

            Str = "";
            for (int i = 50; i <= 400; i += 50)
            {
                TextboxVM tvm = new TextboxVM();
                int txt = i / 10;
                int pos = i;
                if (pos == 50)
                {
                    pos += 5;
                }

                tvm = DrawText(txt.ToString(), pos + 35, 0, 20, 0);
                xline = CreateLine(0 + i, -5, 0 + i, 10, Brushes.Black, 2.5, "zero");
                m_canvasitems.Add(xline);
                textoneItems.Add(tvm);
                if (pos == 55)
                {
                    pos -= 5;
                }

                tvm = DrawText(txt.ToString(), 15, 10, pos + 40, 0);
                yline = CreateLine(-5, 0 + i, 10, 0 + i, Brushes.Black, 2.5, "zero");
                m_canvasitems.Add(yline);
                textoneItems.Add(tvm);

            }

        }


        private LineVM CreateLine(double x1, double y1, double x2, double y2, Brush brush, double thickness, string _type)
        {
            LineVM li = new LineVM();

            li.Linex1 = x1;
            li.Liney1 = y1;

            li.Linex2 = x2;
            li.Liney2 = y2;

            li.Stroke1 = brush;
            li.Thickness = thickness;
            li.Defect_type = _type;


            return li;
        }

        private Lineobj Createobj(double x1, double y1, double x2, double y2, double thickness, string _type)
        {
            Lineobj obj = new Lineobj();

            //Today Click -> Data output


            obj.Lineposs = new Thickness(x1, y1, x2, y2);
            obj.Defect_type = _type;

            //if()
            DefetType dt = (DefetType)Enum.Parse(typeof(DefetType), _type);
            byte r = 0;
            byte g = 0;
            byte b = 0;
            switch (dt)
            {
                case DefetType.one:
                    //Red
                    r = 255;
                    g = 0;
                    b = 0;
                    break;
                case DefetType.two:
                    //black
                    break;
                case DefetType.three:
                    //yellow
                    r = 255;
                    g = 247;
                    b = 0;
                    break;
                case DefetType.four:
                    //orange
                    r = 255;
                    g = 145;
                    b = 0;
                    break;
                case DefetType.five:
                    //blue
                    r = 0;
                    g = 0;
                    b = 255;
                    break;
                case DefetType.six:
                    //sky
                    r = 0;
                    g = 162;
                    b = 255;
                    break;
                case DefetType.END:
                    break;
                default:
                    break;
            }

            //obj.Linergb = _gb;

            obj.Xmv = new xline.XlineMV(r,g,b);
            
            //obj.Editcommand = new intCommand()

            return obj;


        }

        private TextboxVM DrawText(string text, double _left, double _right, double _top, double _bot)
        {
            TextboxVM tb = new TextboxVM();
            Thickness temp = new Thickness();

            tb.Text = text;
            temp.Left = _left;
            temp.Right = _right;
            temp.Top = _top;
            temp.Bottom = _bot;
            tb.Pos = temp;
            
            return tb;
        }
        public void FrameChange()
        {
            List<Defect_info> inters = new List<Defect_info>();
            postdb.DB pdb = new postdb.DB();
            inters = pdb.getGraph(Curframe, m_str);

            for (int i = 0; i < m_objitems.Count; ++i)
            {
                if (!m_objitems[i].Defect_type.Equals("zero"))
                {
                    m_objitems.Remove(m_objitems[i]);
                    --i;
                }
            }

            OnPropertyChanged("ClearOverlay");
            if (inters.Count == 0)
            {
                MessageBox.Show("자료X");
                return;
            }



            
            List<string> type = new List<string>();
            m_Overlaylist = new List<Lineobj>();
            for (int i = 0; i < inters.Count; ++i)
            {
                double axex = (inters[i].X * 10.0f) + 10;
                double axey = (inters[i].Y * 10.0f) + 10;
                Lineobj dline = new Lineobj();
                dline = Createobj(axex - 5, axey - 5, axex + 5, axey + 5, 1, inters[i].Defect_type);
                m_objitems.Add(dline);
                m_Overlaylist.Add(dline);
                OnPropertyChanged("AddData");
                dline.Index = inters[i].Defect_xindex;
                intCommand icmd = new intCommand(linelcommand);
                dline.Editcommand = icmd;
                dline.LineHeight = 500;
                type.Add(inters[i].Defect_type);
                int bit = 1;
                bit = global.GetTypeToBit(inters[i].Defect_type);
                m_resultbit = m_resultbit | bit;
                m_check = m_resultbit;
            }
            
            m_precnt = inters.Count;

            
            KillThread();
        }


        public void Reset()
        {
            for (int i = 0; i < m_canvasitems.Count; ++i)
            {
                if (!m_canvasitems[i].Defect_type.Equals("zero"))
                {
                    m_canvasitems.Remove(m_canvasitems[i]);
                    --i;
                }
            }
            OnPropertyChanged("reset");
            m_resultbit = 0;
            m_check = 0;
            KillThread();

        }

        public void TypeChange(int _bit, bool _bcheck)
        {

            //m_bRealTime = false;
            // 실시간 off -> 01111
            //     바뀌는    01011
            
            
            RealStop = false;

            int re = m_check ^ _bit;
            m_check -= re;
            int temp = 1;
            int cnt = 0;
            string type = "";
            for(cnt=0; cnt < 6; ++cnt) 
            {

                if((re & temp) == temp)
                {
                    break;
                }
                temp <<= 1;
            }

            switch(cnt)
            {
                case 0:
                    type = "one";
                    break;
                case 1:
                    type = "two";
                    break;
                case 2:
                    type = "three";
                    break;
                case 3:
                    type = "four";
                    break;
                case 4:
                    type = "five";
                    break;
                case 5:
                    type = "six";
                    break;
            }

              



            if (_bcheck)
            {
                // add

                List<Defect_info> inters = new List<Defect_info>();
                postdb.DB pdb = new postdb.DB();

                inters = pdb.CheckDefectType(Str, Curframe, type);
                m_Overlaylist = new List<Lineobj>();
                for (int i = 0; i < inters.Count; i++)
                {

                    double axex = (inters[i].X * 10.0f) + 10;
                    double axey = (inters[i].Y * 10.0f) + 10;
                    Lineobj dline = new Lineobj();
                    dline = Createobj(axex - 5, axey - 5, axex + 5, axey + 5, 1, inters[i].Defect_type);
                    
                    m_objitems.Add(dline);
                    m_Overlaylist.Add(dline);
                    OnPropertyChanged("AddData");
                    //  dline = CreateLine(axex + 5, axey - 5, axex - 5, axey + 5, Brushes.Black, 1, inters[i].Defect_type);
                    //  m_canvasitems.Add(dline);
                }

            }
            else
            {

                //삭제
                for (int j = 0; j < m_objitems.Count; ++j)
                {

                    if (m_objitems[j].Defect_type.Equals(type))
                    {
                        m_objitems.Remove(m_objitems[j]);
                        --j;
                    }
                }


            }

            //->실시간 ON
            RealStop = true;
        }

        public void SetMargin(int Max, int _iCur)
        {

            switch (Max)
            {
                case 1:
                    if (_iCur == 1)
                    {
                        Margin = new Thickness(750.0, 400.0, 1200.0, 1000.0);
                    }

                    break;
                case 2:
                    if (_iCur == 1)
                    {
                        Margin = new Thickness(300.0, 400.0, 800.0, 1000.0);
                    }
                    else
                    {
                        Margin = new Thickness(900.0, 400.0, 1200.0, 1000.0);
                    }

                    break;
                case 3:
                    switch (_iCur)
                    {
                        case 1:
                            Margin = new Thickness(100.0, 400.0, 600.0, 1000.0);
                            break;
                        case 2:
                            Margin = new Thickness(750.0, 400.0, 1000.0, 1000.0);
                            break;
                        default:
                            Margin = new Thickness(1300.0, 400.0, 1700.0, 1000.0);
                            break;
                    }

                    break;
                case 4:
                    switch (_iCur)
                    {
                        case 1:
                            Margin = new Thickness(-20.0, 400.0, 300.0, 1000.0);
                            break;
                        case 2:
                            Margin = new Thickness(480.0, 400.0, 700.0, 1000.0);
                            break;
                        case 3:
                            Margin = new Thickness(950.0, 400.0, 1200.0, 1000.0);
                            break;
                        default:
                            Margin = new Thickness(1400.0, 400.0, 1600.0, 1000.0);
                            break;
                    }

                    break;
            }

            OnPropertyChanged("margin");

        }

        public void SetRealTime(bool _breal)
        {
            if(_breal)
            {
                //Thread start;

                RealTime();
            }
            else
            {
                //Thread kill
                KillThread();

            }

        }

        private void RealTime()
        {
            if(m_threaltime == null)
            {
                m_bRealTime = true;
                RealStop = true;
                m_threaltime = new Thread(RealTimeCheck);
                m_threaltime.Start();

            }

        }


        private void RealTimeCheck()
        {
            postdb.DB db = new postdb.DB();
            //
            //db -> getcnt;
            //1안 처음부터 다시그리기 -> reset -> 재세팅
            //2안 이어서 그리기 V
            List<Defect_info> deinfo = new List<Defect_info>();

            double axex = -100000;
            double axey = 10000000;
            Lineobj dline = new Lineobj();
            Lineobj xline = new Lineobj();
            dline = Createobj(axex - 5, axey - 5, axex + 5, axey + 5, 1, "one");

            xline = Createobj(axex + 5, axey - 5, axex - 5, axey + 5, 1, "one");

            DispatcherService.Invoke((System.Action)(() =>
            {
                m_objitems.Add(dline);
                m_objitems.Add(xline);
                m_Overlaylist.Add(dline);
                OnPropertyChanged("AddData");

                // your logic
            }));
            /*
            while (m_bRealTime)
            {
                

               //다른곳 grpah 작업중일때 일시정지
                if(m_RealStop)
                {
                    //카운터 체크
                    int nextcnt = db.GetCnt(m_str, m_curframe);
                    if (m_precnt != nextcnt)
                    {
                        deinfo = db.GetRealtime(m_precnt, m_str, m_curframe);
                        int tempbit = m_resultbit;
                        //graph 추가
                        for (int i = 0; i < deinfo.Count; i++)
                        {

                            int sub = global.GetTypeToBit(deinfo[i].Defect_type);
                            ///1. sub & m_check
                            ///
                            tempbit = tempbit | sub;
                            if (tempbit != m_resultbit)
                            {
                                //checkbox 호출
                                Addbit = tempbit - m_resultbit;
                                OnPropertyChanged("AddType");
                                m_resultbit = tempbit;
                                m_check += sub;
                            }
                            else if ((sub & m_check) != sub)
                            {
                                //NEW X 체크해제
                                continue;
                            }

                            double axex = (deinfo[i].X * 10.0f) + 10;
                            double axey = (deinfo[i].Y * 10.0f) + 10;
                            Lineobj dline = new Lineobj();
                            Lineobj xline = new Lineobj();
                            dline = Createobj(axex - 5, axey - 5, axex + 5, axey + 5, Brushes.Black, 1, deinfo[i].Defect_type);

                            xline = Createobj(axex + 5, axey - 5, axex - 5, axey + 5, Brushes.Black, 1, deinfo[i].Defect_type);

                            DispatcherService.Invoke((System.Action)(() =>
                            {
                                m_objitems.Add(dline);
                                m_objitems.Add(xline);

                                // your logic
                            }));


                        }
                        // New Type
                        m_precnt = nextcnt;

                    }



                    Thread.Sleep(500);
                }
                
            
            }*/

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


        public void resetOverlay()
        {
            m_Overlaylist.Clear();
        }

        public void linelcommand(uint _index)
        {
            uint temp = _index;
            temp += 1;
            //
        }

        public void KillThread()
        {
            if (m_threaltime != null)
            {
                //Thread temp = new Thread(RealtiemKil);
                //temp.Start();
                m_bRealTime = false;
                m_threaltime.Join();
                m_threaltime = null;
                RealStop = false;
                //-> 함수화
            }

        }

        //ScrollChangedEventArgs
        public object Convert(object value, object parameter)
        {
            var args = (ScrollChangedEventArgs)value;
            

            return args;
        }




    }
}
