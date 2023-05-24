using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    public class Soldier : Pawns
    {
        int new_index, old_index;        
        System.Drawing.Point newpoint, oldpoint;
        bool[] Flag = new bool[90];

        public Soldier(bool[] flag, System.Drawing.Point newp, System.Drawing.Point oldp, int newindex, int oldindex, ChessColor color)
            :base(color)
        {           
            this.newpoint = newp;
            this.oldpoint = oldp;
            this.new_index = newindex;
            this.old_index = oldindex;
            this.Flag = flag;
        }
        public override bool CheckPoint()
        {
            switch (color)
            {
                case ChessColor.Black:
                    {
                        if (!isOverSea())//Neu chua qua song
                        {
                            if (newpoint.Y - oldpoint.Y == 60 && this.CheckIndex())
                                return true;
                        }
                        else//New da qua song
                        {
                            if ((newpoint.Y - oldpoint.Y == 60 || Math.Abs(newpoint.X - oldpoint.X) == 60) && this.CheckIndex())
                                return true;
                        }
                    }
                    break;
                case ChessColor.White:
                    {
                        if (!isOverSea())//Neu chua qua song
                        {
                            if (oldpoint.Y - newpoint.Y == 60 && this.CheckIndex())
                                return true;
                        }
                        else//New da qua song
                        {
                            if ((oldpoint.Y - newpoint.Y == 60 || Math.Abs(oldpoint.X - newpoint.X) == 60) && this.CheckIndex())
                                return true;
                        }
                    }
                    break;
            }
            return false;
        }

        public override bool CheckIndex()
        {
            if (!isOverSea())
            {
                switch (color)
                {
                    case ChessColor.Black:
                        {
                            if (new_index - old_index == 9)
                                return true;
                        }
                        break;
                    case ChessColor.White:
                        {
                            if (old_index - new_index == 9)
                                return true;
                        }
                        break;
                }
            }
            else
            {
                if (Math.Abs(new_index - old_index) == 9 || Math.Abs(new_index - old_index) == 1)
                    return true;
            }
            return false;
        }

        //Kiểm tra xem Chốt có qua sông chưa ?
        private bool isOverSea()
        {
            switch (color)
            {
                case ChessColor.Black:
                    {
                        if (oldpoint.Y >= 350)
                            return true;
                    }
                    break;
                case ChessColor.White:
                    {
                        if (oldpoint.Y <= 290)
                            return true;
                    }
                    break;
            }
            return false;
        }
    }
}
