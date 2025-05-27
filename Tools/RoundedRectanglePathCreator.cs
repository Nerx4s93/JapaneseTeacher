using System.Drawing;
using System.Drawing.Drawing2D;

namespace JapaneseTeacher.Tools
{
    internal static class RoundedRectanglePathCreator
    {
        public static Rectangle GetRecrangleWithSize(Size size)
        {
            var rectangle = new Rectangle(0, 0, size.Width, size.Height);
            return rectangle;
        }

        public static RectangleF GetRecrangleFWithSize(Size size)
        {
            var rectangleF = new RectangleF(0, 0, size.Width, size.Height);
            return rectangleF;
        }

        public static GraphicsPath GetRoundRectanglePath(Rectangle rect, int topRadius, int bottomRadius)
        {
            var path = new GraphicsPath();

            if (topRadius > 0)
            {
                var arc = new Rectangle(rect.Left, rect.Top, topRadius * 2, topRadius * 2);
                path.AddArc(arc, 180, 90);
            }
            else
            {
                path.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
            }

            if (topRadius > 0)
            {
                var arc = new Rectangle(rect.Right - topRadius * 2, rect.Top, topRadius * 2, topRadius * 2);
                path.AddArc(arc, 270, 90);
            }
            else
            {
                path.AddLine(rect.Right, rect.Top, rect.Right, rect.Top);
            }

            if (bottomRadius > 0)
            {
                var arc = new Rectangle(rect.Right - bottomRadius * 2, rect.Bottom - bottomRadius * 2, bottomRadius * 2, bottomRadius * 2);
                path.AddArc(arc, 0, 90);
            }
            else
            {
                path.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom);
            }

            if (bottomRadius > 0)
            {
                var arc = new Rectangle(rect.Left, rect.Bottom - bottomRadius * 2, bottomRadius * 2, bottomRadius * 2);
                path.AddArc(arc, 90, 90);
            }
            else
            {
                path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Bottom);
            }

            path.CloseFigure();

            return path;
        }

        public static GraphicsPath GetRoundRectanglePath(Rectangle rect, int topRadius, int bottomRadius, int width)
        {
            rect = new Rectangle(rect.X + width / 2, rect.Y + width / 2, rect.Width - width, rect.Height - width);
            var graphicsPath = GetRoundRectanglePath(rect, topRadius, bottomRadius);

            return graphicsPath;
        }

        public static GraphicsPath GetRoundRectanglePath(RectangleF rect, int topRadius, int bottomRadius)
        {
            var path = new GraphicsPath();

            if (topRadius > 0)
            {
                var arc = new RectangleF(rect.Left, rect.Top, topRadius * 2, topRadius * 2);
                path.AddArc(arc, 180, 90);
            }
            else
            {
                path.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
            }

            if (topRadius > 0)
            {
                var arc = new RectangleF(rect.Right - topRadius * 2, rect.Top, topRadius * 2, topRadius * 2);
                path.AddArc(arc, 270, 90);
            }
            else
            {
                path.AddLine(rect.Right, rect.Top, rect.Right, rect.Top);
            }

            if (bottomRadius > 0)
            {
                var arc = new RectangleF(rect.Right - bottomRadius * 2, rect.Bottom - bottomRadius * 2, bottomRadius * 2, bottomRadius * 2);
                path.AddArc(arc, 0, 90);
            }
            else
            {
                path.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom);
            }

            if (bottomRadius > 0)
            {
                var arc = new RectangleF(rect.Left, rect.Bottom - bottomRadius * 2, bottomRadius * 2, bottomRadius * 2);
                path.AddArc(arc, 90, 90);
            }
            else
            {
                path.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Bottom);
            }

            path.CloseFigure();

            return path;
        }

        public static GraphicsPath GetRoundRectanglePath(RectangleF rect, int topRadius, int bottomRadius, float width)
        {
            rect = new RectangleF(rect.X + width / 2, rect.Y + width / 2, rect.Width - width, rect.Height - width);
            var graphicsPath = GetRoundRectanglePath(rect, topRadius, bottomRadius);

            return graphicsPath;
        }
    }
}
