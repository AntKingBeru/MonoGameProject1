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
    public bool IsActive { get; set; }
    public Dictionary<int, GameObject> SceneObjects { get; set; }

    public Scene()
    {
        
    }

    public abstract void OnEnable();

    public virtual void Init()
    {
        foreach (var obj in SceneObjects)
        {
            obj.Value.Enable();
        }
    }

    public virtual void OnDisable()
    {
        foreach (var obj in SceneObjects)
        {
            obj.Value.Disable();
        }
    }

    public virtual void Update(GameTime gameTime)
    {
      
        if (!IsActive) return;

        
        
        foreach (var gameObject in SceneObjects.Values)
        {
            
            if (gameObject.IsActive)
            {
                gameObject.Update(gameTime);
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var gameObject in SceneObjects.Values)
        {
            if (gameObject.IsActive)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}