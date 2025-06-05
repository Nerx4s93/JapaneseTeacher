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

        public void SendMessage(object sendler, object[] args)
        {
            OnGetMessage?.Invoke(this, args);
        }

        public delegate void GetMessageEventHandler(object sendler, object[] args);
        public event GetMessageEventHandler OnGetMessage;
    }
}
