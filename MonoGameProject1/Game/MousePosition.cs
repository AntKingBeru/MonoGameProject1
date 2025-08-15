using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject1;

public class MousePosition : Text
{
    public MousePosition(SpriteFont font) : base(font)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        _Text = $"({Mouse.GetState().Position.X}, {Mouse.GetState().Position.Y})";
        _textCenter = _font.MeasureString(_Text) * 0.5f;
    }
}