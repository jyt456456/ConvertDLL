
//using graphmodule.CountGraph;
//using graphmodule.MVModel;
//using graphmodule.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UCSearch
{
    internal class UCSearchViewM : MVbase.MVbse
    {

        private string m_strLot;
        private int m_curcount = 0;

        public Command.Command ButtonCommand { get; set; }

        public string StrLot { get => m_strLot; set => m_strLot = value; }
        public int Curcount { get => m_curcount; set => m_curcount = value; }

        public UCSearchViewM()
        {

            ButtonCommand = new Command.Command(BtnSearch);

        }

        public void testfun()
        {
            int adw = 0;
        }


        private void BtnSearch()
        {

            if (m_strLot == null)
                return;


            postdb.DB pdb = new postdb.DB();
            List<int> framelist = new List<int>();
            m_strLot = m_strLot.Replace(" ", string.Empty);
            if (m_strLot.Equals(""))
            {
                MessageBox.Show("LOT번호 입력 X");
                return;
            }


            framelist = pdb.SeachLot(m_strLot);
            
            if(framelist == null)
            {
                MessageBox.Show("서버 연결 실패");
                return;
            }

            if (framelist.Count == 0)
            {
                MessageBox.Show("일치하는 LOT번호가 없습니다.");
                return;
            }

            OnPropertyChanged("SearchLOT");
        }

    }
}
