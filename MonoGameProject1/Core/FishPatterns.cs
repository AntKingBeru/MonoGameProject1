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
}