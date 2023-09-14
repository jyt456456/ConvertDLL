using System.Collections.ObjectModel;
using graphDLL;
using UCSearch;
using FrameSel;
using UCtypecheck;
using graphglobal;
using static graphglobal.global;
using Accessibility;
using graphInterface;

namespace mainview
{
    class MainViewModel : MVbase.MVbse
    {
        //private Model.MainModel model = null;
        
        public ObservableCollection<IUCSearch> m_ucsarch { get; set; }
        public ObservableCollection<UCFrameSel> m_listUCFrame { get; set; }
        
        public ObservableCollection<UCGraph> m_listUCGraph { get; set; }
        
        public ObservableCollection<UCtypecheckbox> m_listcheck { get; set; }
        private int m_curChart = 1;

        

        public MainViewModel()
        {

            m_ucsarch = new ObservableCollection<IUCSearch>();
            m_listUCGraph = new ObservableCollection<UCGraph>();
            m_listUCFrame = new ObservableCollection<UCFrameSel>();
            m_listcheck = new ObservableCollection<UCtypecheckbox>();
            IUCSearch search = new IUCSearch();

            
            m_ucsarch.Add(search);
            UCFrameSel uCFrame = new UCFrameSel();
            uCFrame.SetMargin(1, 1);
            m_listUCFrame.Add(uCFrame);
            search.interactor.Add(uCFrame);
            UCGraph uCGraph = new UCGraph();
            uCGraph.SetMargin(1, 1);
            m_listUCGraph.Add(uCGraph);
            uCFrame.grpahinteractor = uCGraph;
            
            UCtypecheckbox uCtypecheckbox = new UCtypecheckbox();
            uCtypecheckbox.interactor = uCGraph;
            uCFrame.checkinteractor = uCtypecheckbox;
            uCtypecheckbox.SetMargin(1, 1);
            //uCtypecheckbox.Set
            // uCFrame.checkinteractor = uCtypecheckbox;
            m_listcheck.Add(uCtypecheckbox);
        }

        public void CountChangeEvent(int _icount)
        {
            int a = _icount;
            if (m_curChart < _icount)
            {
                //add
                for (int i = m_curChart; i < _icount; ++i)
                {
                    UCFrameSel tempframe = new UCFrameSel();
                    m_ucsarch[0].interactor.Add(tempframe);

                    UCGraph tempgraph = new UCGraph();
                    tempframe.grpahinteractor = tempgraph;
                    
                    UCtypecheckbox tempcheck = new UCtypecheckbox();
                    tempcheck.interactor = tempgraph;
                    tempframe.checkinteractor = tempcheck;
                    m_listUCGraph.Add(tempgraph);
                    m_listUCFrame.Add(tempframe);
                    m_listcheck.Add(tempcheck);
                }
            }
            
            else if(m_curChart > _icount)
            { 
                //delete
                for(int i=m_curChart-1; i> _icount-1; --i)
                {
                    m_ucsarch[0].interactor.RemoveAt(i);
                    m_listUCFrame.RemoveAt(i);
                   m_listUCGraph.RemoveAt(i);
                   m_listcheck.RemoveAt(i);
                }
            }


            for (int i = 0; i < _icount; ++i)
            {
                m_listUCFrame[i].SetMargin(_icount, i+1);
                m_listUCGraph[i].SetMargin(_icount, i + 1);
                m_listcheck[i].SetMargin(_icount, i+1);
            }

            m_curChart = _icount;

            








        }

    }



}
