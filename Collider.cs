using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Collider : Sprite
{
    public Rectangle Rect;
    public bool IsTrigger = false;
    
    //Event-related
    public delegate void CollisionHandler(Collider other);
    public event CollisionHandler OnCollision;
    public event CollisionHandler OnTrigger;

    public Collider() : base("pixel")
    {
        
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        int thickness = 5;
        // Draw outline
        
        // top
        spriteBatch.Draw(
            Texture,
            new Rectangle(
                Rect.X,
                Rect.Y,
                Rect.Width,
                thickness), 
            Color.White);
        
        // left
        spriteBatch.Draw(
            Texture,
            new Rectangle(
                Rect.X,
                Rect.Y,
                thickness,
                Rect.Height), 
            Color.White);
        
        // right
        spriteBatch.Draw(
            Texture,
            new Rectangle(
                Rect.X + Rect.Width - thickness,
                Rect.Y,
                thickness,
                Rect.Height), 
            Color.White);
        
        // bottom
        spriteBatch.Draw(
            Texture,
            new Rectangle(
                Rect.X,
                Rect.Y + Rect.Height - thickness,
                Rect.Width,
                thickness), 
            Color.White);
    }
    
    public bool Intersect(Collider other)
    {
        return Rect.Intersects(other.Rect);
    }

    public void Notify(Collider other)
    {
        if (IsTrigger)
            OnTrigger?.Invoke(other);
        else
            OnCollision?.Invoke(other);
    }
}