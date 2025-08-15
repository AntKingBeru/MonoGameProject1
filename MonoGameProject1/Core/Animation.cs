using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public class Animation : Sprite
{
    protected int columns;
    protected int rows;

    protected float croppedWidth = 1f;
    protected float croppedHeight = 1f;

    protected int indexX = 0;
    protected int indexY = 0;

    protected double frameTimer = 0;
    protected bool animating = false;
    protected int fps;
    protected bool inLoop;

    protected Rectangle Rect { get; set; }

    private Rectangle this[int indexX, int indexY]
    {
        get
        {
            var location = new Point(
                (int)(data.texture.Width * croppedWidth * ((float)indexX / columns)),
                (int)(data.texture.Height * croppedHeight * ((float)indexY / rows))
            );

            var size = new Point(
                (int)(data.texture.Width * croppedWidth * (1.0f / columns)),
                (int)(data.texture.Height * croppedHeight * (1.0f / rows))
            );
            
            return new Rectangle(location, size);
        }
    }

    public Animation() : base()
    {
        
    }

    public Rectangle GetDestRectangle(Rectangle rect)
    {
        var width = (int)(rect.Width * gameObject.Scale.X);
        var height = (int)(rect.Height * gameObject.Scale.Y);

        var posX = (int)(gameObject.Position.X - width * 0.5f);
        var posY = (int)(gameObject.Position.Y - height * 0.5f);

        return new Rectangle(posX, posY, width, height);
    }

    public void PlayAnimation(bool inLoop = true, int fps = 60)
    {
        this.fps = fps;
        this.inLoop = inLoop;
        gameObject.Origin = new Vector2(data.texture.Width * croppedWidth * 0.5f, data.texture.Height * croppedHeight * 0.5f);
        ResetAnimation();
        animating = true;
    }

    public bool IsAnimating()
    {
        return animating;
    }

    public double GetTimeRemaining(bool normalized = true)
    {
        var totalFrames = columns + rows;
        var deltaFrame = 1.0 / fps;
        var totalTime = totalFrames * deltaFrame;

        var remainingTime = MathHelper.Clamp((float)(totalTime - frameTimer), 0.0f, (float)totalTime);

        return (normalized) ? remainingTime / totalTime : remainingTime;
    }

    public void PauseAnimation()
    {
        animating = false;
    }

    public void ResumeAnimation()
    {
        animating = true;
    }

    public void StopAnimation()
    {
        PauseAnimation();
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        frameTimer = 0;
        indexX = 0;
        indexY = 0;
    }

    private bool ShouldGetNextFrame(GameTime gameTime)
    {
        frameTimer += gameTime.ElapsedGameTime.TotalSeconds;

        if (frameTimer > (1.0 / fps))
            return true;

        return false;
    }

    private void MoveNextFrame()
    {
        frameTimer = 0;

        if (inLoop)
        {
            indexX++;

            if (indexX == columns)
            {
                indexY++;
                indexY %= rows;
            }

            indexX %= columns;
        }
        else
        {
            if (indexX + 1 < columns)
                indexX++;
            else if (indexY + 1 < rows)
            {
                indexY++;
                indexX = 0;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (animating)
        {
            if (ShouldGetNextFrame(gameTime))
                MoveNextFrame();
        }

        data.sourceRectangle = this[indexX, indexY];

        var r = data.sourceRectangle;

        Rect = GetDestRectangle(r);

        base.Update(gameTime);
    }
}