using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCoTuong
{
    //public enum Players
    //{
    //    Default,
    //    Player1,
    //    Player2
    //};
    public enum Info
    {
        Default,
        Index,
        Point
    };
    public enum Checked
    {
        Default,
        NoMoveable,
        Moveable,
        MoveAndEated
    };
    public enum StateConnection
    {
        Connecting,//Dang ket noi
        Connected,//da ket noi
        Breaken//Mat ket noi
    };
}
