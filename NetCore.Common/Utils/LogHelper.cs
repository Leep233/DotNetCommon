
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.Common.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class LogHelper
    {

        private static Logger log = LogManager.GetLogger("NlogFile");

        /// <summary>
        /// 设置是否输出Log文件
        /// </summary>
        public static bool isEnable = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            if (!isEnable) return;

            if (log.IsDebugEnabled)
            {
                log.Debug(msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="msg"></param>
        public static void Debug(this object arg, string msg)
        {
            if (!isEnable) return;

            if (log.IsDebugEnabled)
            {
                log.Debug(msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Debug(this object arg, string format, params object[] args)
        {
            if (!isEnable) return;

            if (log.IsDebugEnabled)
            {
                Debug(string.Format(format, args));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            if (!isEnable) return;

            if (log.IsWarnEnabled)
            {
                log.Warn(msg);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="msg"></param>
        public static void Warn(this object arg, string msg)
        {
            if (!isEnable) return;

            if (log.IsWarnEnabled)
            {
                log.Warn(msg);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Warn(this object arg, string format, params object[] args)
        {
            if (!isEnable) return;

            if (log.IsWarnEnabled)
            {
                Warn(string.Format(format, args));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            if (!isEnable) return;

            if (log.IsErrorEnabled)
            {
                log.Error(msg);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="msg"></param>
        public static void Error(this object arg, string msg)
        {
            if (!isEnable) return;

            if (log.IsErrorEnabled)
            {
                log.Error(msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Error(this object arg, string format, params object[] args)
        {
            if (!isEnable) return;

            if (log.IsErrorEnabled)
            {
                Error(string.Format(format, args));
            }
        }
    }
}
