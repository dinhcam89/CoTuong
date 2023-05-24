using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    public class Cannon : Pawns
    {

        int new_index, old_index;        
        System.Drawing.Point newpoint, oldpoint;
        bool[] Flag = new bool[90];

        public Cannon(bool[] flag, System.Drawing.Point newp, System.Drawing.Point oldp, int newindex, int oldindex, ChessColor color)
            :base(color)
        {            
            this.newpoint = newp;
            this.oldpoint = oldp;
            this.new_index = newindex;
            this.old_index = oldindex;
            this.Flag = flag;
        }

        //Kiểm tra ăn, quân pháo dc phép ăn quân khác khi ở giữa nó có 1 con
        public override bool CheckEated(ChessColor nColor)
        {
            if (CheckValid())
                if ((nColor != color) && nColor != ChessColor.Default)
                    return true;
            return false;
        }

        private bool CheckValid()
        {
            //Nếu Pháo đi theo hướng Y(Dọc thì tọa độ X = nhau)
            if (newpoint.X == oldpoint.X)
            {
                if (new_index > old_index)
                {
                    int index = old_index + 9;
                    int count = 0;
                    while (index < new_index)
                    {
                        
                        if (Flag[index] == true)
                            count++;
                        index += 9;
                    }
                    if (count == 1)
                        return true;
                }
                else
                {
                    int index = old_index - 9;
                    int count = 0;
                    while (index > new_index)
                    {                        
                        if (Flag[index] == true)
                            count++;
                        index -= 9;
                    }
                    if (count == 1)
                        return true;
                }
            }
            //Nếu Pháo đi theo hướng X(Dọc thì tọa độ Y = nhau)
            else
            {
                if (new_index > old_index)
                {
                    int index = old_index+1;
                    int count = 0;
                    while (index < new_index)
                    {                        
                        if (Flag[index] == true)
                            count++;
                        index += 1;
                    }
                    if (count == 1)
                        return true;
                }
                else
                {
                    int index = old_index-1;
                    int count = 0;
                    while (index > new_index)
                    {                        
                        if (Flag[index] == true)
                            count++;
                        index -= 1;
                    }
                    if (count == 1)
                        return true;
                }
            }
            return false;
        }


        public override bool CheckIndex()
        {
            //Nếu Pháo đi theo hướng Y(Dọc thì tọa độ X = nhau)
            if (newpoint.X == oldpoint.X)
            {
                if (new_index > old_index)
                {
                    int index = old_index;
                    while (index < new_index)
                    {
                        index += 9;
                        if (Flag[index] == true)
                            return false;
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
            //Nếu Pháo đi theo hướng X(Dọc thì tọa độ Y = nhau)
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
