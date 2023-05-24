using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Chess;
namespace GameCoTuong
{
    public class Networks
    {
        private Socket client;
        private byte[] data = new byte[512];
        private Thread receiver;
        private frmMain form;
        private StateConnection state = StateConnection.Breaken;
        private string ip = "";
        public StateConnection Connection
        {
            get { return state; }
            set { state = value; }            
        }
        public Networks(frmMain form)
        {
            this.form = form;            
        }
        public void CloseConnect()
        {
            client.Disconnect(false);
            state = StateConnection.Breaken;
            return;
        }
        #region Server

        /*
        public void Listen()
        {
            try
            {
                form.SetStatusMessage("Đang Kết Nối...");
                Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
                newsock.Bind(iep);
                newsock.Listen(2);
                state = StateConnection.Connecting;
                newsock.BeginAccept(new AsyncCallback(AcceptConn), newsock);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }   
         * */
        /*
        void AcceptConn(IAsyncResult iar)
        {
            Socket oldserver = (Socket)iar.AsyncState;
            client = oldserver.EndAccept(iar);
            form.SetStatusMessage("Đã Được Kết Nối.");      
            state = StateConnection.Connected;
            receiver = new Thread(new ThreadStart(ReceiveData));
            receiver.Start();
        }
         * */
        #endregion server        
        #region Client

        /*
        public void ConnectTo(string ipServer)
        {
            ip = ipServer;
            form.SetStatusMessage("Đang Kết Nối ...");
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ipServer), 9050);
                client.BeginConnect(iep, new AsyncCallback(Connected), client);
            }
            catch
            {
                form.SetStatusMessage("Không Thể Kết Nối !");
            }
        }  
         * */
        /*
        private void Connected(IAsyncResult iar)
        {
            try
            {
                client.EndConnect(iar);
                form.SetStatusMessage("Đã Được Kết Nối.");
                state = StateConnection.Connected;                
                Thread receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();
            }
            catch (SocketException)
            {
                form.SetStatusMessage("Không Tìm Thấy Server.");
            }
        }
        #endregion client
        #region sent & received packet data
        private void SendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
        }
         * */
        /*
        public void sendPacket(string packet)
        {
            try
            {
                byte[] Sent = Encoding.ASCII.GetBytes(packet);
                client.BeginSend(Sent, 0, Sent.Length, 0, new AsyncCallback(SendData), client);
            }
            catch(SocketException e)
            {
                state = StateConnection.Breaken;
                MessageBox.Show(e.Message.ToString());
            }
        }*/
        /*
        private void ReceiveData()
        {
            int recv;
            string packData = "";
            try
            {
                while (true)
                {
                    recv = client.Receive(data);
                    packData = Encoding.ASCII.GetString(data, 0, recv);
                    if (packData.ToString().Equals(""))
                        return;

                    if (packData.StartsWith("@"))
                    {
                        form.AddChatMessage(packData.ToString());                        
                    }
                    else
                    {
                        switch (packData.ToString())
                        {
                            case "N"://new game
                                {
                                    form.NewGame();
                                    form.Refresh();
                                } break;
                            case "X":
                                {
                                    form.SetStatusMessage("Mất kết nối ...");
                                    form.setEnable(true);
                                    state = StateConnection.Breaken;
                                    MessageBox.Show("Người chơi đã thoát khỏi game!!!");
                                    form.Pause = true;                                    
                                    CloseConnect();                                    
                                }; break;
                            case "W_WIN":
                                {
                                    MessageBox.Show("Bạn đã thua . \nNhấn NewGame để chơi lại");
                                    form.Pause = true;
                                } break;
                            case "B_WIN":
                                {
                                    MessageBox.Show("Bạn đã thua . \nNhấn NewGame để chơi lại");
                                    form.Pause = true;
                                } break;
                            default:
                                {                                    
                                    string[] point = packData.Split(',');
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
                                    Point newPoint = new Point(xnP, ynP);
                                    Point oriPoint = new Point(xoP, yoP);
                                    if (user == 1)
                                    {
                                        form.Process(curIndex, indexNew, indexOld, nIndex, newPoint, oriPoint, color);
                                        form.Player = ChessColor.White;
                                        if (ct > 0)
                                        {

                                            form.Confirm();
                                        }
                                    }
                                    else
                                    {
                                        form.Process(curIndex, indexNew, indexOld, nIndex, newPoint, oriPoint, color);
                                        form.Player = ChessColor.Black;
                                        if (ct > 0)
                                        {
                                            form.Confirm();
                                        }
                                    }
                                } break;
                        }//end switch
                    }
                }//end while
            }//end try
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            client.Close();
            return;
        }
         * */
        #endregion sent & received packet data

    }
}
