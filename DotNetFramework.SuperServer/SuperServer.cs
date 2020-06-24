using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.SuperServer
{

    public class ReceiveInfo : IRequestInfo
    {
        public string Key { get; set; }
    }

    public class ReceiveFilter : IReceiveFilter<ReceiveInfo>
    {
        public int LeftBufferSize { get; }

        public IReceiveFilter<ReceiveInfo> NextReceiveFilter { get; }

        public FilterState State { get; }

        public ReceiveInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    public class ReceiveFilterFactory : IReceiveFilterFactory<ReceiveInfo>
    {
        public IReceiveFilter<ReceiveInfo> CreateFilter(IAppServer appServer, IAppSession appSession, IPEndPoint remoteEndPoint)
        {
            return null;
        }
    }

    public class SuperServer:AppServer<SocketSession>
    {
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            Console.WriteLine("正在准备配置文件");
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            Console.WriteLine("服务已开始");
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            Console.WriteLine("服务已停止");
            base.OnStopped();
        }
        protected override void OnNewSessionConnected(SocketSession session)
        {
            Console.WriteLine("新的连接地址为" + session.LocalEndPoint.Address.ToString() + ",时间为" + DateTime.Now);
            base.OnNewSessionConnected(session);
        }


     

    }
}
