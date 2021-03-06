﻿using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ConnectMultiServ : MonoBehaviour
{
    private Socket mClientSocket;

    public string ipAddress = "172.20.10.3";	// 서버 IP
    public const int lPort = 2738;	        // 로비서버로 접속 포트

    private int SendDataLength;	            // 전송 데이터 길이(byte)
    private int ReceiveDataLength;	        // 수신 데이터 길이(byte)

    private byte[] Sendbyte;
    private byte[] Receivebyte = new byte[500];
    private byte[] tmpbt = new byte[10];

    private string SendString;
    private string ReceiveString;

    StringBuilder sb = new StringBuilder();
    public Transform Serv;

    public Vector3 playerVec;
    public int myNum;
    public int clntNum;
    public string tmpString;
	public GameObject player;
	public GameObject[] otherPlayer = new GameObject[3];
	public float tmpX,tmpY,tmpZ;

    public struct Data
    {
        public int clntNum;
        public float x, y, z;
        public float rX, rY, rZ;
    };

    public Data dtPk, usr;

    void Awake()
    {
        DontDestroyOnLoad(this);

        mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        mClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000);
        mClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);

            IPAddress ipAddr = System.Net.IPAddress.Parse(ipAddress);
            IPEndPoint ipEndPoint = new System.Net.IPEndPoint(ipAddr, lPort);
            mClientSocket.Connect(ipEndPoint);
        Debug.Log(Constants.SERVCONNECT_NUM);
        // 내 접속 번호 받기위한 곳

        StringBuilder sb = new StringBuilder();
        sb.Append("1");  // 의미없음. init 을 부르기위한 첫send임

        SendDataLength = Encoding.Default.GetByteCount(sb.ToString());
        Sendbyte = Encoding.Default.GetBytes(sb.ToString());
        mClientSocket.Send(Sendbyte, Sendbyte.Length, 0);

        mClientSocket.Receive(Receivebyte);
        ReceiveString = Encoding.Default.GetString(Receivebyte);
        ReceiveDataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());

        // 내 클라이언트 번호 (접속 순서대로 0 , 1, 2, 3 의 숫자를 받음)
        myNum = System.Convert.ToInt16(ReceiveString);

        Constants.SERVCONNECT_NUM = myNum;

    }

    void Update()
    {
        // 플레이어 정보 전달(내 좌표, 회전률 송신)
		playerVec = player.transform.position;
        dtPk.x = playerVec.x;
        dtPk.y = playerVec.y;
        dtPk.z = playerVec.z;
		dtPk.rX = player.transform.rotation.eulerAngles.x;
        dtPk.rY = player.transform.rotation.eulerAngles.y;
        dtPk.rZ = player.transform.rotation.eulerAngles.z;
        SendString = dtPk.x.ToString() + '/' + dtPk.y.ToString() + '/' + dtPk.z.ToString() + '/' + dtPk.rX.ToString() + '/' + dtPk.rY.ToString() + '/' + dtPk.rZ.ToString();
        StartCoroutine("CallServ");
    }

	public void SetPlayer(GameObject _player)
	{
		player = _player;
	}

	public void SetOtherPlayer(GameObject _otherPlayer, int _iPlayerNum)
	{
		if (_iPlayerNum > Constants.SERVCONNECT_NUM)
			otherPlayer [_iPlayerNum - Constants.SERVCONNECT_NUM] = _otherPlayer;
		else
			otherPlayer [_iPlayerNum] = _otherPlayer;
	}

    void SendVec()
    {
        // 좌표 송신
            SendDataLength = Encoding.Default.GetByteCount(SendString);
            Sendbyte = Encoding.Default.GetBytes(SendString);
            mClientSocket.Send(Sendbyte, Sendbyte.Length, 0);

    }

    void Recv()
    {
            // Receive
            mClientSocket.Receive(Receivebyte);
            ReceiveString = Encoding.Default.GetString(Receivebyte);
            ReceiveDataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());

        // 리턴객체 변환, 7개로 쪼갬
        string[] result = ReceiveString.Split('/');
        // 0 ~ 2 : xyz, 3 ~ 5 : Rotate xyz, 6 : 클라 번호(내가 아닌 다른 클라들의 번호, b, c , d 로 옴)
        usr.x = System.Convert.ToSingle(result[0]);
        usr.y = System.Convert.ToSingle(result[1]);
        usr.z = System.Convert.ToSingle(result[2]);
        usr.rX = System.Convert.ToSingle(result[3]);
        usr.rY = System.Convert.ToSingle(result[4]);
        usr.rZ = System.Convert.ToSingle(result[5]);
        // 클라정보, 자신은 a, 나머지는 b c d
        string tmp = result[6];

		// 클라번호 이식
        switch (tmp[0])
        {
		case 'a':
			clntNum = 5;
			break;
		case 'b':
			if (Constants.SERVCONNECT_NUM == 1) {
				clntNum = 0;
			} 
			else {
				clntNum = 1;
			}
                break;
		case 'c':
			if (Constants.SERVCONNECT_NUM < 2) {
				clntNum = 2;
			} else {
				clntNum = 1;
			}
                break;
		case 'd':
			if (Constants.SERVCONNECT_NUM == 3) {
				clntNum = 2;
			} else {
				clntNum = 3;
			}
                break;
            default:
                break;
        }

		//  전달받은 다른 유저 정보를 실제 오브젝트에 대입시킴
		if (clntNum != 5) {
			otherPlayer [clntNum].transform.position = new Vector3 (usr.x, usr.y, usr.z);
			otherPlayer [clntNum].transform.rotation = Quaternion.Euler (usr.rX, usr.rY, usr.rZ);
		}
    }

    IEnumerator CallServ()
    {
        yield return new WaitForSeconds(0.3F);
        SendVec();
        Recv();
    }

    void OnApplicationQuit()
    {
        mClientSocket.Close();
        mClientSocket = null;
    }
}
