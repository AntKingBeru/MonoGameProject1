using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class SpriteManager
{
    private static Dictionary<string, SpritesheetInfo> _sprites = new Dictionary<string, SpritesheetInfo>();

    public static ContentManager Content { set; get; }

    public static void AddSprite(string spriteName, string fileName, int columns = 1, int rows = 1)
    {
        _sprites[spriteName] = new SpritesheetInfo
        {
            Columns = columns, Rows = rows,
            Texture = Content.Load<Texture2D>(fileName)
        };
    }

    public static SpritesheetInfo GetSprite(string spriteName)
    {
        return _sprites[spriteName];
    }
}