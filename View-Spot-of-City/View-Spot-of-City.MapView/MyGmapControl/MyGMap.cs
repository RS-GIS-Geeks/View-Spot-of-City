
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using GMap.NET;
using GMap.NET.Internals;
using System.Diagnostics;
using GMap.NET.MapProviders;
using System.Windows.Media.Animation;
using GMap.NET.Projections;
using GMap.NET.WindowsPresentation;

namespace View_Spot_of_City.MapView.MyGmapControl
{
    public class MyGMap : GMapControl
    {
        /// <summary>
        /// 画刷颜色
        /// </summary>
        public SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(255, 136, 76));

        /// <summary>
        /// 线宽
        /// </summary>
        public double lineWidth = 3;

        /// <summary>
        /// 图形不透明度
        /// </summary>
        public double opacity = 0.6;

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }

        public void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// creates path from list of points, for performance set addBlurEffect to false
        /// </summary>
        /// <param name="pl"></param>
        /// <returns></returns>
        public override Path CreatePolygonPath(List<Point> localPath, bool addBlurEffect)
        {
            // Create a StreamGeometry to use to specify myPath.
            StreamGeometry geometry = new StreamGeometry();

            using (StreamGeometryContext ctx = geometry.Open())
            {
                ctx.BeginFigure(localPath[0], true, true);

                // Draw a line to the next specified point.
                ctx.PolyLineTo(localPath, true, true);
            }

            // Freeze the geometry (make it unmodifiable)
            // for additional performance benefits.
            geometry.Freeze();

            // Create a path to draw a geometry with.
            Path myPath = new Path();
            {
                // Specify the shape of the Path using the StreamGeometry.
                myPath.Data = geometry;

                if (addBlurEffect)
                {
                    BlurEffect ef = new BlurEffect();
                    {
                        ef.KernelType = KernelType.Gaussian;
                        ef.Radius = 0.0;
                        ef.RenderingBias = RenderingBias.Performance;
                    }

                    myPath.Effect = ef;
                }

                myPath.Stroke = brush;
                myPath.StrokeThickness = 3;
                myPath.StrokeLineJoin = PenLineJoin.Miter;
                myPath.StrokeStartLineCap = PenLineCap.Triangle;
                myPath.StrokeEndLineCap = PenLineCap.Square;

                myPath.Fill = brush;

                myPath.Opacity = opacity;
                myPath.IsHitTestVisible = false;
            }
            return myPath;
        }

        /// <summary>
        /// creates path from list of points, for performance set addBlurEffect to false
        /// </summary>
        /// <param name="pl"></param>
        /// <returns></returns>
        public override Path CreateRoutePath(List<Point> localPath, bool addBlurEffect)
        {
            // Create a StreamGeometry to use to specify myPath.
            StreamGeometry geometry = new StreamGeometry();

            using (StreamGeometryContext ctx = geometry.Open())
            {
                ctx.BeginFigure(localPath[0], false, false);

                // Draw a line to the next specified point.
                ctx.PolyLineTo(localPath, true, true);
            }

            // Freeze the geometry (make it unmodifiable)
            // for additional performance benefits.
            geometry.Freeze();

            // Create a path to draw a geometry with.
            Path myPath = new Path();
            {
                // Specify the shape of the Path using the StreamGeometry.
                myPath.Data = geometry;

                if (addBlurEffect)
                {
                    BlurEffect ef = new BlurEffect();
                    {
                        ef.KernelType = KernelType.Gaussian;
                        ef.Radius = 0.0;
                        ef.RenderingBias = RenderingBias.Performance;
                    }

                    myPath.Effect = ef;
                }

                myPath.Stroke = brush;
                myPath.StrokeThickness = lineWidth;
                myPath.StrokeLineJoin = PenLineJoin.Round;
                myPath.StrokeStartLineCap = PenLineCap.Triangle;
                myPath.StrokeEndLineCap = PenLineCap.Round;

                myPath.Opacity = opacity;
                myPath.IsHitTestVisible = false;
            }
            return myPath;
        }
    }
}