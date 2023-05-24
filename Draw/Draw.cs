using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Draw
{
    public class Presenter
    {
        #region Variable
        Pen BrownColor = new Pen(Brushes.Brown, 1);
        Pen BlueColor = new Pen(Brushes.Black, 1);
        Graphics cdc = null;

        int GRID_SPACING = 60;
        int num = 0;

        Point[] m_Point = new Point[90];

        #endregion Variable

        #region Properties
        //Tất cả các điểm trên bàn cờ
        public Point[] POINTS
        {
            get { return m_Point; }
        }
        
        public Region REGION
        {
            get
            {
                Rectangle rc = new Rectangle(0, 0, 40, 40);
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(rc);
                Region rg = new Region(path);
                return rg;
            }
        }
        #endregion Properies

        public Presenter(Graphics g)
        {
            cdc = g;
            cdc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
           
        }

        public Presenter()
        {
        }
        
        public void DrawChessBoard()
        {
            cdc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            cdc.FillRectangle(Brushes.Khaki, new Rectangle(new Point(40, 40), new Size(500, 560)));            
            cdc.DrawRectangle(new Pen(Brushes.Black,2), new Rectangle(new Point(40, 40), new Size(500, 560)));
            cdc.DrawRectangle(new Pen(Brushes.Black, 2), new Rectangle(new Point(3, 10), new Size(580, 603)));
            if (cdc != null)
            {
                //Ngang 10
                for (int i = 50; i < 600; i += 60)
                {
                    cdc.DrawLine(BlueColor, new Point(50, i), new Point(530, i));
                }
                //Doc 9
                for (int j = 50; j < 590; j += 60)
                {
                    cdc.DrawLine(BlueColor, new Point(j, 50), new Point(j, 290));
                }
                for (int j = 50; j < 590; j += 60)
                {
                    cdc.DrawLine(BlueColor, new Point(j, 350), new Point(j, 590));
                }
                cdc.DrawLine(BlueColor, new Point(50, 50), new Point(50, 590));
                cdc.DrawLine(BlueColor, new Point(530, 50), new Point(530, 590));
                //Sông
                cdc.DrawLines(BrownColor, new[] 
                { 
                    new Point(50,290),                     
                    new Point(170,350),                    
                    new Point(290,290),                    
                    new Point(410,350),                    
                    new Point(530,290)
                });
                cdc.DrawLines(BrownColor, new[] 
                { 
                    new Point(50,350),                     
                    new Point(170,290),                    
                    new Point(290,350),                   
                    new Point(410,290),                  
                    new Point(530,350)
                });
                //Cung trên
                cdc.DrawLine(BrownColor, new Point(230, 50), new Point(350, 170));
                cdc.DrawLine(BrownColor, new Point(350, 50), new Point(230, 170));
                //Cung dưới
                cdc.DrawLine(BrownColor, new Point(230, 470), new Point(350, 590));
                cdc.DrawLine(BrownColor, new Point(230, 590), new Point(350, 470));
                
                DrawGrid();
            }
        }
        //Ve Luoi
        private void DrawGrid()
        {
            for (int Y = 50; Y < 650; Y += GRID_SPACING)
            {
                for (int X = 50; X < 540; X += GRID_SPACING)
                {
                    Point point = new Point(X - 1, Y - 1);
                    if (
                        (point.X == 109 && point.Y == 169)||
                        (point.X == 469 && point.Y == 169)||
                        (point.X == 109 && point.Y == 469)||
                        (point.X == 469 && point.Y == 469) ||
                        (point.X == 49 && point.Y == 229) ||
                        (point.X == 169 && point.Y == 229) ||
                        (point.X == 289 && point.Y == 229) ||
                        (point.X == 409 && point.Y == 229) ||
                        (point.X == 529 && point.Y == 229) ||
                        (point.X == 49 && point.Y == 409) ||
                        (point.X == 169 && point.Y == 409) ||
                        (point.X == 289 && point.Y == 409) ||
                        (point.X == 409 && point.Y == 409) ||
                        (point.X == 529 && point.Y == 409)
                        )
                        cdc.FillEllipse(Brushes.Lavender, new Rectangle(new Point(X - 3, Y - 3), new Size(6, 6)));
                    if(
                        (point.X == 289 && point.Y == 49) ||
                        (point.X == 289 && point.Y == 589)
                        )
                        cdc.FillEllipse(Brushes.Lavender, new Rectangle(new Point(X - 7, Y - 7), new Size(14, 14)));


                    cdc.FillEllipse(Brushes.Lavender, new Rectangle(point, new Size(2, 2)));
                    
                    //Thêm các Point vào array, để kiểm tra
                    Point p = new Point(X, Y);
                    m_Point[num++] = p;                                       
                }
            }
           
        }
        //Tinh toan diem de hut vao
        public void SnapToPoint(ref int X, ref int Y)
        {
            X = ((GRID_SPACING * int.Parse((X / GRID_SPACING).ToString())) - 10) + GRID_SPACING;
            Y = ((GRID_SPACING * int.Parse((Y / GRID_SPACING).ToString())) - 10) + GRID_SPACING;
        }
        ///Kiểm tra Point có nằm trên bàn cờ không, đề phòng kéo cờ tùm bạy :D
        public bool CheckPoint(Point p)
        {
            foreach (Point pt in POINTS)
                if (p == pt)
                    return true;
            return false;
        }

        public int MappingPointToIndex(Point p)
        {
            for (int i = 0; i < POINTS.Length; i++)
            {
                if (p == POINTS[i])
                    return i;
            }
            return -1;
        }

        public void DrawRect(Graphics dc, Point point)
        {
            Rectangle rect = Rectangle.Empty;
            rect.Location = new Point(point.X - 25, point.Y - 25);
            rect.Size = new Size(50, 50);
            dc.DrawRectangle(Pens.Red, rect);
        }

    }

 
}
