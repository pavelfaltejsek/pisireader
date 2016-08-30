using Raspberry.IO.Components.Displays.Ssd1306;
using Raspberry.IO.Components.Displays.Ssd1306.Fonts;
using Raspberry.IO.GeneralPurpose;
using Raspberry.IO.InterIntegratedCircuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GecoSI.Net.ConsoleApplication
{
    public class DisplayMonoTwoColor
    {
        public IFont fontFixed = new Fixed1L();
        public IFont font2L = new Proportional2L();
        public IFont font3L = new Proportional3L();
        public int displayWidth = 128;
        public int displayHeight = 64;
        private Ssd1306Connection ssd1306;
        public bool Simulated = false;
        private Form form;
        private Label labelY;
        private Label labelB;
        private String fYellow;
        private String fBlue;

        public DisplayMonoTwoColor()
        {
            if (IsLinux)
            {
                const byte ssdI2cAddress = 0x3C;

                var sdaPin = ConnectorPin.P1Pin03;
                var sclPin = ConnectorPin.P1Pin05;

                var driver = new I2cDriver(sdaPin.ToProcessor(), sclPin.ToProcessor());
                ssd1306 = new Ssd1306Connection(driver.Connect(ssdI2cAddress), displayWidth, displayHeight);

                // Clear the screen and turn the display on
                ssd1306.Off();
                ssd1306.DeactivateScroll();
                ssd1306.ClearScreen();
                ssd1306.On();
            }
            else
            { //windows
                Simulated = true;
                form = new Form();
                {
                    labelB = new Label();
                    form.Controls.Add(labelB);
                    labelY = new Label();
                    form.Controls.Add(labelY);
                    labelY.Top = 0;
                    labelY.ForeColor = System.Drawing.Color.AliceBlue;

                    labelB.Top = 30;
                    labelB.AutoSize = true;
                    form.Text = "About Us";
                    // form.Controls.Add(...);
                    form.Show();
                }
            }
            Yellow("Loading");
        }

        public void Idle()
        {
            if (Simulated)
            {
                ///show data
                form.Text = fYellow + "//" + fBlue;
                labelB.Text = fBlue;
                labelY.Text = fYellow;
                Application.DoEvents();
            }
        }

        public void Yellow(string caption)
        {
            if (Simulated)
            {
                fYellow = caption;
            }
            else
            {
                ssd1306.GotoXY(0, 0);
                ssd1306.DrawText(font2L, caption.PadRight(displayWidth / 8));
            }
        }

        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public void Blue1(string caption)
        {
            if (Simulated)
            {
                fBlue = caption;
            }
            else
            {
                ssd1306.GotoXY(0, 2);
                ssd1306.DrawText(fontFixed, caption.PadRight(displayWidth / 8));
            }
        }
    }
}