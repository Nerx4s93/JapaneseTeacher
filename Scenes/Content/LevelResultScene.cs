using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.UI;

namespace JapaneseTeacher.Scenes.Content
{
    internal class LevelResultScene : Scene
    {
        private readonly List<string> Title = new List<string>()
        {
            "🌟 Ты молодец!",
            "🏆 Превосходный результат!",
            "🎯 Метко как всегда!",
            "🔥 Ты в ударе!",
            "🎉 Так держать!",
            "🚀 Прогресс на высоте!"
        };

        private readonly List<string> Subtitle = new List<string>()
        {
            "Ты уверенно движешься вперёд! 👍",
            "Продолжай в том же духе — успех не за горами! 🌈",
            "Каждый шаг делает тебя сильнее! 📚",
            "Знания накапливаются — и ты это доказываешь! 🎓",
            "Ты прокачал(а) свой мозг на славу! 🧠",
            "Вот это мощно! Следующий уровень ждёт! 💪"
        };

        private Control _mainControl;

        private int _totalAnswers;
        private int _wrongAnswers;

        private Label _titleLabel;
        private Label _subtitleLabel;
        private StatCard _correctCard;
        private StatCard _mistakeCard;
        private StatCard _accuracyCard;

        public override void Start(object[] args)
        {
            _mainControl = args[0] as Control;
            _totalAnswers = (int)args[1];
            _wrongAnswers = (int)args[2];

            AdjustControls();
            _mainControl.Resize += Form_Resize;
        }

        public override void Stop()
        {
            _titleLabel.Dispose();
            _subtitleLabel.Dispose();
            _correctCard.Dispose();
            _mistakeCard.Dispose();
            _accuracyCard.Dispose();
        }

        #region Настройка элементов управления

        private void AdjustControls()
        {
            #region Текст

            _titleLabel = new Label
            {
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.YellowGreen,
                AutoSize = true,
                Text = Title[new Random().Next(Title.Count)],
            };

            _subtitleLabel = new Label
            {
                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Text = Subtitle[new Random().Next(Subtitle.Count)],
            };

            #endregion
            #region StatCard

            _correctCard = new StatCard
            {
                BodyColor = Color.Blue,
                Title = "Дано ответов",
                Text = $"{_totalAnswers}",
                Size = new Size(190, 100),
                Location = new Point(10, 10)
            };

            _mistakeCard = new StatCard
            {
                BodyColor = Color.Red,
                Title = "Ошибок",
                Text = $"{_wrongAnswers}",
                Size = new Size(190, 100),
                Location = new Point(220, 10)
            };


            var correctAnswers = _totalAnswers - _wrongAnswers;
            var accuracy = (int)((float)correctAnswers / _totalAnswers * 100);
            _accuracyCard = new StatCard
            {
                BodyColor = Color.Lime,
                Title = "Точность",
                Text = $"{accuracy}%",
                Size = new Size(190, 100),
                Location = new Point(440, 10)
            };

            #endregion

            _mainControl.Controls.Add(_titleLabel);
            _mainControl.Controls.Add(_subtitleLabel);
            _mainControl.Controls.Add(_correctCard);
            _mainControl.Controls.Add(_mistakeCard);
            _mainControl.Controls.Add(_accuracyCard);

            Form_Resize(null, null);
        }

        private void MoveControls()
        {
            var clientSize = _mainControl.ClientSize;

            int spacing = 20;
            int cardWidth = _correctCard.Width;
            int cardsTotalWidth = cardWidth * 3 + spacing * 2;

            int centerY = clientSize.Height / 2;

            // Статистика
            int cardsTop = centerY + 30;
            int cardsStartX = (clientSize.Width - cardsTotalWidth) / 2;

            _correctCard.Location = new Point(cardsStartX, cardsTop);
            _mistakeCard.Location = new Point(cardsStartX + cardWidth + spacing, cardsTop);
            _accuracyCard.Location = new Point(cardsStartX + (cardWidth + spacing) * 2, cardsTop);

            // Заголовки
            _titleLabel.Location = new Point(
                (clientSize.Width - _titleLabel.Width) / 2,
                cardsTop - _subtitleLabel.Height - _titleLabel.Height - 20
            );

            _subtitleLabel.Location = new Point(
                (clientSize.Width - _subtitleLabel.Width) / 2,
                _titleLabel.Bottom + 5
            );
        }


        private void Form_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }

        #endregion
    }
}
//SceneManager.LoadScene(new ModuleScene(), new object[2] { _mainControl, _theme });