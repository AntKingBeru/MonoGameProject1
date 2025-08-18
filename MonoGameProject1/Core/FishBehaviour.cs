using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class FishBehaviour : SimpleComponent
{
    private Func<GameObject, GameTime, Vector2> currentPattern;
    private double lastPatternChangeTime;
    private const float PatternChangeInterval = 0.5f; //change interval in seconds
    private bool invertPattern = false;
    private float timer = 0f;

    private static readonly List<string> FishSprites = new()
        { "GoldFish", "ShrimpsPink", "ShrimpsOrange", "ShrimpsRed", "Abumnapha" };

    private static readonly List<Func<GameObject, GameTime, Vector2>> AllPatterns = new()
    {
        FishPatterns.SineWave,
        FishPatterns.ZigZag,
        FishPatterns.Circle,
        FishPatterns.SpiralOut,
        FishPatterns.Figure8,
        FishPatterns.RandomDrift,
        FishPatterns.Bounce
    };

    public void SetPattern(Func<GameObject, GameTime, Vector2> pattern)
    {
        currentPattern = pattern;
    }

    protected override void OnEnable()
    {
        // Set default pattern and initialize timer
        currentPattern = FishPatterns.SineWave;
        lastPatternChangeTime = 0;
    }

    public override void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        timer += deltaTime;
        if (!IsActive || currentPattern == null) return;

        if (timer - lastPatternChangeTime >= PatternChangeInterval)
        {
            var randomIndex = Random.Shared.Next(0, AllPatterns.Count);

            currentPattern = AllPatterns[randomIndex];
            lastPatternChangeTime = timer;
        }


        var nextPosition = currentPattern(gameObject, gameTime);
        
        if (nextPosition.X >= ScreenPosition.RightGameBoundary().X ||
            nextPosition.X <= ScreenPosition.LeftGameBoundary().X)
        {
            return;
        }

        gameObject.Position = nextPosition;
    }

    public static string GetRandomFishSprite()
    {
        return FishSprites[Random.Shared.Next(0, FishSprites.Count)];
    }
}