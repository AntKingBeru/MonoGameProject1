
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class SpriteSheet(SpriteSheetInfo info)
{
    private SpriteSheetInfo Info {get; } = info;
    
    public Rectangle this[int x, int y] =>
        new(
            new Point(
                (int)(Info.Texture.Width* ((float)x / Info.Columns)), 
                (int)(Info.Texture.Height* ((float)y / Info.Rows))),
            new Point(
                (int)(Info.Texture.Width* (1.0f / Info.Columns)),
                (int)(Info.Texture.Height* (1.0f / Info.Rows)))
        );
}

