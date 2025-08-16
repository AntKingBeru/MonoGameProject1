namespace MonoGameProject1.Core;

public class AnimationConfig : SpriteConfig
{
    public SpriteSheet spriteSheet;
    public float CroppedWidth = 1f;
    public float CroppedHeight = 1f;
    public int IndexX = 0;
    public int IndexY = 0;
    public double FrameTimer = 0;
    public bool Animating = true;
    public int Fps = 30;
    public bool InLoop = true;
    
    public AnimationConfig(SpriteSheetInfo spriteSheetInfo) : base(spriteSheetInfo)
    {
        spriteSheet = new SpriteSheet(spriteSheetInfo);
        CroppedWidth = 1f;
        CroppedHeight = 1f;
        IndexX = 0;
        IndexY = 0;
        FrameTimer = 0;
        Animating = true;
    }
}