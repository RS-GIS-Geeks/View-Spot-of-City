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
    public static class ValidateCodeHelper
    {
        readonly static string codeSource = "qazwsxedcrfvtgbyhnujmikolQAZXSWEDCVFRTGBNHYUJMKIOLP0123456789";
        public static string CodeSource
        {
            get { return codeSource; }
        }

        readonly static List<SolidColorBrush> colors = new List<SolidColorBrush>();

        static ValidateCodeHelper()
        {
            colors.Add(System.Windows.Media.Brushes.Green);
            colors.Add(System.Windows.Media.Brushes.Blue);
            colors.Add(System.Windows.Media.Brushes.Red);
            colors.Add(System.Windows.Media.Brushes.DeepSkyBlue);
            colors.Add(System.Windows.Media.Brushes.DarkSlateBlue);
            colors.Add(System.Windows.Media.Brushes.MediumOrchid);
            colors.Add(System.Windows.Media.Brushes.Red);
            colors.Add(System.Windows.Media.Brushes.SlateGray);
            colors.Add(System.Windows.Media.Brushes.Violet);
            colors.Add(System.Windows.Media.Brushes.Plum);
            colors.Add(System.Windows.Media.Brushes.Purple);
            colors.Add(System.Windows.Media.Brushes.Tan);
        }

        public static ImageSource CreateValidateCodeImage(char[] charsToShow)
        {
            if (charsToShow.Length != 4)
            {
                throw new Exception("参数错误，在生成验证码图片时");
            }

            Random rand = new Random();

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            //画矩形
            Rect rect = new Rect(new System.Windows.Point(0, 0), new System.Windows.Size(95, 20));
            drawingContext.DrawRectangle(System.Windows.Media.Brushes.LightBlue, null, rect);

            int fontSize = rand.Next(10, 16);
            drawingContext.DrawText(
                new FormattedText(charsToShow[0].ToString(), CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), fontSize, colors[rand.Next(0, colors.Count - 1)]),
                new System.Windows.Point(rand.Next(5, 15), 16 - fontSize));

            fontSize = rand.Next(10, 16);
            drawingContext.DrawText(
                new FormattedText(charsToShow[1].ToString(), CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), fontSize, colors[rand.Next(0, colors.Count - 1)]),
                new System.Windows.Point(rand.Next(25, 35), 16 - fontSize));

            fontSize = rand.Next(10, 16);
            drawingContext.DrawText(
                new FormattedText(charsToShow[2].ToString(), CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), fontSize, colors[rand.Next(0, colors.Count - 1)]),
                new System.Windows.Point(rand.Next(45, 55), 16 - fontSize));

            fontSize = rand.Next(10, 16);
            drawingContext.DrawText(
                new FormattedText(charsToShow[3].ToString(), CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface("Verdana"), fontSize, colors[rand.Next(0, colors.Count - 1)]),
                new System.Windows.Point(rand.Next(65, 75), 16 - fontSize));

            drawingContext.Close();

            //利用RenderTargetBitmap对象，以保存图片
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(95, 20, 96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(drawingVisual);

            ImageSource ImageSource = BitmapFrame.Create(renderBitmap);

            return ImageSource;
        }

        public static char[] CreatFourRandomChar()
        {
            Random rand = new Random();
            int index0 = rand.Next(0, codeSource.Length - 1);
            int index1 = rand.Next(0, codeSource.Length - 1);
            int index2 = rand.Next(0, codeSource.Length - 1);
            int index3 = rand.Next(0, codeSource.Length - 1);
            char[] result = new char[4];
            {
                result[0] = codeSource[index0];
                result[1] = codeSource[index1];
                result[2] = codeSource[index2];
                result[3] = codeSource[index3];
            }
            return result;
        }
    }
}
