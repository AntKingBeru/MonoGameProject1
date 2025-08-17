using System;
using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public static class FishPatterns
{
    public static bool Invert;

    public static Vector2 SineWave(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float amplitude = 1f;
        float frequency = 2f;
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

    public static Vector2 ZigZag(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float speed = 1f;
        float width = 15f;
        float x;

        if (!Invert)
        {
            x = go.Position.X +
                (time % 2 < 1 ? speed : -speed) * (float)gt.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            x = go.Position.X -
                (time % 2 < 1 ? speed : -speed) * (float)gt.ElapsedGameTime.TotalSeconds;
        }


        // x = MathHelper.Clamp(x, -width, width);

        return new Vector2(x, go.Position.Y);
    }

    public static Vector2 Circle(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float radius = 5f;
        float speed = 1f;
        float x, y;

        if (!Invert)
        {
            x=go.Position.X + MathF.Cos(time * speed) * radius;
            y= go.Position.Y + MathF.Sin(time * speed) * radius;
        }
        else
        {
            x=go.Position.X - MathF.Cos(time * speed) * radius;
            y= go.Position.Y - MathF.Sin(time * speed) * radius;
        }
        
        
        
        return new Vector2(x,y);
         
       
    }
    
    public static Vector2 SpiralOut(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float spiralSpeed = 0.8f;
        float radiusGrowth = 2f;
        float currentRadius = radiusGrowth * time;
        float x, y;

        if (!Invert)
        {
            x = go.Position.X + MathF.Cos(time * spiralSpeed) * currentRadius;
            y = go.Position.Y + MathF.Sin(time * spiralSpeed) * currentRadius;
        }
        else
        {
            x = go.Position.X - MathF.Cos(time * spiralSpeed) * currentRadius;
            y = go.Position.Y - MathF.Sin(time * spiralSpeed) * currentRadius;
        }
        
        return new Vector2(x, y);
    }
    
    public static Vector2 Figure8(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float speed = 1.5f;
        float amplitude = 8f;
        float x, y;

        if (!Invert)
        {
            x = go.Position.X + MathF.Sin(time * speed) * amplitude;
            y = go.Position.Y + MathF.Sin(time * speed * 2) * amplitude * 0.5f;
        }
        else
        {
            x = go.Position.X - MathF.Sin(time * speed) * amplitude;
            y = go.Position.Y - MathF.Sin(time * speed * 2) * amplitude * 0.5f;
        }
        
        return new Vector2(x, y);
    }
    
    public static Vector2 RandomDrift(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float driftStrength = 3f;
        float noiseScale = 0.5f;
        
        // Using sine functions with different frequencies to create pseudo-random movement
        float noiseX = MathF.Sin(time * noiseScale) + MathF.Sin(time * noiseScale * 2.7f) * 0.5f;
        float noiseY = MathF.Cos(time * noiseScale * 1.3f) + MathF.Cos(time * noiseScale * 3.1f) * 0.3f;
        
        float x, y;

        if (!Invert)
        {
            x = go.Position.X + noiseX * driftStrength * (float)gt.ElapsedGameTime.TotalSeconds;
            y = go.Position.Y + noiseY * driftStrength * (float)gt.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            x = go.Position.X - noiseX * driftStrength * (float)gt.ElapsedGameTime.TotalSeconds;
            y = go.Position.Y - noiseY * driftStrength * (float)gt.ElapsedGameTime.TotalSeconds;
        }
        
        return new Vector2(x, y);
    }
    
    public static Vector2 Bounce(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float bounceHeight = 10f;
        float bounceSpeed = 2f;
        float horizontalSpeed = 5f;
        float x, y;

        if (!Invert)
        {
            x = go.Position.X + horizontalSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
            y = go.Position.Y + MathF.Abs(MathF.Sin(time * bounceSpeed)) * bounceHeight * (float)gt.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            x = go.Position.X - horizontalSpeed * (float)gt.ElapsedGameTime.TotalSeconds;
            y = go.Position.Y - MathF.Abs(MathF.Sin(time * bounceSpeed)) * bounceHeight * (float)gt.ElapsedGameTime.TotalSeconds;
        }
        
        return new Vector2(x, y);
    }
}