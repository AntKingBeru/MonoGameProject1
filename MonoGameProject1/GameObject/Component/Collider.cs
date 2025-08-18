using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class Collider : ConfigurableComponent
{
    public ColliderConfig colliderConfig {get; private set; }
    public delegate void CollisionHandler(Collider other);
    private static Texture2D Texture => SpriteManager.GetSprite("Pixel").Texture;

    public Collider() : base()
    {
    }

    public override void Initialize<T>(T config)
    {
        if (config is ColliderConfig colliderConfig)
        {
            this.colliderConfig = colliderConfig;
            CollisionManager.RegisterCollider(this);
            base.Initialize(config);
        }
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    public event CollisionHandler OnCollision;
    public event CollisionHandler OnTrigger;

    public override void Draw(SpriteBatch _spriteBatch)
    {
        int thickness = 5;
        var color = Color.Red;
        // Draw outline
        
        // top
        _spriteBatch.Draw(Texture,
            new Rectangle(
                colliderConfig.Bounds.X,
                colliderConfig.Bounds.Y,
                colliderConfig.Bounds.Width,
                thickness
            ), 
            color);
        
        // left
        _spriteBatch.Draw(Texture,
            new Rectangle(
                colliderConfig.Bounds.X,
                colliderConfig.Bounds.Y,
                thickness,
                colliderConfig.Bounds.Height
            ), 
            color);
        
        // right
        _spriteBatch.Draw(Texture,
            new Rectangle(
                colliderConfig.Bounds.X + colliderConfig.Bounds.Width - thickness,
                colliderConfig.Bounds.Y,
                thickness,
                colliderConfig.Bounds.Height
            ), 
            color);
        
        // bottom
        _spriteBatch.Draw(Texture,
            new Rectangle(
                colliderConfig.Bounds.X,
                colliderConfig.Bounds.Y + colliderConfig.Bounds.Height - thickness,
                colliderConfig.Bounds.Width,
                thickness
            ), 
            color);
    }
    
    public override void Update(GameTime gameTime)
    {
        colliderConfig.Bounds = new Rectangle((int)gameObject.Position.X, (int)gameObject.Position.Y, colliderConfig.Bounds.Width, colliderConfig.Bounds.Height);
    }
    
    public void Notify(Collider other)
    {
        if (colliderConfig.IsTrigger)
        {
            //Console.WriteLine("Collision Detected: " + gameObject.Name + " with " + other.gameObject.Name);
            OnTrigger?.Invoke(other);
        }
        else
        {
            OnCollision?.Invoke(other);
        }
    }
}