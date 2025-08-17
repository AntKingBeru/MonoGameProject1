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


        // var obj = new GameObject("Test");
        // SceneObjects.Add(obj.Index, obj);
        // obj.Scale = new Vector2(0.2f, 0.2f);
        // obj.Position = new Vector2(200, 600);
        //
        // var info = SpriteManager.GetSprite("Button");
        // var spriteConfig = new SpriteConfig(info);
        //
        // obj.AddComponent<Sprite, SpriteConfig>(spriteConfig);
        // var input = obj.AddComponent<Input>();
        // input.EnableMovement();
        // var colliderConfig = new ColliderConfig( new Rectangle(0, 0, 100, 100));
        // obj.AddComponent<Collider, ColliderConfig>(colliderConfig);
        //
        //
        // var obj2 = new GameObject("Test");
        // SceneObjects.Add(obj2.Index, obj2);
        // obj2.Scale = new Vector2(0.2f, 0.2f);
        // obj2.Position = new Vector2(0, 0);
        //
        // var info2 = SpriteManager.GetSprite("Button");
        // var spriteConfig2 = new SpriteConfig(info2);
        //
        // obj2.AddComponent<Sprite, SpriteConfig>(spriteConfig2);
        // var colliderConfig2 = new ColliderConfig(new Rectangle(0, 0, 100, 100), true);
        // obj2.AddComponent<Collider, ColliderConfig>(colliderConfig2);
        //
        //
        var startButton = new StartButton("Start");
        AddActiveObject(startButton);

        var settingsButton = new SettingsButton("Settings");
        AddActiveObject(settingsButton);

        var exitButton = new ExitButton("Exit");
        AddActiveObject(exitButton);


        Init();
    }
}