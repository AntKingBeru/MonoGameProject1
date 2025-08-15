using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Core;

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
        var newSprite = new Sprite();
        newSprite.spriteConfig.Path = path;
        newSprite.spriteConfig.Texture = Content.Load<Texture2D>(path);
        newSprite.spriteConfig.SourceRectangle = new Rectangle((int)newSprite.gameObject.Position.X, (int)newSprite.Position.Y,
            newSprite.spriteConfig.Texture.Width, newSprite.spriteConfig.Texture.Height);
        newSprite.spriteConfig.Effects = SpriteEffects.None;

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
        newSprite.spriteConfig.Texture = texture;
        newSprite.spriteConfig.SourceRectangle = rectangle;
        newSprite.spriteConfig.Effects = SpriteEffects.None;

        sprites[spriteName] = newSprite;
    }
    

    public static Sprite GetSprite(string spriteName)
    {
        return sprites[spriteName];
    }
    
}