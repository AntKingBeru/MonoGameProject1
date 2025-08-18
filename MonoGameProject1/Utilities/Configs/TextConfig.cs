using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Utilities.Configs;

public class TextConfig : ComponentConfig
{
    public string Name { get; set; }
    public SpriteFont Font { get; set; }
    public string Text { get; set; }
    public Color Color { get; set; }
    public Vector2 TextCenter { get; set; }
    public float Scale { get; set; }
    public SpriteEffects SpriteEffects { get; set; }
    public float Layer { get; set; }
}