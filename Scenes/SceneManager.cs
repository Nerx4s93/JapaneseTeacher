namespace JapaneseTeacher.Scenes;

internal class SceneManager
{
    private Scene _currentScene;

    public void LoadScene(Scene scene, object[] args)
    {
        _currentScene?.Stop();

        scene.SceneManager = this;
        _currentScene = scene;
        scene.Start(args);
    }

    public void StopScene()
    {
        _currentScene?.Stop();
        _currentScene = null;
    }

    public void SendMessage(object sendler, object[] args)
    {
        OnGetMessage?.Invoke(this, args);
    }

    public delegate void GetMessageEventHandler(object sendler, object[] args);

    public event GetMessageEventHandler OnGetMessage;
}