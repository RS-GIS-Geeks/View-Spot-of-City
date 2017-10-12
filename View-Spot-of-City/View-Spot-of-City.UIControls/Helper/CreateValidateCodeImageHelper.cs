using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace View_Spot_of_City.UIControls.Helper
{
    public static class CreateValidateCodeImageHelper
    {
        public static ImageSource CreateValidateCodeImage()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            //画矩形
            Rect rect = new Rect(new System.Windows.Point(0, 0), new System.Windows.Size(95, 20));
            drawingContext.DrawRectangle(System.Windows.Media.Brushes.LightBlue, null, rect);

            drawingContext.DrawText(
                new FormattedText("H", CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), 16, System.Windows.Media.Brushes.Green), 
                new System.Windows.Point(10, 0));


            drawingContext.DrawText(
                new FormattedText("5", CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), 16, System.Windows.Media.Brushes.Blue),
                new System.Windows.Point(30, 0));


            drawingContext.DrawText(
                new FormattedText("z", CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), 16, System.Windows.Media.Brushes.Red),
                new System.Windows.Point(50, 0));


            drawingContext.DrawText(
                new FormattedText("W", CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), 16, System.Windows.Media.Brushes.DeepSkyBlue),
                new System.Windows.Point(70, 0));

            drawingContext.Close();

            //利用RenderTargetBitmap对象，以保存图片
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(95, 20, 96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(drawingVisual);

            ImageSource ImageSource = BitmapFrame.Create(renderBitmap);

            return ImageSource;
        }
    }
}
