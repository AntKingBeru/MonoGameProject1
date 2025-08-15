using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Collider : Sprite
{
    public Rectangle _rect;
    public bool IsTrigger;
    public delegate void CollisionHandler(Collider other);

    public Collider() : base("pixel")
    {
    }

    public event CollisionHandler OnCollision;
    public event CollisionHandler OnTrigger;

    public override void Draw(SpriteBatch _spriteBatch)
    {
        
        int thickness = 5;
        // Draw outline
        
        // top
        _spriteBatch.Draw(Texture,
            new Rectangle(
                _rect.X,
                _rect.Y,
                _rect.Width,
                thickness
            ), 
            Color.White);
        
        // left
        _spriteBatch.Draw(Texture,
            new Rectangle(
                _rect.X,
                _rect.Y,
                thickness,
                _rect.Height
            ), 
            Color.White);
        
        // right
        _spriteBatch.Draw(Texture,
            new Rectangle(
                _rect.X + _rect.Width - thickness,
                _rect.Y,
                thickness,
                _rect.Height
            ), 
            Color.White);
        
        // bottom
        _spriteBatch.Draw(Texture,
            new Rectangle(
                _rect.X,
                _rect.Y + _rect.Height - thickness,
                _rect.Width,
                thickness
            ), 
            Color.White);
    }
    
    public override void Update(GameTime gameTime)
    {
        //destRectangle = GetDestRectangle(_rect);
        Position = new Vector2(_rect.X, _rect.Y);
        base.Update(gameTime);
    }

    public bool Intersect(Collider other)
    {
        return _rect.Intersects(other._rect);
    }

    public void Notify(Collider other)
    {
        if (IsTrigger)
        {
            OnTrigger?.Invoke(other);
        }
        else
        {
            OnCollision?.Invoke(other);
        }
    }
    
    
}