using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.SuperServer
{
    class Program
    {
        static SuperServer server;
        static void Main(string[] args)
        {


             server =new SuperServer();

            server.NewRequestReceived += Server_NewRequestReceived;
            server.NewSessionConnected += Server_NewSessionConnected;
            server.SessionClosed += Server_SessionClosed;
    
            if (server.Setup(10012))
            {
                if (server.Start())
                {


                }
                else
                {
                    Console.WriteLine("AppServer Start Failed");
                }

              
            }
            else
            {
                Console.WriteLine("AppServer SetUp Failed");
            }
          

            Console.Read();

            server.Stop();
        }

        private static void Server_NewRequestReceived(SocketSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            Console.WriteLine($"接收到{session.RemoteEndPoint.ToString()}数据：Key:{requestInfo.Key} | Params:{requestInfo.Body}");
        }

        private static void Server_NewSessionConnected(SocketSession session)
        {
            Console.WriteLine($"{session.RemoteEndPoint.ToString()}已连接！当前客户端数量{server.SessionCount}");
        }

        private static void Server_SessionClosed(SocketSession session, CloseReason value)
        {
            Console.WriteLine($"{session.RemoteEndPoint.ToString()}已断开({value.ToString()})！当前客户端数量{server.SessionCount}");
        }
    }
}
