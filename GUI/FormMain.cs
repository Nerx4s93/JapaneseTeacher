using System;
using System.Drawing;

using JapaneseTeacher.Data;
using JapaneseTeacher.GUI.Components;
using JapaneseTeacher.GUI.Presenters;
using JapaneseTeacher.Properties;

namespace JapaneseTeacher.GUI
{
    internal partial class FormMain : SceneForm
    {
        private readonly GlobalData _globeData;
        private readonly BackgroundRenderer _backgroundRenderer;
        public FormMain(GlobalData globalData)
        {
            InitializeComponent();
            _globeData = globalData;

            var backgroundSize = Resources.MainFon.Size;
            var newBackgroundSizeWidth = Convert.ToInt32(Convert.ToDouble(backgroundSize.Width) / 1.5);
            var newBackgroundSizeHeight = Convert.ToInt32(Convert.ToDouble(backgroundSize.Height) / 1.5);
            var backgroundImage = new Bitmap(Resources.MainFon, new Size(newBackgroundSizeWidth, newBackgroundSizeHeight));
            _backgroundRenderer = new BackgroundRenderer(this, backgroundImage);
        }
    }
}
