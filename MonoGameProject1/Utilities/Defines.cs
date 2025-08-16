using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Defines
{
}

public static class ScreenPosition
{
    private static GraphicsDevice _graphics;
    
    public static Vector2 TopLeft() => new Vector2(0, 0) ;
    public static Vector2 TopCenter() => new Vector2(ScreenWidth * 0.5f, 0) ;
    public static Vector2 TopRight() => new Vector2(ScreenWidth, 0) ;
    public static Vector2 MiddleLeft() => new Vector2(0, ScreenHeight * 0.5f) ;
    public static Vector2 Center() => new Vector2(ScreenWidth * 0.5f, ScreenHeight * 0.5f) ;
    public static Vector2 MiddleRight() => new Vector2(ScreenWidth, ScreenHeight * 0.5f) ;
    public static Vector2 BottomLeft() => new Vector2(0, ScreenHeight) ;
    public static Vector2 BottomCenter() => new Vector2(ScreenWidth * 0.5f, ScreenHeight) ;
    public static Vector2 BottomRight() => new Vector2(ScreenWidth, ScreenHeight) ;
    
    public static Vector2 LeftGameBoundary() => new Vector2(30, 0);
    public static Vector2 RightGameBoundary() => new Vector2(ScreenWidth - 30, 0);
    public static Vector2 TopGameBoundary() => new Vector2(0, 30);
    public static Vector2 BottomGameBoundary() => new Vector2(0, ScreenHeight - 30);


    public static int ScreenWidth => _graphics.Viewport.Width;
    public static int ScreenHeight => _graphics.Viewport.Height;

    public static Vector2 ClampInBoundaries(Vector2 position)
    {
        return new Vector2(
            MathHelper.Clamp(position.X, LeftGameBoundary().X, RightGameBoundary().X),
            MathHelper.Clamp(position.Y, TopGameBoundary().Y, BottomGameBoundary().Y)
        );
    }

    public static void InitializePos(GraphicsDevice graphics)
    {
        _graphics = graphics;
    }
}