using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class CallServ : Singleton<CallServ> {
    private Socket mClientSocket;

    public string ipAddress = "192.168.0.9";    // 서버 IP
    public const int lPort = 2738;          // 로비서버로 접속 포트

    private int SendDataLength;             // 전송 데이터 길이(byte)
    private int ReceiveDataLength;          // 수신 데이터 길이(byte)

    private byte[] Sendbyte;
    private byte[] Receivebyte = new byte[1000];
    private string ReceiveString;
    StringBuilder sb = new StringBuilder();

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
 public void callServ()
    {
        // 소켓 생성
      
    }
}
