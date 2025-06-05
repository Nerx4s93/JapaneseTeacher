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

            _lastScene = scene;
            scene.Start(args);
        }
    }
}
