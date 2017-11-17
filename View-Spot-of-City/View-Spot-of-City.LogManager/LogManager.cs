using System;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace View_Spot_of_City.LogManager
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// 类唯一实例
        /// </summary>
        private static log4net.ILog log;

        /// <summary>
        /// 静态构造
        /// </summary>
        static LogManager()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            log = log4net.LogManager.GetLogger(typeof(LogManager));
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(object message)
        {
            log.Debug(message);
        }

        /// <summary>
        /// 设置调试日志格式
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void DebugFormatted(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        /// <summary>
        /// 输出信息日志
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            log.Info(message);
        }

        /// <summary>
        /// 设置信息日志格式
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void InfoFormatted(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(object message)
        {
            log.Warn(message);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        /// <summary>
        /// 设置警告日志格式
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WarnFormatted(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="message"></param>
        public static void Error(object message)
        {
            log.Error(message);
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        /// <summary>
        /// 设置错误日志格式
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void ErrorFormatted(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }

        /// <summary>
        /// 输出致命错误日志
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(object message)
        {
            log.Fatal(message);
        }

        /// <summary>
        /// 输出致命错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }

        /// <summary>
        /// 设置致命错误日志格式
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void FatalFormatted(string format, params object[] args)
        {
            log.FatalFormat(format, args);
        }
    }
}
