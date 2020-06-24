
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetFramework.Common.Utils
{
    /// <summary>
    /// 控制台打印接口 因为由于平台引擎的不同 所以打印日志的函数也不同
    /// 用户可以实现这个接口从而达到从不同平台打印的效果
    /// </summary>
    public interface IConsolse
    {
        /// <summary>
        /// 控制台的普通日志打印
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        /// <summary>
        /// 控制台的普通日志打印
        /// </summary>
        /// <param name="formart"></param>
        /// <param name="args"></param>
        void Debug(string formart,params object[]args);
        /// <summary>
        /// 控制台的提示日志打印
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
        /// <summary>
        /// 控制台的提示日志打印
        /// </summary>
        /// <param name="formart"></param>
        /// <param name="args"></param>
        void Warn(string formart, params object[] args);
        /// <summary>
        /// 控制台的错误日志打印
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// 控制台的错误日志打印
        /// </summary>
        /// <param name="formart"></param>
        /// <param name="args"></param>
        void Error(string formart, params object[] args);
    }
    /// <summary>
    /// vs的控制台
    /// </summary>
    public sealed class VisualStudioConsole : IConsolse
    {
        
        public void Debug(string message)
        {
            Console.WriteLine($"调试>>{message}");
        }

        public void Debug(string formart, params object[] args)
        {
            Debug(string.Format(formart, args));
        }

        public void Error(string message)
        {
            Console.WriteLine($"错误>>{message}");
        }

        public void Error(string formart, params object[] args)
        {
            Error(string.Format(formart, args));
        }

        public void Warn(string message)
        {
            Console.WriteLine($"提醒>>{message}");
        }

        public void Warn(string formart, params object[] args)
        {
            Warn(string.Format(formart, args));
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public static class LogHelper
    {
     
        private static Logger log = LogManager.GetLogger("NlogFile");

        /// <summary>
        /// 打印到控制端对象 默认 VisualStudioConsole 用户可以实现接口  来定义不同的控制台输出
        /// </summary>
        public static IConsolse Consolse = new VisualStudioConsole();

        /// <summary>
        /// 设置是否输出Log文件
        /// </summary>
        public static bool isEnable = false;

        /// <summary>
        /// 是否控制台打印
        /// </summary>
        public static bool UseConsolse = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {

            if (UseConsolse)
            {
                Consolse?.Debug(msg);
            }

            if (isEnable && log.IsDebugEnabled)
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
            Debug(msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Debug(this object arg, string format, params object[] args)
        {
            Debug(string.Format(format, args));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            if (UseConsolse)
            {
                Consolse?.Warn(msg);
            }

            if (isEnable && log.IsWarnEnabled)
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
            Warn(msg);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Warn(this object arg, string format, params object[] args)
        {
            Warn(string.Format(format, args));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            if (UseConsolse)
            {
                Consolse?.Error(msg);
            }

            if (isEnable && log.IsErrorEnabled)
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
            Error(msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Error(this object arg, string format, params object[] args)
        {
            Error(string.Format(format, args));
        }
    }
}
