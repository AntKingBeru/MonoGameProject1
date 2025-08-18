using System;
using System.Collections.Generic;

namespace MonoGameProject1;

public class MainMenuScene : Scene
{
    public event SceneUnloadHandler OnSceneUnload;
    public Queue<GameObject> Fishes;

    public override void OnEnable()
    {
        IsActive = true;

        var startButton = new StartButton("Start");
        AddActiveObject(startButton);

        var settingsButton = new SettingsButton("Settings");
        AddActiveObject(settingsButton);

        var exitButton = new ExitButton("Exit");
        AddActiveObject(exitButton);

        var title = new MainMenuTitle("MainTitle");

        Init();
    }
}