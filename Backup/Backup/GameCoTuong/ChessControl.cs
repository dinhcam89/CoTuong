using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Chess;
using System.Text;
using System.Windows.Forms;

namespace GameCoTuong
{
    public partial class ChessControl : PictureBox
    {
        ChessType type = ChessType.Default;
        ChessColor color = ChessColor.Default;
        int index = -1;

        public ChessType Type
        {
            get { return type; }
            set { type = value; }
        }
        public ChessColor Color
        {
            get { return color; }
            set { color = value; }
        }
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public ChessControl()
        {
            InitializeComponent();

            this.Size = new Size(40, 40);
            MouseEnter += new EventHandler(ChessControl_MouseEnter);
            
        }

        void ChessControl_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
       
       
    }
}
