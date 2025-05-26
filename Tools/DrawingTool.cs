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
            float width = size.Width;
            float height = size.Height;
            float centerX = topLeft.X + width / 2;
            float centerY = topLeft.Y + height / 2;

            float radiusOuter = Math.Min(width, height) / 2;
            float radiusInner = radiusOuter * 0.5f;

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
