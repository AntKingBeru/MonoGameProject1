using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class Fish : GameObject
{
    FishBehaviour behaviour;

    private SpriteSheetInfo spriteInfo;
    
    public delegate void FishCaughtDelegate(Fish fish , bool isAboomnapha);
    public static event FishCaughtDelegate OnFishCaught;

    public Fish(string name) : base(name)
    {
        Position = ScreenPosition.Center();
        Scale = new Vector2(0.12f, 0.12f);
        
        var spriteName = FishBehaviour.GetRandomFishSprite();
        spriteInfo = SpriteManager.GetSprite(spriteName);
        var spriteConfig = new SpriteConfig(spriteInfo);
        spriteConfig.LayerDepth = 0.6f;
        spriteConfig.Origin = Vector2.Zero;
        AddConfigComponent<Sprite, SpriteConfig>(spriteConfig);
        
        var rect = new Rectangle(
            0,
            0,
            60,
            60);
        var colliderConfig = new ColliderConfig(rect , true);
        AddConfigComponent<Collider, ColliderConfig>(colliderConfig);
        behaviour = AddComponent<FishBehaviour>();
        if (spriteName == "Abumnapha")
        {
            behaviour.isAboomnapha = true;
            behaviour.Speed = 0.5f;
        }
        EnableComponent(behaviour);
    }

    public void RandomizeSprite()
    {
        spriteInfo = SpriteManager.GetSprite(FishBehaviour.GetRandomFishSprite());
    }
    
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Name == "PlayerEdge")
        {
            OnFishCaught?.Invoke(this, behaviour.isAboomnapha);
            PlayFishCaughtSound();
        }
    }

    private void PlayFishCaughtSound()
    {
        if (behaviour.isAboomnapha)
        {
            AudioManager.CreateSFXInstanceAndPlay("Boom");
        }
        else
        {
            var rng = new Random();
            var x = rng.Next(0, 2);
            AudioManager.CreateSFXInstanceAndPlay(x == 0 ? "Splat2" : "Splat1");
        }
    }

    public void SetSpeed(float speed)
    {
        behaviour.Speed = speed;
    }
}