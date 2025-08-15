using System;
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
    public float LayerDepth = 0.5f; // 0.0f = back, 1.0f = front

    private static int NextInt = 0;

    public GameObject(string name)
    {
        this.Name = name;
        Index = NextInt++;
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
        
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        
    }
    
    public void AddComponent<T> (T component) where T : Component
    {
        
    }
}