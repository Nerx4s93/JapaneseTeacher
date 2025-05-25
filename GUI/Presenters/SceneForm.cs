using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JapaneseTeacher.GUI.Presenters
{
    public partial class SceneForm : Form
    {
        private int _currentSceneId = 0;
        private readonly Dictionary<int, List<Control>> _sceneControls = new Dictionary<int, List<Control>>();
        private bool _isInitialized = false;

        public int CurrentSceneId
        {
            get => _currentSceneId;
            set
            {
                if (_currentSceneId == value || !_isInitialized)
                {
                    return;
                }
                SwitchScene(value);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeScenes();
            _isInitialized = true;
            ApplyScene(_currentSceneId);
        }

        private void SwitchScene(int newSceneId)
        {
            // Скрываем элементы текущей сцены
            if (_sceneControls.TryGetValue(_currentSceneId, out var currentControls))
            {
                foreach (var control in currentControls)
                {
                    control.Visible = false;
                }
            }

            // Показываем элементы новой сцены
            ApplyScene(newSceneId);

            _currentSceneId = newSceneId;
        }

        private void ApplyScene(int sceneId)
        {
            if (_sceneControls.TryGetValue(sceneId, out var sceneControls))
            {
                foreach (var control in sceneControls)
                {
                    control.Visible = true;
                }
            }
        }

        private void InitializeScenes()
        {
            var controls = GetAllControls(this);

            foreach (var control in controls)
            {
                if (control.Tag != null && int.TryParse(control.Tag.ToString(), out int sceneId))
                {
                    if (!_sceneControls.ContainsKey(sceneId))
                    {
                        _sceneControls[sceneId] = new List<Control>();
                    }
                    _sceneControls[sceneId].Add(control);
                    control.Visible = (sceneId == _currentSceneId);
                }
                else
                {
                    control.Visible = true;
                }
            }
        }

        private IEnumerable<Control> GetAllControls(Control control)
        {
            var controls = new List<Control>();

            foreach (Control child in control.Controls)
            {
                controls.Add(child);
                controls.AddRange(GetAllControls(child));
            }

            return controls;
        }
    }
}
