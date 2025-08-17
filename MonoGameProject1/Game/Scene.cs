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
    protected bool IsActive { get; set; } = false;
    private Dictionary<int, GameObject> ActiveSceneObjects { get; set; } = new();
    private Dictionary<int, GameObject> InactiveSceneObjects { get; set; } = new();

    public virtual void OnEnable()
    {
    }

    private void OnGameObjectEnable(GameObject gameObject)
    {
        if( gameObject == null) return;
        if (!ActiveSceneObjects.TryAdd(gameObject.Index, gameObject)) return;
        InactiveSceneObjects.Remove(gameObject.Index);
    }

    private void OnGameObjectDisable(GameObject gameObject)
    {
        if( gameObject == null) return;
        if (!InactiveSceneObjects.TryAdd(gameObject.Index, gameObject)) return;
        ActiveSceneObjects.Remove(gameObject.Index);
    }

    protected virtual void Init()
    {
        foreach (var obj in ActiveSceneObjects)
        {
            obj.Value.Enable();
        }
    }
    
    protected void AddActiveObject(GameObject gameObject)
    {
        if (gameObject == null) return;
        if (!ActiveSceneObjects.TryAdd(gameObject.Index, gameObject)) return;
        gameObject.OnGameObjectEnable += OnGameObjectEnable;
        gameObject.OnGameObjectDisable += OnGameObjectDisable;
    }

    protected void AddInactiveObject(GameObject gameObject)
    {
        if (gameObject == null) return;
        if (!InactiveSceneObjects.TryAdd(gameObject.Index, gameObject)) return;
        gameObject.OnGameObjectEnable += OnGameObjectEnable;
        gameObject.OnGameObjectDisable += OnGameObjectDisable;
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