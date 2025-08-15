namespace MonoGameProject1.Core;

public class AnimConfig : SpriteConfig
{
    public SpriteSheet spriteSheet = new SpriteSheet(spriteSheetInfo);
    public float CroppedWidth = 1f;
    public float CroppedHeight = 1f;
    public int IndexX = 0;
    public int IndexY = 0;
    public double FrameTimer = 0;
    public bool Animating = true;
    public int Fps = 30;
    public bool InLoop = true;
}