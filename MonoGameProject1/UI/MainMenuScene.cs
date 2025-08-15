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
        SceneObjects = new Dictionary<int, GameObject>();

        var obj = new GameObject("Test");
        SceneObjects.Add(obj.Index, obj);
        
        var spriteConfig = new SpriteConfig();
        spriteConfig.SpriteInfo = SpriteManager.GetSprite("Button");
        
        var spriteComponent = obj.AddComponent<Sprite, SpriteConfig>(spriteConfig);
        spriteComponent.SetActive(true);
        var inputComponent = obj.AddComponent<Input>();
        inputComponent.SetActive(true);

        //
        // var startButton = new StartButton("Start");
        // SceneObjects.Add(startButton.Index, startButton);
        //
        // var settingsButton = new SettingsButton("Settings");
        // SceneObjects.Add(settingsButton.Index, settingsButton);

        /*var exitButton = new ExitButton("Exit");
        SceneObjects.Add(exitButton.Index, exitButton);*/
        Init();
    }

    public override void Init()
    {
        foreach (var obj in SceneObjects)
        {
            obj.Value.Enable();
        }
    }

    public override void OnDisable()
    {
        foreach (var obj in SceneObjects)
        {
            obj.Value.Disable();
        }
    }
}