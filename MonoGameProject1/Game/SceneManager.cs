using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public static class SceneManager
{
    public static Scene CurrentScene;
    private static Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();
    public static bool Exit { get; set; }
    public static bool IsLoading { get; set; }

    public static void ChangeScene(string scene)
    {
        DisableCurrentScene();
        EnableScene(scene);
    }

    public static void EnableScene(Scene scene)
    {
        CurrentScene = scene;
        CurrentScene.OnEnable();
    }

    public static void EnableScene(string scene)
    {
        CurrentScene = GetScene(scene);
        CurrentScene.OnEnable();

    }

    private static Scene GetScene(string scene)
    {
        return scenes[scene];
    }

    private static void DisableCurrentScene()
    { 
        IsLoading = true;
        CurrentScene?.OnDisable();
    }

    public static void AddScene(string name, Scene scene)
    {
        scenes.Add(name, scene);
    }
    
    public static void Update(GameTime gameTime)
    {
        if (Exit) return;
        if (CurrentScene == null) return;
        CurrentScene.Update(gameTime);
    }
    
    public static void Draw(SpriteBatch spriteBatch)
    {
        if (Exit) return;
        if (CurrentScene == null) return;
        CurrentScene.Draw(spriteBatch);
    }

}