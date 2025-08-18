using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class FishBehaviour : SimpleComponent
{
    private Func<GameObject, GameTime, bool, Vector2> currentPattern;
    private double lastPatternChangeTime;
    private const float PatternChangeInterval = 0.5f; //change interval in seconds
    private bool invertPattern = false;
    private float timer = 0f;
    private SpriteConfig spriteConfiguration;
    private const float SPEED_MULTIPLIER = 0.3f;
    private static GameObject player;
    
    public float Speed;

    private static readonly List<string> FishSprites = new()
        { "GoldFish", "ShrimpsPink", "ShrimpsOrange", "ShrimpsRed", "Abumnapha" };

    private static readonly List<Func<GameObject, GameTime, bool, Vector2>> AllPatterns = new()
    {
        FishPatterns.SineWave,
        FishPatterns.QuickLeftRight,
        FishPatterns.Bounce
    };

    protected override void OnEnable()
    {
        // Set default pattern and initialize timer
        currentPattern = FishPatterns.SineWave;
        lastPatternChangeTime = 0;
        var spr = gameObject.GetComponent<Sprite>();
        spriteConfiguration = spr.spriteConfig;
    }

    public override void Update(GameTime gameTime)
    {
        if (gameObject == null || !gameObject.IsActive) return;
        if (InitializeSpriteConfiguration()) return; // Ensure sprite configuration is available
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        timer += deltaTime;
        if (!IsActive || currentPattern == null) return;

        if (timer - lastPatternChangeTime >= PatternChangeInterval)
        {
            var randomIndex = Random.Shared.Next(0, AllPatterns.Count);

            currentPattern = AllPatterns[randomIndex];
            lastPatternChangeTime = timer;
        }

        var nextPosition = currentPattern(gameObject, gameTime , invertPattern);

        if (nextPosition.X >= ScreenPosition.RightGameBoundary().X ||
            nextPosition.X <= ScreenPosition.LeftGameBoundary().X ||
            nextPosition.Y >= ScreenPosition.BottomGameBoundary().Y + ScreenPosition.ScreenHeight * 0.5f)
        {
            invertPattern = !invertPattern;
        }

        if (gameObject.Position.X > nextPosition.X)
        {
            spriteConfiguration.Effects = SpriteEffects.None;
        }
        else
        {
            spriteConfiguration.Effects = SpriteEffects.FlipHorizontally;
        }

        gameObject.Position = nextPosition;
        
        gameObject.Position.Y -= Speed * deltaTime * SPEED_MULTIPLIER;
    }

    private bool InitializeSpriteConfiguration()
    {
        if (spriteConfiguration != null) return false;
        spriteConfiguration = gameObject.GetComponent<Sprite>().spriteConfig;
        return spriteConfiguration == null;
    }

    public static string GetRandomFishSprite()
    {
        return FishSprites[Random.Shared.Next(0, FishSprites.Count)];
    }
    
    public void SetPattern(Func<GameObject, GameTime, bool, Vector2> pattern)
    {
        currentPattern = pattern;
    }

    public static void SetPlayer(GameObject playerRef)
    {
        player = playerRef;
    }
}