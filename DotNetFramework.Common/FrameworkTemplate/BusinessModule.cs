using DotNetFramework.Common.Events;
using DotNetFramework.Common.Utils;
using System.Reflection;

namespace DotNetFramework.Common.FrameworkTemplate
{
    /// <summary>
    /// 业务模块基类
    /// </summary>
    public abstract class BusinessModule:Module
    {

      private  ModuleEventTable<object> _moduleEventTable;

        /// <summary>
        /// 获取当前业务模块事件表
        /// </summary>
       protected ModuleEventTable<object> EventTable
        {
            get {
                if (_moduleEventTable is null)
                    _moduleEventTable = new ModuleEventTable<object>();
                return _moduleEventTable;
            }
        }

        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="arg"></param>
        public virtual void Create(object arg = null) {
            this.Debug($"BusinessModule [{this.GetType().Name}] Created !");
        }
        /// <summary>
        /// 业务模块监听事件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FrameworkEvent<object> Event(string name)
        {
           return EventTable.Event(name);
        }

        /// <summary>
        /// 外部设置当前业务模块事件表
        /// </summary>
        /// <param name="eventTable"></param>
        internal void SetEventTable(ModuleEventTable<object> eventTable)
        {
            this._moduleEventTable = eventTable;
        }

        /// <summary>
        /// 业务模块与业务模块之间的消息处理
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        internal void HandleMessage(string methodName,params object[] args)
        {
            MethodInfo methodInfo =  this.GetType().GetMethod(methodName, BindingFlags.CreateInstance |BindingFlags.Instance| BindingFlags.NonPublic| BindingFlags.Public);

            if (methodInfo is null)
            {
                this.Warn($"HandleMessage Failed ! (M:{methodName} is null)");
                return;
            }

            methodInfo.Invoke(this, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,null,args,null);
        }
        
        /// <summary>
        /// 重写必须调用基类
        /// </summary>
        /// <param name="arg"></param>
        public override void Release(object arg = null)
        {
            EventTable.Clear();
        }

    }
}
