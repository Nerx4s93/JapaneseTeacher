using System;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.UI;

namespace JapaneseTeacher.Scenes.Content
{
    internal class LevelResultScene : Scene
    {
        private Control _mainControl;

        private int _totalAnswers;
        private int _wrongAnswers;

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
            _correctCard.Dispose();
            _mistakeCard.Dispose();
            _accuracyCard.Dispose();
        }

        #region Настройка элементов управления

        private void AdjustControls()
        {
            _correctCard = new StatCard();
            _correctCard.BodyColor = Color.Blue;
            _correctCard.Title = "Дано ответов";
            _correctCard.Text = $"{_totalAnswers}";
            _correctCard.Size = new Size(190, 100);
            _correctCard.Location = new Point(10, 10);

            _mistakeCard = new StatCard();
            _mistakeCard.BodyColor = Color.Red;
            _mistakeCard.Title = "Ошибок";
            _mistakeCard.Text = $"{_wrongAnswers}";
            _mistakeCard.Size = new Size(190, 100);
            _mistakeCard.Location = new Point(220, 10);

            _accuracyCard = new StatCard();
            _accuracyCard.BodyColor = Color.Lime;
            _accuracyCard.Title = "Точность";
            var correctAnswers = _totalAnswers - _wrongAnswers;
            var accuracy = (int)((float)correctAnswers / _totalAnswers * 100);
            _accuracyCard.Text = $"{accuracy}%";
            _accuracyCard.Size = new Size(190, 100);
            _accuracyCard.Location = new Point(440, 10);

            _mainControl.Controls.Add(_correctCard);
            _mainControl.Controls.Add(_mistakeCard);
            _mainControl.Controls.Add(_accuracyCard);
        }

        private void MoveControls()
        {

        }

        private void Form_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }

        #endregion
    }
}
//SceneManager.LoadScene(new ModuleScene(), new object[2] { _mainControl, _theme });