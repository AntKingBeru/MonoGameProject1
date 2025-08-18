using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class ButtonConfig : ComponentConfig
{
    public Sprite Sprite;
    public Rectangle _clickArea;
    public bool WasPressed = false;


    public ButtonConfig(Sprite sprite)
    {
        Sprite = sprite;
        _clickArea = new Rectangle(0, 0, Sprite.spriteConfig.SpriteInfo.Texture.Width, Sprite.spriteConfig.SpriteInfo.Texture.Height);
    }
}