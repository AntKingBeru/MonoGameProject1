using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Text : IUpdateable, IDrawable
{
    private SpriteFont _font;
    private Vector2 _textCenter;

    public string text = "";
    public float Rotation = 0f;
    public Vector2 Position =  Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    
    public Text(SpriteFont font)
    {
        _font = font;
    }

    public void Update(GameTime gameTime)
    {
        _textCenter = _font.MeasureString(text) * 0.5f;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _font,
            text,
            Position,
            Color.White,
            MathHelper.ToRadians(Rotation),
            Scale,
            SpriteEffects.None,
            0
            );
    }
}