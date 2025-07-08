using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Sprite : IUpdateable, IDrawable
{
    private Texture2D _texture;

    public float Rotation = 0f;
    public Vector2 Position =  Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    
    public Sprite(Texture2D texture)
    {
        _texture = texture;
    }
    
    public virtual void Update(GameTime gameTime)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _texture,
            Position,
            null,
            Color.White,
            Rotation,
            new Vector2(_texture.Width * 0.5f, _texture.Height * 0.5f),
            Scale,
            SpriteEffects.None,
            0
            );
    }
}