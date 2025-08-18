using System;
using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public static class FishPatterns
{
    private static GameObject player;

    public static void SetPlayer(GameObject playerRef)
    {
        player = playerRef;
    }

    public static Vector2 RunAway(GameObject go, GameTime gt, bool Invert = false)
    {
        var speed = 4f;
        var result = go.Position - player.Position;
        result.Normalize();
        result *= speed;
        return go.Position + result;
    }

    public static Vector2 SineWave(GameObject go, GameTime gt , bool Invert = false)
    {
        var time = (float)gt.ElapsedGameTime.TotalSeconds;
        var amplitude = 5f;
        var frequency = 20f;
        float x;
        if (!Invert)
        {
            x = go.Position.X + MathF.Sin(time * frequency) * amplitude;
        }
        else
        {
            x = go.Position.X - MathF.Sin(time * frequency) * amplitude;
        }
        
        return new Vector2(
            x,
            go.Position.Y
        );
    }

    public static Vector2 QuickLeftRight(GameObject go, GameTime gt, bool Invert = false)
    {
        float time = (float)gt.ElapsedGameTime.TotalSeconds;
        float radius = 5f;
        float speed = 5f;
        float x, y;

        if (!Invert)
        {
            x=go.Position.X + MathF.Cos(time * speed) * radius;
            y= (go.Position.Y + MathF.Sin(time * speed) * radius);
        }
        else
        {
            x=go.Position.X - MathF.Cos(time * speed) * radius;
            y= (go.Position.Y - MathF.Sin(time * speed) * radius);
        }
        return new Vector2(x,y);
    }
    
    public static Vector2 Bounce(GameObject go, GameTime gt, bool Invert = false)
    {
        float time = (float)gt.ElapsedGameTime.TotalSeconds;
        float bounceHeight = 100f;
        float bounceSpeed = 20f;
        float horizontalSpeed = 50f;
        float x, y;

        if (!Invert)
        {
            x = go.Position.X + horizontalSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
            y = (go.Position.Y + MathF.Abs(MathF.Sin(time * bounceSpeed)) * bounceHeight * (float)gt.ElapsedGameTime.TotalSeconds);
        }
        else
        {
            x = go.Position.X - horizontalSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
            y = (go.Position.Y - MathF.Abs(MathF.Sin(time * bounceSpeed)) * bounceHeight * (float)gt.ElapsedGameTime.TotalSeconds);
        }
        
        return new Vector2(x, y);
    }
}