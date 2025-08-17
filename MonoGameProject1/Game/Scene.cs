using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class Scene : IUpdateables, IDrawables
{
    public delegate void SceneUnloadHandler();
    public event SceneUnloadHandler OnSceneUnload;

    public string Name { get; set; }
    public bool IsActive { get; set; } = false;
    public Dictionary<int, GameObject> ActiveSceneObjects { get; set; } = new();
    public Dictionary<int, GameObject> InactiveSceneObjects { get; set; } = new();

    public abstract void OnEnable();

    public virtual void Init()
    {
        foreach (var obj in ActiveSceneObjects)
        {
            obj.Value.Enable();
        }
    }

    public virtual void OnDisable()
    {
        foreach (var obj in ActiveSceneObjects)
        {
            obj.Value.Disable();
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        if (!IsActive) return;
        
        foreach (var gameObject in ActiveSceneObjects.Values)
        {
            if (gameObject.IsActive)
            {
                gameObject.Update(gameTime);
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var gameObject in ActiveSceneObjects.Values)
        {
            if (gameObject.IsActive)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}