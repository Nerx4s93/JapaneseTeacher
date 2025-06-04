using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JapaneseTeacher.UI
{
    internal class AnswerResultPanel : Control
    {
        private readonly Font IconFont = new Font("Segoe UI Emoji", 40f, FontStyle.Bold);
        private readonly Font TitleFont = new Font("Segoe UI Emoji", 18f, FontStyle.Bold);
        private readonly Font TextFont = new Font("Segoe UI", 14f);

        // Цвет фона
        private readonly Brush BrushCorrect = new SolidBrush(Color.FromArgb(215, 255, 184));
        private readonly Brush BrushNoCorrect = new SolidBrush(Color.FromArgb(255, 223, 224));
        // Цвет заголовка
        private readonly Brush BrushTitleTextCorrect = new SolidBrush(Color.FromArgb(88, 167, 0));
        private readonly Brush BrushTitleTextNoCorrect = new SolidBrush(Color.FromArgb(234, 43, 43));
        // Цвет текста
        private readonly Brush BrushTextNoCorrect = new SolidBrush(Color.FromArgb(234, 43, 78));
        // Текст заголовка
        private readonly List<string> CorrectTitle = new List<string>()
        {
            "Молодец! 🎯", "Отлично! ✨", "Прекрасно! 🌟", "Супер! 🚀",
            "Идеально! 💯", "Великолепно! 👑", "Потрясающе! 🌪", "Замечательно! 👍",
            "Блестяще! 💎", "Фантастика! 🦄", "Как настоящий японец! 🎎", "Японский мастер! 🥋",
            "В яблочко! 🎯", "Ты покоряешь Японию! 🎌"
        };
        private readonly List<string> NoCorrectTitle = new List<string>()
        {
            "Даже самураи ошибаются! ⚔️", "Не та клавиша? Бывает! ⌨️", "Всё в порядке! 🌸",
            "Жаль! 😔", "Ещё раз! 🔄", "Нужно больше тренироваться! 🥋",
            "В следующий раз получится!", "Не сдавайся! Ты почти у цели 💪",
            "Ошибаться — часть обучения!", "Почти получилось! Попробуй ещё раз 🌱"
        };

        public bool WasCorrect { get; set; }

        public AnswerResultPanel()
        {
            Dock = DockStyle.Bottom;
            Height = 90;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            DrawBackground(graphics);
            DrawIcon(graphics);
            DrawTitle(graphics);
            DrawText(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            var brush = WasCorrect ? BrushCorrect : BrushNoCorrect;
            graphics.FillRectangle(brush, 0, 0, Width, Height);
        }

        private void DrawIcon(Graphics graphics)
        {
            int iconDiameter = 80;
            int iconX = 40;
            int iconY = (Height - iconDiameter) / 3;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillEllipse(Brushes.White, iconX, iconY, iconDiameter, iconDiameter);

            var iconSymbol = WasCorrect ? "✓" : "✗";
            var iconBrush = WasCorrect ? BrushTitleTextCorrect : BrushTitleTextNoCorrect;

            var symbolSize = graphics.MeasureString(iconSymbol, IconFont);

            var symbolX = iconX + (iconDiameter - symbolSize.Width) / 2;
            var symbolY = iconY + (iconDiameter - symbolSize.Height) / 2;

            graphics.DrawString(iconSymbol, IconFont, iconBrush, symbolX, symbolY);
        }

        private void DrawTitle(Graphics graphics)
        {
            var title = WasCorrect ? 
                CorrectTitle[new Random().Next(CorrectTitle.Count)] :
                NoCorrectTitle[new Random().Next(NoCorrectTitle.Count)];
            var brush = WasCorrect ? BrushTitleTextCorrect : BrushTitleTextNoCorrect;

            var textSize = graphics.MeasureString(title, TitleFont);

            var x = 180;
            var y = (Height - textSize.Height) / 3;

            graphics.DrawString(title, TitleFont, brush, x, y);
        }

        private void DrawText(Graphics graphics)
        {
            if (!WasCorrect)
            {
                var textSize = graphics.MeasureString(Text, TextFont);

                var x = 185;
                var y = (Height - textSize.Height) * 10 / 12;

                graphics.DrawString(Text, TextFont, BrushTextNoCorrect, x, y);
            }
        }
    }
}
