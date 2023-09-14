using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wavepeak
{
    public static class global
    {
        public struct PeakList
        {
           private double maxPeak;
           private double maxLength;
           private double startPeak;
           private double startLenth;
           private double endPeak;
           private double endLenth;
           private double weigth; // 무게중심
           private int startindex;
           private int stpeaklen;
           private int maxpeakindex;
           private int endpeakindex;
           private int relativeindex;
           private double relativePeak;
           private double centergrive;
           
           


            public int Relativeindex { get => relativeindex; set => relativeindex = value; }
            public double RelativePeak { get => relativePeak; set => relativePeak = value; }
            public int Stpeaklen { get => stpeaklen; set => stpeaklen = value; }
            public int Startindex { get => startindex; set => startindex = value; }
            public int Maxpeakindex { get => maxpeakindex; set => maxpeakindex = value; }
            public int Endpeakindex { get => endpeakindex; set => endpeakindex = value; }
            public double MaxPeak { get => maxPeak; set => maxPeak = value; }
            public double MaxLength { get => maxLength; set => maxLength = value; }
            public double StartPeak { get => startPeak; set => startPeak = value; }
            public double StartLenth { get => startLenth; set => startLenth = value; }
            public double EndPeak { get => endPeak; set => endPeak = value; }
            public double EndLenth { get => endLenth; set => endLenth = value; }
            public double Weigth { get => weigth; set => weigth = value; }
            public double Centergrive { get => centergrive; set => centergrive = value; }

        }
    }
}
