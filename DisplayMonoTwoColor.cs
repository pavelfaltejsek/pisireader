using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PISI.Net.ConsoleApplication
{
    public class DisplayMonoTwoColor
    {
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
            }
        }
    }
}