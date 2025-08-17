using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class FishBehaviour : SimpleComponent
{
    private Func<GameObject, GameTime, Vector2> currentPattern;
    private double lastPatternChangeTime;
    private const double PatternChangeInterval = 0.3;
    private bool invertPattern = false;

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

    protected override void OnDisable()
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (!IsActive || currentPattern == null) return;

        if (gameTime.TotalGameTime.TotalSeconds - lastPatternChangeTime >= PatternChangeInterval)
        {
            int randomIndex = Random.Shared.Next(0, AllPatterns.Count);

            currentPattern = AllPatterns[randomIndex];
            lastPatternChangeTime = gameTime.TotalGameTime.TotalSeconds;
        }


        Vector2 nextPosition = currentPattern(gameObject, gameTime);
        if (nextPosition.X >= ScreenPosition.RightGameBoundary().X ||
            nextPosition.X <= ScreenPosition.LeftGameBoundary().X)
        {
            return;
        }

        gameObject.Position = nextPosition;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
    }

    public static string GetRandomFishSprite()
    {
        return FishSprites[Random.Shared.Next(0, FishSprites.Count)];
    }
}