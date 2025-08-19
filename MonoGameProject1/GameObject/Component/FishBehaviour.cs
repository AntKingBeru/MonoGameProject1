using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class FishBehaviour : SimpleComponent
{
    private static GameObject player;
    private Func<GameObject, GameTime, bool, Vector2> currentPattern;
    private SpriteConfig spriteConfiguration;
    
    private double lastPatternChangeTime;
    private bool invertPattern = false;
    private float timer = 0f;
    public float Speed;
    public bool isAboomnapha = false;
    
    private const float PATTERN_CHANGE_INTERVAL = 0.5f; //change interval in seconds
    private const float SPEED_MULTIPLIER = 0.5f;
    private const float MINIMUM_RUN_AWAY_DISTANCE = 100f;

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
        if (gameObject is not { IsActive: true }) return;
        if (InitializeSpriteConfiguration()) return; // Ensure sprite configuration is available
        if (!IsActive || currentPattern == null) return;
        if (player == null) return;
        
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        ChangeBehaviourByTimer(deltaTime);
        
        ChangeBehaviourByPlayerPosition();

        var nextPosition = currentPattern(gameObject, gameTime , invertPattern);

        if (nextPosition.X >= ScreenPosition.RightGameBoundary().X ||
            nextPosition.X <= ScreenPosition.LeftGameBoundary().X ||
            nextPosition.Y >= ScreenPosition.BottomGameBoundary().Y + ScreenPosition.ScreenHeight * 0.5f)
        {
            invertPattern = !invertPattern;
            nextPosition = gameObject.Position;
        }

        spriteConfiguration.Effects = gameObject.Position.X > nextPosition.X ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

        gameObject.Position = nextPosition;
        
        gameObject.Position.Y -= Speed * deltaTime * SPEED_MULTIPLIER;
    }

    private void ChangeBehaviourByPlayerPosition()
    {
        float distanceToPlayer = Vector2.Distance(gameObject.Position, player.Position);
        if (distanceToPlayer < MINIMUM_RUN_AWAY_DISTANCE)
        {
            // If too close to player, run away
            currentPattern = FishPatterns.RunAway;
            timer = 0;
        }
    }

    private void ChangeBehaviourByTimer(float deltaTime)
    {
        timer += deltaTime;

        if (timer - lastPatternChangeTime >= PATTERN_CHANGE_INTERVAL)
        {
            var randomIndex = Random.Shared.Next(0, AllPatterns.Count);

            currentPattern = AllPatterns[randomIndex];
            lastPatternChangeTime = timer;
        }
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
    public static void SetPlayer(GameObject playerRef)
    {
        player = playerRef;
    }
}