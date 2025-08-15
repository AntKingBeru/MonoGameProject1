using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class SpriteConfig : ComponentConfig
{
    public string Name { get; set; }

    public SpriteSheetInfo spriteSheetInfo;
    public SpriteSheetInfo SpriteInfo
    {
        get => spriteSheetInfo;
        set
        {
            if (!(value == null || value == spriteSheetInfo))
            {
                spriteSheetInfo = value;
                SourceRectangle = new Rectangle(0, 0, spriteSheetInfo.Texture.Width, spriteSheetInfo.Texture.Height);
                Origin = new Vector2(spriteSheetInfo.Texture.Width * 0.5f, spriteSheetInfo.Texture.Height * 0.5f);
            }
        }
    }
    public Rectangle? SourceRectangle { get; set; }
    public Rectangle DestRectangle;
    public Color Color = Color.White;
    public SpriteEffects Effects = SpriteEffects.None;
    public Vector2 Origin = Vector2.Zero;
    public float LayerDepth { get; set; }
    
    public SpriteConfig()
    {
        SourceRectangle = new Rectangle(0, 0, 100, 100);
        Color = Color.White;
        Effects = SpriteEffects.None;
        LayerDepth = 0f;
        Name = "";
        DestRectangle = new Rectangle(0, 0, 100, 100);
        SpriteInfo = null;
    }
}