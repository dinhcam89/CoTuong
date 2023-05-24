using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Chess;
using Draw;
using System.Net;
using System.Net.Sockets;
using Main;
using System.IO;
namespace GameCoTuong
{
    public partial class frmMain : Form
    {
        #region Variable
        Presenter draw;
        int m_X1, m_Y1;
        int m_X2, m_Y2;
        Point startDragPoint = Point.Empty;
        Point chessPoint = Point.Empty;// Lưu lại vị trí ban đầu của con cờ khi chuột click nó
        Point _OriginPoint = Point.Empty;// Lưu lại vi trí củ của con cờ
        Point Rect = Point.Empty;

        ChessControl[] chessBlack = new ChessControl[16];//Quân cờ đen
        ChessControl[] chessWhite = new ChessControl[16];//Quân cờ trắng 
        ChessColor me = ChessColor.Default;

        bool[] HasChess = new bool[90];//Mảng này cho biết vị trí nào đang có cờ
        ChessColor[] cColor = new ChessColor[90]; // Mảng này cho biết tại vị trí HasChess[index], cờ đó màu ji`. để biết mà ăn nó :D        

        string[] WB = new string[90];

        ChessColor player = ChessColor.Default;
        bool opposite = false;
        bool pausegame = false;
        //Biến kiểm tra xem có được phép ăn hay không
        bool eated = false;
        Size curSize;
        string url = Form_Main.url;

        public int check_loai_user;
        public int songuoichoi = 0;

        int time_player1 = 60;
        int timetong_player1 = 1800;
        int ctr_time = 0;

        int time_player2 = 60;
        int timetong_player2 = 1800;
        public string url_anh = "/webservice/upanh/avarta_mini/";
        public int kinhnghiem1, thang1, thua1, kinhnghiem2, thang2, thua2, diemthanggame, diemthuagame;
        public string capbac1, capbac2, ava1, ava2;
        public string user1, user2;
       
        public static string vitriban;
       // public string toadoban;
        public string name;
        public static bool nguoixem = false;
        public static string vitrinut;
        public static string vitrinut_stamp;
        public static string user_id;

        public string luatchoi = "Tự Do";
        public bool chuban;
        public int sonuocdi_player1=0;
        public int sonuocdi_player2=0;
        public int color;

        #endregion



        public string str_resp;        
        public void start_get(string strPage, string strVars)
        {
            
            //khoi tao một request.
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", strPage, strVars));

            //This time, our method is GET.
            WebReq.Method = "GET";
            //From here on, it's all the same as above.
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();


            //Now, we read the response (the string), and output it.
            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer);
            str_resp = _Answer.ReadToEnd();
            str_resp = str_resp.Trim();          
            Answer.Close();
            _Answer.Close();
            


            if (str_resp.StartsWith("NULL") == false)
            {
                
                xulycotuong();
            }


            // else
            // MessageBox.Show("Khong Co Du Lieu");  
        }



        private void xulycotuong()
        {
            //MessageBox.Show(str_resp);
            //label6.Text = str_resp;
            int n_mang = 0;
            if(str_resp!="" && Setting_Game.showdata==1)
                txt_ndnhan.AppendText(str_resp + (char)13 + (char)10);


            string[] dataArray;            
            dataArray = str_resp.Split((char)124);
            n_mang = dataArray.Length;
           // if (dataArray[0] == "Thoat")
               
            switch (dataArray[0])
            {

                case "ReNewGame":

                   /*
                    int kt;
                    //string ds_online="";
                    kt = Convert.ToInt32(dataArray[1]);
                    user_id = Convert.ToInt32(dataArray[2]);
                    //MessageBox.Show("user_id" + user_id.ToString());
                    avata = dataArray[3];
                    songuoi_onnline = Convert.ToInt32(dataArray[4]);
                    //string data_huy = dataArray[0] + "|" + kt + "|" + user_id + "|" + avata + "|" + songuoi_onnline;
                    //MessageBox.Show("Data : " + data_huy);
                    if (kt == 1)
                    { }
                    * */
                    //MessageBox.Show(dataArray[1]);
                    break;
                case "Vao_CoTuong":// truong hop ban chua co ai hoac chi moi co 1 nguoi , nguoi vao la nguoi choi
                    {

                        
                        ////lbl_player2.Text = dataArray[1];                        
                       //// pb_player2.ImageLocation = url + "/webservice/upanh/avarta_mini/" + dataArray[1] + ".jpg";

                       
                         if (nguoixem == true)
                        {
                            if (lbl_player1.Text == "")
                            {
                                lbl_player1.Text = dataArray[1];
                                user1 = dataArray[1];
                                kinhnghiem1 = Convert.ToInt32(dataArray[3]);
                                thang1 = Convert.ToInt32(dataArray[4]);
                                thua1 = Convert.ToInt32(dataArray[5]);
                                capbac1 = dataArray[6];
                                ava1 = dataArray[11];
                                pb_player1.ImageLocation = url + url_anh + ava1;
                                tt_clearchat.SetToolTip(pb_player1, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);

                            }
                            if (lbl_player2.Text == "")
                            {
                                lbl_player2.Text = dataArray[1];
                                user2 = dataArray[1];
                                ava2 = dataArray[11];
                                kinhnghiem2 = Convert.ToInt32(dataArray[3]);
                                thang2 = Convert.ToInt32(dataArray[4]);
                                thua2 = Convert.ToInt32(dataArray[5]);
                                capbac2 = dataArray[6];
                                pb_player2.ImageLocation = url + url_anh + ava2;
                                tt_clearchat.SetToolTip(pb_player2, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);

                            }
                        }
                        else
                        {
                            lbl_player2.Text = dataArray[1];
                            user2 = dataArray[1];
                            pb_player2.ImageLocation = url + "/webservice/upanh/avarta_mini/" + dataArray[1] + ".jpg";
                            kinhnghiem2 = Convert.ToInt32(dataArray[3]);
                            thang2 = Convert.ToInt32(dataArray[4]);
                            thua2 = Convert.ToInt32(dataArray[5]);
                            capbac2 = dataArray[6];
                            tt_clearchat.SetToolTip(pb_player2, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);
                           
                            //luotdi = 10;
                        }
                        //khungchat.SelectionColor = Color.Red;
                        //System.Drawing.Font currentFont = khungchat.SelectionFont;
                        ///khungchat.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, FontStyle.Italic);
                        //khungchat.AppendText(dataArray[1] + " đã tham gia trò chơi\n");
                        //clearkhungchat.Enabled = true; 

                    } break;
                case "TuChoi_CoTuong":
                    {
                        lbl_player2_ss.Text = "";
                    } break;
                case "SanSang_CoTuong":
                    {
                        if (nguoixem == false)
                        {
                            btn_sansang.Enabled = true;
                        }
                        luatchoi = dataArray[3];
                        lbl_player2_ss.Text = "Sẵn Sàng";

                        
                        lbl_luatchoi.Text = luatchoi;
                        txt_diem.Text = dataArray[4];
                        
                        diemthanggame = Convert.ToInt32(dataArray[4]);
                        diemthuagame = diemthanggame;
                        diemthangthua_1.Text = "";
                        diemthangthua_2.Text = "";
                        xuly_luatco();                        
                        
                    } break;
                case "New_CoTuong":
                    {
                        diemthangthua_1.Text = "";
                        diemthangthua_2.Text = "";
                        lbl_player2_ss.Text = "";                       
                        lbl_sangsang.Text = "";
                        lbl_player1_ss.Text = "Đang đánh...";

                        ctr_time = 1;
                        btn_sansang.Text = "Sẵn Sàng";
                        btn_xinthua.Enabled = true;
                        btn_xinhoa.Enabled = true;
                        btn_sansang.Enabled = false;
                        if (nguoixem == true)
                        {
                            btn_xinthua.Enabled = false;
                            btn_xinhoa.Enabled = false;
                        }
                    } break;
                case "Thoat_CoTuong":
                    {

                        //if (nguoixem == false)
                        //{
                           vitrinut_stamp = dataArray[2];
                            //int i = Convert.ToInt16(vitrinut_stamp.Substring(0, 1));
                            //int j = Convert.ToInt16(vitrinut_stamp.Substring(1, 1));
                            //vitrinut_stamp = i.ToString() + j.ToString();
                           
                        //}
                           // MessageBox.Show(str_resp.ToString());
                        //classxulynut.xl_userthoat_formgame(cotuong.BTN_CoTuong, cotuong.Ban_CoTuong, cotuong.LBL_CoTuongTen, vitrinut_stamp, vitriban);  
                        if (dataArray[1] == lbl_player2.Text)
                        {
                            pb_player2.ImageLocation = "";
                            lbl_player2.Text = "";
                            lbl_player2_ss.Text = "";
                            lbl_sangsang.Text = "";
                            chuban = true;
                            lbl_chuban_khach.Text = "Chủ Bàn";
                            cb_luat.Visible = true;
                            lbl_luatchoi.Visible = false;
                            
                            if (lbl_nguoixem.Text == "")
                            {
                                MessageBox.Show("Bạn bị đá ra ngoài vì tất cả người chơi đã thoát ra ngoài1 ", "Thông Báo");
                                this.Close();
                            }
                            
                        }
                        else if (dataArray[1] == lbl_player1.Text || dataArray[1] == lbl_nguoixem.Text)
                        {
                            pb_player1.ImageLocation = "";
                            if (nguoixem == true)
                                lbl_nguoixem.Text = "";
                            else
                                lbl_player1.Text = "";
                        
                            lbl_player1_ss.Text = "";
                            lbl_sangsang.Text = "";
                            //classxulynut.xl_userthoat_formgame(cotuong.BTN_CoTuong, cotuong.LBL_CoTuongTen, vitrinut, vitriban);

                            if (lbl_player2.Text == "")
                            {
                                MessageBox.Show("Bạn bị đá ra ngoài vì tất cả người chơi đã thoát ra ngoài2 ", "Thông Báo");
                                this.Close();
                            }
                        }
                    } break;
                case "Di_CoTuong":
                    {
                        //##########################
                        string tamp_dicotuong;
                        string[] point;
                        tamp_dicotuong = dataArray[2];
                        //MessageBox.Show(dataArray[2]);
                        point = tamp_dicotuong.Split(','); 
                        int n = point.Length;
                         
                        int indexNew = Convert.ToInt16(point[0]);
                        int indexOld = Convert.ToInt16(point[1]);
                        int nIndex = Convert.ToInt16(point[2]);
                        int user = Convert.ToInt16(point[3]);
                        int cIndex = Convert.ToInt16(point[4]);
                        int xnP = Convert.ToInt16(point[5]);
                        int ynP = Convert.ToInt16(point[6]);
                        int xoP = Convert.ToInt16(point[7]);
                        int yoP = Convert.ToInt16(point[8]);
                        int curIndex = Convert.ToInt16(point[9]);
                        int ct = Convert.ToInt16(point[10]);
                        int color = Convert.ToInt16(point[11]);
                        int kt_thang = Convert.ToInt16(point[12]);
                        int ctr_time_tamp = Convert.ToInt16(point[13]);
                        int time_tong_tamp = Convert.ToInt16(point[14]);



                        if (luatchoi == "Cờ Nhanh")
                        {
                        //MessageBox.Show(str_resp);
                            if (ctr_time_tamp == 1)
                            {
                                ctr_time = 2;
                                timetong_player1 = time_tong_tamp;

                                lbl_time_player1.Hide();

                                lbl_timetong_player2.Text = (timetong_player1 / 60).ToString();
                                lbl_timegiay_player2.Text = (timetong_player1 % 60).ToString();
                                time_player2 = 60;

                            }
                            else
                            {
                                if (ctr_time_tamp == 2)
                                {
                                    ctr_time = 1;
                                    timetong_player2 = time_tong_tamp;

                                    lbl_time_player2.Hide();
                                    lbl_timetong_player2.Text = (timetong_player2 / 60).ToString();
                                    lbl_timegiay_player2.Text = (timetong_player2 % 60).ToString();
                                    time_player1 = 60;

                                }

                            }
                        }

                        if (luatchoi == "Quốc Tế")
                        {
                            //MessageBox.Show(str_resp);
                            if (ctr_time_tamp == 1)
                            {
                                ctr_time = 2;
                                timetong_player1 = time_tong_tamp;

                                //lbl_time_player1.Hide();

                                lbl_timetong_player2.Text = (timetong_player1 / 60).ToString();
                                lbl_timegiay_player2.Text = (timetong_player1 % 60).ToString();
                                //time_player2 = 60;

                            }
                            else
                            {
                                if (ctr_time_tamp == 2)
                                {
                                    ctr_time = 1;
                                    timetong_player2 = time_tong_tamp;

                                    //lbl_time_player2.Hide();
                                    lbl_timetong_player2.Text = (timetong_player2 / 60).ToString();
                                    lbl_timegiay_player2.Text = (timetong_player2 % 60).ToString();
                                   // time_player1 = 60;

                                }

                            }

                            if (sonuocdi_player1 >= 10)
                            {
                                sonuocdi_player1 = 0;
                                lbl_time_player1.Text = sonuocdi_player1.ToString();
                            }
                            if (sonuocdi_player2 >= 10)
                            {
                                sonuocdi_player2 = 0;
                                lbl_time_player2.Text = sonuocdi_player2.ToString();
                            }                              
 
                            
                        }




                        Point newPoint = new Point(xnP, ynP);
                        Point oriPoint = new Point(xoP, yoP);

                        if (user == 1)
                        {
                            Process(curIndex, indexNew, indexOld, nIndex, newPoint, oriPoint, color);

                            lbl_player1_ss.Text = "Đang đánh...";
                            lbl_player2_ss.Text = "";

                            amthanh("file_sound2");
                            sonuocdi_player1++;
                           //MessageBox.Show("den" + sonuocdi_player1.ToString());


                            Player = ChessColor.White;
                            if (ct > 0)
                            {
                                //xu lý thông báo chiếu tướng
                                Confirm();
                                //MessageBox.Show("Cơ Đen Đã Thắng");
                            }
                            if (kt_thang == 1)
                            {
                                amthanh("file_sound5");
                               
                                MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Trắng" + "\n Kết Quả: Thắng");
                                btn_banmoi.Enabled = true;
                                btn_xinthua.Enabled = false;
                                btn_xinhoa.Enabled = false;
                                ctr_time = 0;


                                NewGame();
                                btn_banmoi.Enabled = false;
                                lbl_player1_ss.Text = "";
                                lbl_player2_ss.Text = "";
                                diemthangthua_1.Text = "";
                                diemthangthua_2.Text = "";
                                xuly_luatco();
                                color = 100;
                               
                            }
                            if (kt_thang == 2)
                            {
                                amthanh("file_sound5");
                               // ctr_time = 0;
                                MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Đen" + "\n Kết Quả: Thắng");
                                btn_banmoi.Enabled = true;
                                btn_xinthua.Enabled = false;
                                btn_xinhoa.Enabled = false;
                                ctr_time = 0;

                                NewGame();
                                btn_banmoi.Enabled = false;
                                lbl_player1_ss.Text = "";
                                lbl_player2_ss.Text = "";
                                diemthangthua_1.Text = "";
                                diemthangthua_2.Text = "";
                                xuly_luatco();
                                color = 100;
                               
                            }
                        }
                        else
                        {
                            Process(curIndex, indexNew, indexOld, nIndex, newPoint, oriPoint, color);

                            lbl_player1_ss.Text = "Đang đánh...";
                            lbl_player2_ss.Text = "";

                            if (nguoixem == true)
                            {
                                lbl_player2_ss.Text = "Đang đánh...";
                                lbl_player1_ss.Text = "";
 
                            }

                            amthanh("file_sound2");
                            sonuocdi_player2++;
                            //MessageBox.Show("Trang" + sonuocdi_player2.ToString());

                            Player = ChessColor.Black;
                            if (ct > 0)
                            {
                                //xu lý thông báo chiêu tuong
                                Confirm();
                                // MessageBox.Show("Cơ Trắng Đã Thắng");
                            }
                            if (kt_thang == 1)
                            {
                                amthanh("file_sound5");
                                ctr_time = 0;
                                MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Trắng" + "\n Kết Quả: Thắng");
                                btn_banmoi.Enabled = true;
                                btn_xinthua.Enabled = false;
                                btn_xinhoa.Enabled = false;


                                NewGame();
                                btn_banmoi.Enabled = false;
                                lbl_player1_ss.Text = "";
                                lbl_player2_ss.Text = "";
                                diemthangthua_1.Text = "";
                                diemthangthua_2.Text = "";
                                xuly_luatco();
                                color = 100;
                            }
                            if (kt_thang == 2)
                            {
                                amthanh("file_sound5");
                                ctr_time = 0;
                                MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Đen" + "\n Kết Quả: Thắng");
                                btn_banmoi.Enabled = true;
                                btn_xinthua.Enabled = false;
                                btn_xinhoa.Enabled = false;



                                NewGame();
                                btn_banmoi.Enabled = false;
                                lbl_player1_ss.Text = "";
                                lbl_player2_ss.Text = "";
                                diemthangthua_1.Text = "";
                                diemthangthua_2.Text = "";
                                xuly_luatco();
                                color = 100;
                               
                            }
                        }

                    }
                    break;
                case "Load_BanCo":
                    {

                        string tamp_dicotuong = "";
                        string[] point;
                        for (int i_mang = 1; i_mang < n_mang; i_mang++)
                        {
                            if (tamp_dicotuong != dataArray[i_mang])
                            {
                                tamp_dicotuong = dataArray[i_mang];
                                //MessageBox.Show(tamp_dicotuong);
                                point = tamp_dicotuong.Split(',');
                                int n = point.Length;

                                int indexNew = Convert.ToInt16(point[0]);
                                int indexOld = Convert.ToInt16(point[1]);
                                int nIndex = Convert.ToInt16(point[2]);
                                int user = Convert.ToInt16(point[3]);
                                int cIndex = Convert.ToInt16(point[4]);
                                int xnP = Convert.ToInt16(point[5]);
                                int ynP = Convert.ToInt16(point[6]);
                                int xoP = Convert.ToInt16(point[7]);
                                int yoP = Convert.ToInt16(point[8]);
                                int curIndex = Convert.ToInt16(point[9]);
                                int ct = Convert.ToInt16(point[10]);
                                int color = Convert.ToInt16(point[11]);
                                int kt_thang = Convert.ToInt16(point[12]);
                                int ctr_time_tamp = Convert.ToInt16(point[13]);
                                int time_tong_tamp = Convert.ToInt16(point[14]);

                                if (ctr_time_tamp == 1)
                                {
                                    ctr_time = 2;
                                    timetong_player1 = time_tong_tamp;

                                    lbl_time_player1.Hide();

                                    lbl_timetong_player2.Text = (timetong_player1 / 60).ToString();
                                    lbl_timegiay_player2.Text = (timetong_player1 % 60).ToString();
                                    time_player2 = 60;


                                }
                                else
                                {
                                    if (ctr_time_tamp == 2)
                                    {
                                        ctr_time = 1;
                                        timetong_player2 = time_tong_tamp;

                                        lbl_time_player2.Hide();
                                        lbl_timetong_player2.Text = (timetong_player2 / 60).ToString();
                                        lbl_timegiay_player2.Text = (timetong_player2 % 60).ToString();
                                        time_player1 = 60;


                                    }

                                }

                                Point newPoint = new Point(xnP, ynP);
                                Point oriPoint = new Point(xoP, yoP);

                                if (user == 1)
                                {
                                    Process(curIndex, indexNew, indexOld, nIndex, newPoint, oriPoint, color);

                                    lbl_player1_ss.Text = "Đang đánh...";
                                    lbl_player2_ss.Text = "";

                                    amthanh("file_sound2");

                                    Player = ChessColor.White;
                                    if (ct > 0)
                                    {
                                        //xu lý thông báo chiếu tướng
                                        Confirm();
                                        //MessageBox.Show("Cơ Đen Đã Thắng");
                                    }
                                    if (kt_thang == 1)
                                    {
                                        amthanh("file_sound5");
                                        ctr_time = 0;
                                        MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Trắng" + "\n Kết Quả: Thắng");
                                        btn_banmoi.Enabled = true;
                                        btn_xinthua.Enabled = false;
                                        btn_xinhoa.Enabled = false;

                                    }
                                    if (kt_thang == 2)
                                    {
                                        amthanh("file_sound5");
                                        ctr_time = 0;
                                        MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Đen" + "\n Kết Quả: Thắng");
                                        btn_banmoi.Enabled = true;
                                        btn_xinthua.Enabled = false;
                                        btn_xinhoa.Enabled = false;

                                    }
                                }
                                else
                                {
                                    Process(curIndex, indexNew, indexOld, nIndex, newPoint, oriPoint, color);

                                    lbl_player2_ss.Text = "Đang đánh...";
                                    lbl_player1_ss.Text = "";

                                    amthanh("file_sound2");
                                    Player = ChessColor.Black;
                                    if (ct > 0)
                                    {
                                        //xu lý thông báo chiêu tuong
                                        Confirm();
                                        // MessageBox.Show("Cơ Trắng Đã Thắng");
                                    }
                                    if (kt_thang == 1)
                                    {
                                        amthanh("file_sound5");
                                        ctr_time = 0;
                                        MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Trắng" + "\n Kết Quả: Thắng");
                                        btn_banmoi.Enabled = true;
                                        btn_xinthua.Enabled = false;
                                        btn_xinhoa.Enabled = false;
                                    }
                                    if (kt_thang == 2)
                                    {
                                        amthanh("file_sound5");
                                        ctr_time = 0;
                                        MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Đen" + "\n Kết Quả: Thắng");
                                        btn_banmoi.Enabled = true;
                                        btn_xinthua.Enabled = false;
                                        btn_xinhoa.Enabled = false;

                                    }
                                }

                            }
                        }

                    }
                    break;
                case "BanMoi":
                    {
                        NewGame();
                        lbl_player1_ss.Text = "";
                        lbl_player2_ss.Text = "";
                        diemthangthua_1.Text = "";
                        diemthangthua_2.Text = "";
                        btn_banmoi.Enabled = false;
                        xuly_luatco();
                        break;
                    }
                case "CauHoa_CoTuong":
                    {



                        if (MessageBox.Show("Đối phương xin hoà. \n Bạn có đồng ý hoà? ", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            guitin("guidulieu_cotuong.php", "CauHoa_CoTuong_NOT", lbl_player1.Text, lbl_vitriban.Text, "2", "CauHoa_CoTuong_NOT");
                        }
                        else
                        {                          

                            guitin("guidulieu_cotuong.php", "CauHoa_CoTuong_OK", lbl_player1.Text, lbl_vitriban.Text, "2", "CauHoa_CoTuong_OK");
                            btn_banmoi.Enabled = true;
                            ctr_time = 0;
                            btn_sansang.Enabled = false;
                            NewGame();
                            lbl_player1_ss.Text = "";
                            lbl_player2_ss.Text = "";
                            diemthangthua_1.Text = "";
                            diemthangthua_2.Text = "";
                            btn_banmoi.Enabled = false;
                            xuly_luatco();
                        }
                        
                        break;
                    }
                case "CauHoa_CoTuong_OK":
                    {
                    {
                            btn_banmoi.Enabled = true;
                            ctr_time = 0;
                            btn_sansang.Enabled = false;
                            NewGame();
                            lbl_player1_ss.Text = "";
                            lbl_player2_ss.Text = "";
                            diemthangthua_1.Text = "";
                            diemthangthua_2.Text = "";
                            btn_banmoi.Enabled = false;
                            xuly_luatco();
                    }

                        break;
                    }
                case "CauHoa_CoTuong_NOT":
                    {
                        MessageBox.Show("Đối phương không chấp nhận cầu hoà");
                        break;
                    }




                case "XinThua_CoTuong":
                    {
                        if (luatchoi == "Tự Do")
                            MessageBox.Show("Bạn Đã Thắng");
                        else
                        {

                            kinhnghiem2 = Convert.ToInt32(dataArray[3]);
                            thang2 = Convert.ToInt32(dataArray[4]);
                            thua2 = Convert.ToInt32(dataArray[5]);
                            capbac2 = dataArray[6];

                            kinhnghiem1 = Convert.ToInt32(dataArray[7]);
                            thang1 = Convert.ToInt32(dataArray[8]);
                            thua1 = Convert.ToInt32(dataArray[9]);
                            capbac1 = dataArray[10];

                            // user 1 la thua , user 2 la thang
                            if (nguoixem == false)
                            {
                                /*
                                
                                if (dataArray[1] == user1)
                                {
                                    diemthangthua_1.Text = "-" + diemthanggame.ToString();
                                    diemthangthua_2.Text = "+" + diemthuagame.ToString();
                                    
                                    
                                    lbl_player1_ss.Text = "Thua1";
                                    lbl_player2_ss.Text = "Thắng1";
                                    tt_clearchat.SetToolTip(pb_player1, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                                    tt_clearchat.SetToolTip(pb_player2, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);
                            
                                }
                                else if (dataArray[1] == user2)
                                {*/


                                diemthangthua_1.Text = "+" + diemthanggame.ToString();
                                diemthangthua_2.Text = "-" + diemthuagame.ToString();


                                lbl_player1_ss.Text = "Thắng2";
                                lbl_player2_ss.Text = "Thua2";
                                tt_clearchat.SetToolTip(pb_player1, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                                tt_clearchat.SetToolTip(pb_player2, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);


                                ctr_time = 0;
                                //}
                                btn_sansang.Enabled = false;
                                btn_xinthua.Enabled = false;
                                btn_xinhoa.Enabled = false;
                                btn_banmoi.Enabled = true;

                            }
                            else
                            {

                                //time_playing_other.Enabled = false;
                                diemthangthua_1.Text = "+" + diemthanggame.ToString();
                                diemthangthua_2.Text = "-" + diemthuagame.ToString();
                                lbl_player1_ss.Text = "Thắng3";
                                lbl_player2_ss.Text = "Thua3";
                                btn_sansang.Enabled = true;

                                check_bt_ss = 0;
                                //time_suy_nghi = 30;
                                //time_suy_nghi_other = 30;
                                btn_xinthua.Enabled = false;
                                btn_xinhoa.Enabled = false;
                                tt_clearchat.SetToolTip(pb_player1, "Tên : " + lbl_nguoixem.Text + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                                tt_clearchat.SetToolTip(pb_player2, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);

                            }
                        }

                        NewGame();
                        btn_banmoi.Enabled = false;
                        //lbl_player1_ss.Text = "";
                        //lbl_player2_ss.Text = "";
                        //diemthangthua_1.Text = "";
                        // diemthangthua_2.Text = "";
                        xuly_luatco();

                    }
                    break;



                     case "Thang_CoTuong":
                         {
                             //theloai|usergui|dulieu|kinhnghiemgui|sotranthanggui|sotranthuagui|capbacgui|kinhnghiemcuaminh|sotranthangcuaminh|sotranthuacuaminh|capbaccuaminh

                             //MessageBox.Show(str_resp);
                            
                             
                             if (luatchoi == "Tự Do")
                             {
                                 MessageBox.Show("Bạn Đã Thua.");
                             }
                             else
                             {
                                 string toado;
                                 toado = dataArray[2];
                                 //Point P = vanco.LastPoint();
                                 //int X = Convert.ToInt32(toado[0]);
                                 //int Y = Convert.ToInt32(toado[1]);

                                 //vanco.Human_Push_Chess(G, X, Y);
                                 //Rectangle rc = new Rectangle(P.X, P.Y, vanco.banco.cell, vanco.banco.cell);
                                 //Invalidate(rc);
                                 //is_playing = false;
                                 kinhnghiem2 = Convert.ToInt32(dataArray[3]);
                                 thang2 = Convert.ToInt32(dataArray[4]);
                                 thua2 = Convert.ToInt32(dataArray[5]);
                                 capbac2 = dataArray[6];

                                 kinhnghiem1 = Convert.ToInt32(dataArray[7]);
                                 thang1 = Convert.ToInt32(dataArray[8]);
                                 thua1 = Convert.ToInt32(dataArray[9]);
                                 capbac1 = dataArray[10];
                                 //MessageBox.Show(nguoixem.ToString());
                                 if (nguoixem == true)
                                 {
                                     // user gui la nguoi thang cuoc , user 2 la thua cuoc
                                     if (dataArray[1] == user1)
                                     {
                                         lbl_player1_ss.Text = "Thua1";
                                         lbl_player2_ss.Text = "Thắng1";
                                         tt_clearchat.SetToolTip(pb_player1, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                                         tt_clearchat.SetToolTip(pb_player2, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);

                                         diemthangthua_2.Text = "++" + diemthanggame.ToString();
                                         diemthangthua_1.Text = "--" + diemthuagame.ToString();

                                     }
                                     else if (dataArray[1] == user2)
                                     {
                                         lbl_player1_ss.Text = "Thua2";
                                         lbl_player2_ss.Text = "Thắng2";


                                         tt_clearchat.SetToolTip(pb_player1, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                                         tt_clearchat.SetToolTip(pb_player2, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);
                                         diemthangthua_2.Text = "+" + diemthanggame.ToString();
                                         diemthangthua_1.Text = "-" + diemthuagame.ToString();

                                     }
                                     btn_sansang.Enabled = false;
                                 }
                                 else
                                 {
                                     //s_thua.Play();
                                     diemthangthua_2.Text = "+" + diemthanggame.ToString();
                                     diemthangthua_1.Text = "-" + diemthuagame.ToString();
                                     lbl_player1_ss.Text = "Thua";
                                     lbl_player2_ss.Text = "Thắng";
                                     tt_clearchat.SetToolTip(pb_player1, "Tên : " + lbl_nguoixem.Text + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);
                                     btn_sansang.Enabled = true;
                                     //luyentap.Enabled = true;
                                     //time_playing_other.Enabled = false;
                                 }

                                 check_bt_ss = 0;
                                 btn_xinthua.Enabled = false;
                                 btn_xinhoa.Enabled = false;
                             }




                            //// NewGame();



                             btn_banmoi.Enabled = false;
                             //lbl_player1_ss.Text = "";
                            // lbl_player2_ss.Text = "";
                             //diemthangthua_1.Text = "";
                            // diemthangthua_2.Text = "";
                             xuly_luatco();

                         } break;

                             















                case "XinThua":
                    string user_tamp = dataArray[1];
                    NewGame();
                    MessageBox.Show("User : " + user_tamp + " đã xin thua ! \n" + "Bạn được cộng 100 điểm");
                    
                    break;

                case "Chat_CoTuong":
                    string tennguoigui = dataArray[1];
                    string dulieu_chat = dataArray[2];
                    string s;
                    s = tennguoigui + " : " + dulieu_chat + "";
                    txt_ndnhan.AppendText(s + (char)13 + (char)10);
                    break;
               // case "MY":
                  //  {
                        
                       // MessageBox.Show ("-" + dataArray[1] + ";" + dataArray[2] + "-");
                   // }
                   // break;
                    
            }
        }

        void amthanh(string file_sound_x)
        {
            //string sound_url = Application.StartupPath + "\\Sound\\file_sound2.wav";
            if (nguoixem == false)
            {
                string sound_url = Application.StartupPath + "\\Sound\\" + file_sound_x + ".wav";
                System.Media.SoundPlayer s = new System.Media.SoundPlayer(sound_url);
                //if (amthanh == 1)
                s.Play();
                //else
                //  s.Stop();
            }
        }

        private void loadform()
        {
            try
            {        
                
                int songuoichoi = 0;
                diemthangthua_1.Text = "";
                diemthangthua_2.Text = "";
                
                //MessageBox.Show("vi tri nut : " + vitrinut);
                //MessageBox.Show("toadobanformload" + toadoban);
                if (nguoixem == false)
                {
                    lbl_vitriban.Text = Form_Main.vitriban.ToString();
                    vitriban = Form_Main.vitriban.ToString();
                    lbl_player1.Text = Form_Main.user;
                    name = Form_Main.user;
                    lbl_nguoixem.Visible = false;
                    btn_xem.Visible = false;
                    
                }
                else
                {
                    //MessageBox.Show(page3);
                    lbl_vitriban.Text = vitriban;
                    lbl_player1.Text = Form_Main.user;
                    name = Form_Main.user;
                    btn_xem.Visible = true;
                    btn_hadiem.Visible = false;
                    txt_diem.Visible = false;
                    btn_nangdiem.Visible = false;



                   // string page3 = url + "/webservice/gamecotuong/loaddau.php";  //khai bao trang web trang web service.
                    //string vars3 = "?cmd=Load_BanCo";
                   // vars3 += "&nhan=" + Form_Main.user + "&idban=" + lbl_vitriban.Text;  //2 là game co tuong                    
                    //start_get(page3, vars3);  
                    
                  
                }                

                lbl_sangsang.Text = "";
                btn_banmoi.Enabled = false;
                btn_xinthua.Enabled = false;
                btn_xinhoa.Enabled = false;
                lbl_sangsang.Text = "Hãy Click Sẵn Sàng!";
                user_id = Form_Main.user_id.ToString();


               string page2 = url + "/webservice/gamecotuong/kiemtradulieu_cotuong.php";  //khai bao trang web trang web service.
               string vars2 = "?cmd=KiemTraDau";
            
               vars2 += "&gui=" + Form_Main.user + "&idban=" + lbl_vitriban.Text + "&iduser=" + Form_Main.user_id;  //2 là game co tuong
               start_get(page2, vars2);                
                check_loai_user = 1;
                ///////////////////////

                string[] dataArray;
                string url1 = page2 + vars2;               
                dataArray = str_resp.Split((char)124);


                 //MessageBox.Show(str_resp);
               

                 if (nguoixem == false)
                 {
                     //MessageBox.Show(str_resp);
                     songuoichoi = Convert.ToInt32(dataArray[0]);
                     //load avarta
                     if (songuoichoi < 1)
                     {

                         // chua co nguoi choi nao trong ban nay
                         // $songuoichoi|tenuserchu|avatar|kinhnghiem|thang|thua|diemthanggame|diemthuagame
                         lbl_player1.Text = Form_Main.user; ;
                         pb_player1.ImageLocation = url + url_anh + dataArray[1];
                         pb_player2.ImageLocation = url + url_anh + "noavatar.jpg";
                         //Chess chs = new Chess();
                         capbac1 = dataArray[2];
                         kinhnghiem1 = Convert.ToInt32(dataArray[3]);
                         thang1 = Convert.ToInt32(dataArray[4]);
                         thua1 = Convert.ToInt32(dataArray[5]);
                         //diemkinhnghiem.Text = kinhnghiem1.ToString();
                         //sotranthang.Text = thang1.ToString();
                         //sotranthua.Text = thua1.ToString();
                         diemthanggame = Convert.ToInt32(dataArray[6]);
                         diemthuagame = Convert.ToInt32(dataArray[7]);
                         //vanco.banco.SetChess(chs.pictureBox5.Image, chs.pictureBox6.Image);
                         tt_clearchat.SetToolTip(pb_player1, "Tên : " + Form_Main.user + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                     }
                     else if (songuoichoi == 1) // da co 1 nguoi
                     {
                         ////player1.Text = user_test;
                         pb_player1.ImageLocation = url + "/webservice/upanh/avarta_mini/" + lbl_player1.Text + ".jpg";
                         lbl_player2.Text = dataArray[2];
                         //lbl_player2_ss.Text = "Sẵn Sàng";
                         pb_player2.ImageLocation = url + "/webservice/upanh/avarta_mini/" + dataArray[2] + ".jpg";
                         ///////////////////////////////////////////////////
                         //////////////////////////////////////////////////
                         // $songuoichoi|tenuser1|loaiuser|avatar|kinhnghiem1|thang1|thua1|tenuserchu|avatar|kinhnghiemchu|thangchu|thuachu|diemthanggame|diemthuagame
                         lbl_player1.Text = Form_Main.user;
                         pb_player1.ImageLocation = url + url_anh + dataArray[9];
                         user2 = dataArray[1];
                         lbl_player2.Text = user2;
                         pb_player2.ImageLocation = url + url_anh + dataArray[3];
                         //xinthua.Enabled = false;
                         if (Convert.ToInt32(dataArray[2]) == 2)
                         {
                             lbl_player2_ss.Text = "Sẵn Sàng";
                         }
                         //Chess chs = new Chess();
                         kinhnghiem1 = Convert.ToInt32(dataArray[5]);
                         thang1 = Convert.ToInt32(dataArray[6]);
                         thua1 = Convert.ToInt32(dataArray[7]);
                         capbac1 = dataArray[4];
                         kinhnghiem2 = Convert.ToInt32(dataArray[11]);
                         thang2 = Convert.ToInt32(dataArray[12]);
                         thua2 = Convert.ToInt32(dataArray[13]);
                         capbac2 = dataArray[10];
                         diemthanggame = Convert.ToInt32(dataArray[14]);
                         diemthuagame = Convert.ToInt32(dataArray[15]);
                         // vanco.banco.SetChess(chs.pictureBox5.Image, chs.pictureBox6.Image);
                         tt_clearchat.SetToolTip(pb_player1, "Tên : " + lbl_player1.Text + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);
                         tt_clearchat.SetToolTip(pb_player2, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                         




                     }

                 }
                 else // day la loai nguoi xem
                 {

                     // $songuoichoi|tenuser1|loaiuser1|kinhnghiem1|thang1|thua1|tenuser2|loaiuser2|kinhnghiem2|thang2|thua2|diemthanggame|diemthuagame

                     ///////////////////////////////////////////////////
                    
                     user1 = dataArray[1];
                     user2 = dataArray[8];
                     lbl_player1.Visible= false;
                     lbl_nguoixem.Text = user1;
                     lbl_player2.Text = user2;
                     string dulieu1 = dataArray[2];
                     string dulieu2 = dataArray[9];
                     pb_player1.ImageLocation = url + url_anh + dataArray[3];
                     pb_player2.ImageLocation = url + url_anh + dataArray[10];
                     btn_sansang.Enabled = false;
                     //luyentap.Enabled = false;
                     //current_player = 0;
                     switch (dulieu1)
                     {
                         case "2": { lbl_player1_ss.Text = "Sẵn Sàng"; } break;
                         case "3": break;
                         case "4": { lbl_player1_ss.Text = "Đang Đánh"; } break;
                     }
                     switch (dulieu2)
                     {
                         case "2": { lbl_player2_ss.Text = "Sẵn Sàng"; } break;
                         case "3": break;
                         case "4": { lbl_player2_ss.Text = "Đang Đánh"; } break;
                     }
                     kinhnghiem1 = Convert.ToInt32(dataArray[5]);
                     thang1 = Convert.ToInt32(dataArray[6]);
                     thua1 = Convert.ToInt32(dataArray[7]);
                     capbac1 = dataArray[4];
                     kinhnghiem2 = Convert.ToInt32(dataArray[12]);
                     thang2 = Convert.ToInt32(dataArray[13]);
                     thua2 = Convert.ToInt32(dataArray[14]);
                     capbac2 = dataArray[11];
                     diemthanggame = Convert.ToInt32(dataArray[15]);
                     diemthuagame = Convert.ToInt32(dataArray[16]);
                     //vanco.banco.SetChess(chs.pictureBox5.Image, chs.pictureBox6.Image);
                     tt_clearchat.SetToolTip(pb_player1, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                     tt_clearchat.SetToolTip(pb_player2, "Tên : " + user2 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem2 + "\n----------------------\nSố trận thắng : " + thang2 + "\n----------------------\nSố trận thua : " + thua2 + "\n----------------------\nCấp bậc : " + capbac2);
                      





                     lbl_sangsang.Text = "Người Xem";
                     btn_sansang.Enabled = false;


                 }



             

        
            string page = url + "/webservice/gamecotuong/guidulieu_cotuong.php";  //khai bao trang web trang web service.
            string vars = "?cmd=DangNhapCoTuong";
            vars += "&gui=" + lbl_player1.Text + "&idban=" + lbl_vitriban.Text + "&game_id=2";
            start_get(page, vars);
            //MessageBox.Show("So Nguoi Choi : " + songuoichoi + "\n URL kiem tra dau : " + page2 + vars2 + "\n Url dang nhap caro : " + page + vars);

            diemthanggame = diemthuagame = 100;
                
            }
        catch (Exception e)
        {
            MessageBox.Show("Loi : " + e);
        }
    

               


            
        }












        #region Properties
        //Tra về số index của con cờ
        private int this[Point p]
        {
            get
            {                
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i] is ChessControl)
                    {
                        if (Controls[i].Location == p)
                            return ((ChessControl)Controls[i]).Index;
                    }
                }
                return -1;
            }
        }
        public bool Pause
        {
            get { return pausegame; }
            set { pausegame = value; }
        }
        #endregion        
        ////#region Network
        //Networks net;
        string mess = "";
        int p = 0;
        public ChessColor Player
        {
            get { return player; }
            set { player = value; }
        }
      
///////// #endregion Network
        public frmMain()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            Size = curSize = new Size(890, 615);   //770,615
          


            Load += new EventHandler(frmMain_Load);
            BackColor = Color.Khaki;
            //net = new Networks(this);
            buttonStartServer.Click += new EventHandler(buttonStartServer_Click);
            buttonConnect.Click+=new EventHandler(buttonConnect_Click);
            buttonNewGame.Click += new EventHandler(buttonNewGame_Click);
            //timerFlash.Tick += new EventHandler(timerFlash_Tick);
            //timerFlash.Start();
            //timerFlash.Interval = 100;
           // btn_showpanel.Click += new EventHandler(buttonShowPanel_Click);            
            Resize += new EventHandler(frmMain_Resize);
            FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
           // FormClosed += new FormClosedEventHandler(frmMain_FormClosed);
            MouseDown += new MouseEventHandler(Form_MouseDown);
            MouseMove += new MouseEventHandler(Form_MouseMove);
            MouseUp += new MouseEventHandler(Form_MouseUp);
            panelRight.MouseDown += new MouseEventHandler(Form_MouseDown);
            panelRight.MouseMove += new MouseEventHandler(Form_MouseMove);
            panelRight.MouseUp += new MouseEventHandler(Form_MouseUp);            
            //End Move Form
            SetToolTip();          
        }               
        #region Event Form
        public int diff_x;
        public int diff_y;
        public bool mouse_down = false;
        private void frmMain_Load(object sender, EventArgs e)
        {
            Graphics dc = this.CreateGraphics();
            dc.SmoothingMode = SmoothingMode.AntiAlias;
            draw = new Presenter(dc);
            DatQuanCo();
          
            lbl_player1.Text = "";
            lbl_player2.Text = "";
            lbl_player1_ss.Text = "";
            lbl_player2_ss.Text = "";

            loadform();

            if (nguoixem == false)
            {
                if (lbl_player2.Text == "")
                {
                    cb_luat.Visible = true;
                    label6.Visible = true;
                    lbl_chuban_khach.Text = "Chủ Bàn";
                    lbl_luatchoi.Visible = false;
                }
                else
                {
                    cb_luat.Visible = false;
                    //label6.Visible = false;
                    btn_sansang.Enabled = false;
                    lbl_chuban_khach.Text = "Khách";
                    lbl_luatchoi.Text = "Tự Do";
                }
            }
            else
            {
                cb_luat.Visible = false;
                label6.Visible = false;
                lbl_chuban_khach.Visible = false;
                lbl_luatchoi.Visible = false;


                lbl_time_player1.Visible = false;
                lbl_time_player2.Visible = false;

                lbl_timetong_player1.Visible = false;
                lbl_timetong_player2.Visible = false;

                lbl_timegiay_player1.Visible = false;
                lbl_timegiay_player2.Visible = false;
            }
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
            Size = curSize;
        }        
        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_down = false; 
        }
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down == true)
            {
                Point p = new Point(MousePosition.X - diff_x, MousePosition.Y - diff_y);
                Form.ActiveForm.Location = p;
            } 
        }
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            diff_x = Form.MousePosition.X - Form.ActiveForm.Location.X;
            diff_y = Form.MousePosition.Y - Form.ActiveForm.Location.Y;
            mouse_down = true;
        }
        #endregion     
        
////////////////////////////////////////////////////////
          
        public void setEnable(bool p)
        {
            buttonStartServer.Enabled = p;
            buttonNewGame.Enabled = p;
        }
        public override void Refresh()
        {
            base.Refresh();
        }
        private delegate void SetStatusMessageDelegate(string strArg);
        private void SetToolTip()
        {
            ToolTip ttip = new ToolTip();
            ttip.IsBalloon = true;
            ttip.SetToolTip(buttonStartServer, "Start Server");
            ttip.SetToolTip(buttonNewGame, "New Game");
            ttip.SetToolTip(buttonConnect, "Connect to Server");
            ttip.SetToolTip(buttonSend, "Send a Message");
        }
        public void Confirm()
        {
            FlashMessageBox f = new FlashMessageBox();
            f.Location = new Point(Left + 100, Top + 290);
            f.ShowDialog();

            
        }
 
////////////////////////////////////////////////////////
       
        private delegate void AddChatMessageDelegate(string message);
        public void SetStatusMessage(string mess)
       {

           this.mess = mess;
           if (InvokeRequired)
           {
               Invoke(new SetStatusMessageDelegate(SetStatusMessage), new object[] { mess });
               return;
           }

           lblStatus.Text = mess;

       }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            draw = new Presenter(e.Graphics);
            draw.DrawChessBoard();
           /* mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
            if (net.Connection == StateConnection.Connected)
                pausegame = false;
            else
                pausegame = true;
           mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm **/
        }
        private Checked CheckMoveValid(int cControlIndex, int indexNew, int indexOld, int nIndex, Point newpoint, Point OriginPoint, int color)
        {
            ChessControl cControl = new ChessControl();
            if (color == 1)
                cControl = chessBlack[cControlIndex];
            else if (color == 2)
                cControl = chessWhite[cControlIndex];
            else
                return Checked.Default;

            bool eated = false;
            bool m = false;
            switch (cControl.Type)
            {
                case ChessType.General:
                    {
                        Pawns general = new General(newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                        eated = general.CheckEated(cColor[nIndex]);

                        m = general.CheckIndex();
                        if ((m == true && eated == true) || (m == true && eated == false))
                            return Checked.MoveAndEated;
                        else
                            return Checked.NoMoveable;
                    }; //break;
                case ChessType.Advisor:
                    {
                        Pawns advisor = new Advisor(indexNew, indexOld, cControl.Color);
                        eated = advisor.CheckEated(cColor[nIndex]);
                        m = advisor.CheckIndex();
                        if ((m == true && eated == true) || (m == true && eated == false))
                            return Checked.MoveAndEated;
                        else
                            return Checked.NoMoveable;
                    }; //break;
                case ChessType.Elephant:
                    {
                        Pawns elephant = new Elephant(HasChess, indexNew, indexOld, cControl.Color);
                        eated = elephant.CheckEated(cColor[nIndex]);
                        m = elephant.CheckIndex();
                        if ((m == true && eated == true) || (m == true && eated == false))
                            return Checked.MoveAndEated;
                        else
                            return Checked.NoMoveable;
                    } //break;
                case ChessType.Chariot:
                    {
                        Pawns chariot = new Chariot(HasChess, newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                        m = (chariot.CheckPoint() && chariot.CheckIndex());
                        eated = chariot.CheckEated(cColor[nIndex]);
                        if ((m == true && eated == true) || (m == true && eated == false))
                            return Checked.MoveAndEated;
                        else
                            return Checked.NoMoveable;
                    }; //break;
                case ChessType.Cannon:
                    {
                        Pawns cannon = new Cannon(HasChess, newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                        m = (/*cannon.CheckIndex() &&*/ cannon.CheckPoint());
                        eated = cannon.CheckEated(cColor[nIndex]);
                        if ((m == true && eated == true) || (m == true && eated == false))
                            return Checked.MoveAndEated;
                        else
                            return Checked.NoMoveable;

                    }; //break;
                case ChessType.Horse:
                    {
                        Pawns horse = new Horse(HasChess, newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                        m = (horse.CheckIndex() && horse.CheckPoint());
                        eated = horse.CheckEated(cColor[nIndex]);
                        if ((m == true && eated == true) || (m == true && eated == false))
                            return Checked.MoveAndEated;
                        else
                            return Checked.NoMoveable;
                    } //break;
                case ChessType.Soldier:
                    {
                        Pawns soldier = new Soldier(HasChess, newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                        m = soldier.CheckPoint();
                        eated = soldier.CheckEated(cColor[nIndex]);
                        if ((m == true && eated == true) || (m == true && eated == false))
                            return Checked.MoveAndEated;
                        else
                            return Checked.NoMoveable;
                    } //break;
            }
            return Checked.Default;
        }     
        private void DatQuanCo()
        {          
            Region rg = draw.REGION;
            #region Quân cờ đen            
            chessBlack[0] = new ChessControl();
            chessBlack[0].Image = ChessBlackImage.Images[0];
            chessBlack[0].Location = new Point(270, 30);
            chessBlack[0].Size = new Size(40, 40);
            chessBlack[0].Region = rg;
            chessBlack[0].Tag = "General_B";
            chessBlack[0].Type = ChessType.General;
            chessBlack[0].Color = ChessColor.Black;
            chessBlack[0].Index = 0;
            chessBlack[0].Parent = this;

            chessBlack[1] = new ChessControl();
            chessBlack[1].Image = ChessBlackImage.Images[3];
            chessBlack[1].Location = new Point(30, 30);
            chessBlack[1].Size = new Size(40, 40);
            chessBlack[1].Region = rg;
            chessBlack[1].Tag = "Chariot_B_1";
            chessBlack[1].Type = ChessType.Chariot;
            chessBlack[1].Color = ChessColor.Black;
            chessBlack[1].Index = 1;
            chessBlack[1].Parent = this;

            chessBlack[2] = new ChessControl();
            chessBlack[2].Image = ChessBlackImage.Images[3];
            chessBlack[2].Location = new Point(510, 30);
            chessBlack[2].Size = new Size(40, 40);
            chessBlack[2].Region = rg;
            chessBlack[2].Tag = "Chariot_B_2";
            chessBlack[2].Type = ChessType.Chariot;
            chessBlack[2].Color = ChessColor.Black;
            chessBlack[2].Index = 2;
            chessBlack[2].Parent = this;

            chessBlack[3] = new ChessControl();
            chessBlack[3].Image = ChessBlackImage.Images[5];
            chessBlack[3].Location = new Point(90, 30);
            chessBlack[3].Size = new Size(40, 40);
            chessBlack[3].Region = rg;
            chessBlack[3].Tag = "Horse_B_1";
            chessBlack[3].Type = ChessType.Horse;
            chessBlack[3].Color = ChessColor.Black;
            chessBlack[3].Index = 3;                 
            chessBlack[3].Parent = this;

            chessBlack[4] = new ChessControl();
            chessBlack[4].Image = ChessBlackImage.Images[5];
            chessBlack[4].Location = new Point(450, 30);
            chessBlack[4].Size = new Size(40, 40);
            chessBlack[4].Region = rg;
            chessBlack[4].Tag = "Horse_B_2";
            chessBlack[4].Type = ChessType.Horse;
            chessBlack[4].Color = ChessColor.Black;
            chessBlack[4].Index = 4;
            chessBlack[4].Parent = this;

            chessBlack[5] = new ChessControl();
            chessBlack[5].Image = ChessBlackImage.Images[2];
            chessBlack[5].Location = new Point(150, 30);
            chessBlack[5].Size = new Size(40, 40);
            chessBlack[5].Region = rg;
            chessBlack[5].Tag = "Elephant_B_1";
            chessBlack[5].Type = ChessType.Elephant;
            chessBlack[5].Color = ChessColor.Black;
            chessBlack[5].Index = 5;
            chessBlack[5].Parent = this;

            chessBlack[6] = new ChessControl();
            chessBlack[6].Image = ChessBlackImage.Images[2];
            chessBlack[6].Location = new Point(390, 30);
            chessBlack[6].Size = new Size(40, 40);
            chessBlack[6].Region = rg;
            chessBlack[6].Tag = "Elephant_B_2";
            chessBlack[6].Type = ChessType.Elephant;
            chessBlack[6].Color = ChessColor.Black;
            chessBlack[6].Index = 6;
            chessBlack[6].Parent = this;

            chessBlack[7] = new ChessControl();
            chessBlack[7].Image = ChessBlackImage.Images[1];
            chessBlack[7].Location = new Point(210, 30);
            chessBlack[7].Size = new Size(40, 40);
            chessBlack[7].Region = rg;
            chessBlack[7].Tag = "Advisor_B_1";
            chessBlack[7].Type = ChessType.Advisor;
            chessBlack[7].Color = ChessColor.Black;
            chessBlack[7].Index = 7;
            chessBlack[7].Parent = this;

            chessBlack[8] = new ChessControl();
            chessBlack[8].Image = ChessBlackImage.Images[1];
            chessBlack[8].Location = new Point(330, 30);
            chessBlack[8].Size = new Size(40, 40);
            chessBlack[8].Region = rg;
            chessBlack[8].Tag = "Advisor_B_2";
            chessBlack[8].Type = ChessType.Advisor;
            chessBlack[8].Color = ChessColor.Black;
            chessBlack[8].Index = 8;
            chessBlack[8].Parent = this;

            chessBlack[9] = new ChessControl();
            chessBlack[9].Image = ChessBlackImage.Images[4];
            chessBlack[9].Location = new Point(90, 150);
            chessBlack[9].Size = new Size(40, 40);
            chessBlack[9].Region = rg;
            chessBlack[9].Tag = "Cannon_B_1";
            chessBlack[9].Type = ChessType.Cannon;
            chessBlack[9].Color = ChessColor.Black;
            chessBlack[9].Index = 9;
            chessBlack[9].Parent = this;

            chessBlack[10] = new ChessControl();
            chessBlack[10].Image = ChessBlackImage.Images[4];
            chessBlack[10].Location = new Point(450, 150);
            chessBlack[10].Size = new Size(40, 40);
            chessBlack[10].Region = rg;
            chessBlack[10].Tag = "Cannon_B_2";
            chessBlack[10].Type = ChessType.Cannon;
            chessBlack[10].Color = ChessColor.Black;
            chessBlack[10].Index = 10;
            chessBlack[10].Parent = this;

            chessBlack[11] = new ChessControl();
            chessBlack[11].Image = ChessBlackImage.Images[6];
            chessBlack[11].Location = new Point(30, 210);
            chessBlack[11].Size = new Size(40, 40);
            chessBlack[11].Region = rg;
            chessBlack[11].Tag = "Soldier_B_1";
            chessBlack[11].Type = ChessType.Soldier;
            chessBlack[11].Color = ChessColor.Black;
            chessBlack[11].Index = 11;
            chessBlack[11].Parent = this;

            chessBlack[12] = new ChessControl();
            chessBlack[12].Image = ChessBlackImage.Images[6];
            chessBlack[12].Location = new Point(150, 210);
            chessBlack[12].Size = new Size(40, 40);
            chessBlack[12].Region = rg;
            chessBlack[12].Tag = "Soldier_B_2";
            chessBlack[12].Type = ChessType.Soldier;
            chessBlack[12].Color = ChessColor.Black;
            chessBlack[12].Index = 12;
            chessBlack[12].Parent = this;

            chessBlack[13] = new ChessControl();
            chessBlack[13].Image = ChessBlackImage.Images[6];
            chessBlack[13].Location = new Point(270, 210);
            chessBlack[13].Size = new Size(40, 40);
            chessBlack[13].Region = rg;
            chessBlack[13].Tag = "Soldier_B_3";
            chessBlack[13].Type = ChessType.Soldier;
            chessBlack[13].Color = ChessColor.Black;
            chessBlack[13].Index = 13;
            chessBlack[13].Parent = this;

            chessBlack[14] = new ChessControl();
            chessBlack[14].Image = ChessBlackImage.Images[6];
            chessBlack[14].Location = new Point(390, 210);
            chessBlack[14].Size = new Size(40, 40);
            chessBlack[14].Region = rg;
            chessBlack[14].Tag = "Soldier_B_4";
            chessBlack[14].Type = ChessType.Soldier;
            chessBlack[14].Color = ChessColor.Black;
            chessBlack[14].Index = 14;
            chessBlack[14].Parent = this;

            chessBlack[15] = new ChessControl();
            chessBlack[15].Image = ChessBlackImage.Images[6];
            chessBlack[15].Location = new Point(510, 210);
            chessBlack[15].Size = new Size(40, 40);
            chessBlack[15].Region = rg;
            chessBlack[15].Tag = "Soldier_B_5";
            chessBlack[15].Type = ChessType.Soldier;
            chessBlack[15].Color = ChessColor.Black;
            chessBlack[15].Index = 15;
            chessBlack[15].Parent = this;
            #endregion

            #region Quân cờ trắng            
            chessWhite[0] = new ChessControl();
            chessWhite[0].Image = ChessWhiteImage.Images[0];
            chessWhite[0].Location = new Point(270, 570);
            chessWhite[0].Size = new Size(40, 40);
            chessWhite[0].Region = rg;
            chessWhite[0].Tag = "General_W";
            chessWhite[0].Type = ChessType.General;
            chessWhite[0].Color = ChessColor.White;
            chessWhite[0].Index = 0;
            chessWhite[0].Parent = this;

            chessWhite[1] = new ChessControl();
            chessWhite[1].Image = ChessWhiteImage.Images[3];
            chessWhite[1].Location = new Point(30, 570);
            chessWhite[1].Size = new Size(40, 40);
            chessWhite[1].Region = rg;
            chessWhite[1].Tag = "Chariot_W_1";
            chessWhite[1].Type = ChessType.Chariot;
            chessWhite[1].Color = ChessColor.White;
            chessWhite[1].Index = 1;
            chessWhite[1].Parent = this;

            chessWhite[2] = new ChessControl();
            chessWhite[2].Image = ChessWhiteImage.Images[3];
            chessWhite[2].Location = new Point(510, 570);
            chessWhite[2].Size = new Size(40, 40);
            chessWhite[2].Region = rg;
            chessWhite[2].Tag = "Chariot_W_2";
            chessWhite[2].Type = ChessType.Chariot;
            chessWhite[2].Color = ChessColor.White;
            chessWhite[2].Index = 2;
            chessWhite[2].Parent = this;

            chessWhite[3] = new ChessControl();
            chessWhite[3].Image = ChessWhiteImage.Images[5];
            chessWhite[3].Location = new Point(90, 570);
            chessWhite[3].Size = new Size(40, 40);
            chessWhite[3].Region = rg;
            chessWhite[3].Tag = "Horse_W_1";
            chessWhite[3].Type = ChessType.Horse;
            chessWhite[3].Color = ChessColor.White;
            chessWhite[3].Index = 3;
            chessWhite[3].Parent = this;

            chessWhite[4] = new ChessControl();
            chessWhite[4].Image = ChessWhiteImage.Images[5];
            chessWhite[4].Location = new Point(450, 570);
            chessWhite[4].Size = new Size(40, 40);
            chessWhite[4].Region = rg;
            chessWhite[4].Tag = "Horse_W_2";
            chessWhite[4].Type = ChessType.Horse;
            chessWhite[4].Color = ChessColor.White;
            chessWhite[4].Index = 4;
            chessWhite[4].Parent = this;

            chessWhite[5] = new ChessControl();
            chessWhite[5].Image = ChessWhiteImage.Images[2];
            chessWhite[5].Location = new Point(150, 570);
            chessWhite[5].Size = new Size(40, 40);
            chessWhite[5].Region = rg;
            chessWhite[5].Tag = "Elephant_W_1";
            chessWhite[5].Type = ChessType.Elephant;
            chessWhite[5].Color = ChessColor.White;
            chessWhite[5].Index = 5;
            chessWhite[5].Parent = this;

            chessWhite[6] = new ChessControl();
            chessWhite[6].Image = ChessWhiteImage.Images[2];
            chessWhite[6].Location = new Point(390, 570);
            chessWhite[6].Size = new Size(40, 40);
            chessWhite[6].Region = rg;
            chessWhite[6].Tag = "Elephant_W_2";
            chessWhite[6].Type = ChessType.Elephant;
            chessWhite[6].Color = ChessColor.White;
            chessWhite[6].Index = 6;
            chessWhite[6].Parent = this;

            chessWhite[7] = new ChessControl();
            chessWhite[7].Image = ChessWhiteImage.Images[1];
            chessWhite[7].Location = new Point(210, 570);
            chessWhite[7].Size = new Size(40, 40);
            chessWhite[7].Region = rg;
            chessWhite[7].Tag = "Advisor_W_1";
            chessWhite[7].Type = ChessType.Advisor;
            chessWhite[7].Color = ChessColor.White;
            chessWhite[7].Index = 7;
            chessWhite[7].Parent = this;

            chessWhite[8] = new ChessControl();
            chessWhite[8].Image = ChessWhiteImage.Images[1];
            chessWhite[8].Location = new Point(330, 570);
            chessWhite[8].Size = new Size(40, 40);
            chessWhite[8].Region = rg;
            chessWhite[8].Tag = "Advisor_W_2";
            chessWhite[8].Type = ChessType.Advisor;
            chessWhite[8].Color = ChessColor.White;
            chessWhite[8].Index = 8;
            chessWhite[8].Parent = this;

            chessWhite[9] = new ChessControl();
            chessWhite[9].Image = ChessWhiteImage.Images[4];
            chessWhite[9].Location = new Point(90, 450);
            chessWhite[9].Size = new Size(40, 40);
            chessWhite[9].Region = rg;
            chessWhite[9].Tag = "Cannon_W_1";
            chessWhite[9].Type = ChessType.Cannon;
            chessWhite[9].Color = ChessColor.White;
            chessWhite[9].Index = 9;
            chessWhite[9].Parent = this;

            chessWhite[10] = new ChessControl();
            chessWhite[10].Image = ChessWhiteImage.Images[4];
            chessWhite[10].Location = new Point(450, 450);
            chessWhite[10].Size = new Size(40, 40);
            chessWhite[10].Region = rg;
            chessWhite[10].Tag = "Cannon_W_2";
            chessWhite[10].Type = ChessType.Cannon;
            chessWhite[10].Color = ChessColor.White;
            chessWhite[10].Index = 10;
            chessWhite[10].Parent = this;

            chessWhite[11] = new ChessControl();
            chessWhite[11].Image = ChessWhiteImage.Images[6];
            chessWhite[11].Location = new Point(30, 390);
            chessWhite[11].Size = new Size(40, 40);
            chessWhite[11].Region = rg;
            chessWhite[11].Tag = "Soldier_W_1";
            chessWhite[11].Type = ChessType.Soldier;
            chessWhite[11].Color = ChessColor.White;
            chessWhite[11].Index = 11;
            chessWhite[11].Parent = this;

            chessWhite[12] = new ChessControl();
            chessWhite[12].Image = ChessWhiteImage.Images[6];
            chessWhite[12].Location = new Point(150, 390);
            chessWhite[12].Size = new Size(40, 40);
            chessWhite[12].Region = rg;
            chessWhite[12].Tag = "Soldier_W_2";
            chessWhite[12].Type = ChessType.Soldier;
            chessWhite[12].Color = ChessColor.White;
            chessWhite[12].Index = 12;
            chessWhite[12].Parent = this;

            chessWhite[13] = new ChessControl();
            chessWhite[13].Image = ChessWhiteImage.Images[6];
            chessWhite[13].Location = new Point(270, 390);
            chessWhite[13].Size = new Size(40, 40);
            chessWhite[13].Region = rg;
            chessWhite[13].Tag = "Soldier_W_3";
            chessWhite[13].Type = ChessType.Soldier;
            chessWhite[13].Color = ChessColor.White;
            chessWhite[13].Index = 13;
            chessWhite[13].Parent = this;

            chessWhite[14] = new ChessControl();
            chessWhite[14].Image = ChessWhiteImage.Images[6];
            chessWhite[14].Location = new Point(390, 390);
            chessWhite[14].Size = new Size(40, 40);
            chessWhite[14].Region = rg;
            chessWhite[14].Tag = "Soldier_W_4";
            chessWhite[14].Type = ChessType.Soldier;
            chessWhite[14].Color = ChessColor.White;
            chessWhite[14].Index = 14;
            chessWhite[14].Parent = this;

            chessWhite[15] = new ChessControl();
            chessWhite[15].Image = ChessWhiteImage.Images[6];
            chessWhite[15].Location = new Point(510, 390);
            chessWhite[15].Size = new Size(40, 40);
            chessWhite[15].Region = rg;
            chessWhite[15].Tag = "Soldier_W_5";
            chessWhite[15].Type = ChessType.Soldier;
            chessWhite[15].Color = ChessColor.White;
            chessWhite[15].Index = 15;
            chessWhite[15].Parent = this;
            #endregion

            #region Add event cho các quân cờ
            for (int i = 0; i < 16; i++)
            {
                chessWhite[i].MouseDown += new MouseEventHandler(Chess_MouseDown);
                chessWhite[i].MouseMove += new MouseEventHandler(Chess_MouseMove);
                chessWhite[i].MouseUp += new MouseEventHandler(Chess_MouseUp);

                chessBlack[i].MouseDown += new MouseEventHandler(Chess_MouseDown);
                chessBlack[i].MouseMove += new MouseEventHandler(Chess_MouseMove);
                chessBlack[i].MouseUp += new MouseEventHandler(Chess_MouseUp);
            }
            #endregion
            
            #region Chổ nào có quân cờ, chỗ đó là True
            for (int i = 0; i < 90; i++)
            {
                HasChess[i] = false;
                cColor[i] = ChessColor.Default;
                WB[i] = "";
            }
            //Vị trí đặt quân cờ Đen
            HasChess[0] = true; cColor[0] = ChessColor.Black; WB[0] = "Chariot_B_1";
            HasChess[1] = true; cColor[1] = ChessColor.Black; WB[1] = "Horse_B_1";
            HasChess[2] = true; cColor[2] = ChessColor.Black; WB[2] = "Elephant_B_1";
            HasChess[3] = true; cColor[3] = ChessColor.Black; WB[3] = "Advisor_B_1";
            HasChess[4] = true; cColor[4] = ChessColor.Black; WB[4] = "General_B";
            HasChess[5] = true; cColor[5] = ChessColor.Black; WB[5] = "Advisor_B_2";
            HasChess[6] = true; cColor[6] = ChessColor.Black; WB[6] = "Elephant_B_2";
            HasChess[7] = true; cColor[7] = ChessColor.Black; WB[7] = "Horse_B_2";
            HasChess[8] = true; cColor[8] = ChessColor.Black; WB[8] = "Chariot_B_2";
            HasChess[19] = true; cColor[19] = ChessColor.Black; WB[19] = "Cannon_B_1";
            HasChess[25] = true; cColor[25] = ChessColor.Black; WB[25] = "Cannon_B_2";
            HasChess[27] = true; cColor[27] = ChessColor.Black; WB[27] = "Soldier_B_1";
            HasChess[29] = true; cColor[29] = ChessColor.Black; WB[29] = "Soldier_B_2";
            HasChess[31] = true; cColor[31] = ChessColor.Black; WB[31] = "Soldier_B_3";
            HasChess[33] = true; cColor[33] = ChessColor.Black; WB[33] = "Soldier_B_4";
            HasChess[35] = true; cColor[35] = ChessColor.Black; WB[35] = "Soldier_B_5";
            //Vị trí đặt quân cờ Trắng
            HasChess[54] = true; cColor[54] = ChessColor.White; WB[54] = "Soldier_W_1";
            HasChess[56] = true; cColor[56] = ChessColor.White; WB[56] = "Soldier_W_2";
            HasChess[58] = true; cColor[58] = ChessColor.White; WB[58] = "Soldier_W_3";
            HasChess[60] = true; cColor[60] = ChessColor.White; WB[60] = "Soldier_W_4";
            HasChess[62] = true; cColor[62] = ChessColor.White; WB[62] = "Soldier_W_5";
            HasChess[64] = true; cColor[64] = ChessColor.White; WB[64] = "Cannon_W_1";
            HasChess[70] = true; cColor[70] = ChessColor.White; WB[70] = "Cannon_W_2";
            HasChess[81] = true; cColor[81] = ChessColor.White; WB[81] = "Chariot_W_1";
            HasChess[82] = true; cColor[82] = ChessColor.White; WB[82] = "Horse_W_1";
            HasChess[83] = true; cColor[83] = ChessColor.White; WB[83] = "Elephant_W_1";
            HasChess[84] = true; cColor[84] = ChessColor.White; WB[84] = "Advisor_W_1";
            HasChess[85] = true; cColor[85] = ChessColor.White; WB[85] = "General_W";
            HasChess[86] = true; cColor[86] = ChessColor.White; WB[86] = "Advisor_W_2";
            HasChess[87] = true; cColor[87] = ChessColor.White; WB[87] = "Elephant_W_2";
            HasChess[88] = true; cColor[88] = ChessColor.White; WB[88] = "Horse_W_2";
            HasChess[89] = true; cColor[89] = ChessColor.White; WB[89] = "Chariot_W_2";
            #endregion            
        }        
        private void Chess_MouseDown(object sender, MouseEventArgs e)
        {
            if (nguoixem == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    ChessControl ch = (ChessControl)sender;
                    ch.BringToFront();
                    startDragPoint = e.Location;
                    chessPoint = ch.Location;
                    _OriginPoint = new Point(chessPoint.X + 20, chessPoint.Y + 20);
                }
                else
                    return;
            }
            else
            {
                MessageBox.Show("Bạn đang là người xem. \n Bạn không có quyền đi");
            }
        }
        private void Chess_MouseMove(object sender, MouseEventArgs e)
        {
           
                ChessControl ch = (ChessControl)sender;
                //if (e.Button == MouseButtons.Left && !pausegame)
                if (e.Button == MouseButtons.Left)
                {
                    ch.Location = new Point(ch.Left + e.X - startDragPoint.X, ch.Top + e.Y - startDragPoint.Y);
                }
                else
                    return;
           
        }        
        private void Chess_MouseUp(object sender, MouseEventArgs e)
        {
            
            
            if (e.Button == MouseButtons.Right)
                return;
            ChessControl cControl = (ChessControl)sender;

            //Tranh truong hop chua ket noi ma choi.
            //if (net.Connection ==StateConnection.Breaken || player == ChessColor.Default)
            if (player == ChessColor.Default)
            {
                cControl.Location = chessPoint;
                return;
            }            
            m_X1 = cControl.Location.X;
            m_Y1 = cControl.Location.Y;
            draw.SnapToPoint(ref m_X1, ref m_Y1);
            m_X2 = m_X1;
            m_Y2 = m_Y1;
            Point newpoint = new Point(m_X2, m_Y2);
            //Nếu Point cu~ == Point mới ==> ko làm gì hết.
            if (newpoint == _OriginPoint || draw.CheckPoint(newpoint)==false)
            {
                cControl.Location = chessPoint;
                return;
            }            
            //2 index này là tính trên cả bàn cờ 0->89
            int indexNew = draw.MappingPointToIndex(newpoint);
            int indexOld = draw.MappingPointToIndex(_OriginPoint);
            //index này thì trả về index của ChessControl tương ứng với 2 mảng chessBlack & chessWhite 0->15
            int nIndex = draw.MappingPointToIndex(newpoint);
            int cIndex = -1;                      
            if (draw.CheckPoint(newpoint))
                cIndex = this[new Point(newpoint.X - 20, newpoint.Y - 20)];
            #region Khi 2 Tướng đối mặt nhau
            int bGIndex = cControl.Color == ChessColor.Black ? indexNew : (int)GeneralInfo("General_B", Info.Index);
            int wGIndex = cControl.Color == ChessColor.White ? indexNew : (int)GeneralInfo("General_W", Info.Index);
            #endregion
            opposite = GameAI.GeneralOpposition(HasChess, bGIndex, wGIndex);            
            Checked move = CheckMoveValid(cControl.Index, indexNew, indexOld, nIndex, newpoint, _OriginPoint, (int)(cControl.Color));
            if (move != Checked.MoveAndEated)
            {
                cControl.Location = chessPoint;
                return;
            }
            else
            {
                int kt_thang=0;
                //kiem tra thang thua trong tran dau.
                

                switch (player)
                {
                    case ChessColor.Black://Black
                        {
                           color = (int)cControl.Color;
                            if (color != 1)
                            {
                                
                                cControl.Location = chessPoint;
                                MessageBox.Show("Không được đi");
                                return;
 
                            }
                            

                            if (cControl.Type == ChessType.General)
                                if (opposite)
                                {
                                    MessageBox.Show("Đối Mặt Tướng .\nBạn Không Thể Đi Quân Này...", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    cControl.Location = chessPoint;
                                    return;
                                }
                           //////// player = ChessColor.Default;
                           
                           

                            //MessageBox.Show("a :" + color);
                            if (color == 1)
                            {
                                //MessageBox.Show("b :" + color);
                                Process(cControl.Index, indexNew, indexOld, nIndex, newpoint, _OriginPoint, color);
                                amthanh("file_sound2");
                                sonuocdi_player1++;

                                if (luatchoi == "Quốc Tế")
                                {
                                    
                                    if (timetong_player2 > 0 && sonuocdi_player1 >=10)
                                    {
                                            sonuocdi_player1 = 0;
                                            timetong_player2 = timetong_player2 + 900;
                                            lbl_timetong_player1.Text = (timetong_player2 / 60).ToString() + "'";
                                            lbl_timegiay_player1.Text = (timetong_player2 % 60).ToString();
                                       

                                    }
                                }









                                lbl_player1_ss.Text = "";
                                lbl_player2_ss.Text = "Đang đánh...";

                                player = ChessColor.Default;
                                
                            }
                            //player = ChessColor.Default;
                           // MessageBox.Show("Xu ly Den :" + color);


                            int ct = Convert.ToInt32(KiemTraChieuTuong(cControl.Color));// MessageBox.Show("Chiếu tướng nè");
                            #region Khi 2 Tướng đối mặt nhau
                            int bGeneralIndex = (int)GeneralInfo("General_B", Info.Index);
                            int wGeneralIndex = (int)GeneralInfo("General_W", Info.Index);
                            #endregion

                            if (GameAI.GeneralOpposition(HasChess, bGeneralIndex, wGeneralIndex))
                            {
                                MessageBox.Show("Đối Mặt Tướng .\nBạn Không Thể Đi Quân Này...", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cControl.Location = chessPoint;
                                HasChess[indexNew] = false;
                                HasChess[indexOld] = true;
                                cColor[indexNew] = ChessColor.Default;
                                cColor[indexOld] = cControl.Color;
                                WB[indexOld] = cControl.Tag.ToString();
                                WB[indexNew] = "";
                                player = ChessColor.Black;
                                return;
                            }
                            else
                            {
                                //xac dinh gia tri kt_thang
                                if ((int)GeneralInfo("General_B", Info.Index) == -1)
                                {
                                    kt_thang = 1;                                  
                                }
                                if ((int)GeneralInfo("General_W", Info.Index) == -1)
                                {
                                    kt_thang = 2;                                   
                                }


                                if (color == 1)
                                {
                                    
                                    string data_dico1;
                                    data_dico1 = indexNew.ToString() + "," + indexOld.ToString() + "," + nIndex.ToString() + ",1" + "," + cIndex.ToString() + "," + newpoint.X.ToString() + "," + newpoint.Y.ToString() + "," + _OriginPoint.X.ToString() + "," + _OriginPoint.Y.ToString() + "," + cControl.Index.ToString() + "," + ct.ToString() + "," + color.ToString() + "," + kt_thang + "," + ctr_time + "," + timetong_player2; 
                                    guitin("gui_dicotuong.php", "Di_CoTuong", lbl_player1.Text, lbl_vitriban.Text, "2", data_dico1);

                                    ctr_time = 0;
                                    color = 100;
                                    //player = ChessColor.Default;
                                }

                            }
                           //if (me == ChessColor.Default)
                                //me = ChessColor.Black;

                        } break;
                    case ChessColor.White://White
                        
                        {
                            int color = (int)cControl.Color;
                            if (color != 2)
                            {
                                
                                cControl.Location = chessPoint;
                                MessageBox.Show("Không được đi");
                                return;

                            }
                            if (cControl.Type == ChessType.General)
                                if (opposite)
                                {

                                    MessageBox.Show("Đối Mặt Tướng .\nBạn Không Thể Đi Quân Này...", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    cControl.Location = chessPoint;
                                    return;
                                }
                            //player = ChessColor.Default;
                            //player = ChessColor.Default;
                            

                          
                            
                            if (color == 2)
                            {
                                Process(cControl.Index, indexNew, indexOld, nIndex, newpoint, _OriginPoint, color);

                                lbl_player1_ss.Text = "";
                                lbl_player2_ss.Text = "Đang đánh...";

                                amthanh("file_sound2");
                                sonuocdi_player2++;



                                if (luatchoi == "Quốc Tế")
                                {

                                    if (timetong_player1 > 0 && sonuocdi_player2 >= 10)
                                    {
                                        sonuocdi_player2 = 0;
                                        timetong_player1 = timetong_player1 + 900;
                                        lbl_timetong_player1.Text = (timetong_player1 / 60).ToString() + "'";
                                        lbl_timegiay_player1.Text = (timetong_player1 % 60).ToString();


                                    }
                                }







                                player = ChessColor.Default;
                            }

                           // MessageBox.Show("Xu ly White" + color);


                            int ct = Convert.ToInt32(KiemTraChieuTuong(cControl.Color));
                            #region Khi 2 Tướng đối mặt nhau
                            int bGeneralIndex = (int)GeneralInfo("General_B", Info.Index);
                            int wGeneralIndex = (int)GeneralInfo("General_W", Info.Index);
                            #endregion                         
                            if (GameAI.GeneralOpposition(HasChess, bGeneralIndex, wGeneralIndex))
                            {
                                MessageBox.Show("Đối Mặt Tướng .\nBạn Không Thể Đi Quân Này...", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cControl.Location = chessPoint;
                                HasChess[indexNew] = false;
                                HasChess[indexOld] = true;
                                cColor[indexNew] = ChessColor.Default;
                                cColor[indexOld] = cControl.Color;
                                WB[indexOld] = cControl.Tag.ToString();
                                WB[indexNew] = "";
                                player = ChessColor.White;
                                return;
                            }
                            else
                            {
                                //xac dinh gia tri kt_thang
                                if ((int)GeneralInfo("General_B", Info.Index) == -1)
                                {
                                    kt_thang = 1;
                                }
                                if ((int)GeneralInfo("General_W", Info.Index) == -1)
                                {
                                    kt_thang = 2;
                                }

                                if (color == 2)
                                {
                                    
                                    string data_dico2;
                                    data_dico2 = indexNew.ToString() + "," + indexOld.ToString() + "," + nIndex.ToString() + ",2" + "," + cIndex.ToString() + "," + newpoint.X.ToString() + "," + newpoint.Y.ToString() + "," + _OriginPoint.X.ToString() + "," + _OriginPoint.Y.ToString() + "," + cControl.Index.ToString() + "," + ct.ToString() + "," + color.ToString() + "," + kt_thang + "," + ctr_time + "," + timetong_player1;
                                    guitin("gui_dicotuong.php", "Di_CoTuong", lbl_player1.Text, lbl_vitriban.Text, "2", data_dico2);

                                    ctr_time = 0;
                                    color = 100;
                                    //player = ChessColor.Default;
                                }
                            }
                            //if (me == ChessColor.Default)
                                //me = ChessColor.White;
                        } 
                        break;
                    default:
                        cControl.Location = chessPoint;
                        break;
                }
            }
            if ((int)GeneralInfo("General_B", Info.Index) == -1)
            {
               
                ctr_time = 0;
                
                if (luatchoi != "Tự Do")
                {
                    lbl_player1_ss.Text = "Thắng";
                    lbl_player2_ss.Text = "Thua";
                    diemthangthua_1.Text = "+" + diemthanggame.ToString();
                    diemthangthua_2.Text = "-" + diemthuagame.ToString();
                    btn_xinthua.Enabled = false;
                    btn_xinhoa.Enabled = false;

                    btn_sansang.Enabled = true;
                    //check_bt_ss = 0;
                    //is_playing = false;
                    //user thang
                    thang1++;
                    kinhnghiem1 += diemthanggame;
                    tt_clearchat.SetToolTip(pb_player1, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                }


                string page = url + "/webservice/gamecotuong/" + "guidulieu_cotuong.php";  //khai bao trang web trang web service.
                string vars = "?cmd=Thang_CoTuong";
                vars += "&gui=" + Form_Main.user + "&iduser=" + Form_Main.user_id + "&idban=" + vitriban + "&userthua=" + lbl_player2.Text + "&dulieu=Thang_CoTuong";
               // MessageBox.Show(page + vars);
                start_get(page, vars);

                player = ChessColor.Default;






               
                amthanh("file_sound4");

                 MessageBox.Show("Chúc Mừng! Bạn Đã Thắng.\n Click Bàn Mới Để Chơi Lại...");
                 
                 btn_banmoi.Enabled = true;
                 btn_xinthua.Enabled = false;
                 btn_xinhoa.Enabled = false;
            }
            if ((int)GeneralInfo("General_W", Info.Index) == -1)
            {

                ctr_time = 0;
                if (luatchoi != "Tự Do")
                {
                    lbl_player1_ss.Text = "Thắng";
                    lbl_player2_ss.Text = "Thua";
                    diemthangthua_1.Text = "+" + diemthanggame.ToString();
                    diemthangthua_2.Text = "-" + diemthuagame.ToString();

                    btn_xinthua.Enabled = false;
                    btn_xinhoa.Enabled = false;


                    btn_sansang.Enabled = true;
                    //check_bt_ss = 0;
                    //is_playing = false;
                    //user thang
                    thang1++;
                    kinhnghiem1 += diemthanggame;
                    tt_clearchat.SetToolTip(pb_player1, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);
                }




                string page = url + "/webservice/gamecotuong/" + "guidulieu_cotuong.php";  //khai bao trang web trang web service.
                string vars = "?cmd=Thang_CoTuong";
               // vars += "&gui=" + Form_Main.user + "&iduser=" + Form_Main.user_id + "&idban=" + vitriban + "&dulieu=Thang_CoTuong";
                vars += "&gui=" + Form_Main.user + "&iduser=" + Form_Main.user_id + "&idban=" + vitriban + "&userthua=" + lbl_player2.Text + "&dulieu=Thang_CoTuong";
                //MessageBox.Show(page + vars);
                start_get(page, vars);

                




                 amthanh("file_sound4");
                 MessageBox.Show("Chúc Mừng! Bạn Đã Thắng.\n Click Bàn Mới Để Chơi Lại...");
                 btn_banmoi.Enabled = true;
                 btn_xinthua.Enabled = false;
                 btn_xinhoa.Enabled = false;

                 player = ChessColor.Default;


                 

               
            }

          
            this.Cursor = Cursors.Default;                        
        }        
        private delegate void ProcessDelegate(int cControlIndex, int indexNew, int indexOld, int nIndex, Point newpoint, Point OriginPoint, int color);        
        public void Process(int cControlIndex, int indexNew, int indexOld, int nIndex, Point newpoint, Point OriginPoint, int color)
        {

            if (InvokeRequired)
            {
                Invoke(new ProcessDelegate(Process), cControlIndex, indexNew, indexOld, nIndex, newpoint, OriginPoint, color);
                return;
            }            
            ChessControl cControl = new ChessControl();
            if (color == 1)
                cControl = chessBlack[cControlIndex];
            else if (color == 2)
                cControl = chessWhite[cControlIndex];
            else
                return;                   
            #region if process
            if (draw.CheckPoint(newpoint))
            {
                switch (cControl.Type)
                {
                    case ChessType.General://Tướng
                        {
                            Pawns general = new General(newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                            bool ok = general.CheckIndex();
                            eated = general.CheckEated(cColor[nIndex]);
                            if (ok)
                            {
                                if (HasChess[indexNew] == false)
                                {
                                    this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, false);
                                }
                                else if (HasChess[indexNew] == true)
                                {
                                    if (eated)
                                    {
                                        this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, true);
                                    }
                                    else
                                        cControl.Location = chessPoint;
                                }
                            }
                            else
                                cControl.Location = chessPoint;
                        }
                        break;
                    case ChessType.Advisor://Sĩ
                        {
                            Pawns advisor = new Advisor(indexNew, indexOld, cControl.Color);
                            bool ok = advisor.CheckIndex();
                            eated = advisor.CheckEated(cColor[nIndex]);
                            if (ok)
                            {
                                if (HasChess[indexNew] == false)
                                {
                                    this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, false);
                                }
                                else if (HasChess[indexNew] == true)
                                {
                                    if (eated)
                                    {                                        
                                        this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, true);                                                                               
                                    }
                                    else
                                        cControl.Location = chessPoint;
                                }
                            }
                            else
                                cControl.Location = chessPoint;
                        }
                        break;
                    case ChessType.Elephant://Tượng.........................
                        {
                            Pawns elephant = new Elephant(HasChess, indexNew, indexOld, cControl.Color);
                            bool ok = elephant.CheckIndex();
                            eated = elephant.CheckEated(cColor[nIndex]);
                            if (ok)
                            {
                                if (HasChess[indexNew] == false)
                                {
                                    this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, false);
                                }
                                else if (HasChess[indexNew] == true)
                                {
                                    if (eated)
                                    {                                        
                                        this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, true);                                                                                   
                                    }
                                    else
                                        cControl.Location = chessPoint;
                                }
                            }
                            else
                                cControl.Location = chessPoint;
                        }
                        break;
                    case ChessType.Chariot://Xe
                        {
                            Pawns chariot = new Chariot(HasChess, newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                            bool ok = chariot.CheckPoint();
                            bool ok2 = chariot.CheckIndex();
                            eated = chariot.CheckEated(cColor[nIndex]);
                            if (ok)
                            {
                                if (ok2)
                                {
                                    if (HasChess[indexNew] == false)
                                    {
                                        this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, false);
                                    }
                                    else if (HasChess[indexNew] == true)
                                    {
                                        if (eated)
                                        {                                            
                                            this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, true);
                                        }
                                        else
                                            cControl.Location = chessPoint;
                                    }
                                }
                                else
                                    cControl.Location = chessPoint;
                            }
                            else
                                cControl.Location = chessPoint;
                        }
                        break;
                    case ChessType.Cannon://Pháo
                        {
                            Pawns cannon = new Cannon(HasChess, newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                            bool ok = cannon.CheckIndex();
                            bool ok2 = cannon.CheckPoint();
                            eated = cannon.CheckEated(cColor[nIndex]);
                            if (ok2)
                            {
                                if (HasChess[indexNew] == false)
                                {
                                    if (ok)
                                    {
                                        this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, false);
                                    }
                                    else
                                        cControl.Location = chessPoint;
                                }
                                else if (HasChess[indexNew] == true)
                                {
                                    if (eated)
                                    {                                        
                                        this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, true);
                                    }
                                    else
                                        cControl.Location = chessPoint;
                                }
                            }
                            else
                                cControl.Location = chessPoint;
                        }
                        break;
                    case ChessType.Horse://Mã
                        {
                            Pawns horse = new Horse(HasChess, newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                            bool ok = horse.CheckIndex();
                            bool ok2 = horse.CheckPoint();
                            eated = horse.CheckEated(cColor[nIndex]);

                            if (ok && ok2)
                            {
                                if (HasChess[indexNew] == false)
                                {
                                    this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, false);
                                }
                                else if (HasChess[indexNew] == true)
                                {
                                    if (eated)
                                    {                                        
                                        this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, true);
                                    }
                                    else
                                        cControl.Location = chessPoint;
                                }
                            }
                            else
                                cControl.Location = chessPoint;
                        }
                        break;
                    case ChessType.Soldier://Chốt
                        {
                            Pawns soldier = new Soldier(HasChess, newpoint, OriginPoint, indexNew, indexOld, cControl.Color);
                            bool ok2 = soldier.CheckPoint();
                            eated = soldier.CheckEated(cColor[nIndex]);
                            if (ok2)//Nếu vị trí đi là OK...
                            {
                                if (HasChess[indexNew] == false)//...và nếu vị trí mới CHƯA CÓ quân cờ nào được đặt
                                {
                                    this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, false);
                                }
                                else if (HasChess[indexNew] == true)//...và nếu vị trí mới ĐÃ CÓ quân cờ nào được đặt...
                                {
                                    if (eated)//...Nếu vị trí mới có quân cờ và khác màu thì ĂN NÓ
                                    {                                        
                                        this.MoveChess(cControl, new Point(newpoint.X - 20, newpoint.Y - 20), indexNew, indexOld, true);
                                    }
                                    else//...Còn nếu cùng màu thì quay lại vị trí cũ
                                        cControl.Location = chessPoint;
                                }
                            }
                            else
                                cControl.Location = chessPoint;
                        }
                        break;
                    default:
                        break;
                }

            }
            else
                cControl.Location = chessPoint;
            #endregion
        }
        //Duy chuyển cờ đến vị trí mới
        private delegate void MoveChessDelegate(ChessControl cControl, Point newpoint, int indexNew, int indexOld, bool eated);
        private void MoveChess(ChessControl cControl, Point newpoint, int indexNew, int indexOld, bool eated)
        {              
            ///
            if (InvokeRequired)
            {
                Invoke(new MoveChessDelegate(MoveChess), cControl, newpoint, indexNew, indexOld, eated);
                return;
            }
            if (eated)
            {                                                               
                this.Controls.Remove(cControl.Color == ChessColor.Black ? chessWhite[this[new Point(newpoint.X, newpoint.Y)]] : chessBlack[this[new Point(newpoint.X, newpoint.Y)]]);
            }
            cControl.Location = newpoint;
            HasChess[indexNew] = true;
            HasChess[indexOld] = false;
            cColor[indexNew] = cControl.Color;            
            cColor[indexOld] = ChessColor.Default;
            WB[indexOld] = "";
            WB[indexNew] = cControl.Tag.ToString();

            this.Refresh();
            draw.DrawRect(this.CreateGraphics(), new Point(newpoint.X + 20, newpoint.Y + 20));
            return;
        }        
        //Lấy chỉ số index của 2 con tướng
        private object GeneralInfo(string name,Info info)
        {
            switch (info)
            {
                case Info.Index://Tra ve Index
                    {
                        switch (name)
                        {
                            case "General_B":
                                {
                                    for (int i = 0; i < 90; i++)
                                        if (WB[i] == name)
                                            return i;
                                } break;
                            case "General_W":
                                {
                                    for (int i = 0; i < 90; i++)
                                        if (WB[i] == name)
                                            return i;
                                } break;
                            default: break;
                        }
                    } break;
                case Info.Point://Tra ve Vi tri
                    {
                        switch (name)
                        {
                            case "General_B":
                                {
                                    for (int i = 0; i < Controls.Count; i++)
                                    {
                                        if (Controls[i] is ChessControl)
                                        {
                                            if (((ChessControl)Controls[i]).Tag.ToString() == "General_B")
                                            {
                                                return ((ChessControl)Controls[i]).Location;
                                            }
                                        }
                                    }
                                } break;
                            case "General_W":
                                {
                                    for (int i = 0; i < Controls.Count; i++)
                                    {
                                        if (Controls[i] is ChessControl)
                                        {
                                            if (((ChessControl)Controls[i]).Tag.ToString() == "General_W")
                                            {
                                                return ((ChessControl)Controls[i]).Location;
                                            }
                                        }
                                    }
                                } break;
                            default: break;
                        }
                    } break;
            }
         
            return -1;
        }
        //Khi mình đi 1 con cờ, thì kiểm tra xem có chiếu tướng đối phương không ???
        private bool KiemTraChieuTuong(ChessColor color)
        {
            #region Khi 2 Tướng đối mặt nhau
            int bGeneralIndex = (int)GeneralInfo("General_B", Info.Index);
            int wGeneralIndex = (int)GeneralInfo("General_W", Info.Index);
            if (GameAI.GeneralOpposition(HasChess, bGeneralIndex, wGeneralIndex))
                return true;                   
            #endregion            
            Point bGeneralPoint;
            Point wGeneralPoint;
            try
            {
                bGeneralPoint = (Point)GeneralInfo("General_B", Info.Point);
                wGeneralPoint = (Point)GeneralInfo("General_W", Info.Point);
            }
            catch
            {
                return false;
            }

            bool checkmate = false;
            switch (color)
            {
                case ChessColor.Black:
                    {
                        for (int i = 0; i < Controls.Count; i++)
                        {
                            if (Controls[i] is ChessControl)
                            {
                                if (((ChessControl)Controls[i]).Color == color)
                                {                                    
                                    checkmate = GameAI.CheckMate(((ChessControl)Controls[i]).Type, ((ChessControl)Controls[i]).Color, wGeneralPoint, ((ChessControl)Controls[i]).Location, wGeneralIndex, draw.MappingPointToIndex(new Point(((ChessControl)Controls[i]).Location.X + 20, ((ChessControl)Controls[i]).Location.Y + 20)));
                                }
                            }
                            if (checkmate)
                                return checkmate;
                        }
                    } break;
                case ChessColor.White:
                    {
                        for (int i = 0; i < Controls.Count; i++)
                        {
                            if (Controls[i] is ChessControl)
                            {
                                if (((ChessControl)Controls[i]).Color == color)
                                {
                                    checkmate = GameAI.CheckMate(((ChessControl)Controls[i]).Type, ((ChessControl)Controls[i]).Color, bGeneralPoint, ((ChessControl)Controls[i]).Location, bGeneralIndex, draw.MappingPointToIndex(new Point(((ChessControl)Controls[i]).Location.X + 20, ((ChessControl)Controls[i]).Location.Y + 20)));
                                }
                            }
                            if (checkmate)
                                return checkmate;
                        }
                    } break;
                default:
                    break;
            }
            return false;
        }
        //Khi mình đi 1 con cờ, thì kiểm tra xem mình có đang bị chiếu tướng không ???. hàm này chuyền thông số ngược với hàm trên 
        private bool KiemTraBiChieuTuong(ChessColor color)
        {
            #region Khi 2 Tướng đối mặt nhau
            int bGeneralIndex = (int)GeneralInfo("General_B", Info.Index);
            int wGeneralIndex = (int)GeneralInfo("General_W", Info.Index);

            if (GameAI.GeneralOpposition(HasChess, bGeneralIndex, wGeneralIndex))
                return true;

            #endregion
            Point bGeneralPoint;
            Point wGeneralPoint;
            try
            {
                bGeneralPoint = (Point)GeneralInfo("General_B", Info.Point);
                wGeneralPoint = (Point)GeneralInfo("General_W", Info.Point);
            }
            catch
            {
                return false;
            }

            bool checkmate = false;
            switch (color)
            {
                case ChessColor.Black:
                    {
                        for (int i = 0; i < Controls.Count; i++)
                        {
                            if (Controls[i] is ChessControl)
                            {
                                if (((ChessControl)Controls[i]).Color != color )
                                {
                                    if (bGeneralPoint.X == ((ChessControl)Controls[i]).Location.X || bGeneralPoint.Y == ((ChessControl)Controls[i]).Location.Y)
                                        checkmate = GameAI.CheckMate(((ChessControl)Controls[i]).Type, ((ChessControl)Controls[i]).Color, bGeneralPoint, ((ChessControl)Controls[i]).Location, bGeneralIndex, draw.MappingPointToIndex(new Point(((ChessControl)Controls[i]).Location.X + 20, ((ChessControl)Controls[i]).Location.Y + 20)));
                                }
                            }
                            if (checkmate)
                                return checkmate;
                        }
                    } break;
                case ChessColor.White:
                    {
                        for (int i = 0; i < Controls.Count; i++)
                        {
                            if (Controls[i] is ChessControl)
                            {
                                if (((ChessControl)Controls[i]).Color != color)
                                {
                                    if (wGeneralPoint.X == ((ChessControl)Controls[i]).Location.X || wGeneralPoint.Y == ((ChessControl)Controls[i]).Location.Y)
                                        checkmate = GameAI.CheckMate(((ChessControl)Controls[i]).Type, ((ChessControl)Controls[i]).Color, wGeneralPoint, ((ChessControl)Controls[i]).Location, wGeneralIndex, draw.MappingPointToIndex(new Point(((ChessControl)Controls[i]).Location.X + 20, ((ChessControl)Controls[i]).Location.Y + 20)));
                                }
                            }
                            if (checkmate)
                                return checkmate;
                        }
                    } break;
                default:
                    break;
            }
            return false;
        }
        private delegate void NewGameDelegate();
        public void NewGame()
        {          
            /*
            pausegame = false;
            if (InvokeRequired)
            {
                Invoke(new NewGameDelegate(NewGame));
                return;
            }

            for (int i = 0; i < 16; i++)
            {
                Controls.Remove(chessWhite[i]);
                Controls.Remove(chessBlack[i]);
            }

            Graphics dc = this.CreateGraphics();
            dc.SmoothingMode = SmoothingMode.AntiAlias;
            draw = new Presenter(dc);
            DatQuanCo();
            Random rnd = new Random();
            p = rnd.Next(0, 2);
            if (p == 1)
            {
                player = ChessColor.Black;
            }
            else
            {
                player = ChessColor.White;                
            }        
             * */

            pausegame = false;
            if (InvokeRequired)
            {
                Invoke(new NewGameDelegate(NewGame));
                return;
            }
            for (int i = 0; i < 16; i++)
            {
                Controls.Remove(chessWhite[i]);
                Controls.Remove(chessBlack[i]);
            }
            Graphics dc = this.CreateGraphics();
            dc.SmoothingMode = SmoothingMode.AntiAlias;
            draw = new Presenter(dc);
            DatQuanCo();


            // lbl_player1_ss.Text = "";
            //lbl_player2_ss.Text = "";

          

            player = ChessColor.Default;
            ctr_time = 0;
            btn_sansang.Enabled = true;
            btn_xinthua.Enabled = false;
            btn_xinhoa.Enabled = false;
            
            cb_luat.Enabled = true;
            Refresh();
        }

////////////////////////////////////////////////////////

        // CAC HAM SU KIEN //////////////////////////////////
       
        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            ////////if (net.Connection == StateConnection.Connected)
           //// {
                NewGame();
              /////  net.sendPacket("N");
                pausegame = false;
                Refresh();
           ///// }
           ///// else
         /// ///  {
                /////MessageBox.Show("Bạn Chưa Kết Nối !.\nHoặc Bị Mất Kết Nối.!!\nXin Hãy Kết Nối Lại!!!.:)");
            /////}
        }
        private void buttonStartServer_Click(object sender, EventArgs e)
        {
            /* mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
            if (net.Connection == StateConnection.Breaken)
            {
                net.Listen();
                pausegame = false;
            }
           mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm **/
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            /* mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm
            if (net.Connection == StateConnection.Connecting || net.Connection == StateConnection.Breaken)
            {
                net.ConnectTo(textBoxIpAddress.Text);
                setEnable(false);
                pausegame = false;
            }
            mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm **/
        }                                  
        private void linkLabel_uit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://uit.edu.vn");
        }

       
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxMessage.Text != "")
            {
                guitin("guidulieu_cotuong.php", "Chat_CoTuong", lbl_player1.Text, lbl_vitriban.Text, "2", textBoxMessage.Text.Trim());

                string s = "Tôi : " + textBoxMessage.Text.Trim() + "";
                txt_ndnhan.AppendText(s + (char)13 + (char)10);
                textBoxMessage.Text = "";
            }



        }                       
        
////////////////////////////////////////////////////////

        // CLOSE GAME////////////////////////////////////
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
      
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (nguoixem == false)
            {
                //MessageBox.Show("toadoban" + toadoban);
                classxulynut.xl_userthoat_formgame(cotuong.BTN_CoTuong,cotuong.Ban_CoTuong, cotuong.LBL_CoTuongTen, vitrinut,vitriban);

                string page = url + "/webservice/gamecotuong/guidulieu_cotuong.php";  //khai bao trang web trang web service.
                string vars = "?cmd=Thoat_CoTuong";
                vars += "&gui=" + lbl_player1.Text + "&idban=" + lbl_vitriban.Text + "&dulieu=" + vitrinut + "&game_id=2";
                start_get(page, vars);
            }
            
            this.Close();
            
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("1. Click vào Server để làm Server và chờ kết nối từ Client .\n2. Nếu là Client , nhập địa chỉ ip của Server vào và nhấn Connect .\n3. Khi đã có thông bao kết nối thành công là có thể bắt đầu trò chơi .\n4. Ai đi trước đều được.\n5. UIT", "Game Cờ Tướng - Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_showpanel_Click(object sender, EventArgs e)
        {
            if (btn_showpanel.Text == ">>>")
            {
                panelRight.Hide();
                MaximumSize = MinimumSize = Size = curSize = new Size(585, 615);
                btn_showpanel.Text = "<<<";
            }
            else
            {
                panelRight.Show();
                //                MaximumSize = MinimumSize = Size = curSize = new Size(770, 615);
                MaximumSize = MinimumSize = Size = curSize = new Size(890, 615);
                btn_showpanel.Text = ">>>";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            
            
        }

        private void buttonNewGame_Click_1(object sender, EventArgs e)
        {

        }


        void xuly_thoigian_cotuong_conhanh()
        {
            //xu ly thoi gian
            if (ctr_time == 1)
            {
                time_player1 -= 1;
                timetong_player1 -= 1;


                if (time_player1 < 0 || timetong_player1 < 0)
                {
                     ctr_time = 0;
                     player = ChessColor.Default;
                     if (nguoixem == false)
                     {
                         guitin("guidulieu_cotuong.php", "XinThua", lbl_player1.Text, lbl_vitriban.Text, "2", "XinThua");
                         MessageBox.Show("Bạn đã thua vì đã hết thời gian");
                     }
                    btn_xinthua.Enabled = false;
                    btn_xinhoa.Enabled = false;


                    btn_banmoi.Enabled = true;
                }


                if (time_player1 < 10)
                    amthanh("file_sound3");

                if (time_player1 >= 0)
                {
                   lbl_time_player1.Text = time_player1.ToString();

                }
                else
                {
                    time_player1 = 60;
                    //timetong_coden = timetong_coden - 60;
                }

                if ((timetong_player1 / 60) == 0 && (timetong_player1 % 60) == 0)
                {
                    ctr_time = 0;
                }
                else
                {
                    lbl_timetong_player1.Text = (timetong_player1 / 60).ToString() + "'";
                    lbl_timegiay_player1.Text = (timetong_player1 % 60).ToString();
                }
            }
            else
            {

                if (ctr_time == 2)
                {
                    time_player2 -= 1;
                    timetong_player2 -= 1;

                    //xu ly thua do het thoi gian

                    if (time_player2 < 0 || timetong_player2 < 0)
                    {
                        ctr_time = 0;
                        player = ChessColor.Default;

                        if (nguoixem == false)
                        {
                            guitin("guidulieu_cotuong.php", "XinThua", lbl_player1.Text, lbl_vitriban.Text, "2", "XinThua");
                            MessageBox.Show("Bạn đã thua vì đã hết thời gian");
                        }
                        btn_xinthua.Enabled = false;
                        btn_xinhoa.Enabled = false;

                        btn_banmoi.Enabled = true;
                    }



                    if (time_player2 < 10)
                        amthanh("file_sound3");

                    if (time_player2 >= 0)
                    {
                        lbl_time_player2.Text = time_player2.ToString();

                    }
                    else
                    {
                        time_player2 = 60;
                        //timetong_coden = timetong_coden - 60;
                    }

                    if ((timetong_player2 / 60) == 0 && (timetong_player2 % 60) == 0)
                    {
                        ctr_time = 0;
                    }
                    else
                    {
                        lbl_timetong_player1.Text = (timetong_player2 / 60).ToString() + "'";
                        lbl_timegiay_player1.Text = (timetong_player2 % 60).ToString();
                    }
                }
            }
        }

        void xuly_thoigian_cotuong_quocte()
        {
           

            //if (sonuocdi_player1 < 3)
          // {
                lbl_time_player1.Text = sonuocdi_player1.ToString();
                lbl_time_player2.Text = sonuocdi_player2.ToString();

           // }
            /*
            else
            {
                if (timetong_player1 > 0)
                {
                    sonuocdi_player1 = 0;
                    if (ctr_time == 2)
                    {
                        
                        timetong_player1 = timetong_player1 + 900;
                        lbl_timetong_player1.Text = (timetong_player1 / 60).ToString() + "'";
                        lbl_timegiay_player1.Text = (timetong_player1 % 60).ToString();
                    }

                }

            }

            if (sonuocdi_player2 < 3)
            {
                lbl_time_player2.Text = sonuocdi_player2.ToString();
                lbl_time_player1.Text = sonuocdi_player1.ToString();

            }
            else
            {

                if (timetong_player2 > 0)
                {
                    sonuocdi_player2 = 0;
                    //if (ctr_time == 1)
                   //{
                       
                       // timetong_player2 = timetong_player2 + 900;
                        lbl_timetong_player1.Text = (timetong_player2 / 60).ToString() + "'";
                        lbl_timegiay_player1.Text = (timetong_player2 % 60).ToString();
                  // }
                }


            }
             * */

            if (ctr_time == 1)
            {
                //time_player1 -= 1;
                timetong_player1 -= 1;


                if (sonuocdi_player1 < 10 && timetong_player1 < 0 )
                {
                    ctr_time = 0;
                    player = ChessColor.Default;
                    if (nguoixem == false)
                    {
                        guitin("guidulieu_cotuong.php", "XinThua", lbl_player1.Text, lbl_vitriban.Text, "2", "XinThua");
                        MessageBox.Show("Bạn đã thua vì đi không đủ nước");
                    }
                    btn_xinthua.Enabled = false;
                    btn_xinhoa.Enabled = false;


                    btn_banmoi.Enabled = true;
                }

                if (timetong_player1 < 30)
                    amthanh("file_sound3");

                //lbl_time_player1.Text = sonuocdi_player1.ToString();
                //lbl_time_player2.Text = sonuocdi_player2.ToString();

               


                if ((timetong_player1 / 60) == 0 && (timetong_player1 % 60) == 0)
                {
                    ctr_time = 0;
                    if (nguoixem == false)
                    {
                        guitin("guidulieu_cotuong.php", "XinThua", lbl_player1.Text, lbl_vitriban.Text, "2", "XinThua");
                        MessageBox.Show("Bạn đã thua vì đã hết thời gian");
                    }
                    btn_xinthua.Enabled = false;
                    btn_xinhoa.Enabled = false;
                    btn_banmoi.Enabled = true;
                }
                else
                {
                   lbl_timetong_player1.Text = (timetong_player1 / 60).ToString() + "'''";
                   lbl_timegiay_player1.Text = (timetong_player1 % 60).ToString();
                }

               
            }
            else
            {

                if (ctr_time == 2)
                {
                   // time_player2 -= 1;
                    timetong_player2 -= 1;

                    //xu ly thua do het thoi gian

                    if (sonuocdi_player1 < 10 && timetong_player2 < 0)
                    {
                        ctr_time = 0;
                        player = ChessColor.Default;

                        if (nguoixem == false)
                        {
                            guitin("guidulieu_cotuong.php", "XinThua", lbl_player1.Text, lbl_vitriban.Text, "2", "XinThua");
                            MessageBox.Show("Bạn đã thua vì đi không du 10 nước trong 15'");
                        }
                        btn_xinthua.Enabled = false;
                        btn_xinhoa.Enabled = false;
                        btn_banmoi.Enabled = true;
                    }



                    if (timetong_player2 < 30)
                        amthanh("file_sound3");

                   

                    if ((timetong_player2 / 60) == 0 && (timetong_player2 % 60) == 0)
                    {
                        ctr_time = 0;
                        if (nguoixem == false)
                        {
                            guitin("guidulieu_cotuong.php", "XinThua", lbl_player1.Text, lbl_vitriban.Text, "2", "XinThua");
                            MessageBox.Show("Bạn đã thua vì đã hết thời gian");
                        }
                        btn_xinthua.Enabled = false;
                        btn_xinhoa.Enabled = false;
                        btn_banmoi.Enabled = true;

                    }
                    else
                    {
                        lbl_timetong_player1.Text = (timetong_player2 / 60).ToString() + "'";
                        lbl_timegiay_player1.Text = (timetong_player2 % 60).ToString();
                    }
                }
            }
        }
        
        private void timer_check_dl_Tick(object sender, EventArgs e)
        {

            switch (luatchoi)
            {
                case "Cờ Nhanh":
                    {
                        xuly_thoigian_cotuong_conhanh();
                        break;
                    }
                case "Tự Do":
                    {
                        
                        
                        lbl_time_player1.Visible = false;
                        lbl_time_player2.Visible = false;

                        lbl_timetong_player1.Visible = false;
                        lbl_timetong_player2.Visible = false;

                        lbl_timegiay_player1.Visible = false;
                        lbl_timegiay_player2.Visible = false;
                        break;
                    }
                case "Quốc Tế":
                    {
                        
                        xuly_thoigian_cotuong_quocte();
                        break;
                    }
            }

            
            
            int a = 2;
            if (a == 1)//// Player 1 thang
            {
                //status.Text = "Chúc Mừng A , Bạn Đã Thắng";
               //// ctr_time=0;
                //amthanh("file_sound4");
               //// guitin("gui_dicotuong.php", "Thang", lbl_player1.Text, lbl_vitriban.Text, "2", "CoTrang_Thang");
              ////  a = 0;
                //return;
            }
            else if (a == -1)//// Player 2 thang
            {
               //// ctr_time = 0;
               // amthanh("file_sound4");
              ////  guitin("gui_dicotuong.php", "Thang", lbl_player1.Text, lbl_vitriban.Text, "2", "Co_Thang");
              ////  a = 0;
               
            }
            else
            {
               
                string page = url + "/webservice/gamecotuong/kiemtradulieu_cotuong.php";  //khai bao trang web trang web service.
                string vars = "?cmd=KiemTraChoi";
                vars += "&nhan=" + lbl_player1.Text + "&iduser=" + user_id + "&idban=" + lbl_vitriban.Text;

                //string url_test = page + vars;
                start_get(page, vars);
            }
           
        }
        public int check_bt_ss = 0;
        private void btn_sansang_Click(object sender, EventArgs e)
        {
            if (check_bt_ss == 0)
            {
                if (lbl_player2_ss.Text == "Sẵn Sàng")
                {
                    player = ChessColor.White;
                    guitin("guidulieu_cotuong.php", "New_CoTuong", lbl_player1.Text, lbl_vitriban.Text, "2", "SanSang");
                    //guitin("guidulieu_cotuong.php", "SanSang_CoTuong", lbl_player1.Text, lbl_vitriban.Text, "2", "SanSang|" + luatchoi + "|" + diemthanggame.ToString());
                   // lbl_sangsang.Text = "Đang Đánh ...";
                    btn_sansang.Text = "Sẵn Sàng";
                    btn_sansang.Enabled = false;
                    btn_xinthua.Enabled = true;
                    btn_xinhoa.Enabled = true;
                    lbl_player1_ss.Text = "";
                    lbl_player2_ss.Text = "Đang Đánh ...";
                    //lbl_sangsang.Text = "";
                    player = ChessColor.Default;
                    //status.Text = current_player.ToString();
                }
                else
                {    
                    
                    guitin("guidulieu_cotuong.php", "SanSang_CoTuong", lbl_player1.Text, lbl_vitriban.Text, "2", "SanSang|" + luatchoi + "|" + diemthanggame.ToString());
                    // start_get(page, vars);
                    lbl_player1_ss.Text = "Sẵn Sàng";
                    player = ChessColor.White;
                    //btn_sansang.Enabled = false;
                    lbl_sangsang.Text = "";
                    lbl_player1_ss.Text = "Sẵn Sàng";
                    btn_sansang.Text = "Từ Chối";
                    check_bt_ss = 1;
                    lbl_sangsang.Text = "";
                    cb_luat.Enabled = false;
                }

               
            }
            else
            {
                guitin("guidulieu_cotuong.php", "TuChoi_CoTuong", lbl_player1.Text, lbl_vitriban.Text, "2", "TuChoi");
                btn_sansang.Text = "Sẵn Sàng";
                cb_luat.Enabled = true;

                lbl_player1_ss.Text = "";
                
                lbl_sangsang.Text = "Hãy Click Sẵn Sàng!";
                check_bt_ss = 0;

            }
            sonuocdi_player1 = 0;
            sonuocdi_player2 = 0;

            diemthangthua_1.Text = "";
            diemthangthua_2.Text = "";
            
        }

        public void guitin(string tofile,string cmd,string nguoigui,string idban,string game_id,string dulieu)
        {
            string page = url + "/webservice/gamecotuong/" + tofile;  //khai bao trang web trang web service.
            string vars = "?cmd=" + cmd;            
            vars += "&gui=" + nguoigui + "&idban=" +idban + "&game_id=" + game_id + "&dulieu=" + dulieu;            
            start_get(page, vars);
        }
        public void gui_dico(string tofile, string cmd, string nguoigui, string idban, string game_id, string dulieu1,string dulieu2)
        {
            string page = url + "/webservice/gamecotuong/" + tofile;  //khai bao trang web trang web service.
            string vars = "?cmd=" + cmd;
            vars += "&gui=" + nguoigui + "&idban=" + idban + "&game_id=" + game_id + "&dulieu1=" + dulieu1 + "&dulieu2=" + dulieu2;
            start_get(page, vars);
        }

        private void lbl_timegiay_coden_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load_1(object sender, EventArgs e)
        {

        }

        private void btn_banmoi_Click(object sender, EventArgs e)
        {
            guitin("guidulieu_cotuong.php", "BanMoi", lbl_player1.Text, lbl_vitriban.Text, "2", "BanMoi");
            NewGame();
            btn_banmoi.Enabled = false;
            lbl_player1_ss.Text = "";
            lbl_player2_ss.Text = "";
            diemthangthua_1.Text = "";
            diemthangthua_2.Text = "";
            xuly_luatco();

            
        }

        private void lbl_batdau_Click(object sender, EventArgs e)
        {

        }

        private void btn_xinthua_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Thực Sự Muốn Thoát Game ??? \n Bạn sẽ bị xử thua nếu bạn chọn Yes", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            else
            {

                if (luatchoi == "Tự Do")
                {
                    MessageBox.Show("Bạn Đã Thua");
                    string page = url + "/webservice/gamecotuong/" + "guidulieu_cotuong.php";  //khai bao trang web trang web service.
                    string vars = "?cmd=XinThua_CoTuong";
                    vars += "&gui=" + Form_Main.user + "&iduser=" + Form_Main.user_id + "&idban=" + vitriban + "&dulieu=Thang_CoTuong";
                    start_get(page, vars);
                }
                else
                {

                    lbl_player1_ss.Text = "Thua4";
                    lbl_player2_ss.Text = "Thắng4";
                    diemthangthua_1.Text = "-" + diemthuagame.ToString();
                    diemthangthua_2.Text = "+" + diemthanggame.ToString();
                    btn_xinthua.Enabled = false;
                    btn_xinhoa.Enabled = false;
                    btn_sansang.Enabled = true;
                    //check_bt_ss = 0;
                    //is_playing = false;
                    //user thang
                    thua1++;
                    kinhnghiem1 -= diemthuagame;
                    tt_clearchat.SetToolTip(pb_player1, "Tên : " + user1 + "\n----------------------\nĐiểm kinh nghiệm : " + kinhnghiem1 + "\n----------------------\nSố trận thắng : " + thang1 + "\n----------------------\nSố trận thua : " + thua1 + "\n----------------------\nCấp bậc : " + capbac1);


                    //guitin("guidulieu_cotuong.php", "XinThua", lbl_player1.Text, lbl_vitriban.Text, "2", "XinThua");
                    string page = url + "/webservice/gamecotuong/" + "guidulieu_cotuong.php";  //khai bao trang web trang web service.
                    string vars = "?cmd=XinThua_CoTuong";
                    vars += "&gui=" + Form_Main.user + "&iduser=" + Form_Main.user_id + "&idban=" + vitriban + "&dulieu=Thang_CoTuong";
                    start_get(page, vars);

                }

                btn_banmoi.Enabled = true; 
                ctr_time = 0;
                btn_sansang.Enabled = false;                
                NewGame();
                
            }
             
          
        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string page3 = url + "/webservice/gamecotuong/loaddau.php";  //khai bao trang web trang web service.
            string vars3 = "?cmd=Load_BanCo";
            vars3 += "&nhan=" + Form_Main.user + "&idban=" + lbl_vitriban.Text;  //2 là game co tuong
            start_get(page3, vars3);
        }

        private void btn_xem_Click(object sender, EventArgs e)
        {
            string page3 = url + "/webservice/gamecotuong/loaddau.php";  //khai bao trang web trang web service.
            string vars3 = "?cmd=Load_BanCo";
            vars3 += "&nhan=" + Form_Main.user + "&idban=" + lbl_vitriban.Text;  //2 là game co tuong
            start_get(page3, vars3);
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_ndnhan.SelectionStart = txt_ndnhan.Text.Length;
            txt_ndnhan.ScrollToCaret();
            txt_ndnhan.Clear();
        }

        private void cb_luat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(e.ToString());
        }

        private void cb_luat_SelectedValueChanged(object sender, EventArgs e)
        {
            
            luatchoi = cb_luat.SelectedItem.ToString();
            xuly_luatco();
          
        }
        void xuly_luatco()
        {
            switch (luatchoi)
            {
                case "Cờ Nhanh":
                    {

                        time_player1 = 60;
                        timetong_player1 = 1800;
                        lbl_timetong_player1.Text = "30'";
                        ctr_time = 0;
                        time_player2 = 60;
                        lbl_timetong_player2.Text = "30'";
                        timetong_player2 = 1800;


                        lbl_time_player1.Visible = true;
                        lbl_time_player2.Visible = true;

                        lbl_time_player1.Text = time_player1.ToString();
                        lbl_time_player2.Text = time_player2.ToString();

                        lbl_timetong_player1.Visible = true;
                        lbl_timetong_player2.Visible = true;

                        lbl_timegiay_player1.Visible = true;
                        lbl_timegiay_player2.Visible = true;



                        break;
                    }
                case "Tự Do":
                    {


                        lbl_time_player1.Visible = false;
                        lbl_time_player2.Visible = false;

                        lbl_timetong_player1.Visible = false;
                        lbl_timetong_player2.Visible = false;

                        lbl_timegiay_player1.Visible = false;
                        lbl_timegiay_player2.Visible = false;
                        break;
                    }
                case "Quốc Tế":
                    {


                        sonuocdi_player1 = 0;
                        timetong_player1 = 900;
                        lbl_timetong_player1.Text = "15'";
                        ctr_time = 0;
                        sonuocdi_player2 = 0;
                        timetong_player2 = 900;
                        lbl_timetong_player2.Text = "15'";



                        lbl_time_player1.Visible = true;
                        lbl_time_player2.Visible = true;

                        lbl_time_player1.Text = sonuocdi_player1.ToString();
                        lbl_time_player2.Text = sonuocdi_player2.ToString();

                        lbl_timetong_player1.Visible = true;
                        lbl_timetong_player2.Visible = true;

                        lbl_timegiay_player1.Visible = true;
                        lbl_timegiay_player2.Visible = true;

                        //xuly_thoigian_cotuong_quocte();
                        break;
                    }
            }
        }


        void nut_xemlai(PictureBox pb_x)
        {
            pb_play.BorderStyle = System.Windows.Forms.BorderStyle.None;
            pb_back.BorderStyle = System.Windows.Forms.BorderStyle.None;
            pb_next.BorderStyle = System.Windows.Forms.BorderStyle.None;
            pb_pause.BorderStyle = System.Windows.Forms.BorderStyle.None;
            pb_stop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            pb_x.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
 
        }


        private void pb_play_Click(object sender, EventArgs e)
        {
           

            //MessageBox.Show("og");
            //btn_resume.Enabled = false;            
            //btn_pause.Enabled = true;
            //btn_stop.Enabled = true;
        }


        public int sonuoc_lg = 0;
        public string user1_lg;
        public string user2_lg;
        public string[] dulieu_lg;
        public string[] toado_lg;
        public string str_resp_full;
        public int sln = 1;
        public void start_get_full(string strPage, string strVars)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", strPage, strVars));
            WebReq.Method = "GET";
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer);
            str_resp_full = _Answer.ReadToEnd();
            //MessageBox.Show(str_resp_full);
            str_resp_full = str_resp_full.Trim();
        }


        private void btn_loadgame_Click(object sender, EventArgs e)
        {

            try
            {
                Load lg = new Load();
                lg.ShowDialog();

                if (lg.txt_trandau.Text != "")
                {

                    string page = url + "/webservice/gamecotuong/" + "kiemtradulieu_cotuong.php";  //khai bao trang web trang web service.
                    string vars = "?cmd=XemLai_CoTuong";
                    vars += "&idtrandau=" + lg.txt_trandau.Text;
                    start_get_full(page, vars);

                    dulieu_lg = str_resp_full.Split((char)124);
                    sonuoc_lg = Convert.ToInt32(dulieu_lg[0]);

                    if (sonuoc_lg != 0)
                    {

                        //gb_play.Location = new Point(350, 150);
                        gb_play.Visible = true;                        
                        user1_lg = dulieu_lg[1];
                        user2_lg = dulieu_lg[2];
                        toado_lg = dulieu_lg[3].Split('/');
                        timer_xem_lai.Enabled = true;

                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Trận Đấu !");
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi : " + ex);
            }
        }

        private void pb_play_Click_1(object sender, EventArgs e)
        {
            timer_xem_lai.Enabled = true;
            pb_play.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            nut_xemlai(pb_play);

            
            timer_xem_lai.Enabled = true;
            
            //btn_resume.Enabled = false;
            pb_pause.Enabled = true;
            pb_stop.Enabled = true;
        }

        private void pb_stop_Click(object sender, EventArgs e)
        {
            nut_xemlai(pb_stop);
            gb_play.Hide();

            timer_xem_lai.Enabled = false;
            sln = 1;
            gb_play.Hide();
        }

        private void pb_pause_Click(object sender, EventArgs e)
        {
            nut_xemlai(pb_pause);

            timer_xem_lai.Enabled = false;
            pb_play.Enabled = true;
            //btn_play.Enabled = false;
            pb_pause.Enabled = false;
            pb_stop.Enabled = true;
        }

        private void pb_back_Click(object sender, EventArgs e)
        {
            nut_xemlai(pb_back);

            if (sln > 1)
            {
                //Graphics G = this.CreateGraphics();
                //vanco.Undo();
                //vanco.RePaint(G);
                //Invalidate();
                string[] toado;
                
                sln = sln - 1;
                toado = toado_lg[sln].Split(',');
                xemlai_trandau(toado_lg, sln);
            }


        }

        private void pb_next_Click(object sender, EventArgs e)
        {
            nut_xemlai(pb_next);

           // Graphics G = this.CreateGraphics();
            string[] toado;
            toado = toado_lg[sln - 1].Split(',');

            xemlai_trandau(toado_lg, sln - 1);

            sln++;
        }


        void xemlai_trandau( string[] dataArray,int i_mang)
        {
           // for (int i_mang = 1; i_mang < n_mang; i_mang++)
            //{
                string tamp_dicotuong;
                string[] point;
                tamp_dicotuong = dataArray[i_mang];
                //MessageBox.Show(dataArray[2]);
                point = tamp_dicotuong.Split(',');
                int n = point.Length;

                int indexNew = Convert.ToInt16(point[0]);
                int indexOld = Convert.ToInt16(point[1]);
                int nIndex = Convert.ToInt16(point[2]);
                int user = Convert.ToInt16(point[3]);
                int cIndex = Convert.ToInt16(point[4]);
                int xnP = Convert.ToInt16(point[5]);
                int ynP = Convert.ToInt16(point[6]);
                int xoP = Convert.ToInt16(point[7]);
                int yoP = Convert.ToInt16(point[8]);
                int curIndex = Convert.ToInt16(point[9]);
                int ct = Convert.ToInt16(point[10]);
                int color = Convert.ToInt16(point[11]);
                int kt_thang = Convert.ToInt16(point[12]);
                int ctr_time_tamp = Convert.ToInt16(point[13]);
                int time_tong_tamp = Convert.ToInt16(point[14]);

                if (ctr_time_tamp == 1)
                {
                    ctr_time = 2;
                    timetong_player1 = time_tong_tamp;

                    // MessageBox.Show(timetong_coden.ToString());
                    // MessageBox.Show(ctr_time_tamp.ToString());
                    lbl_time_player1.Hide();

                    lbl_timetong_player2.Text = (timetong_player1 / 60).ToString();
                    lbl_timegiay_player2.Text = (timetong_player1 % 60).ToString();
                    time_player2 = 60;


                }
                else
                {
                    if (ctr_time_tamp == 2)
                    {
                        ctr_time = 1;
                        timetong_player2 = time_tong_tamp;

                        lbl_time_player2.Hide();
                        lbl_timetong_player2.Text = (timetong_player2 / 60).ToString();
                        lbl_timegiay_player2.Text = (timetong_player2 % 60).ToString();
                        time_player1 = 60;


                    }

                }

                Point newPoint = new Point(xnP, ynP);
                Point oriPoint = new Point(xoP, yoP);

                if (user == 1)
                {
                    Process(curIndex, indexNew, indexOld, nIndex, newPoint, oriPoint, color);

                    lbl_player1_ss.Text = "Đang đánh...";
                    lbl_player2_ss.Text = "";

                   // amthanh("file_sound2");

                    Player = ChessColor.White;
                    if (ct > 0)
                    {
                        //xu lý thông báo chiếu tướng
                        Confirm();
                        //MessageBox.Show("Cơ Đen Đã Thắng");
                    }
                    if (kt_thang == 1)
                    {
                        //amthanh("file_sound5");
                        ctr_time = 0;
                        
                        btn_banmoi.Enabled = true;
                        //btn_xinthua.Enabled = false;
                        timer_xem_lai.Enabled = false;
                        MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Trắng" + "\n Kết Quả: Thắng");
                        sln = 1;
                        NewGame();

                    }
                    if (kt_thang == 2)
                    {
                        //amthanh("file_sound5");
                        ctr_time = 0;
                        
                       // btn_banmoi.Enabled = true;
                        btn_xinthua.Enabled = false;
                        btn_xinhoa.Enabled = false;
                        timer_xem_lai.Enabled = false;
                        MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Đen" + "\n Kết Quả: Thắng");
                        sln = 1;
                        NewGame();

                    }
                }
                else
                {
                    Process(curIndex, indexNew, indexOld, nIndex, newPoint, oriPoint, color);

                    lbl_player1_ss.Text = "Đang đánh...";
                    lbl_player2_ss.Text = "";

                   // amthanh("file_sound2");

                    Player = ChessColor.Black;
                    if (ct > 0)
                    {
                        //xu lý thông báo chiêu tuong
                        Confirm();
                        // MessageBox.Show("Cơ Trắng Đã Thắng");
                    }
                    if (kt_thang == 1)
                    {
                        amthanh("file_sound5");
                        ctr_time = 0;
                        
                       // btn_banmoi.Enabled = true;
                        btn_xinthua.Enabled = false;
                        btn_xinhoa.Enabled = false;
                        timer_xem_lai.Enabled = false;
                        MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Trắng" + "\n Kết Quả: Thắng");
                        sln = 1;
                        NewGame();
                    }
                    if (kt_thang == 2)
                    {
                        amthanh("file_sound5");
                        ctr_time = 0;
                        
                       // btn_banmoi.Enabled = true;
                        btn_xinthua.Enabled = false;
                        btn_xinhoa.Enabled = false;
                        timer_xem_lai.Enabled = false;
                        MessageBox.Show("Người chơi :" + dataArray[1] + "\n" + "Nước Cờ : Đen" + "\n Kết Quả: Thắng");
                        sln = 1;
                        NewGame();



                    }
                }

            //}
        }

        private void timer_xem_lai_Tick(object sender, EventArgs e)
        {
            try
            {

               
                if (sln == sonuoc_lg + 1)
                {
                    timer_xem_lai.Enabled = false;
                }
                else
                {
                    Graphics G = this.CreateGraphics();
                    string[] toado;
                    toado = toado_lg[sln - 1].Split(',');
                    int n_mang = toado_lg.Length;
                    //MessageBox.Show(toado_lg[sln - 1]);
                    
                    xemlai_trandau(toado_lg, sln-1);
                    //if

                    sln++;
                }
                if (sln > sonuoc_lg)
                    timer_xem_lai.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi timer : " + ex);
            }
        }

        private void gb_play_Enter(object sender, EventArgs e)
        {

        }

        private void btn_thunho_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_xinhoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn cầu hoà không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            else
            {

             
               // string page = url + "/webservice/gamecotuong/" + "guidulieu_cotuong.php";  //khai bao trang web trang web service.
                //string vars = "?cmd=CauHoa_CoTuong";
                //vars += "&gui=" + Form_Main.user + "&iduser=" + Form_Main.user_id + "&idban=" + vitriban + "&dulieu=CauHoa_CoTuong";
                //start_get(page, vars);    

                guitin("guidulieu_cotuong.php", "CauHoa_CoTuong", lbl_player1.Text, lbl_vitriban.Text, "2", "CauHoa_CoTuong");

               // btn_banmoi.Enabled = true;
                //ctr_time = 0;
                //btn_sansang.Enabled = false;
                //NewGame();

            }
        }

        private void btn_hadiem_Click(object sender, EventArgs e)
        {

            int bien = Convert.ToInt32(txt_diem.Text);
            diemthuagame = bien - 50;
            diemthanggame = bien - 50;
            if (bien > 50)
            {
                txt_diem.Text = (bien - 50).ToString();
            }
            else
            {
                txt_diem.Text = "100";
                diemthuagame =100;
                diemthanggame = 100;
            }
        }

        private void btn_nangdiem_Click(object sender, EventArgs e)
        {
            int bien = Convert.ToInt32(txt_diem.Text);
            diemthuagame = bien + 50;
            diemthanggame = bien + 50;
           
            if (bien <= 500)
            {
                txt_diem.Text = (bien + 50).ToString();

            }
            else
            {
                txt_diem.Text = "100";
                diemthuagame = 100;
                diemthanggame = 100;
            }
        }

        private void pb_player1_Click(object sender, EventArgs e)
        {

        }

     
        




       
    }
}
