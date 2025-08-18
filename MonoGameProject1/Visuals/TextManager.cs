using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Visuals;

public class TextManager
{
    static Dictionary<string, FontInfo> fonts = new Dictionary<string, FontInfo>();
    public static ContentManager ContentMan { set; get; }

    public static void AddFont(string name, string filePath)
    {
        fonts[name] = new FontInfo();
        fonts[name].Font = ContentMan.Load<SpriteFont>(filePath);
    }

    public static FontInfo GetFont(string name)
    {
        return fonts.GetValueOrDefault(name);
    }
}