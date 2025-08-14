using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class GameObject : IUpdateable, IDrawable
{
    public string Name;

    public int Index { get; private set; }
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

    public bool Enabled { get; }
    public int UpdateOrder { get; }
    public event EventHandler<EventArgs> EnabledChanged;
    public event EventHandler<EventArgs> UpdateOrderChanged;

    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }
}