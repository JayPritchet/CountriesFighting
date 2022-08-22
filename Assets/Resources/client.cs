using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class client : MonoBehaviour
{
    private Socket tcpClient;
    private string serverIP = "127.0.0.1";//服务器ip地址
    private int serverPort = 50000;//端口号
    private ballManager ballMgr;

    private string msg;
    private Thread recvProcess;

    enum ClientStatus { connected, disconnected, connectting };
    private ClientStatus clientStatus = ClientStatus.disconnected;

    // Start is called before the first frame update
    void Start()
    {
        msg = "";
        ballMgr = GameObject.Find("EventSystem").GetComponent<ballManager>();

       // checkConnectting();
    }
    void connectToHost()
    {
        while (true)
        {

            print("retrying");
            //1、创建socket
            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //2、建立一个连接请求
            IPAddress iPAddress = IPAddress.Parse(serverIP);
            EndPoint endPoint = new IPEndPoint(iPAddress, serverPort);

            print("retrying1");


            try
            {
                tcpClient.Connect(endPoint);
            }//end of try 
            catch (SocketException e)
            {
                print(e.Message);
                continue;
            }

            print("retrying2");

            try
            {
                tcpClient.Send(Encoding.Default.GetBytes("connectted"));
            }//end of try 
            catch (SocketException e)
            {
                print(e.Message);
                continue;
            }
            if (tcpClient.Connected)
            {
                print("连接成功");

                //tcpClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout,400);
                //tcpClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 400);

                recvProcess = new Thread(new ThreadStart(recvCmd));
                recvProcess.Start();
                clientStatus = ClientStatus.connected;
                Thread.CurrentThread.Abort();
            }
            else
                print("failed,retrying");
        }
    }
    void checkConnectting()
    {
        if (clientStatus == ClientStatus.disconnected)
        {
            if (recvProcess != null)
                recvProcess.Abort();
            if (tcpClient != null)
            {
                tcpClient.Shutdown(SocketShutdown.Both);
                tcpClient.Close();
            }
            clientStatus = ClientStatus.connectting;
            print("reconnectting");
            Thread t = new Thread(new ThreadStart(connectToHost));
            t.Start();
        }

        if (clientStatus == ClientStatus.connectting)
        { 
           // print("connectting");
        }

        return;

    }

    // Update is called once per frame
    void Update()
    {
        checkConnectting();

        if (msg == "")
            return;
        for (int i = 0; i < msg.Length; i++)
        {
            if (!(msg[i] == 'a' || (msg[i] == 'b') || (msg[i] == 'c') || (msg[i] == 'd') || (msg[i] == 'e')))
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
        byte[] data = new byte[128];
        while (true)
        { 
            int length=0;
            try
            {
            length = tcpClient.Receive(data);
            }//end of try 
            catch (SocketException e)
            {
                clientStatus = ClientStatus.disconnected;
                print(e.Message);
                print("disconnectted");
                /*
            if (length <= 0 || tcpClient.Connected == false)
            {
                clientStatus = ClientStatus.disconnected;
                print("disconnectted");
                return;
            }
            */
                return;
            }

            if (length <= 0 || tcpClient.Connected == false)
            {
                clientStatus = ClientStatus.disconnected;
                print("disconnectted");
                return;
            }
            msg = Encoding.UTF8.GetString(data, 0, length);
            print("msg:" + msg);
            Thread.Sleep(50);
        }
    }
    void dealCmd(string cmd)
    {
        switch (cmd[0])
        {
            case 'a':
                ballMgr.CreateBall(cmd[1] - '0', cmd.Substring(3));
                break;
            case 'b':
                ballMgr.SetSpeed(cmd.Substring(3), 4, 10);
                break;
            case 'e':
                ballMgr.ChangeSize(cmd.Substring(3), 1.5f, 10);
                break;
            case 'd':
                ballMgr.ReverseDirection(cmd.Substring(3));
                break;
            case 'c':
                ballMgr.ReverseDirection(cmd.Substring(3));
                break;
        }

    }

    private void OnDestroy()
    {
        recvProcess.Abort();
        tcpClient.Shutdown(SocketShutdown.Both);
        tcpClient.Close();
    }
}
