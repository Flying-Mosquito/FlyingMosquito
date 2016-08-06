using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ConnectLobbyServ : Singleton<ConnectLobbyServ>
{
	private Socket mClientSocket;

	public string ipAddress = "192.168.0.147";	// 서버 IP
	public const int lPort = 2738;	        // 로비서버로 접속 포트

	private int SendDataLength;	            // 전송 데이터 길이(byte)
	private int ReceiveDataLength;	        // 수신 데이터 길이(byte)

	private byte[] Sendbyte;
	private byte[] Receivebyte = new byte[1000];
	private string ReceiveString;
    StringBuilder sb = new StringBuilder();
    
    void Init()
    {
        DontDestroyOnLoad(this);
        print("serv");
        mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        mClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000);
        mClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);

        // 소켓 연결
        try
        {
            IPAddress ipAddr = System.Net.IPAddress.Parse(ipAddress);
            IPEndPoint ipEndPoint = new System.Net.IPEndPoint(ipAddr, lPort);
            mClientSocket.Connect(ipEndPoint);
        }
        catch (SocketException SCE)
        {
            Debug.Log("Socket connect error : " + SCE.ToString());
            return;
        }


        //try
        //{
        //    // Send
        //    SendDataLength = Encoding.Default.GetByteCount(sb.ToString());
        //    Sendbyte = Encoding.Default.GetBytes(sb.ToString());
        //    mClientSocket.Send(Sendbyte, Sendbyte.Length, 0);

        //    // Receive
        //    mClientSocket.Receive(Receivebyte);
        //    ReceiveString = Encoding.Default.GetString(Receivebyte);
        //    ReceiveDataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());
        //    Debug.Log("Receive Data: " + ReceiveString + "(" + ReceiveDataLength + ")");
        //}
        //catch (SocketException err)
        //{
        //    Debug.Log("Socket Send or Receive error : " + err.ToString());
        //}

        // 데이터
        // StringBuilder sb = new StringBuilder();     // 보낼 데이터
        // sb.Append("1"); // 1 = 방 입장, 2 = 방 참가, 3 = 방 나감, 4 = (방입장후) 레디 신호, 5 = 게임 시작(게임서버 Port 받아옴)

        // 기본 구조, Send로 위 sb를 보내줌. Append 로 번호당 GUI 버튼에 붙이면 됨!

    }
 

    public void ComeinRoom()
    {
        sb.Append("1");
        CallServ();
    }
    public void MakeRoom()
    {
        sb.Append("2");
        CallServ();
    }

  
    void CallServ()
    {
        try
        {
            // Send
            SendDataLength = Encoding.Default.GetByteCount(sb.ToString());
            Sendbyte = Encoding.Default.GetBytes(sb.ToString());
            mClientSocket.Send(Sendbyte, Sendbyte.Length, 0);

            // Receive
            mClientSocket.Receive(Receivebyte);
            ReceiveString = Encoding.Default.GetString(Receivebyte);
            ReceiveDataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());
            Debug.Log("Receive Data: " + ReceiveString + "(" + ReceiveDataLength + ")");
        }
        catch (SocketException err)
        {
            Debug.Log("Socket Send or Receive error : " + err.ToString());
        }

    }

    void OnApplicationQuit()
	{
		mClientSocket.Close();
		mClientSocket = null;
	}
}
