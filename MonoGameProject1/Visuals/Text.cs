using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Text : IUpdatable, IDrawable
{
    //Semi-Dynamic variables
    protected SpriteFont _font;
    protected Vector2 _textCenter;

    //Dynamic variables
    public string _Text;
    public float Rotation = 0f;
    public Vector2 Position =  Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    
    public Text(SpriteFont font)
    {
        _font = font;
    }

    public virtual void Update(GameTime gameTime)
    {
        _textCenter = _font.MeasureString(_Text) * 0.5f;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(
            _font,
            _Text,
            Position,
            Color.White,
            MathHelper.ToRadians(Rotation),
            _textCenter,
            Scale,
            SpriteEffects.None,
            0
            );
    }
}