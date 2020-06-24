using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Common.FrameworkTemplate
{
    /// <summary>
    /// 模块基础类
    /// </summary>
   public abstract class Module
    {
        /// <summary>
        /// 释放模块资源
        /// </summary>
        /// <param name="arg"></param>
        public abstract void Release(object arg = null);
    }
}
