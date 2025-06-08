using System;
using System.Drawing;

namespace JapaneseTeacher.Tools
{
    internal static class DrawingTool
    {
        public static void DrawStar(Graphics graphics, PointF topLeft, SizeF size, Pen pen)
        {
            var points = BuildStar(topLeft, size);
            graphics.DrawPolygon(pen, points);

        }

        public static void FillStar(Graphics graphics, PointF topLeft, SizeF size, Brush brush)
        {
            var points = BuildStar(topLeft, size);
            graphics.FillPolygon(brush, points);
        }

        private static PointF[] BuildStar(PointF topLeft, SizeF size)
        {
            var width = size.Width;
            var height = size.Height;
            var centerX = topLeft.X + width / 2;
            var centerY = topLeft.Y + height / 2;

            var radiusOuter = Math.Min(width, height) / 2;
            var radiusInner = radiusOuter * 0.5f;

            var points = new PointF[10];

            for (int i = 0; i < 10; i++)
            {
                double angle = Math.PI / 5 * i;
                double radius = (i % 2 == 0) ? radiusOuter : radiusInner;

                points[i] = new PointF(
                    centerX + (float)(Math.Cos(angle - Math.PI / 2) * radius),
                    centerY + (float)(Math.Sin(angle - Math.PI / 2) * radius)
                );
            }

            return points;
        }
    }
}
