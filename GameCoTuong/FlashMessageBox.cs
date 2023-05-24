using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameCoTuong
{
    public partial class FlashMessageBox : Form
    {
        Timer t;
        int i = 0;



        public FlashMessageBox()
        {
            InitializeComponent();
            Load += new EventHandler(FlashMessageBox_Load);
            Click += new EventHandler(FlashMessageBox_Click);
            TransparencyKey = this.BackColor;
            TopMost = true;            
        }

        void FlashMessageBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        void t_Tick(object sender, EventArgs e)
        {
            i += 1;
            if (i > 10)
            {
                i = 0;
                t.Stop();
                Close();
            }
        }

        void FlashMessageBox_Load(object sender, EventArgs e)
        {
            t = new Timer();
            t.Interval = 100;
            t.Start();
            t.Tick += new EventHandler(t_Tick);
        }

    }
}
