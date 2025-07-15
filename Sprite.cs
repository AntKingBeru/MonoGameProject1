using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Sprite : IUpdatable, IDrawable
{
    //Semi-Dynamic variables
    protected Texture2D _texture;
    private Vector2 _pivot;
    protected Rectangle? sourceRectangle = null;
    
    //Dynamic variables
    public float Rotation = 0f;
    public Vector2 Position =  Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public SpriteEffects Effect = SpriteEffects.None;
    
    public Sprite(string name)
    {
        ChangeSprite(name);
    }
    
    public void ChangeSprite(string name)
    {
        _texture = SpriteManager.GetSprite(name).Texture;
        _pivot = new Vector2(_texture.Width * 0.5f, _texture.Height * 0.5f);
    }
    
    public virtual void Update(GameTime gameTime)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _texture,
            Position,
            sourceRectangle,
            Color.White,
            MathHelper.ToRadians(Rotation),
            _pivot,
            Scale,
            Effect,
            0
            );
    }
}