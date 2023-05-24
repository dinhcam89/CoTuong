using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    public abstract class Pawns
    {
        protected ChessColor color;
        
        public Pawns(ChessColor color)
        {
            this.color = color;
        }
        public abstract bool CheckPoint();
        public abstract bool CheckIndex();
        public virtual bool CheckEated(ChessColor nColor)
        {
            if ((nColor != color) && nColor != ChessColor.Default)
                return true;
            return false;
        }
        //public virtual bool CheckMate(int wIndex, int bIndex)
        //{            
        //    return false;
        //}
    }
}
