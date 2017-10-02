using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using View_Spot_of_City.MapView.MyGmapControl;

namespace View_Spot_of_City.MapView.Markers
{
    /// <summary>
    /// Interaction logic for MyIcon.xaml
    /// </summary>
    public partial class MyIcon
    {
        Popup Popup;
        Label Label;
        MyGMap mapControl;

        public MyIcon(MyGMap map, ImageSource image, double width, double height, string tip = null, bool showTipAlways = false)
        {
            this.InitializeComponent();

            mapControl = map;

            this.Width = width;
            this.Height = height;

            this.IsHitTestVisible = false;

            if (tip != null)
            {
                this.IsHitTestVisible = true;
                //this.ToolTip = tip;

                this.MouseEnter += MyIcon_MouseEnter;
                this.MouseLeave += MyIcon_MouseLeave;
                this.MouseWheel += MyIcon_MouseWheel;

                Popup = new Popup();
                {
                    Popup.Placement = PlacementMode.Mouse;
                }
                Label = new Label();
                Popup.PlacementTarget = this;
                {
                    Label.Background = Brushes.White;
                    Label.Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                    Label.BorderBrush = new SolidColorBrush(Color.FromRgb(170, 170, 170));
                    Label.BorderThickness = new Thickness(1);
                    Label.Padding = new Thickness(5);
                    Label.FontSize = 16;
                    Label.Content = tip;
                }
                Popup.Child = Label;
            }

            if(showTipAlways)
            {
                Popup.Placement = PlacementMode.Center;
                IsHitTestVisible = false;
                Popup.IsOpen = true;
            }

            icon.Source = image;
            if (icon.Source.CanFreeze)
            {
                icon.Source.Freeze();
            }
        }

        private void MyIcon_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mapControl._OnMouseWheel(e);
        }

        private void MyIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            Popup.IsOpen = true;
        }

        private void MyIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.IsOpen = false;
        }
    }
}