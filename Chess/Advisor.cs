using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Sĩ
namespace Chess
{
    public class Advisor : Pawns
    {
        #region Vị trí mà Sĩ được phép đi (theo Index)
        int[] Black = new int[] { 3, 5, 13, 21, 23 };
        int[] White = new int[] { 66, 68, 76, 84, 86 };
        #endregion

        int new_index, old_index;        

        public Advisor(int newindex, int oldindex, ChessColor color)
            :base(color)
        {
            this.new_index = newindex;
            this.old_index = oldindex;            
        }

        public override bool CheckIndex()
        {
            switch (color)
            {
                case ChessColor.Black:
                    {
                        if (Black.Contains(new_index))
                            if (Math.Abs(new_index - old_index) == 10 || Math.Abs(new_index - old_index) == 8)
                                return true;
                    }
                    break;
                case ChessColor.White:
                    {
                        if (White.Contains(new_index))
                            if (Math.Abs(new_index - old_index) == 10 || Math.Abs(new_index - old_index) == 8)
                                return true;
                    }
                    break;
            }
            return false;
        }

        public override bool CheckPoint()
        {
            return false;
        }        
    }
}
