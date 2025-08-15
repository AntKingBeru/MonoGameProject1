using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public static class SpriteManager
{
    static Dictionary<string, SpriteSheetInfo> sprites = new Dictionary<string, SpriteSheetInfo>();
    public static ContentManager ContentMan { set; get; }
    public static GraphicsDevice Graphics { set; get; }

    public static void AddSprite(string name, string filePath, int columns = 1, int rows = 1)
    {
        sprites[name] = new SpriteSheetInfo();
        sprites[name].Texture = ContentMan.Load<Texture2D>(filePath);
        sprites[name].Columns = columns;
        sprites[name].Rows = rows;
    }


    public static void AddSprite(string name, SpriteConfig config)

    {
        sprites[name] = new SpriteSheetInfo();
        sprites[name].Texture =
            new Texture2D(Graphics, config.SourceRectangle.Value.Width, config.SourceRectangle.Value.Height);
        Color[] colorData = new Color[config.SourceRectangle.Value.Width * config.SourceRectangle.Value.Height];
        for (int i = 0; i < colorData.Length; i++)
            colorData[i] = config.Color;
        sprites[name].Texture.SetData(colorData);
        sprites[name].Columns = 1;
        sprites[name].Rows = 1;
    }


    // public static SpriteSheetInfo GetSprite(string name)
    // {
    //     return sprites.GetValueOrDefault(name);
    // }
}