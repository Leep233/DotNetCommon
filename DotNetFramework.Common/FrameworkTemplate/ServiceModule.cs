using DotNetFramework.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Common.FrameworkTemplate
{
    /// <summary>
    /// 服务模块基类  这个模块一般都是全局单例的
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public abstract class ServiceModule<T>:Module where T: ServiceModule<T> ,new ()
    {

        private readonly static object _locker = new object();

        private static T _instance = default(T);

        /// <summary>
        /// 服务模块实例
        /// </summary>
        public static T Instance
        {
            get {

                if (_instance is null)
                {
                    lock (_locker)
                    {
                        if (_instance is null)
                            _instance = new T();
                    }
                }
                return _instance;
            }          
        }

        /// <summary>
        /// 服务模块初始化
        /// </summary>
        /// <param name="arg"></param>
        public virtual void Initialize(object arg = null)
        {
            this.Debug($"ServiceModule [{this.GetType().Name}] Initialize !");
        }


    }
}
