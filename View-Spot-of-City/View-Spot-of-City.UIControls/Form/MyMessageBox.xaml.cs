using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

using View_Spot_of_City.UIControls.Converter;
using static View_Spot_of_City.UIControls.Converter.MyMessageBoxButtonConverter;

namespace View_Spot_of_City.UIControls.Form
{
    /// <summary>
    /// MyMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MyMessageBox : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public enum MyMessageBoxButtons : int
        {
            Ok = 0,
            OkCancel = 1,
            YesNo = 2,
            YesNoCancel = 3
        }

        public enum DialogResults : int
        {
            Ok = 1,
            Cancel = 2,
            Yes = 3,
            No = 4
        }

        /// <summary>
        /// 显示按钮
        /// </summary>
        private MyMessageBoxButtons? _ButtonPanel = null;
        /// <summary>
        /// 显示按钮
        /// </summary>
        public MyMessageBoxButtons? ButtonPanel
        {
            get { return _ButtonPanel; }
            set
            {
                if(_ButtonPanel != value)
                {
                    _ButtonPanel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ButtonPanel"));
                }
            }
        }

        private MyMessageBox(string message)
        {
            InitializeComponent();
            messagetextBox.Text = message;
            Title = null;
            ButtonPanel = MyMessageBoxButtons.Ok;
        }

        private MyMessageBox(string message, string title)
        {
            InitializeComponent();
            messagetextBox.Text = message;
            Title = title;
            ButtonPanel = MyMessageBoxButtons.Ok;
        }

        private MyMessageBox(string message, string title, MyMessageBoxButtons buttons)
        {
            InitializeComponent();
            messagetextBox.Text = message;
            Title = title;
            ButtonPanel = buttons;
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>点击OK这返回DialogResults.Ok，否则返回DialogResults.Cancel</returns>
        public static DialogResults ShowMyDialog(string message)
        {
            bool? dialogresult = (new MyMessageBox(message)).ShowDialog();

            if (dialogresult == true)
                return DialogResults.Ok;
            else
                return DialogResults.Cancel;
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="title">标题</param>
        /// <returns>点击OK这返回DialogResults.Ok，否则返回DialogResults.Cancel</returns>
        public static DialogResults ShowMyDialog(string message, string title)
        {
            bool? dialogresult = (new MyMessageBox(message, title)).ShowDialog();

            if (dialogresult == true)
                return DialogResults.Ok;
            else
                return DialogResults.Cancel;
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="title">标题</param>
        /// <param name="buttons">显示哪些按钮</param>
        /// <returns>点击OK这返回DialogResults.Ok，点击Yes返回DialogResults.Yes，点击No返回DialogResults.No，否则返回DialogResults.Cancel</returns>
        public static DialogResults ShowMyDialog(string message, string title, MyMessageBoxButtons buttons)
        {
            bool? dialogresult = (new MyMessageBox(message, title, buttons)).ShowDialog();
            
            if(buttons == MyMessageBoxButtons.Ok)
            {
                if (dialogresult == true)
                    return DialogResults.Ok;
                else
                    return DialogResults.Cancel;
            }
            else if(buttons == MyMessageBoxButtons.OkCancel)
            {
                if (dialogresult == true)
                    return DialogResults.Ok;
                else
                    return DialogResults.Cancel;
            }
            else if(buttons == MyMessageBoxButtons.YesNo)
            {
                if (dialogresult == true)
                    return DialogResults.Yes;
                else
                    return DialogResults.No;
            }
            else if(buttons == MyMessageBoxButtons.YesNoCancel)
            {
                if (dialogresult == null)
                    return DialogResults.Cancel;
                else if (dialogresult == true)
                    return DialogResults.Yes;
                else
                    return DialogResults.No;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void OKCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try { this.DialogResult = true; }
            catch { }
        }

        private void CancelCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try { this.DialogResult = null; }
            catch { }
        }

        private void YesCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try { this.DialogResult = true; }
            catch { }
        }

        private void NoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try { this.DialogResult = false; }
            catch { }
        }
    }
}
