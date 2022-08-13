using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class client : MonoBehaviour
{
    private Socket tcpClient;
    private string serverIP = "127.0.0.1";//服务器ip地址
    private int serverPort = 50000;//端口号
    private createBall ballCreator;

    private string msg;
    private Thread recvProcess;

    // Start is called before the first frame update
    void Start()
    {
        msg = "";
        ballCreator=GameObject.Find("EventSystem").GetComponent<createBall>();
        //1、创建socket
        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //2、建立一个连接请求
        IPAddress iPAddress = IPAddress.Parse(serverIP);
        EndPoint endPoint = new IPEndPoint(iPAddress, serverPort);
        tcpClient.Connect(endPoint);
        print("请求服务器连接");

        recvProcess = new Thread(new ThreadStart(recvCmd));
        recvProcess.Start();

    }

    // Update is called once per frame
    void Update()
    {
        if (msg == "")
            return;
        for (int i = 0; i < msg.Length; i++)
        {
            if (!(msg[i] == 'a' || (msg[i] == 'b') || (msg[i] == 'c')))
                continue;

            for (int j = i + 1; j < msg.Length; j++)
                if (msg[j] == '\n')
                {
                    dealCmd(msg.Substring(i, j - i));
                    i = j + 1;
                }
        }
        msg = "";
    }

    void recvCmd()
    {
        while(true)
        {
        byte[] data = new byte[1024];
        int length = tcpClient.Receive(data);
        msg = Encoding.UTF8.GetString(data, 0, length);
        print("msg:"+msg);
        Thread.Sleep(50);
        }
    }
    void dealCmd(string cmd) 
    {

        print("cmd:"+cmd);
        switch (cmd[0])
        {
            case 'a':
                ballCreator.CreateBullet(cmd[1]-'0',cmd.Substring(3));
                break;
            case 'b':
                break;
            case 'c':
                break;
        }
    
    }

    private void OnDestroy()
    {
        recvProcess.Abort();
        tcpClient.Close();
    }
}
