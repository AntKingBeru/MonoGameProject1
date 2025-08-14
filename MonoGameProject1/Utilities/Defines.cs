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
    public static Vector2 MiddleCenter() => new Vector2(ScreenWidth * 0.5f, ScreenHeight * 0.5f) ;
    public static Vector2 MiddleRight() => new Vector2(ScreenWidth, ScreenHeight * 0.5f) ;
    public static Vector2 BottomLeft() => new Vector2(0, ScreenHeight) ;
    public static Vector2 BottomCenter() => new Vector2(ScreenWidth * 0.5f, ScreenHeight) ;
    public static Vector2 BottomRight() => new Vector2(ScreenWidth, ScreenHeight) ;


    private static int ScreenWidth => _graphics.Viewport.Width;
    private static int ScreenHeight => _graphics.Viewport.Height;


    public static void InitializePos(GraphicsDevice graphics)
    {
        _graphics = graphics;
    }
}