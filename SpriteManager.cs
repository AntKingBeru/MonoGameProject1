using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class SpriteManager
{
    static Dictionary<string, SpriteSheetInfo> sprites = new Dictionary<string, SpriteSheetInfo>();
    public static ContentManager ContentMan { set; get;  }
    
    
    public static void AddSprite(string name, string filePath, int columns = 1, int rows = 1)
    {
        sprites[name] = new SpriteSheetInfo();
        sprites[name].Texture = ContentMan.Load<Texture2D>(filePath);
        sprites[name].columns = columns;
        sprites[name].rows = rows;
    }

    public static SpriteSheetInfo GetSprite(string name)
    {
        return sprites.GetValueOrDefault(name);
    }
}