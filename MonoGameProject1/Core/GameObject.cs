using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class GameObject : IUpdateables, IDrawables
{
    public string Name;

    public readonly int Index;
    public bool IsActive;
    public Vector2 Position;
    public Vector2 Size;
    public Vector2 Scale;
    public float Rotation;
    public Vector2 Origin = Vector2.Zero;
    
    public List<Component> ActiveComponents;
    public List<Component> InactiveComponents;

    private static int GameObjectCounter = 0;

    public GameObject(string name)
    {
        Name = name;
        Index = GameObjectCounter++;
    }

    public virtual void Enable()
    {
        IsActive = true;
        //Enable Logic 
    }

    public virtual void Disable()
    {
        IsActive = false;
        //Disable Logic
    }
    
    public virtual void Update(GameTime gameTime)
    {
        foreach (Component component in ActiveComponents)
        {
            if (component.IsActive)
            {
                component.Update(gameTime);
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        
    }
    
    public T AddComponent<T> () where T : Component, new()
    {
        var newComponent = new T();
        newComponent.Initialize(this);
        newComponent.SetActive(true);
        ActiveComponents.Add(newComponent);
        return newComponent;
    }

    public void DisableComponent<T>(T component) where T : Component
    {
        foreach (var c in ActiveComponents)
        {
            if (c == component)
            {
                InactiveComponents.Add(c);
                ActiveComponents.Remove(c);
            }
        }
    }
    
    public void EnableComponent<T>(T component) where T : Component
    {
        foreach (var c in InactiveComponents)
        {
            if (c == component)
            {
                ActiveComponents.Add(c);
                InactiveComponents.Remove(c);
            }
        }
    }
}