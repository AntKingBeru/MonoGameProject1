
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class SpriteSheet(SpriteSheetInfo info)
{
    private SpriteSheetInfo Info {get; } = info;
    
    public Rectangle this[int x, int y] =>
        new(
            new Point(
                (int)(Info.Texture.Width* ((float)x / Info.columns)), 
                (int)(Info.Texture.Height* ((float)y / Info.rows))),
            new Point(
                (int)(Info.Texture.Width* (1.0f / Info.columns)),
                (int)(Info.Texture.Height* (1.0f / Info.rows)))
        );
}

