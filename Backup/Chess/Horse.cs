using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    public class Horse : Pawns
    {
        
        int new_index, old_index;        
        System.Drawing.Point newpoint, oldpoint;
        bool[] Flag = new bool[90];

        public Horse(bool[] flag, System.Drawing.Point newp, System.Drawing.Point oldp, int newindex, int oldindex, ChessColor color)
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
            if (
                (Math.Abs(newpoint.X - oldpoint.X) == 60 && Math.Abs(newpoint.Y - oldpoint.Y) == 120)||
                (Math.Abs(newpoint.Y - oldpoint.Y) == 60 && Math.Abs(newpoint.X - oldpoint.X) == 120)
                )
                return true;
            return false;
        }

        public override bool CheckIndex()
        {
            int y = newpoint.Y - oldpoint.Y;
            switch (y)
            {
                case 120:
                    {
                        if (Flag[old_index + 9] == false)
                            return true;
                    }
                    break;
                case -120:
                    {
                        if (Flag[old_index - 9] == false)
                            return true;
                    }
                    break;
            }

            int x = newpoint.X - oldpoint.X;
            switch (x)
            {
                case 120:
                    {
                        if (Flag[old_index + 1] == false)
                            return true;
                    }
                    break;
                case -120:
                    {
                        if (Flag[old_index - 1] == false)
                            return true;
                    }
                    break;
            }
            return false;
        }       
    }
}
