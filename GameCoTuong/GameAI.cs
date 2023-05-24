using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Chess;
namespace GameCoTuong
{
    public class GameAI
    {
        private static bool[] Flag = new bool[90];
        private static Point gPoint, cPoint;
        private static int gIndex, cIndex;
        private static ChessColor color;

        public GameAI()
        {
            
        }
        //Trường hợp 2 tướng đối mặt nhau, bất kỳ quân cờ nào duy chuyển đều kiểm tra hàm này
        public static bool GeneralOpposition(bool []flag,int bIndex, int wIndex)
        {
            Flag = flag;
            int i = bIndex;
            bool isOpposited = false;
            while (i < wIndex)
            {
                i += 9;
                if (i == wIndex)
                    isOpposited = true;

            }
            // 2 Tuong doi mat. Nhung o giua con` 1 con Co` kha'c
            if (isOpposited)
            {
                for (int index = bIndex + 9; index < wIndex; index += 9)
                {
                    if (Flag[index])
                    {
                        isOpposited = false;
                        break;
                    }
                }
            }
            return isOpposited;
        }
        //Kiểm tra chiếu tướng khi đi Chốt , Pháo , Xe , Mã
        public static bool CheckMate(ChessType type, ChessColor _color,Point gXY, Point cXY,int _gIndex,int _cIndex)
        {
            gPoint = gXY;
            cPoint = cXY;
            gIndex = _gIndex;
            cIndex = _cIndex;
            color = _color;
            switch (type)
            {                
                case ChessType.Cannon://Pháo
                    {
                        return CannonCheckMate();
                    } //break;
                case ChessType.Chariot://Xe
                    {
                        return ChariotCheckMate();
                    } //break;
                
                case ChessType.Horse://Mã
                    {
                        return HorseCheckMate();
                    } //break;
                case ChessType.Soldier://Tốt
                    {
                        return SoldierCheckMate();
                    } //break;
                default: break;
            }
            return false;
        }
        private static bool HorseCheckMate()
        {
            switch (color)
            {
                case ChessColor.Black:
                    {
                        if (Math.Abs(cIndex - gIndex) == 19 || Math.Abs(cIndex - gIndex) == 7 || Math.Abs(cIndex - gIndex) == 17 || Math.Abs(cIndex - gIndex) == 11)
                        {
                            int y = gPoint.Y - cPoint.Y;
                            switch (y)
                            {
                                case 120:
                                    {
                                        if (Flag[cIndex + 9] == false)
                                            return true;
                                    }
                                    break;
                                case -120:
                                    {
                                        if (Flag[cIndex - 9] == false)
                                            return true;
                                    }
                                    break;
                            }

                            int x = gPoint.X - cPoint.X;
                            switch (x)
                            {
                                case 120:
                                    {
                                        if (Flag[cIndex + 1] == false)
                                            return true;
                                    }
                                    break;
                                case -120:
                                    {
                                        if (Flag[cIndex - 1] == false)
                                            return true;
                                    }
                                    break;
                            }
                        }

                    } break;
                case ChessColor.White:
                    {
                        if (Math.Abs(cIndex - gIndex) == 19 || Math.Abs(cIndex - gIndex) == 7 || Math.Abs(cIndex - gIndex) == 17 || Math.Abs(cIndex - gIndex) == 11)
                        {
                            int y = gPoint.Y - cPoint.Y;
                            switch (y)
                            {
                                case 120:
                                    {
                                        if (Flag[cIndex + 9] == false)
                                            return true;
                                    }
                                    break;
                                case -120:
                                    {
                                        if (Flag[cIndex - 9] == false)
                                            return true;
                                    }
                                    break;
                            }

                            int x = gPoint.X - cPoint.X;
                            switch (x)
                            {
                                case 120:
                                    {
                                        if (Flag[cIndex + 1] == false)
                                            return true;
                                    }
                                    break;
                                case -120:
                                    {
                                        if (Flag[cIndex - 1] == false)
                                            return true;
                                    }
                                    break;
                            }
                        }
                    } break;
                default: break;
            }
            return false;
        }
        private static bool CannonCheckMate()
        {
                        if (gPoint.X == cPoint.X)
                        {
                            if (gIndex > cIndex)
                            {
                                int index = cIndex + 9;
                                int count = 0;
                                while (index < gIndex)
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
                                int index = cIndex - 9;
                                int count = 0;
                                while (index > gIndex)
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
                        else if (cPoint.Y == gPoint.Y)
                        {
                            if (gIndex > cIndex)
                            {
                                int index = cIndex + 1;
                                int count = 0;
                                while (index < gIndex)
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
                                int index = cIndex - 1;
                                int count = 0;
                                while (index > gIndex)
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
        private static bool ChariotCheckMate()
        {
            switch (color)
            {
                case ChessColor.Black:
                    {
                        if (cPoint.X == gPoint.X)//Dọc
                        {
                            bool isOpposited = true;
                            if (gIndex > cIndex)
                            {
                                for (int index = cIndex + 9; index < gIndex; index += 9)
                                {
                                    if (Flag[index])
                                    {
                                        isOpposited = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int index = gIndex + 9; index < cIndex; index += 9)
                                {
                                    if (Flag[index])
                                    {
                                        isOpposited = false;
                                        break;
                                    }
                                }
                            }
                            return isOpposited;
                        }
                        else if (cPoint.Y == gPoint.Y)//Ngang
                        {
                            bool isOpposited = true;
                            if (gIndex > cIndex)
                            {
                                for (int index = cIndex + 1; index < gIndex; index += 1)
                                {
                                    if (Flag[index])
                                    {
                                        isOpposited = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int index = gIndex + 1; index < cIndex; index += 1)
                                {
                                    if (Flag[index])
                                    {
                                        isOpposited = false;
                                        break;
                                    }
                                }
                            }

                            return isOpposited;
                        }
                    } break;
                case ChessColor.White:
                    {
                        if (cPoint.X == gPoint.X)//Dọc
                        {
                            bool isOpposited = true;

                            if (gIndex < cIndex)
                            {
                                for (int index = gIndex + 9; index < cIndex; index += 9)
                                {
                                    if (Flag[index])
                                    {
                                        isOpposited = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int index = cIndex + 9; index < gIndex; index += 9)
                                {
                                    if (Flag[index])
                                    {
                                        isOpposited = false;
                                        break;
                                    }
                                }
                            }
                            return isOpposited;
                        }
                        else if (cPoint.Y == gPoint.Y)//Ngang
                        {
                            bool isOpposited = true;

                            if (gIndex < cIndex)
                            {
                                for (int index = gIndex + 1; index < cIndex; index += 1)
                                {
                                    if (Flag[index])
                                    {
                                        isOpposited = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int index = cIndex + 1; index < gIndex; index += 1)
                                {
                                    if (Flag[index])
                                    {
                                        isOpposited = false;
                                        break;
                                    }
                                }
                            }

                            return isOpposited;
                        }
                    } break;
            }
            return false;
        }
        private static bool SoldierCheckMate()
        {
            switch (color)
            {
                case ChessColor.Black:
                    {
                        if (cPoint.X == gPoint.X && cPoint.Y < gPoint.Y)
                        {
                            if (Math.Abs(cPoint.Y - gPoint.Y) == 60)
                                return true;
                        }
                        else if (cPoint.Y == gPoint.Y)
                        {
                            if (Math.Abs(cPoint.X - gPoint.X) == 60)
                                return true;
                        }
                    } break;
                case ChessColor.White:
                    {
                        if (cPoint.X == gPoint.X && cPoint.Y > gPoint.Y)
                        {
                            if (Math.Abs(cPoint.Y - gPoint.Y) == 60)
                                return true;
                        }
                        else if (cPoint.Y == gPoint.Y)
                        {
                            if (Math.Abs(cPoint.X - gPoint.X) == 60)
                                return true;
                        }
                    } break;
            }
            return false;
        }
    }
}
