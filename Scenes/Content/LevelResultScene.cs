using System;
using System.Windows.Forms;

namespace JapaneseTeacher.Scenes.Content
{
    internal class LevelResultScene : Scene
    {
        private Control _mainControl;

        private int _totalAnswers;
        private int _wrongAnswers;

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
            
        }

        #region Настройка элементов управления

        private void AdjustControls()
        {

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