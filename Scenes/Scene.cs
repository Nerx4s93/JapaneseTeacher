﻿namespace JapaneseTeacher.Scenes;

internal abstract class Scene
{
    public SceneManager SceneManager;

    public virtual void Start(object[] args) { }

    public virtual void Stop() { }

    protected void SendMessage(object[] args)
    {
        SceneManager.SendMessage(this, args);
    }
}