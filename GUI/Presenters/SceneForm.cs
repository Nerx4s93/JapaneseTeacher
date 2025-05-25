using System.Collections.Generic;
using System.Windows.Forms;

namespace JapaneseTeacher.GUI.Presenters
{
    public partial class SceneForm : Form
    {
        private int _currentSceneId = 0;

        public int CurrentSceneId
        {
            get
            {
                return _currentSceneId;
            }
            set
            {
                if (_currentSceneId == value)
                {
                    return;
                }

                SwitchScene(value);
            }
        }

        private void SwitchScene(int newSceneId)
        {
            RemoveCurrentSceneControls();
            ApplyScene(newSceneId);

            _currentSceneId = newSceneId;
        }

        private void RemoveCurrentSceneControls()
        {
            var controls = GetAllControls(this);

            foreach (var control in controls)
            {
                if (control.Tag != null && int.Parse(control.Tag.ToString()) == _currentSceneId)
                {
                    control.Visible = false;
                }
            }
        }

        private void ApplyScene(int sceneId)
        {
            var controls = GetAllControls(this);

            foreach (var control in controls)
            {
                if (control.Tag != null && int.Parse(control.Tag.ToString()) == sceneId)
                {
                    control.Visible = true;
                }
            }
        }

        private IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                yield return child;

                foreach (var descendant in GetAllControls(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}