using System;
using System.Collections.Generic;
using System.Linq;
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
                InitializeScenes();

                if (_currentSceneId == value || !_isInitialized || !_sceneControls.ContainsKey(value))
                    return;

                SwitchScene(value);
            }
        }

        public SceneForm() : base()
        {
            InitializeScenes();
            _isInitialized = true;
            ApplyScene(_currentSceneId);
        }

        private void SwitchScene(int newSceneId)
        {
            // Удаляем элементы текущей сцены
            RemoveCurrentSceneControls();

            // Добавляем элементы новой сцены
            ApplyScene(newSceneId);

            _currentSceneId = newSceneId;
        }

        private void RemoveCurrentSceneControls()
        {
            if (_sceneControls.TryGetValue(_currentSceneId, out var currentControls))
            {
                foreach (var control in currentControls.Where(c => c != null && Controls.Contains(c)))
                {
                    Controls.Remove(control);
                }
            }
        }

        private void ApplyScene(int sceneId)
        {
            if (_sceneControls.TryGetValue(sceneId, out var sceneControls))
            {
                foreach (var control in sceneControls.Where(c => c != null && !Controls.Contains(c)))
                {
                    Controls.Add(control);
                }
            }
        }

        private void InitializeScenes()
        {
            // Сначала собираем все элементы формы
            var allControls = GetAllControls(this).ToList();

            // Очищаем форму (оставляем только не помеченные элементы)
            foreach (var control in allControls)
            {
                if (control.Tag != null && int.TryParse(control.Tag.ToString(), out int sceneId))
                {
                    Controls.Remove(control);

                    if (!_sceneControls.ContainsKey(sceneId))
                    {
                        _sceneControls[sceneId] = new List<Control>();
                    }
                    _sceneControls[sceneId].Add(control);
                }
                else
                {
                    // Элементы без Tag остаются на форме постоянно
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