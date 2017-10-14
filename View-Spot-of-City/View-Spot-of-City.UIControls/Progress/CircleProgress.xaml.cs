using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View_Spot_of_City.UIControls.Progress
{
    /// <summary>
    /// CircleProgress.xaml 的交互逻辑
    /// </summary>
    public partial class CircleProgress : Window
    {
        private volatile bool _shouldStop;
        public CircleProgress()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Topmost = true;
        }

        private delegate void ShowDefaultDelegate(string value);

        /// <summary>
        /// 设置默认描述信息
        /// </summary>
        /// <param name="strLog"></param>
        public void SetDefaultDescriptionAsync(string value)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new ShowDefaultDelegate(SetDefaultDescriptionAsync), value);
                return;
            }
            SetProgressText(value);
        }

        private delegate void ShowLogLabelDelegate(string strLog);

        /// <summary>
        /// 设置进度条描述信息
        /// </summary>
        /// <param name="strLog"></param>
        public void SetProgressText(string strLog)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new ShowLogLabelDelegate(SetProgressText), strLog);
                return;
            }
            this.tbxInfo.Text = strLog;
        }

        private delegate void CloseDelegate();
        /// <summary>
        /// 关闭
        /// </summary>
        public void RequestStop()
        {
            _shouldStop = true;
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new CloseDelegate(Close));
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
