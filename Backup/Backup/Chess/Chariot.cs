using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Xe
namespace Chess
{
    public class Chariot: Pawns
    {
        
        int new_index, old_index;        
        System.Drawing.Point newpoint, oldpoint;
        bool[] Flag = new bool[90];

       
        public Chariot(bool[] flag, System.Drawing.Point newp, System.Drawing.Point oldp,int newindex, int oldindex, ChessColor color)
            :base(color)
        {            
            this.newpoint = newp;
            this.oldpoint = oldp;
            this.new_index = newindex;
            this.old_index = oldindex;
            this.Flag = flag;
        }
        
        public override bool CheckIndex()
        {
            //Nếu Xe đi theo hướng Y(Dọc thì tọa độ X = nhau)
            if (newpoint.X == oldpoint.X)
            {
                if (new_index > old_index)
                {
                    int index = old_index+9;
                    while (index < new_index)
                    {                        
                        if (Flag[index] == true)
                            return false;
                        index += 9;
                    }
                }
                else
                {
                    int index = old_index-9;
                    while (index > new_index)
                    {                        
                        if (Flag[index] == true)
                            return false;
                        index -= 9;
                    }
                }
            }
            //Nếu Xe đi theo hướng X(Dọc thì tọa độ Y = nhau)
            else
            {
                if (new_index > old_index)
                {
                    int index = old_index+1;
                    while (index < new_index)
                    {                        
                        if (Flag[index] == true)
                            return false;
                        index += 1;
                    }
                }
                else
                {
                    int index = old_index-1;
                    while (index > new_index)
                    {                        
                        if (Flag[index] == true)
                            return false;
                        index -= 1;
                    }
                }
            }
            return true;
        }
        public override bool CheckPoint()
        {
            if (newpoint.X == oldpoint.X || newpoint.Y == oldpoint.Y)
                return true;
            return false;
        }

    }
}
