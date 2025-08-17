using System;
using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public static class FishPatterns
{
    public static Vector2 SineWave(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float amplitude = 100f;
        float frequency = 2f;

        return new Vector2(
            go.Position.X + MathF.Sin(time * frequency) * amplitude,
            go.Position.Y
        );
    }

    public static Vector2 ZigZag(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float speed = 100f;
        float width = 150f;

        float x = go.Position.X +
                  (time % 2 < 1 ? speed : -speed) * (float)gt.ElapsedGameTime.TotalSeconds;
        x = MathHelper.Clamp(x, -width, width);

        return new Vector2(x, go.Position.Y);
    }

    public static Vector2 Circle(GameObject go, GameTime gt)
    {
        float time = (float)gt.TotalGameTime.TotalSeconds;
        float radius = 5f;
        float speed = 2f;

        return new Vector2(
            go.Position.X + MathF.Cos(time * speed) * radius,
            go.Position.Y + MathF.Sin(time * speed) * radius
        );
    }
}