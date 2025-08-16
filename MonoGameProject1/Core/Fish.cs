using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public class Fish : GameObject
{
    private static readonly List<string> FishSprites = new()
        { "GoldFish", "SrimpPink", "SrimpPurple", "ShrimpRed", "Abumnapha" };

    FishBehaviour behaviour;


    public Fish(string name) : base(name)
    {
        Position = ScreenPosition.Center();
        Scale = new Vector2(0.1f, 0.1f);

        var spriteInfo = SpriteManager.GetSprite(GetRandomFishSprite());
        var spriteConfig = new SpriteConfig(spriteInfo);
        spriteConfig.LayerDepth = 0.6f;
        spriteConfig.Origin = new Vector2(spriteInfo.Texture.Width * 0.5f, spriteInfo.Texture.Height * 0.5f);
        AddComponent<Sprite, SpriteConfig>(spriteConfig);


        var width = (Position.X + spriteInfo.Texture.Width) * Scale.X;
        var height = (Position.Y + spriteInfo.Texture.Height) * Scale.Y;
        var rect = new Rectangle((int)(Position.X - width * 0.5f), (int)(Position.Y - height * 0.5f), (int)width,
            (int)height);
        // var colliderConfig = new ColliderConfig(rect);
        // AddComponent<Collider, ColliderConfig>(colliderConfig);

        behaviour = AddComponent<FishBehaviour>();
        behaviour.SetPattern(FishPatterns.Circle);

        EnableComponent(behaviour);
    }


    private static string GetRandomFishSprite()
    {
        return FishSprites[Random.Shared.Next(0, FishSprites.Count)];
    }
}