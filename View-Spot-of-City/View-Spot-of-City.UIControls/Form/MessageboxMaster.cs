namespace View_Spot_of_City.UIControls.Form
{
    public static class MessageboxMaster
    {
        public enum MyMessageBoxButtons : int
        {
            Ok = 0,
            OkCancel = 1,
            YesNo = 2,
            YesNoCancel = 3
        }

        public enum MyMessageBoxButton : int
        {
            Ok = 1,
            Cancel = 2,
            Yes = 3,
            No = 4
        }

        public enum DialogResults : int
        {
            Ok = 1,
            Cancel = 2,
            Yes = 3,
            No = 4
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>点击OK这返回DialogResults.Ok，否则返回DialogResults.Cancel</returns>
        public static DialogResults Show(string message)
        {
            return MyMessageBox.ShowMyDialog(message);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="title">标题</param>
        /// <returns>点击OK这返回DialogResults.Ok，否则返回DialogResults.Cancel</returns>
        public static DialogResults Show(string message, string title)
        {
            return MyMessageBox.ShowMyDialog(message, title);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="title">标题</param>
        /// <param name="buttons">显示哪些按钮</param>
        /// <param name="defaultButton">默认响应回车的按钮</param>
        /// <returns>点击OK这返回DialogResults.Ok，点击Yes返回DialogResults.Yes，点击No返回DialogResults.No，否则返回DialogResults.Cancel</returns>
        public static DialogResults Show(string message, string title, MyMessageBoxButtons buttons, MyMessageBoxButton defaultButton)
        {
            return MyMessageBox.ShowMyDialog(message, title, buttons, defaultButton);
        }
    }
}