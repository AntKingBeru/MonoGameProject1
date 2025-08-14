using Microsoft.Xna.Framework;

namespace MonoGameProject1;

public class Animation : Sprite
{
    public int Columns;
    public int Rows;

    public float CroppedWidth = 1f;
    public float CroppedHeight = 1f;

    private int _indexX = 0;
    private int _indexY = 0;

    private double _frameTimer = 0;
    private bool _animating = false;
    private int _fps;
    private bool _inLoop;

    private Rectangle Rect { get; set; }

    public Rectangle this[int indexX, int indexY]
    {
        get
        {
            Point location = new Point(
                (int)(Texture.Width * CroppedWidth * ((float)indexX / Columns)),
                (int)(Texture.Height * CroppedHeight * ((float)indexY / Rows))
            );

            Point size = new Point(
                (int)(Texture.Width * CroppedWidth * (1.0f / Columns)),
                (int)(Texture.Height * CroppedHeight * (1.0f / Rows))
            );
            
            return new Rectangle(location, size);
        }
    }

    public Animation(string animationName) : base("Animation_" + animationName)
    {
    }

    public Rectangle GetDestRectangle(Rectangle rect)
    {
        int width = (int)(rect.Width * Scale.X);
        int height = (int)(rect.Height * Scale.Y);

        int posX = (int)(Position.X - width * 0.5f);
        int posY = (int)(Position.Y - height * 0.5f);

        return new Rectangle(posX, posY, width, height);
    }

    public void PlayAnimation(bool inLoop = true, int fps = 60)
    {
        this._fps = fps;
        this._inLoop = inLoop;
        Origin = new Vector2(Texture.Width * CroppedWidth * 0.5f, Texture.Height * CroppedHeight * 0.5f);
        ResetAnimation();
        _animating = true;
    }

    public bool IsAnimating()
    {
        return _animating;
    }

    public double GetTimeRemaining(bool normalized = true)
    {
        int totalFrames = Columns + Rows;
        double deltaFrame = 1.0 / _fps;
        double totalTime = totalFrames * deltaFrame;

        float remainingTime = MathHelper.Clamp((float)(totalTime - _frameTimer), 0.0f, (float)totalTime);

        return (normalized) ? remainingTime / totalTime : remainingTime;
    }

    public void PauseAnimation()
    {
        _animating = false;
    }

    public void ResumeAnimation()
    {
        _animating = true;
    }

    public void StopAnimation()
    {
        PauseAnimation();
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        _frameTimer = 0;
        _indexX = 0;
        _indexY = 0;
    }

    private bool ShouldGetNextFrame(GameTime gameTime)
    {
        _frameTimer += gameTime.ElapsedGameTime.TotalSeconds;

        if (_frameTimer > (1.0 / _fps))
            return true;

        return false;
    }

    private void MoveNextFrame()
    {
        _frameTimer = 0;

        if (_inLoop)
        {
            _indexX++;

            if (_indexX == Columns)
            {
                _indexY++;
                _indexY %= Rows;
            }

            _indexX %= Columns;
        }
        else
        {
            if (_indexX + 1 < Columns)
                _indexX++;
            else if (_indexY + 1 < Rows)
            {
                _indexY++;
                _indexX = 0;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (_animating)
        {
            if (ShouldGetNextFrame(gameTime))
                MoveNextFrame();
        }

        SourceRectangle = this[_indexX, _indexY];

        var r = SourceRectangle;

        Rect = GetDestRectangle(r);

        base.Update(gameTime);
    }
}