using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Core;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoGameProject1;

public class MainMenuScene : Scene
{
    public event Scene.SceneUnloadHandler OnSceneUnload;
    // public string Name { get; set; }
    // public bool IsActive { get; set; }

    public override void OnEnable()
    {
        IsActive = true;
        SceneObjects = new Dictionary<int, GameObject>();

        //
        // var startButton = new StartButton("Start");
        // SceneObjects.Add(startButton.Index, startButton);
        //
        // var settingsButton = new SettingsButton("Settings");
        // SceneObjects.Add(settingsButton.Index, settingsButton);

        var exitButton = new ExitButton("Exit");
        SceneObjects.Add(exitButton.Index, exitButton);
        Init();
    }
}