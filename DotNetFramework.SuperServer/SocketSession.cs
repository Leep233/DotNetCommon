using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.SuperServer
{
    public class SocketSession: AppSession<SocketSession>
    {
        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            Console.WriteLine($"位置请求：{requestInfo.Body}");
            base.HandleUnknownRequest(requestInfo);
        }
        public override void Send(string message)
        {
             Console.WriteLine($"发送消息>>:{message}");
            base.Send(message);
        }

       

        protected override void OnSessionClosed(CloseReason reason)
        {
            Console.WriteLine($"会话关闭>{reason.ToString()}");
            base.OnSessionClosed(reason);
        }

        protected override void OnInit()
        {
            this.Charset = Encoding.UTF8;
            Console.WriteLine("初始化");

            base.OnInit();
        }

        public override void Close()
        {
            Console.WriteLine("会话关闭!");

            base.Close();
        }
    }
}
