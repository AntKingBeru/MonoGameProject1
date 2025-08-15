using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public class Animation : Sprite
{
    private AnimConfig config;

    protected Rectangle? Rect { get; set; }

    private Rectangle this[int indexX, int indexY]
    {
        get
        {
            var location = new Point(
                (int)(config.Texture.Width * config.CroppedWidth * ((float)indexX / config.Columns)),
                (int)(config.Texture.Height * config.CroppedHeight * ((float)indexY / config.Rows))
            );

            var size = new Point(
                (int)(config.Texture.Width * config.CroppedWidth * (1.0f / config.Columns)),
                (int)(config.Texture.Height * config.CroppedHeight * (1.0f / config.Rows))
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
            this.config = spriteConfig;
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
        config.Fps = fps;
        config.InLoop = inLoop;
        gameObject.Origin = new Vector2(config.Texture.Width * config.CroppedWidth * 0.5f, config.Texture.Height * config.CroppedHeight * 0.5f);
        ResetAnimation();
        config.Animating = true;
    }

    public bool IsAnimating()
    {
        return config.Animating;
    }

    public double GetTimeRemaining(bool normalized = true)
    {
        var totalFrames = config.Columns + config.Rows;
        var deltaFrame = 1.0 / config.Fps;
        var totalTime = totalFrames * deltaFrame;

        var remainingTime = MathHelper.Clamp((float)(totalTime - config.FrameTimer), 0.0f, (float)totalTime);

        return (normalized) ? remainingTime / totalTime : remainingTime;
    }

    public void PauseAnimation()
    {
        config.Animating = false;
    }

    public void ResumeAnimation()
    {
        config.Animating = true;
    }

    public void StopAnimation()
    {
        PauseAnimation();
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        config.FrameTimer = 0;
        config.IndexX = 0;
        config.IndexY = 0;
    }

    private bool ShouldGetNextFrame(GameTime gameTime)
    {
        config.FrameTimer += gameTime.ElapsedGameTime.TotalSeconds;

        if (config.FrameTimer > (1.0 / config.Fps))
            return true;

        return false;
    }

    private void MoveNextFrame()
    {
        config.FrameTimer = 0;

        if (config.InLoop)
        {
            config.IndexX++;

            if (config.IndexX == config.Columns)
            {
                config.IndexY++;
                config.IndexY %= config.Rows;
            }

            config.IndexX %= config.Columns;
        }
        else
        {
            if (config.IndexX + 1 < config.Columns)
                config.IndexX++;
            else if (config.IndexY + 1 < config.Rows)
            {
                config.IndexY++;
                config.IndexX = 0;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (config.Animating)
        {
            if (ShouldGetNextFrame(gameTime))
                MoveNextFrame();
        }

        config.SourceRectangle = this[config.IndexX, config.IndexY];

        var r = config.DestRectangle;

        Rect = GetDestRectangle(r);

        base.Update(gameTime);
    }
}