using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Evolution
{
    public static class Extenstions
    {
        public static float NextFloat(this Random random, float minimum, float maximum)
        {
            var limitedRandom = ((float)random.NextDouble()) * (maximum - minimum) + minimum;
            return limitedRandom;
        }

        public static Point ToCanvas(this Point point, Canvas canvas)
        {
            double x = ((point.X * canvas.ActualWidth) / 360.0) - 180.0;
            double y = ((point.Y * canvas.ActualHeight) / 180.0) - 90.0;
            return new Point(x, y);
        }

        public static Canvas SetCoordinateSystem(this Canvas canvas, Double xMin, Double xMax, Double yMin, Double yMax)
        {
            var width = xMax - xMin;
            var height = yMax - yMin;

            var translateX = -xMin;
            var translateY = height + yMin;

            var group = new TransformGroup();

            group.Children.Add(new TranslateTransform(translateX, -translateY));
            group.Children.Add(new ScaleTransform(canvas.ActualWidth / width, canvas.ActualHeight / -height));

            canvas.RenderTransform = group;

            return canvas;
        }

        public static float DistanceFrom(this Vector thisVector, Vector other)
        {
            return (float)Math.Sqrt(Math.Pow((other.X - thisVector.X),2) + Math.Pow((other.Y - thisVector.Y), 2));
        }

        public static float DistanceFrom(this Point thisVector, Point other)
        {
            return (float)Math.Sqrt(Math.Pow((other.X - thisVector.X), 2) + Math.Pow((other.Y - thisVector.Y), 2));
        }

        public static float Angle(this Vector thisVector, Vector other)
        {
            return (float)Math.Atan2(thisVector.Y-other.Y, thisVector.X - other.X);
        }

        public static float Angle(this Point thisVector, Point other)
        {
            return (float)Math.Atan2(thisVector.Y - other.Y, thisVector.X - other.X);
        }
    }
}
