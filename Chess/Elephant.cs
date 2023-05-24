using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Tượng
namespace Chess
{
    public class Elephant : Pawns
    {
        #region Vị trí mà Tượng được phép đi
        int[] Black = new int[] { 2, 6, 18, 22, 26, 38, 42 };
        int[] White = new int[] { 47, 51, 63, 67, 71, 83, 87 };
        #endregion
        int new_index, old_index;
        
        bool[] Flag = new bool[90];

        public Elephant(bool []flag,int newindex, int oldindex, ChessColor color)
            :base(color)
        {
            this.new_index = newindex;
            this.old_index = oldindex;
            this.Flag = flag;
        }

        public override bool CheckIndex()
        {
            if (!CheckValid())
            {
                switch (color)
                {
                    case ChessColor.Black:
                        {
                            if (Black.Contains(new_index))
                                if (Math.Abs(new_index - old_index) == 20 || Math.Abs(new_index + old_index) == 20 || Math.Abs(new_index - old_index) == 16)
                                    return true;
                        }
                        break;
                    case ChessColor.White:
                        {
                            if (White.Contains(new_index))
                                if (Math.Abs(new_index - old_index) == 20 || Math.Abs(new_index + old_index) == 20 || Math.Abs(new_index - old_index) == 16)
                                    return true;
                        }
                        break;
                }
            }
            return false;
        }
        public override bool CheckPoint()
        {
            return false;
        }
        private bool CheckValid()
        {
            return Flag[(new_index + old_index)/2];
        }
       
    }
}
