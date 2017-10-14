using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;

namespace View_Spot_of_City.UIControls.Progress
{
    /// <summary>
    /// CircleProgressAsync.xaml 的交互逻辑
    /// </summary>
    public partial class CircleProgressAsync : Window
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CircleProgressAsync()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 改变进度条显示文本
        /// </summary>
        /// <param name="text"></param>
        private void _ChangeText(string text)
        {
            this.tbxInfo.Text = text;
        }

        private delegate void ChangeTextHandle(string text);

        /// <summary>
        /// 显示窗口
        /// </summary>
        private void _ShowWindow()
        {
            this.ShowInTaskbar = false;
            this.Topmost = true;
            this.Show();
        }

        private delegate void ShowWindowHandle();

        /// <summary>
        /// 关闭进度条
        /// </summary>
        private void _CloseProgress()
        {
            this.Close();
        }

        private delegate void CloseHandle();

        /// <summary>
        /// 开始显示进度条
        /// </summary>
        public void Begin()
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new ShowWindowHandle(_ShowWindow));

            string prefixText = GetString("ProgressText") as string;
            string[] text = new string[] {"", ".", "..", "..." };
            int i = 0;
            while (true)
            {
                i++;
                i %= 4;
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new ChangeTextHandle(_ChangeText), prefixText + text[i]);
                Thread.Sleep(300);
            }
        }

        /// <summary>
        /// 关闭进度条
        /// </summary>
        public void CloseProgress()
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new CloseHandle(_CloseProgress));
        }
    }
}
