using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class SpriteConfig : ComponentConfig
{
    public string Name { get; set; }
    public SpriteSheetInfo SpriteInfo;
    public Rectangle? SourceRectangle { get; set; }
    public Rectangle DestRectangle;
    public Color Color;
    public SpriteEffects Effects;
    public float LayerDepth { get; set; }
}