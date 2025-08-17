using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public class Fish : GameObject
{
    FishBehaviour behaviour;

    public Fish(string name) : base(name)
    {
        Position = ScreenPosition.Center();
        Scale = new Vector2(0.1f, 0.1f);
        var spriteInfo = SpriteManager.GetSprite(FishBehaviour.GetRandomFishSprite());
        var spriteConfig = new SpriteConfig(spriteInfo);
        spriteConfig.LayerDepth = 0.6f;
        spriteConfig.Origin = Vector2.Zero;
        AddConfigComponent<Sprite, SpriteConfig>(spriteConfig);

        var width = (Position.X + spriteInfo.Texture.Width) * Scale.X;
        var height = (Position.Y + spriteInfo.Texture.Height) * Scale.Y;
        var rect = new Rectangle(
            0,
            0,
            50,
            50);
        var colliderConfig = new ColliderConfig(rect);
        AddConfigComponent<Collider, ColliderConfig>(colliderConfig);

        behaviour = AddComponent<FishBehaviour>();
        EnableComponent(behaviour);
    }
}