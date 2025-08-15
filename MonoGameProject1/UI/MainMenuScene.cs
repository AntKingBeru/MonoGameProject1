using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Core;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoGameProject1;

public class MainMenuScene : Scene
{
    public event Scene.SceneUnloadHandler OnSceneUnload;
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public override void OnEnable()
    {
        SceneObjects = new Dictionary<int, GameObject>();

        var testObject = new GameObject("Test");
        SceneObjects.Add(0, testObject);

        var animConfig = new AnimConfig
        {
            Name = "TestAnim",
            SpriteInfo = SpriteManager.GetSprite("TestAnim"),
            SourceRectangle = new Rectangle(0, 0, 64, 64),
            DestRectangle = new Rectangle(100, 100, 64, 64),
            Color = Color.White,
            Effects = SpriteEffects.None,
            LayerDepth = 0.5f,
            Fps = 30,
            InLoop = true
        };

        var c = testObject.AddComponent<Animation, AnimConfig>(animConfig);
        
        testObject.EnableComponent(c);
        
        /*var startButton = new StartButton("Start");
        SceneObjects.Add(startButton.Index, startButton);

        var settingsButton = new SettingsButton("Settings");
        SceneObjects.Add(settingsButton.Index, settingsButton);

        var exitButton = new ExitButton("Exit");
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