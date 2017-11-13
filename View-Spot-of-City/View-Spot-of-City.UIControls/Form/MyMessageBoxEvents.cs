using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace View_Spot_of_City.UIControls.Form
{
    public partial class MyMessageBoxEvents
    {
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window win = (Window)((FrameworkElement)sender).TemplatedParent;
                win.DragMove();
            }
        }

        //拖动改变窗体大小
        bool isWiden = false;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isWiden = true;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isWiden = false;
            Border b = (Border)sender;
            b.ReleaseMouseCapture();
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            Border b = (Border)sender;
            if (isWiden)
            {
                Window win = (Window)((FrameworkElement)sender).TemplatedParent;
                b.CaptureMouse();
                double newWidth = e.GetPosition(win).X + 5;
                double newheight = e.GetPosition(win).Y + 5;
                if (newWidth > 0)
                {
                    win.Width = newWidth;

                }
                if (newheight > 0)
                {
                    win.Height = newheight;
                }
            }
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            Window win = (Window)((FrameworkElement)sender).TemplatedParent;
            win.WindowState = WindowState.Minimized;
        }

        private void btnMaxOrMin_Click(object sender, RoutedEventArgs e)
        {
            Window win = (Window)((FrameworkElement)sender).TemplatedParent;
            if (win.WindowState == WindowState.Maximized)
            {
                win.WindowState = WindowState.Normal;
                return;
            }
            win.WindowState = WindowState.Maximized;
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Window win = (Window)((FrameworkElement)sender).TemplatedParent;
            win.Close();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Window win = (Window)((FrameworkElement)sender).TemplatedParent;
            if (win.WindowState == WindowState.Maximized)
            {
                win.WindowState = WindowState.Normal;
                return;
            }
            win.WindowState = WindowState.Maximized;
        }
        
    }
}
