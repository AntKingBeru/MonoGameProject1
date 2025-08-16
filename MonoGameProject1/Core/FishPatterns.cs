using System;
using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public static class FishPatterns
{
    public static Func<GameObject, GameTime, Vector2> SineWave = (gameObject, gameTime) =>
    {
        float time = (float)gameTime.TotalGameTime.TotalSeconds;
        float amplitude = 100f;
        float frequency = 2f;
        
        return new Vector2(
            gameObject.Position.X + MathF.Sin(time * frequency) * amplitude,
            gameObject.Position.Y
        );
    };
    
    public static Func<GameObject, GameTime, Vector2> ZigZag = (gameObject, gameTime) =>
    {
        float time = (float)gameTime.TotalGameTime.TotalSeconds;
        float speed = 100f;
        float width = 150f;
        
        float x = gameObject.Position.X + (time % 2 < 1 ? speed : -speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        x = MathHelper.Clamp(x, -width, width);
        
        return new Vector2(x, gameObject.Position.Y);
    };
    
    public static Func<GameObject, GameTime, Vector2> Circle = (gameObject, gameTime) =>
    {
        float time = (float)gameTime.TotalGameTime.TotalSeconds;
        float radius = 100f;
        float speed = 2f;
        
        return new Vector2(
            gameObject.Position.X + MathF.Cos(time * speed) * radius,
            gameObject.Position.Y + MathF.Sin(time * speed) * radius
        );
    };
}