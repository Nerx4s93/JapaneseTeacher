﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI;

internal sealed class LevelInformation : Control
{
    private const int Radius = 8;
    private const int PadingX = 10;
    private const int PadingY = 10;

    private readonly Brush _noActiveBackgroundBrush = new SolidBrush(Color.FromArgb(247, 247, 247));
    private readonly Brush _noActiveOverlayBrush = new SolidBrush(Color.FromArgb(229, 229, 229));
    private readonly Brush _noActiveTextBrush = new SolidBrush(Color.FromArgb(175, 175, 175));

    private ButtonLevel _parent;
    private AnimatedPressButton _animatedPressButton;

    private bool _active;
    private Color _activeBackgroundColor;
    private Brush _activeBackgroundBrush;
    private int _compliteSublevels;
    private int _totalSublevels;

    private Size _titleSize;
    private Size _textSize;

    public bool Active
    {
        get => _active;
        set
        {
            if (_animatedPressButton != null)
            {
                _animatedPressButton.Active = value;
                _animatedPressButton.Text = value ? "Начать" : "НЕДОСТУПНО";
            }

            _active = value;
            Invalidate();
        }
    }

    public Color ActiveBackgroundColor
    {
        get => _activeBackgroundColor;
        set
        {
            _activeBackgroundColor = value;
            _activeBackgroundBrush?.Dispose();
            _activeBackgroundBrush = new SolidBrush(value);
            Invalidate();
        }
    }

    public int CompliteSublevels
    {
        get => _compliteSublevels;
        set
        {
            _compliteSublevels = value;
            Invalidate();
        }
    }

    public int TotalSublevels
    {
        get => _totalSublevels;
        set
        {
            _totalSublevels = value;
            Invalidate();
        }
    }

    public LevelInformation()
    {
        DoubleBuffered = true;
        ForeColor = Color.White;
        Font = new Font("Segoe UI Emoji", 16);

        ActiveBackgroundColor = Color.Orange;
    }

    protected override void CreateHandle()
    {
        base.CreateHandle();
        _animatedPressButton = new AnimatedPressButton
        {
            CustomAutoSize = false,
            Active = _active,
            Text = _active ? "Начать" : "НЕДОСТУПНО"
        };
        _animatedPressButton.Click += AnimatedPressButton_Click;
        Controls.Add(_animatedPressButton);

        CalculateTextsSize();
        UpdateAnimatedButtonPosition();
    }

    protected override void DestroyHandle()
    {
        base.DestroyHandle();
        _animatedPressButton.Dispose();
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
        BringToFront();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        UpdateAnimatedButtonPosition();
    }

    protected override void OnInvalidated(InvalidateEventArgs e)
    {
        base.OnInvalidated(e);
        CalculateTextsSize();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var graphics = e.Graphics;

        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        DrawBackground(graphics);
        DrawTitle(graphics);
        DrawText(graphics);
        UpdateAnimatedButtonPosition();
    }

    private void DrawBackground(Graphics graphics)
    {
        var rectangle = RoundedRectanglePathCreator.GetRecrangleWithSize(Size);
        var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, Radius, Radius);

        var brush = _active ? _activeBackgroundBrush : _noActiveBackgroundBrush;
        graphics.FillPath(brush, path);

        if (!_active)
        {
            var pen = new Pen(_noActiveOverlayBrush, 2)
            {
                Alignment = PenAlignment.Inset
            };
            graphics.DrawPath(pen, path);
        }
    }

    private void DrawTitle(Graphics graphics)
    {
        using var font = new Font(Font, FontStyle.Bold);
        var brush = _active ? new SolidBrush(ForeColor) : _noActiveTextBrush;
        var point = new Point(PadingX, PadingY);

        graphics.DrawString(Text, font, brush, point);

        if (_active)
        {
            brush.Dispose();
        }
    }

    private void DrawText(Graphics graphics)
    {
        var text = GetButtonText();
        var brush = _active ? new SolidBrush(ForeColor) : _noActiveTextBrush;
        var point = new Point(PadingX, PadingY + _titleSize.Height + PadingY / 2);

        graphics.DrawString(text, Font, brush, point);

        if (_active)
        {
            brush.Dispose();
        }
    }

    private void UpdateAnimatedButtonPosition()
    {
        if (_animatedPressButton == null)
        {
            return;
        }

        var buttonY = PadingY + _titleSize.Height + PadingY / 2 + _textSize.Height + PadingY / 2;

        _animatedPressButton.Width = Width - 2 * PadingX;
        _animatedPressButton.Height = 50;
        _animatedPressButton.Location = new Point(PadingX, buttonY);

        Height = PadingY * 5 / 2 + _titleSize.Height + _textSize.Height + _animatedPressButton.Height;
    }

    private void CalculateTextsSize()
    {
        _titleSize = TextRenderer.MeasureText(Text, new Font(Font, FontStyle.Bold));
        var text = GetButtonText();
        _textSize = TextRenderer.MeasureText(text, Font, new Size(Width - 2 * PadingX, 0), TextFormatFlags.WordBreak);
    }

    private string GetButtonText()
    {
        if (_active)
        {
            if (_compliteSublevels >= _totalSublevels)
            {
                return "Вы уже прошли этот уровень!";
            }
            return $"Урок {_compliteSublevels + 1} из {_totalSublevels}";
        }
        else
        {
            return "Пройдите все уровни выше,\nчтобы открыть доступ!";
        }
    }

    private void AnimatedPressButton_Click(object sender, EventArgs e)
    {
        _parent.MessageLoadLevel();
    }

    public void SetParent(ButtonLevel parent)
    {
        _parent = parent;
    }
}