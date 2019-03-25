using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Google.Protobuf;
public class GameClient
{
    public static void StartSync()
    {
        byte[] buffer = new byte[4096];

        try{

            //测试用本地服务器, 端口3333
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
            IPAddress ipAddress = ipHostInfo.AddressList[0];  
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 3333);  

            // 创建tcp socket
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp );

            try{
                // 尝试连接
                sender.Connect(remoteEP);
                Debug.LogFormat("Socket connected to {0}", sender.RemoteEndPoint.ToString());
                Msg.FirstMsg fm = new Msg.FirstMsg
                {
                    Name = "QingXin",
                    Pw   = 1988,
                };
                var sendData = fm.ToByteArray();
                sender.Send(sendData);

                Debug.LogFormat("Socket send data, length = {0}", sendData.Length);

                sender.Shutdown(SocketShutdown.Both);  
                sender.Close(); 
            }
            catch(System.Exception e)
            {
                Debug.LogErrorFormat("Socket exception: {0}", e.Message);
            }
        }
            catch(System.Exception e)
            {
                Debug.LogErrorFormat("Socket exception: {0}", e.Message);
            }
    }
}