using GecoSI.Net;
using GecoSI.Net.Internal;
using Raspberry.IO.Components.Displays.Ssd1306;
using Raspberry.IO.Components.Displays.Ssd1306.Fonts;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.InterIntegratedCircuit;
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Threading;

namespace GecoSI.Net.ConsoleApplication
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            //musi behat dokolecka a bude se snazit neco delat
            Console.WriteLine("Starting pi si reader");

            SimpleHTTPServer ws = new SimpleHTTPServer(".", 8085);

            DisplayMonoTwoColor display = new DisplayMonoTwoColor();

            Console.WriteLine("Starting pi si reader2");
            byte[] data = { 0xF7, 0x00 };
            int crc = CrcCalculator.Crc(data);

            display.Yellow("SI");

            //a opakovat dokud je co brat
            while (1 == 1)
            {
                display.Blue1("hledam si");
                for (byte i = 0; i < 20; i++)
                {
                    try
                    {
                        if (IsLinux)
                        {
                            string cport = "/dev/ttyUSB" + i;
                            display.Blue1("Zkousim " + cport);
                            if (File.Exists(cport))
                            {
                                Console.WriteLine("CYCLE start");

                                Console.WriteLine("Create Handler");
                                ConsoleListener CL = new ConsoleListener();
                                CL.adisplay = display;

                                var handler = new SiHandler(CL);
                                display.Blue1("Port: " + i);
                                Console.WriteLine("connecting handler " + cport);
                                handler.Connect(cport);
                                while (handler.IsAlive()) Thread.Sleep(1000);
                                Console.WriteLine("CYCLE continue!");
                            }
                        }
                        else
                        {
                            //windows - cycle ports? ne, musim rict jaky
                            //get ports
                            String portname = "";
                            using (var searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
                            {
                                string[] portnames = SerialPort.GetPortNames();
                                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
                                var tList = (from n in portnames join p in ports on n equals p["DeviceID"].ToString() select n + " - " + p["Caption"]).ToList();

                                foreach (ManagementBaseObject s in ports)
                                {
                                    portname = s["DeviceID"].ToString()
                                        ;
                                    if (s["Caption"].ToString().Contains("SPORTident"))
                                    {
                                        //je to sportident, load
                                        ConsoleListener CL = new ConsoleListener();
                                        CL.adisplay = display;

                                        var handler = new SiHandler(CL);
                                        display.Blue1("Port: " + i);
                                        Console.WriteLine("connecting handler " + portname);
                                        handler.Connect(portname);
                                        while (handler.IsAlive())
                                        {
                                            display.Idle();
                                            Thread.Sleep(50);
                                        }
                                        Console.WriteLine("CYCLE continue!");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        display.Yellow("Chyba");
                        display.Blue1(e.ToString());
                        Thread.Sleep(1000);
                    }
                }//for
                //hledam, sleep
                display.Idle();
                Thread.Sleep(500);
            }//while
            return 0;
        }

        private static void PrintUsage()
        {
            System.Console.WriteLine("Usage: pisireader <serial portname> ");
        }

        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
    }
}