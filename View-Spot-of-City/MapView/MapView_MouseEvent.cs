using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GMapView.Markers;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using GMapView.Helpers;

namespace GMapView
{
    /// <summary>
    /// MapView.xaml 的交互逻辑
    /// </summary>
    public partial class MapView : UserControl
    {
        #region Mouse
        void mapControl_MouseEnter(object sender, MouseEventArgs e)
        {
            mapControl.Focus();
        }

        void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(mapControl);
            PointLatLng pll = mapControl.FromLocalToLatLng((int)p.X, (int)p.Y);
            switch (operationNow)
            {
                case Operations.Nothing:
                    {
                        e.Handled = true;
                        break;
                    }
                case Operations.Test:
                    {
                        #region Test
                        polyLinePoints.Add(pll);
                        //DrawDamageCircleAsync(pll, 100, 200, 500);
                        //AddFirePlace(100, pll, "数据", true);
                        //DrawIcon(pll, Data.Substance.DataObjType.hydrant, 0, IconDictionaryHelper.iconDictionary[IconDictionaryHelper.Icons.Car], LayerIndex.Car, layerVisibilityDictionary[LayerIndex.Car], "消防车");
                        e.Handled = true;
                        #endregion
                        break;
                    }
                default:
                    {
#if DEBUG
                        MessageBox.Show("操作错误");
#endif
                        break;
                    }
            }
        }

        async void mapControl_MouseDoubleClickAsync(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(mapControl);
            PointLatLng pll = mapControl.FromLocalToLatLng((int)p.X, (int)p.Y);
            switch (operationNow)
            {
                case Operations.Nothing:
                    {
                        //ZoonInWithDoubleClick(e);
                        e.Handled = true;
                        break;
                    }
                case Operations.Test:
                    {
                        #region Test
                        #endregion
                        break;
                    }
                default:
                    {
#if DEBUG
                        MessageBox.Show("操作错误");
#endif
                        break;
                    }
            }
        }

        void mapControl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            rBtnDownPoint = e.GetPosition(mapControl);
            mapControl.CaptureMouse();
            e.Handled = true;
        }
        
        void mapControl_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            mapControl.ReleaseMouseCapture();
            InitOperation();
            e.Handled = true;
        }

        void mapControl_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(mapControl);
            PointLatLng pll = mapControl.FromLocalToLatLng((int)p.X, (int)p.Y);

            switch (operationNow)
            {
                case (int)Operations.Nothing:
                    {
                        break;
                    }
                case Operations.Test:
                    {
                        #region Test

                        #endregion
                        break;
                    }
                default:
                    {
#if DEBUG
                        MessageBox.Show("操作错误");
#endif
                        break;
                    }
            }
        }

        /// <summary>
        /// 双击放大时调用
        /// </summary>
        /// <param name="e"></param>
        private void ZoonInWithDoubleClick(MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(mapControl);
            PointLatLng beforePll = mapControl.FromLocalToLatLng((int)p.X, (int)p.Y);
            mapControl.Zoom += 1;
            PointLatLng afterPll = mapControl.FromLocalToLatLng((int)p.X, (int)p.Y);
            GPoint afterPoint = mapControl.FromLatLngToLocal(beforePll);
            mapControl.Position = new PointLatLng(mapControl.Position.Lat + (beforePll.Lat - afterPll.Lat), mapControl.Position.Lng + (beforePll.Lng - afterPll.Lng));
        }
        #endregion
    }
}