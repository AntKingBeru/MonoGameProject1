using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public class Animation : Sprite
{
    public AnimConfig AnimationConfig { get; private set; }

    protected Rectangle? Rect { get; set; }

    private Rectangle this[int indexX, int indexY]
    {
        get
        {
            var location = new Point(
                (int)(AnimationConfig.SpriteInfo.Texture.Width * AnimationConfig.CroppedWidth * ((float)indexX / AnimationConfig.SpriteInfo.Columns)),
                (int)(AnimationConfig.SpriteInfo.Texture.Height * AnimationConfig.CroppedHeight * ((float)indexY / AnimationConfig.SpriteInfo.Rows))
            );

            var size = new Point(
                (int)(AnimationConfig.SpriteInfo.Texture.Width * AnimationConfig.CroppedWidth * (1.0f / AnimationConfig.SpriteInfo.Columns)),
                (int)(AnimationConfig.SpriteInfo.Texture.Height * AnimationConfig.CroppedHeight * (1.0f / AnimationConfig.SpriteInfo.Rows))
            );
            
            return new Rectangle(location, size);
        }
    }

    public Animation() : base()
    {
        
    }

    public void setAnimConfig(AnimConfig animConfig)
    {
        AnimationConfig = animConfig;
    }
    
    public override void Initialize<T>(T config)
    {
        if (config is AnimConfig spriteConfig)
        {
            this.AnimationConfig = spriteConfig;
        }
        base.Initialize(config);
    }

    public Rectangle? GetDestRectangle(Rectangle rect)
    {
        var width = (int)(rect.Width * gameObject.Scale.X);
        var height = (int)(rect.Height * gameObject.Scale.Y);

        var posX = (int)(gameObject.Position.X - width * 0.5f);
        var posY = (int)(gameObject.Position.Y - height * 0.5f);

        return new Rectangle(posX, posY, width, height);
    }

    public void PlayAnimation(bool inLoop = true, int fps = 60)
    {
        AnimationConfig.Fps = fps;
        AnimationConfig.InLoop = inLoop;
        gameObject.Origin = new Vector2(AnimationConfig.SpriteInfo.Texture.Width * AnimationConfig.CroppedWidth * 0.5f, AnimationConfig.SpriteInfo.Texture.Height * AnimationConfig.CroppedHeight * 0.5f);
        ResetAnimation();
        AnimationConfig.Animating = true;
    }

    public bool IsAnimating()
    {
        return AnimationConfig.Animating;
    }

    public double GetTimeRemaining(bool normalized = true)
    {
        var totalFrames = AnimationConfig.SpriteInfo.Columns + AnimationConfig.SpriteInfo.Rows;
        var deltaFrame = 1.0 / AnimationConfig.Fps;
        var totalTime = totalFrames * deltaFrame;

        var remainingTime = MathHelper.Clamp((float)(totalTime - AnimationConfig.FrameTimer), 0.0f, (float)totalTime);

        return (normalized) ? remainingTime / totalTime : remainingTime;
    }

    public void PauseAnimation()
    {
        AnimationConfig.Animating = false;
    }

    public void ResumeAnimation()
    {
        AnimationConfig.Animating = true;
    }

    public void StopAnimation()
    {
        PauseAnimation();
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        AnimationConfig.FrameTimer = 0;
        AnimationConfig.IndexX = 0;
        AnimationConfig.IndexY = 0;
    }

    private bool ShouldGetNextFrame(GameTime gameTime)
    {
        AnimationConfig.FrameTimer += gameTime.ElapsedGameTime.TotalSeconds;

        if (AnimationConfig.FrameTimer > (1.0 / AnimationConfig.Fps))
            return true;

        return false;
    }

    private void MoveNextFrame()
    {
        AnimationConfig.FrameTimer = 0;

        if (AnimationConfig.InLoop)
        {
            AnimationConfig.IndexX++;

            if (AnimationConfig.IndexX == AnimationConfig.SpriteInfo.Columns)
            {
                AnimationConfig.IndexY++;
                AnimationConfig.IndexY %= AnimationConfig.SpriteInfo.Rows;
            }

            AnimationConfig.IndexX %= AnimationConfig.SpriteInfo.Columns;
        }
        else
        {
            if (AnimationConfig.IndexX + 1 < AnimationConfig.SpriteInfo.Columns)
                AnimationConfig.IndexX++;
            else if (AnimationConfig.IndexY + 1 < AnimationConfig.SpriteInfo.Rows)
            {
                AnimationConfig.IndexY++;
                AnimationConfig.IndexX = 0;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (AnimationConfig.Animating)
        {
            if (ShouldGetNextFrame(gameTime))
                MoveNextFrame();
        }

        AnimationConfig.SourceRectangle = this[AnimationConfig.IndexX, AnimationConfig.IndexY];

        var r = AnimationConfig.DestRectangle;

        Rect = GetDestRectangle(r);

        base.Update(gameTime);
    }
}