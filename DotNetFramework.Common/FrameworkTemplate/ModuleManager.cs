using DotNetFramework.Common.Events;
using DotNetFramework.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Common.FrameworkTemplate
{
    /// <summary>
    /// 业务模块管理
    /// </summary>
    public sealed class ModuleManager : ServiceModule<ModuleManager>
    {

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, BusinessModule> _loadedModule;

        /// <summary>
        /// 已加载模块
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, BusinessModule> LoadedModules
        {
            get
            {
                if (_loadedModule == null)
                    _loadedModule = new Dictionary<string, BusinessModule>();
                return _loadedModule;
            }
        }

        /// <summary>
        /// 模块还未创建时 需要等待监听的事件
        /// </summary>
        private Dictionary<string, ModuleEventTable<object>> _perListenerEvents;

        /// <summary>
        /// 模块还未创建时 需要等待处理的事件
        /// </summary>
        private Dictionary<string, List<ModuleMessage>> _perHandleMessages;

        /// <summary>
        ///  业务模块实例所在的程序域(命名空间)
        /// </summary>
        public string DefualtDomain { get; set; }


        /// <summary>
        /// 业务模块管理初始化
        /// </summary>
        /// <param name="arg">业务模块实例所在的命名空间</param>
        public override void Initialize(object arg = null)
        {
            _perListenerEvents = new Dictionary<string, ModuleEventTable<object>>();

            _perHandleMessages = new Dictionary<string, List<ModuleMessage>>();

            if (arg != null) DefualtDomain = arg.ToString();

            base.Initialize(arg);
        }

        /// <summary>
        /// 创建业务模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <returns></returns>
        public T CreateModule<T>(object arg = null) where T : BusinessModule
        {
            Type type = typeof(T);

            string moduleName = type.Name;

            T module = default(T);

            if (type is null)
            {
                this.Warn($"Create Module {type.FullName}  Faild ! ");
            }
            else
            {
                module = Activator.CreateInstance(type) as T;

                if (module is null)
                {
                    this.Warn($"Create Module  Instance {type.FullName} Faild ! ");
                }
                else
                {
                    module.Create(arg);

                    if (_perListenerEvents.ContainsKey(moduleName))
                        module.SetEventTable(_perListenerEvents[moduleName]);

                    if (_perHandleMessages.ContainsKey(moduleName))
                    {
                        List<ModuleMessage> msgList = _perHandleMessages[moduleName];

                        for (int i = 0; i < msgList.Count; i++)
                        {
                            module.HandleMessage(msgList[i].MethodName, msgList[i].Args);
                        }
                    }

                    LoadedModules.Add(moduleName, module);
                }
            }
            return module;
        }

        /// <summary>
        /// 创建业务模块
        /// </summary>
        /// <param name="moduleName">模块类名</param>
        /// <param name="assemblyName">所在程序集名称</param>
        /// <param name="arg">创建参数</param>
        /// <returns></returns>
        public BusinessModule CreateModule(string moduleName, string assemblyName, object arg = null)
        {
            return CreateModule(DefualtDomain, assemblyName, moduleName, arg);
        }

        /// <summary>
        /// 创建业务模块
        /// </summary>
        /// <param name="domain">模块所在程序域（命名空间）名称</param>
        /// <param name="moduleName">模块类名</param>
        /// <param name="assemblyName">所在程序集名称</param>
        /// <param name="arg">创建参数</param>
        /// <returns></returns>
        public BusinessModule CreateModule(string domain, string assemblyName, string moduleName, object arg)
        {
            string typePath = $"{domain}.{moduleName},{assemblyName}";

            BusinessModule module = null;

            Type type = Type.GetType(typePath, false);

            if (type is null)
            {
                this.Warn($"Create Module {typePath} Faild ! ");
            }
            else
            {
                module = Activator.CreateInstance(type) as BusinessModule;

                if (module is null)
                {
                    this.Warn($"Create Module  Instance {typePath} Faild ! ");
                }
                else
                {
                    module.Create(arg);

                    if (_perListenerEvents.ContainsKey(moduleName))
                        module.SetEventTable(_perListenerEvents[moduleName]);

                    if (_perHandleMessages.ContainsKey(moduleName))
                    {
                        List<ModuleMessage> msgList = _perHandleMessages[moduleName];

                        for (int i = 0; i < msgList.Count; i++)
                        {
                            module.HandleMessage(msgList[i].MethodName, msgList[i].Args);
                        }
                    }

                    LoadedModules.Add(moduleName, module);
                }
            }
            return module;
        }

        /// <summary>
        /// 获取已经加载的模块
        /// </summary>
        /// <typeparam name="T">模块类</typeparam>
        /// <returns></returns>
        public T Module<T>() where T : BusinessModule
        {
            return Module(typeof(T).Name) as T;
        }

        /// <summary>
        /// 获取已经加载的模块
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <returns></returns>
        public BusinessModule Module(string moduleName)
        {
            if (LoadedModules.ContainsKey(moduleName))
                return LoadedModules[moduleName];
            return null;
        }


        /// <summary>
        /// 事件监听 ，如果模块未创建 将会缓存，待模块创建时 设置监听
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        public FrameworkEvent<object> Event(string moduleName, string eventName)
        {
            FrameworkEvent<object> frameworkEvent = null;

            if (LoadedModules.ContainsKey(moduleName))
            {
                frameworkEvent = LoadedModules[moduleName].Event(eventName);
            }
            else
            {
                frameworkEvent = PerListenerEvents(moduleName, eventName);
            }
            return frameworkEvent;
        }

        /// <summary>
        /// 指定模块需要处理的函数 ,如果模块未创建 将会缓存，待模块创建时执行
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="methodName">函数名称</param>
        /// <param name="args">函数参数</param>
        public void HandleMessage(string moduleName, string methodName, params object[] args)
        {
            if (LoadedModules.ContainsKey(moduleName))
            {
                LoadedModules[moduleName].HandleMessage(methodName, args);
            }
            else
            {
                PerHandleMessage(moduleName).Add(new ModuleMessage() { MethodName = methodName, Args = args });
            }
        }

        /// <summary>
        /// 释放模块
        /// </summary>
        /// <param name="arg"></param>
        public override void Release(object arg = null)
        {
            if (_perListenerEvents != null)
            {
                foreach (var item in _perListenerEvents)
                {
                    item.Value.Clear();
                }
                _perListenerEvents.Clear();
            }

            if (_perHandleMessages != null)
            {
                foreach (var item in _perHandleMessages)
                {
                    item.Value.Clear();
                }
                _perHandleMessages.Clear();
            }

            if (_loadedModule != null)
            {
                foreach (var item in _loadedModule)
                {
                    item.Value.Release();
                }
                _loadedModule.Clear();
            }
        }

        private FrameworkEvent<object> PerListenerEvents(string moduleName, string eventName)
        {
            if (!_perListenerEvents.ContainsKey(moduleName))
                _perListenerEvents.Add(moduleName, new ModuleEventTable<object>());

            return _perListenerEvents[moduleName].Event(eventName);
        }

        private List<ModuleMessage> PerHandleMessage(string moduleName)
        {
            if (!_perHandleMessages.ContainsKey(moduleName))
                _perHandleMessages.Add(moduleName, new List<ModuleMessage>());

            return _perHandleMessages[moduleName];

        }
        /// <summary>
        /// 
        /// </summary>
        public class ModuleMessage
        {
            /// <summary>
            /// 函数名称
            /// </summary>
            public string MethodName { get; set; }
            /// <summary>
            /// 函数需要传入的参数
            /// </summary>
            public object[] Args { get; set; }
        }
    }
}
