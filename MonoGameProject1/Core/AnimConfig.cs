namespace MonoGameProject1.Core;

public class AnimConfig : SpriteConfig
{
    public int Columns;
    public int Rows;
    public float CroppedWidth = 1f;
    public float CroppedHeight = 1f;
    public int IndexX = 0;
    public int IndexY = 0;
    public double FrameTimer = 0;
    public bool Animating = false;
    public int Fps;
    public bool InLoop;
}