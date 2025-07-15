using Microsoft.Xna.Framework;

namespace MonoGameProject1;

public class Spritesheet
{
    private SpritesheetInfo Info { get; }

    public Spritesheet(SpritesheetInfo info)
    {
        Info = info;
    }
    
    public Rectangle this[int x, int y] =>
        new(
            new Point(
                (int)(Info.Texture.Width * ((float)x / Info.Columns)),
                (int)(Info.Texture.Height * ((float)y / Info.Rows))),
            new Point(
                (int)(Info.Texture.Width * (1.0f / Info.Columns)),
                (int)(Info.Texture.Height * (1.0f / Info.Rows)))
        );
}