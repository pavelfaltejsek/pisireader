//
//  Copyright (c) 2013-2014 Simon Denier & Yannis Guedel
//
using System;

namespace PISI.Net.Dataframe
{
    public interface ISiDataFrame
    {
        ISiDataFrame StartingAt(long zerohour);

        int NbPunches { get; }

        string SiNumber { get; }

        string SiSeries { get; }

        long StartTime { get; }

        long FinishTime { get; }

        long CheckTime { get; }

        long ReadOutTime { get; }

        SiPunch[] Punches { get; }

        string RunnerName { get; set; }
        string CourseName { get; set; }

        void PrintString();

        string GetString();

        string GetCSVReadoutString();
    }
}