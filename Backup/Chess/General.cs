using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
//Tướng

namespace Chess
{
    public class General : Pawns
    {
        #region Vi trí mà Tướng được phép đi (theo POINT)
        Point[] General_B = new Point[]
        {
            new Point(230,50),
            new Point(290,50),
            new Point(350,50),//
            new Point(230,110),
            new Point(290,110),
            new Point(350,110),
            new Point(230,170),//
            new Point(290,170),
            new Point(350,170)
        };
        Point[] General_W = new Point[]
        {
            new Point(230,470),
            new Point(290,470),
            new Point(350,470),
            new Point(230,530),
            new Point(290,530),
            new Point(350,530),
            new Point(230,590),
            new Point(290,590),
            new Point(350,590)
        };
        #endregion
        #region Vị trí mà Tướng được phép đi (theo Index)
        int[] Black = new int[]
        {
            3,4,5,12,13,14,21,22,23
        };
        int[] White = new int[]
        {
            66,67,68,75,76,77,84,85,86
        };
        #endregion
        Point new_p;
        Point old_p;
        int new_index,old_index;        
        public General(Point newp, Point oldp,int newindex, int oldindex, ChessColor color)
            :base(color)
        {            
            this.new_p = newp;
            this.old_p = oldp;
            this.new_index = newindex;
            this.old_index = oldindex;            
            
        }
        
        //Kiểm tra xem Tướng đi có đúng không
        public override bool CheckIndex()
        {
            switch (color)
            {
                case ChessColor.Black:
                    {
                        if (Black.Contains(new_index))
                        {
                            if ((Math.Abs(new_index - old_index) == 1) || (Math.Abs(new_index - old_index) == 9))
                                return true;
                        }                        
                    }
                    break;
                case ChessColor.White:
                    {
                        if (White.Contains(new_index))
                        {
                            if ((Math.Abs(new_index - old_index) == 1) || (Math.Abs(new_index - old_index) == 9))
                                return true;
                        }                        
                    }
                    break;
            }
            return false;
        }
        public override bool CheckPoint()
        {
            return false;
        }
        public override bool CheckEated(ChessColor nColor)
        {
            if ((nColor != color) && nColor != ChessColor.Default)
                return true;
            return false;
        }

        
    }
}