using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JapaneseTeacher.UI;

internal sealed class AnswerResultPanel : Control
{
    // Шрифты
    private readonly Font _iconFont = new Font("Segoe UI Emoji", 40f, FontStyle.Bold);
    private readonly Font _titleFont = new Font("Segoe UI Emoji", 18f, FontStyle.Bold);
    private readonly Font _textFont = new Font("Segoe UI", 14f);

    // Цвет фона
    private readonly Brush _brushCorrect = new SolidBrush(Color.FromArgb(215, 255, 184));
    private readonly Brush _brushNoCorrect = new SolidBrush(Color.FromArgb(255, 223, 224));

    // Цвет заголовка
    private readonly Brush _brushTitleTextCorrect = new SolidBrush(Color.FromArgb(88, 167, 0));
    private readonly Brush _brushTitleTextNoCorrect = new SolidBrush(Color.FromArgb(234, 43, 43));

    // Цвет текста
    private readonly Brush _brushTextNoCorrect = new SolidBrush(Color.FromArgb(234, 43, 78));

    // Текст заголовка
    private readonly List<string> _correctTitle = new List<string>()
        {
            "Молодец! 🎯", "Отлично! ✨", "Прекрасно! 🌟", "Супер! 🚀",
            "Идеально! 💯", "Великолепно! 👑", "Потрясающе! 🌪", "Замечательно! 👍",
            "Блестяще! 💎", "Фантастика! 🦄", "Как настоящий японец! 🎎", "Японский мастер! 🥋",
            "В яблочко! 🎯", "Ты покоряешь Японию! 🎌"
        };
    private readonly List<string> _noCorrectTitle = new List<string>()
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

        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        DrawBackground(graphics);
        DrawIcon(graphics);
        DrawTitle(graphics);
        DrawText(graphics);
    }

    private void DrawBackground(Graphics graphics)
    {
        var brush = WasCorrect ? _brushCorrect : _brushNoCorrect;
        graphics.FillRectangle(brush, 0, 0, Width, Height);
    }

    private void DrawIcon(Graphics graphics)
    {
        var iconDiameter = 80;
        var iconX = 40;
        var iconY = (Height - iconDiameter) / 3;

        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(Color.White))
        {
            graphics.FillEllipse(brush, iconX, iconY, iconDiameter, iconDiameter);
        }

        var iconSymbol = WasCorrect ? "✓" : "✗";
        var iconBrush = WasCorrect ? _brushTitleTextCorrect : _brushTitleTextNoCorrect;

        var symbolSize = graphics.MeasureString(iconSymbol, _iconFont);

        var symbolX = iconX + (iconDiameter - symbolSize.Width) / 2;
        var symbolY = iconY + (iconDiameter - symbolSize.Height) / 2;

        graphics.DrawString(iconSymbol, _iconFont, iconBrush, symbolX, symbolY);
    }

    private void DrawTitle(Graphics graphics)
    {
        var title = WasCorrect ?
            _correctTitle[new Random().Next(_correctTitle.Count)] :
            _noCorrectTitle[new Random().Next(_noCorrectTitle.Count)];
        var brush = WasCorrect ? _brushTitleTextCorrect : _brushTitleTextNoCorrect;

        var textSize = graphics.MeasureString(title, _titleFont);

        var x = 180;
        var y = (Height - textSize.Height) / 3;

        graphics.DrawString(title, _titleFont, brush, x, y);
    }

    private void DrawText(Graphics graphics)
    {
        if (!WasCorrect)
        {
            var textSize = graphics.MeasureString(Text, _textFont);

            var x = 185;
            var y = (Height - textSize.Height) * 10 / 12;

            graphics.DrawString(Text, _textFont, _brushTextNoCorrect, x, y);
        }
    }
}