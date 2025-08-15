using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public class Animation : Sprite
{
    public AnimConfig AnimConfig { get; private set; }

    protected Rectangle? Rect { get; set; }

    private Rectangle this[int indexX, int indexY]
    {
        get
        {
            var location = new Point(
                (int)(AnimConfig.SpriteInfo.Texture.Width * AnimConfig.CroppedWidth * ((float)indexX / AnimConfig.SpriteInfo.Columns)),
                (int)(AnimConfig.SpriteInfo.Texture.Height * AnimConfig.CroppedHeight * ((float)indexY / AnimConfig.SpriteInfo.Rows))
            );

            var size = new Point(
                (int)(AnimConfig.SpriteInfo.Texture.Width * AnimConfig.CroppedWidth * (1.0f / AnimConfig.SpriteInfo.Columns)),
                (int)(AnimConfig.SpriteInfo.Texture.Height * AnimConfig.CroppedHeight * (1.0f / AnimConfig.SpriteInfo.Rows))
            );
            
            return new Rectangle(location, size);
        }
    }

    public Animation() : base()
    {
        
    }
    
    public override void Initialize<T>(T config)
    {
        base.Initialize(config);
        if (config is AnimConfig spriteConfig)
        {
            this.AnimConfig = spriteConfig;
        }
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
        AnimConfig.Fps = fps;
        AnimConfig.InLoop = inLoop;
        gameObject.Origin = new Vector2(AnimConfig.SpriteInfo.Texture.Width * AnimConfig.CroppedWidth * 0.5f, AnimConfig.SpriteInfo.Texture.Height * AnimConfig.CroppedHeight * 0.5f);
        ResetAnimation();
        AnimConfig.Animating = true;
    }

    public bool IsAnimating()
    {
        return AnimConfig.Animating;
    }

    public double GetTimeRemaining(bool normalized = true)
    {
        var totalFrames = AnimConfig.SpriteInfo.Columns + AnimConfig.SpriteInfo.Rows;
        var deltaFrame = 1.0 / AnimConfig.Fps;
        var totalTime = totalFrames * deltaFrame;

        var remainingTime = MathHelper.Clamp((float)(totalTime - AnimConfig.FrameTimer), 0.0f, (float)totalTime);

        return (normalized) ? remainingTime / totalTime : remainingTime;
    }

    public void PauseAnimation()
    {
        AnimConfig.Animating = false;
    }

    public void ResumeAnimation()
    {
        AnimConfig.Animating = true;
    }

    public void StopAnimation()
    {
        PauseAnimation();
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        AnimConfig.FrameTimer = 0;
        AnimConfig.IndexX = 0;
        AnimConfig.IndexY = 0;
    }

    private bool ShouldGetNextFrame(GameTime gameTime)
    {
        AnimConfig.FrameTimer += gameTime.ElapsedGameTime.TotalSeconds;

        if (AnimConfig.FrameTimer > (1.0 / AnimConfig.Fps))
            return true;

        return false;
    }

    private void MoveNextFrame()
    {
        AnimConfig.FrameTimer = 0;

        if (AnimConfig.InLoop)
        {
            AnimConfig.IndexX++;

            if (AnimConfig.IndexX == AnimConfig.SpriteInfo.Columns)
            {
                AnimConfig.IndexY++;
                AnimConfig.IndexY %= AnimConfig.SpriteInfo.Rows;
            }

            AnimConfig.IndexX %= AnimConfig.SpriteInfo.Columns;
        }
        else
        {
            if (AnimConfig.IndexX + 1 < AnimConfig.SpriteInfo.Columns)
                AnimConfig.IndexX++;
            else if (AnimConfig.IndexY + 1 < AnimConfig.SpriteInfo.Rows)
            {
                AnimConfig.IndexY++;
                AnimConfig.IndexX = 0;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (AnimConfig.Animating)
        {
            if (ShouldGetNextFrame(gameTime))
                MoveNextFrame();
        }

        AnimConfig.SourceRectangle = this[AnimConfig.IndexX, AnimConfig.IndexY];

        var r = AnimConfig.DestRectangle;

        Rect = GetDestRectangle(r);

        base.Update(gameTime);
    }
}