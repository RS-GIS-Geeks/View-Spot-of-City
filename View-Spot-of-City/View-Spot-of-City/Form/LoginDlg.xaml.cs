using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using View_Spot_of_City.ClassModel;
using View_Spot_of_City.UIControls.Form;
using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;
using static View_Spot_of_City.UIControls.Helper.LoginDlgMaster;

namespace View_Spot_of_City.Form
{
    /// <summary>
    /// LoginDlg.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDlg : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private LoginControls? _Page = null;

        public LoginControls? Page
        {
            get { return _Page; }
            set
            {
                if (_Page != value)
                {
                    _Page = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Page"));
                }
            }
        }

        /// <summary>
        /// 是否点击了按钮
        /// </summary>
        bool _ClickButton = false;

        public LoginDlg(string defaultMail)
        {
            InitializeComponent();
            login.SetDefautMail(defaultMail);
            Page = LoginControls.Login;
        }

        private void OKAndCloseFormCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try { _ClickButton = true; this.DialogResult = true; }
            catch { }
        }

        private void CancelAndCloseFormCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try { this.DialogResult = false; }
            catch { }
        }

        private void ChangePageCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null) return;
            Page = e.Parameter as LoginControls?;
            this.Title = (Page == LoginControls.Login) ? (GetString("LoginTitle") as string) : (GetString("RegisterTitle") as string);
        }

        private void loginDlg_Closed(object sender, EventArgs e)
        {

        }

        private void loginDlg_Loaded(object sender, RoutedEventArgs e)
        {
            this.Activate();
        }

        private void ChangeCurrentUserCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (Application.Current as App).CurrentUser = e.Parameter as UserInfo;
        }

        private void loginDlg_Closing(object sender, CancelEventArgs e)
        {
            if(!_ClickButton)
            {
                if (MessageboxMaster.DialogResults.Yes == MessageboxMaster.Show(GetString("MainWindowCloseConfirm"), GetString("MessageBox_Tip_Title"), MessageboxMaster.MyMessageBoxButtons.YesNo, MessageboxMaster.MyMessageBoxButton.Yes))
                    Application.Current.Shutdown(0);
                else
                    e.Cancel = true;
            }
        }
    }
}
