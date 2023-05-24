using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Main;

namespace GameCoTuong
{
    public partial class Load : DevExpress.XtraEditors.XtraForm
    {
        public Load()
        {
            InitializeComponent();
            string url_danhsach = Form_Main.url + "/webgame/danhsach.php#cotuong";
            linkLabel1.Links.Add(0, 20, url_danhsach);
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (txt_trandau.Text != "")
            {
                Close();
            }
        }

        private void txt_trandau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void Load_Load(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_trandau.Text = "";
            this.Close();
        }
    }
}