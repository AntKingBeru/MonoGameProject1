using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public static class SceneManager
{
    private static Scene currentScene;
    private static Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();
    public static bool Exit { get; set; }
    public static bool IsLoading { get; set; }

    private static void ChangeScene(string scene)
    {
        DisableCurrentScene();
        EnableScene(scene);
    }

    public static void ReloadNextScene(string scene)
    {
        if (!scenes.ContainsKey(scene))return;
        scenes[scene].OnDisable();
        var sceneType = scenes[scene].GetType();
        scenes[scene] = (Scene)Activator.CreateInstance(sceneType);
        ChangeScene(scene);
    }

    public static void EnableScene(string scene)
    {
        currentScene = GetScene(scene);
        currentScene.OnEnable();
    }

    private static Scene GetScene(string scene)
    {
        return scenes[scene];
    }

    private static void DisableCurrentScene()
    { 
        IsLoading = true;
        currentScene?.OnDisable();
    }

    public static T AddScene<T>(string name) where T : Scene , new()
    {
        if (scenes.ContainsKey(name))
        {
            throw new Exception($"Scene with name {name} already exists.");
        }
        var scene = new T { Name = name };
        scenes.Add(name, scene);
        return scene;
    }
    
    public static void Update(GameTime gameTime)
    {
        if (Exit) return;
        if (currentScene == null) return;
        currentScene.Update(gameTime);
    }
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        if (Exit) return;
        if (currentScene == null) return;
        currentScene.Draw(spriteBatch);
    }

}