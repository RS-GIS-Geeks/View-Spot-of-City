using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using View_Spot_of_City.MapView.MyGmapControl;

namespace View_Spot_of_City.MapView.Markers
{
    /// <summary>
    /// MyIcon2.xaml 的交互逻辑
    /// </summary>
    public partial class MyIcon2 : UserControl
    {
        MyGMap mapControl;

        Dictionary<string, object> buttonDownCommandParameter;

        GMapMarker contentViewer;

        bool _isContentVisable = false;

        bool isContentVisiable
        {
            get
            {
                return _isContentVisable;
            }
            set
            {
                _isContentVisable = value;
                if (_isContentVisable)
                {
                    contentViewer.Shape.Visibility = Visibility.Visible;
                }
                else
                {
                    contentViewer.Shape.Visibility = Visibility.Hidden;
                }
            }
        }

        public MyIcon2(MyGMap map, ImageSource image, GMapMarker iconContentViewer, Dictionary<string, object> iconButtonDownCommandParameter = null, double width = 22, double height = 22, bool showTipAlways = false)
        {
            this.InitializeComponent();

            mapControl = map;
            
            buttonDownCommandParameter = iconButtonDownCommandParameter;

            this.Width = width;
            this.Height = height;

            contentViewer = iconContentViewer;

            this.MouseEnter += MyIcon2_MouseEnter;
            this.MouseLeave += MyIcon2_MouseLeave;
            this.MouseWheel += MyIcon2_MouseWheel;
            MouseLeftButtonDown += MyIcon2_MouseLeftButtonDown;

            if(showTipAlways)
            {
                contentViewer.Shape.Visibility = Visibility.Visible;
                IsHitTestVisible = false;
            }
            
            icon.Source = image;
            if (icon.Source.CanFreeze)
            {
                icon.Source.Freeze();
            }
        }

        private void MyIcon2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (buttonDownCommandParameter != null)
                ;// IconClickCommands.IconClicked.Execute(buttonDownCommandParameter, this);
        }

        private void MyIcon2_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mapControl._OnMouseWheel(e);
        }

        private void MyIcon2_MouseEnter(object sender, MouseEventArgs e)
        {
            isContentVisiable = true;
        }

        private void MyIcon2_MouseLeave(object sender, MouseEventArgs e)
        {
            isContentVisiable = false;
        }
    }
}
