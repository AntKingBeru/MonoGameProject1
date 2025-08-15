using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public static class SpriteManager
{
    static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public static ContentManager Content { set; get; }

    public static GraphicsDevice Graphics { set; get; }

//add sprite with texture from file
    public static void AddSprite(string spriteName, string path)
    {
        Console.WriteLine("Add sprite {" + spriteName + "} with path {" + path + "}");
        Sprite newSprite = new Sprite(spriteName);
        newSprite.Path = path;
        newSprite.Texture = Content.Load<Texture2D>(path);
        newSprite.SourceRectangle = new Rectangle((int)newSprite.Position.X, (int)newSprite.Position.Y,
            newSprite.Texture.Width, newSprite.Texture.Height);
        newSprite.SpriteEffects = SpriteEffects.None;

        sprites[spriteName] = newSprite;
    }

    public static void AddSprite(string spriteName, Rectangle rectangle, Color color)
    {
        Sprite newSprite = new Sprite(spriteName);
        Texture2D texture = new Texture2D(Graphics, rectangle.Width, rectangle.Height);
        Color[] colorData = new Color[rectangle.Width * rectangle.Height];
        for (int i = 0; i < colorData.Length; i++)
            colorData[i] = color;
        texture.SetData(colorData);
        newSprite.Texture = texture;
        newSprite.SourceRectangle = rectangle;
        newSprite.SpriteEffects = SpriteEffects.None;

        sprites[spriteName] = newSprite;
    }
    

    public static Sprite GetSprite(string spriteName)
    {
        return sprites[spriteName];
    }
    
}