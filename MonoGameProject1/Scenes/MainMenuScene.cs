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
        
        AudioManager.PlaySong("Theme");

        var startButton = new StartButton("Start");
        AddActiveObject(startButton);


        var howToPlayButton = new HowToPlayButton("HowToPlay");
        AddActiveObject(howToPlayButton);

        var exitButton = new ExitButton("Exit");
        AddActiveObject(exitButton);


        var title = new MainMenuTitle("MainTitle");


        AddActiveObject(title);
        Init();
    }
}