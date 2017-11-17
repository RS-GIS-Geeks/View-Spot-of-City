using System.Windows.Input;

namespace View_Spot_of_City.UIControls.Command
{
    /// <summary>
    /// 自定义Messagebox的路由命令
    /// </summary>
    public static class MyMessageBoxCommands
    {
        public static RoutedCommand OKButtonClick { set; get; }
        public static RoutedCommand CancelButtonClick { set; get; }
        public static RoutedCommand YesButtonClick { set; get; }
        public static RoutedCommand NoButtonClick { set; get; }

        static MyMessageBoxCommands()
        {
            OKButtonClick = new RoutedCommand();
            CancelButtonClick = new RoutedCommand();
            YesButtonClick = new RoutedCommand();
            NoButtonClick = new RoutedCommand();
        }
    }
}
