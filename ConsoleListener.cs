﻿using GecoSI.Net.Dataframe;
using IniParser;
using IniParser.Model;
using Raspberry.IO.Components.Displays.Ssd1306;
using Raspberry.IO.Components.Displays.Ssd1306.Fonts;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;

namespace GecoSI.Net.ConsoleApplication
{
    public class Zavod
    {
        public static string GetZavodnik(string cip)
        {
            try
            {
                Dictionary<string, string> data =
            new Dictionary<string, string>();
                foreach (var row in File.ReadAllLines("zavod.txt"))
                    data.Add(row.Split('=')[0], row.Split('=')[1]);
                string zavodnik = "";
                data.TryGetValue(cip, out zavodnik);
                return zavodnik;
            }
            catch { }
            return "";
        }

        public static string GetCourse(ISiDataFrame frame)
        {
            string trasa = "";
            try
            {
                if (frame.StartTime != AbstractDataFrame.NO_TIME) trasa += "S-";
                foreach (SiPunch p in frame.Punches)
                {
                    trasa += p.Code + "-";
                }

                if (frame.FinishTime != AbstractDataFrame.NO_TIME) trasa += "C";
                else
                    if (frame.ReadOutTime != AbstractDataFrame.NO_TIME) trasa += "C";

                Dictionary<string, string> data =
            new Dictionary<string, string>();
                foreach (var row in File.ReadAllLines("trasy.txt"))
                    data.Add(row.Split('=')[0], row.Split('=')[1]);
                string ftr = "";
                if (data.TryGetValue(trasa, out ftr))
                {
                    trasa = ftr;
                }

                return trasa;
            }
            catch
            {
                return trasa;
            }
            return "";
        }
    }

    public class ConsoleListener : ISiListener

    {
        public DisplayMonoTwoColor adisplay;

        public void HandleEcard(ISiDataFrame dataFrame)
        {
            //nejprve zalogovat data - append na sireader.csv
            string path = "./readout.csv";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("No;Read at;SIID;Start no;Clear CN;Clear DOW;Clear time;Check CN;Check DOW;Check time;Start CN;Start DOW;Start time;Start_r CN;Start_r DOW;Start_r time;Finish CN;Finish DOW;Finish time;Finish_r CN;Finish_r DOW;Finish_r time;Class;First name;Last name;Club;Country;Email;Date of birth;Sex;Phone;Street;ZIP;City;Hardware version;Software version;Battery date;Battery voltage;Clear count;Character set;SEL_FEEDBACK;No. of records;Record 1 CN;Record 1 DOW;Record 1 time;Record 2 CN;Record 2 DOW;Record 2 time;Record 3 CN;Record 3 DOW;Record 3 time;Record 4 CN;Record 4 DOW;Record 4 time;Record 5 CN;Record 5 DOW;Record 5 time;Record 6 CN;Record 6 DOW;Record 6 time;Record 7 CN;Record 7 DOW;Record 7 time;Record 8 CN;Record 8 DOW;Record 8 time;Record 9 CN;Record 9 DOW;Record 9 time;Record 10 CN;Record 10 DOW;Record 10 time;Record 11 CN;Record 11 DOW;Record 11 time;Record 12 CN;Record 12 DOW;Record 12 time;Record 13 CN;Record 13 DOW;Record 13 time;Record 14 CN;Record 14 DOW;Record 14 time;Record 15 CN;Record 15 DOW;Record 15 time;Record 16 CN;Record 16 DOW;Record 16 time;Record 17 CN;Record 17 DOW;Record 17 time;Record 18 CN;Record 18 DOW;Record 18 time;Record 19 CN;Record 19 DOW;Record 19 time;Record 20 CN;Record 20 DOW;Record 20 time;Record 21 CN;Record 21 DOW;Record 21 time;Record 22 CN;Record 22 DOW;Record 22 time;Record 23 CN;Record 23 DOW;Record 23 time;Record 24 CN;Record 24 DOW;Record 24 time;Record 25 CN;Record 25 DOW;Record 25 time;Record 26 CN;Record 26 DOW;Record 26 time;Record 27 CN;Record 27 DOW;Record 27 time;Record 28 CN;Record 28 DOW;Record 28 time;Record 29 CN;Record 29 DOW;Record 29 time;Record 30 CN;Record 30 DOW;Record 30 time;Record 31 CN;Record 31 DOW;Record 31 time;Record 32 CN;Record 32 DOW;Record 32 time;Record 33 CN;Record 33 DOW;Record 33 time;Record 34 CN;Record 34 DOW;Record 34 time;Record 35 CN;Record 35 DOW;Record 35 time;Record 36 CN;Record 36 DOW;Record 36 time;Record 37 CN;Record 37 DOW;Record 37 time;Record 38 CN;Record 38 DOW;Record 38 time;Record 39 CN;Record 39 DOW;Record 39 time;Record 40 CN;Record 40 DOW;Record 40 time;Record 41 CN;Record 41 DOW;Record 41 time;Record 42 CN;Record 42 DOW;Record 42 time;Record 43 CN;Record 43 DOW;Record 43 time;Record 44 CN;Record 44 DOW;Record 44 time;Record 45 CN;Record 45 DOW;Record 45 time;Record 46 CN;Record 46 DOW;Record 46 time;Record 47 CN;Record 47 DOW;Record 47 time;Record 48 CN;Record 48 DOW;Record 48 time;Record 49 CN;Record 49 DOW;Record 49 time;Record 50 CN;Record 50 DOW;Record 50 time;Record 51 CN;Record 51 DOW;Record 51 time;Record 52 CN;Record 52 DOW;Record 52 time;Record 53 CN;Record 53 DOW;Record 53 time;Record 54 CN;Record 54 DOW;Record 54 time;Record 55 CN;Record 55 DOW;Record 55 time;Record 56 CN;Record 56 DOW;Record 56 time;Record 57 CN;Record 57 DOW;Record 57 time;Record 58 CN;Record 58 DOW;Record 58 time;Record 59 CN;Record 59 DOW;Record 59 time;Record 60 CN;Record 60 DOW;Record 60 time;Record 61 CN;Record 61 DOW;Record 61 time;Record 62 CN;Record 62 DOW;Record 62 time;Record 63 CN;Record 63 DOW;Record 63 time;Record 64 CN;Record 64 DOW;Record 64 time;Record 65 CN;Record 65 DOW;Record 65 time;Record 66 CN;Record 66 DOW;Record 66 time;Record 67 CN;Record 67 DOW;Record 67 time;Record 68 CN;Record 68 DOW;Record 68 time;Record 69 CN;Record 69 DOW;Record 69 time;Record 70 CN;Record 70 DOW;Record 70 time;Record 71 CN;Record 71 DOW;Record 71 time;Record 72 CN;Record 72 DOW;Record 72 time;Record 73 CN;Record 73 DOW;Record 73 time;Record 74 CN;Record 74 DOW;Record 74 time;Record 75 CN;Record 75 DOW;Record 75 time;Record 76 CN;Record 76 DOW;Record 76 time;Record 77 CN;Record 77 DOW;Record 77 time;Record 78 CN;Record 78 DOW;Record 78 time;Record 79 CN;Record 79 DOW;Record 79 time;Record 80 CN;Record 80 DOW;Record 80 time;Record 81 CN;Record 81 DOW;Record 81 time;Record 82 CN;Record 82 DOW;Record 82 time;Record 83 CN;Record 83 DOW;Record 83 time;Record 84 CN;Record 84 DOW;Record 84 time;Record 85 CN;Record 85 DOW;Record 85 time;Record 86 CN;Record 86 DOW;Record 86 time;Record 87 CN;Record 87 DOW;Record 87 time;Record 88 CN;Record 88 DOW;Record 88 time;Record 89 CN;Record 89 DOW;Record 89 time;Record 90 CN;Record 90 DOW;Record 90 time;Record 91 CN;Record 91 DOW;Record 91 time;Record 92 CN;Record 92 DOW;Record 92 time;Record 93 CN;Record 93 DOW;Record 93 time;Record 94 CN;Record 94 DOW;Record 94 time;Record 95 CN;Record 95 DOW;Record 95 time;Record 96 CN;Record 96 DOW;Record 96 time;Record 97 CN;Record 97 DOW;Record 97 time;Record 98 CN;Record 98 DOW;Record 98 time;Record 99 CN;Record 99 DOW;Record 99 time;Record 100 CN;Record 100 DOW;Record 100 time;Record 101 CN;Record 101 DOW;Record 101 time;Record 102 CN;Record 102 DOW;Record 102 time;Record 103 CN;Record 103 DOW;Record 103 time;Record 104 CN;Record 104 DOW;Record 104 time;Record 105 CN;Record 105 DOW;Record 105 time;Record 106 CN;Record 106 DOW;Record 106 time;Record 107 CN;Record 107 DOW;Record 107 time;Record 108 CN;Record 108 DOW;Record 108 time;Record 109 CN;Record 109 DOW;Record 109 time;Record 110 CN;Record 110 DOW;Record 110 time;Record 111 CN;Record 111 DOW;Record 111 time;Record 112 CN;Record 112 DOW;Record 112 time;Record 113 CN;Record 113 DOW;Record 113 time;Record 114 CN;Record 114 DOW;Record 114 time;Record 115 CN;Record 115 DOW;Record 115 time;Record 116 CN;Record 116 DOW;Record 116 time;Record 117 CN;Record 117 DOW;Record 117 time;Record 118 CN;Record 118 DOW;Record 118 time;Record 119 CN;Record 119 DOW;Record 119 time;Record 120 CN;Record 120 DOW;Record 120 time;Record 121 CN;Record 121 DOW;Record 121 time;Record 122 CN;Record 122 DOW;Record 122 time;Record 123 CN;Record 123 DOW;Record 123 time;Record 124 CN;Record 124 DOW;Record 124 time;Record 125 CN;Record 125 DOW;Record 125 time;Record 126 CN;Record 126 DOW;Record 126 time;Record 127 CN;Record 127 DOW;Record 127 time;Record 128 CN;Record 128 DOW;Record 128 time;Record 129 CN;Record 129 DOW;Record 129 time;Record 130 CN;Record 130 DOW;Record 130 time;Record 131 CN;Record 131 DOW;Record 131 time;Record 132 CN;Record 132 DOW;Record 132 time;Record 133 CN;Record 133 DOW;Record 133 time;Record 134 CN;Record 134 DOW;Record 134 time;Record 135 CN;Record 135 DOW;Record 135 time;Record 136 CN;Record 136 DOW;Record 136 time;Record 137 CN;Record 137 DOW;Record 137 time;Record 138 CN;Record 138 DOW;Record 138 time;Record 139 CN;Record 139 DOW;Record 139 time;Record 140 CN;Record 140 DOW;Record 140 time;Record 141 CN;Record 141 DOW;Record 141 time;Record 142 CN;Record 142 DOW;Record 142 time;Record 143 CN;Record 143 DOW;Record 143 time;Record 144 CN;Record 144 DOW;Record 144 time;Record 145 CN;Record 145 DOW;Record 145 time;Record 146 CN;Record 146 DOW;Record 146 time;Record 147 CN;Record 147 DOW;Record 147 time;Record 148 CN;Record 148 DOW;Record 148 time;Record 149 CN;Record 149 DOW;Record 149 time;Record 150 CN;Record 150 DOW;Record 150 time;Record 151 CN;Record 151 DOW;Record 151 time;Record 152 CN;Record 152 DOW;Record 152 time;Record 153 CN;Record 153 DOW;Record 153 time;Record 154 CN;Record 154 DOW;Record 154 time;Record 155 CN;Record 155 DOW;Record 155 time;Record 156 CN;Record 156 DOW;Record 156 time;Record 157 CN;Record 157 DOW;Record 157 time;Record 158 CN;Record 158 DOW;Record 158 time;Record 159 CN;Record 159 DOW;Record 159 time;Record 160 CN;Record 160 DOW;Record 160 time;Record 161 CN;Record 161 DOW;Record 161 time;Record 162 CN;Record 162 DOW;Record 162 time;Record 163 CN;Record 163 DOW;Record 163 time;Record 164 CN;Record 164 DOW;Record 164 time;Record 165 CN;Record 165 DOW;Record 165 time;Record 166 CN;Record 166 DOW;Record 166 time;Record 167 CN;Record 167 DOW;Record 167 time;Record 168 CN;Record 168 DOW;Record 168 time;Record 169 CN;Record 169 DOW;Record 169 time;Record 170 CN;Record 170 DOW;Record 170 time;Record 171 CN;Record 171 DOW;Record 171 time;Record 172 CN;Record 172 DOW;Record 172 time;Record 173 CN;Record 173 DOW;Record 173 time;Record 174 CN;Record 174 DOW;Record 174 time;Record 175 CN;Record 175 DOW;Record 175 time;Record 176 CN;Record 176 DOW;Record 176 time;Record 177 CN;Record 177 DOW;Record 177 time;Record 178 CN;Record 178 DOW;Record 178 time;Record 179 CN;Record 179 DOW;Record 179 time;Record 180 CN;Record 180 DOW;Record 180 time;Record 181 CN;Record 181 DOW;Record 181 time;Record 182 CN;Record 182 DOW;Record 182 time;Record 183 CN;Record 183 DOW;Record 183 time;Record 184 CN;Record 184 DOW;Record 184 time;Record 185 CN;Record 185 DOW;Record 185 time;Record 186 CN;Record 186 DOW;Record 186 time;Record 187 CN;Record 187 DOW;Record 187 time;Record 188 CN;Record 188 DOW;Record 188 time;Record 189 CN;Record 189 DOW;Record 189 time;Record 190 CN;Record 190 DOW;Record 190 time;Record 191 CN;Record 191 DOW;Record 191 time;Record 192 CN;Record 192 DOW;Record 192 time;");
                }
            }
            string dataline = dataFrame.GetCSVReadoutString();
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(dataline);
            }

            //nejprve zalogovat data - append na sireader.csv
            if (IsLinux)
            {
                path = "/var/www/readout.csv";
                //na linuxu jo, ale na win je to u mne

                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("No;atd...");
                    }
                }

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(dataline);
                }
            }

            //a display
            //fill data o clovekovi a trati

            dataFrame.RunnerName = Zavod.GetZavodnik(dataFrame.SiNumber);
            dataFrame.CourseName = Zavod.GetCourse(dataFrame);

            string data = dataFrame.GetString();
            Console.WriteLine(data);
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff"));
            long timesec = (dataFrame.FinishTime - dataFrame.StartTime) / 1000;//msec
            int minuty = (int)timesec / 60;
            int sec = (int)timesec % 60;

            string cas = String.Format("{2}: {0}:{1}", minuty, sec.ToString().PadLeft(2, '0'), dataFrame.SiNumber);
            adisplay.Yellow(cas);
            adisplay.Blue1(cas);

            //a tisk
            try
            {
                System.IO.Ports.SerialPort tisk = new SerialPort("/dev/ttyAMA0", 9600);
                tisk.Open();
                tisk.WriteLine(data);
                tisk.Close();
            }
            catch { }
        }

        public void Notify(CommStatus status)
        {
            Console.WriteLine("Status" + DateTime.Now.ToString("hh:mm:ss.fff") + " -> " + status);
            adisplay.Yellow(status.ToString());
        }

        public void Notify(CommStatus errorStatus, String errorMessage)
        {
            Console.WriteLine("Error -> " + errorStatus + " " + errorMessage);
            adisplay.Yellow("E:" + errorStatus);
            adisplay.Blue1(errorMessage);
        }

        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public bool OnEcardDown(string siNumber)
        {
            Console.WriteLine(siNumber);
            adisplay.Yellow(siNumber + " wait ...");

            //return false; nic,konec
            //zjistit punch
            return true;
        }
    }
}