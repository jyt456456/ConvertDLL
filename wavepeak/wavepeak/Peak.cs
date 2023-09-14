using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

using static wavepeak.global;

namespace wavepeak
{


    public class Peak
    {

        private List<PeakList> m_PeakList;
        private List<double> m_AllSpect;
        private List<double> m_AllLength;
        private int m_PrevEndPeak = 0;
        public List<PeakList> ReadCSV(string Path)
        {

            StreamReader sr = new StreamReader(Path);

            // 스트림의 끝까지 읽기
            bool Wavelen = false;
            bool WaveSpect = false;

            m_AllLength = new List<double>();
            m_AllSpect =new List<double>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] data = line.Split(',');
                string temp = "";

                    //temp = data[0].Substring(0, 14);
                    if (data[0].Equals("Thick WaveLength"))
                    {
                        Wavelen = true;
                        continue;
                    }

                    //temp = data[0].Substring(0, 12);
                    if (data[0].Equals("Thick Spectrum"))
                    {
                        Wavelen = false;
                        WaveSpect = true;
                        continue;
                    }

                if(WaveSpect)
                {
                    for (int i = 0; i < data.Length; ++i)
                    {
                        if (data[i].Equals("") == false)
                        {
                            m_AllSpect.Add(Convert.ToDouble(data[i]));
                        }
                        
                    }
                    WaveSpect = false;

                }
                else if(Wavelen)
                {
                   for(int i=0; i< data.Length; ++i)
                    {
                        if (data[i].Equals("") == false)
                        {
                            m_AllLength.Add(Convert.ToDouble(data[i]));
                        }
                        
                    }

                    Wavelen = false;
                }
            }

            SetPeak(m_AllLength, m_AllSpect);
            return m_PeakList;
        }



        public List<PeakList> SetPeak(List<double> Length, List<double> Wave)
        {
            global.PeakList peak = new global.PeakList();
            m_PeakList = new List<global.PeakList>();
            bool bmax = false;
            // bool bend = false;
            //처음 끝 예외처리
            double prevSpec;
            double curSpect;

            prevSpec = Wave[0];
            // peak.StartPeak = prevSpec;
            //peak.Startindex = 0;
            peak.Relativeindex = 1;

            
            for (int i = 1; i < Wave.Count; i++)
            {
                curSpect = Wave[i];

                if (bmax == false)
                {
                    if (curSpect < prevSpec)
                    {
                        peak.MaxPeak = prevSpec;
                        peak.Maxpeakindex = i;
                        peak.MaxLength = Length[i];
                        bmax = true;
                    }
                }
                else //bst = true, bmax = true
                {
                    if (curSpect > prevSpec)
                    {

                        
                        peak.EndPeak = prevSpec;
                        peak.Endpeakindex = i - 1;
                        peak.EndLenth = Length[i - 1];
                        peak =  getStartPeak(peak);
                        peak =  getEndPeak(peak);
                        peak.Centergrive = (peak.StartPeak + peak.MaxPeak + peak.EndPeak) / 3;
                        m_PeakList.Add(peak);
                        m_PrevEndPeak = i-1;
                        bmax = false;

                    }
                }


                prevSpec = curSpect;

            }

            //끝 예외처리
            int end = Wave.Count - 1;
            if (bmax == false)
            {
                //끝이 MAX
                peak.MaxPeak = Wave[end];
                peak.EndPeak = peak.MaxPeak;
                peak.EndLenth = Length[end];
                peak = getStartPeak(peak);
                peak = getEndPeak(peak);
                peak.Centergrive = (peak.StartPeak + peak.MaxPeak + peak.EndPeak) / 3;
                peak.Relativeindex = m_PeakList.Count - 1;
                m_PeakList.Add(peak);
            }
            else
            {
                //끝이 end
                peak.EndPeak = Wave[end];
                peak.EndLenth = Length[end];
                peak.Relativeindex = m_PeakList.Count - 1;
                peak = getStartPeak(peak);
                peak = getEndPeak(peak);
                peak.Centergrive = (peak.StartPeak + peak.MaxPeak + peak.EndPeak) / 3;
                m_PeakList.Add(peak);
            }


            //X축 대입   
                
//                m_PeakList[i] = peak;


            //Relative 적용
            peak = m_PeakList[0];
            peak.RelativePeak = m_PeakList[1].MaxPeak;
            m_PeakList[0] = peak;

            peak = m_PeakList[m_PeakList.Count - 1];
            peak.RelativePeak = m_PeakList[m_PeakList.Count - 2].MaxPeak;
            m_PeakList[m_PeakList.Count - 1] = peak;

            for (int i = 1; i < m_PeakList.Count - 1; ++i)
            {


                peak = m_PeakList[i];
                double next = m_PeakList[i + 1].MaxLength - m_PeakList[i].MaxLength;

                double prev = m_PeakList[i].MaxLength - m_PeakList[i - 1].MaxLength;

                if (next < prev)
                {

                    peak.Relativeindex = i + 1;
                    peak.RelativePeak = peak.MaxPeak - m_PeakList[i + 1].MaxPeak;
                    
                }
                else
                {
                    peak.Relativeindex = i - 1;
                    peak.RelativePeak = peak.MaxPeak - m_PeakList[i - 1].MaxPeak;
                    
                }

                
                m_PeakList[i] = peak;
            }

            // 결과
            
            for (int i = 0; i < m_PeakList.Count; ++i)
            {
                Console.WriteLine(i + "번째");
                Console.WriteLine("StartLen " + m_PeakList[i].StartLenth);
                Console.WriteLine("StartPeak " + m_PeakList[i].StartPeak);
                Console.WriteLine("MaxLen " + m_PeakList[i].MaxLength);
                Console.WriteLine("MaxPeak " + m_PeakList[i].MaxPeak);
                Console.WriteLine("EndLen " + m_PeakList[i].EndLenth);
                Console.WriteLine("EndPeak " + m_PeakList[i].EndPeak);
                Console.WriteLine("무게중심 " + m_PeakList[i].Centergrive);
                Console.WriteLine("RelativeIndex " + m_PeakList[i].Relativeindex);
                Console.WriteLine("RelatPeak " + m_PeakList[i].RelativePeak);

            }
            return m_PeakList;
        }

        private PeakList getStartPeak(PeakList _pl)
        {
            double min = 100f;
            double tmp = 0f;
            for (int j = _pl.Maxpeakindex - 1; j > m_PrevEndPeak - 1; --j)
            {
                tmp = m_AllSpect[j] - _pl.EndPeak;

                if (Math.Abs(min) > Math.Abs(tmp))
                {
                    min = tmp;
                    _pl.Startindex = j;
                    _pl.StartPeak = m_AllSpect[j];
                    _pl.StartLenth = m_AllLength[j];
                }
            }
            return _pl;
        }

        private PeakList getEndPeak(PeakList _pl)
        {
            double min = 100f;
            double tmp = 0f;
            int endindex = _pl.Endpeakindex;
            for (int j = _pl.Maxpeakindex+1; j <= endindex; ++j)
            {
                tmp = m_AllSpect[j] - _pl.StartPeak;

                if (Math.Abs(min) > Math.Abs(tmp))
                {
                    min = tmp;
                    _pl.Endpeakindex = j;
                    _pl.EndPeak = m_AllSpect[j];
                    _pl.EndLenth = m_AllLength[j];
                }
            }
            return _pl;
        }

        public List<double> getAllwave()
        {
            return m_AllSpect;
        }

        public List<double> getAllLen()
        {
            
            return m_AllLength;
        }


    }
}
