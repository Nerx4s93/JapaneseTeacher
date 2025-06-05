namespace JapaneseTeacher.Scenes
{
    internal class SceneManager
    {
        private Scene _lastScene;

        public void LoadScene(Scene scene, object[] args)
        {
            if (_lastScene != null)
            {
                _lastScene.Stop();
            }

            scene.SceneManager = this;
            _lastScene = scene;
            scene.Start(args);
        }

        public delegate void GetMessageEventHandler(object sendler, object[] args);
        public GetMessageEventHandler OnGetMessage;
    }
}
