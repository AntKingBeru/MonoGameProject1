using Microsoft.Xna.Framework;

namespace MonoGameProject1;

public class Animation : Sprite
{
   
    private Spritesheet _spritesheet;
    private SpritesheetInfo _info;

    private double _timeCounter = 0;
    private int _fps;
    private bool _isLooping;
    private bool _isAnimating;
    private int _indexX, _indexY;
    
    
    protected Animation(string spriteName) : base(spriteName)
    {
        ChangeAnimation(spriteName);
    }

    protected void ChangeAnimation(string animationName)
    {
        _info = SpriteManager.GetSprite(animationName);
        _spritesheet = new Spritesheet(_info);
        Texture = SpriteManager.GetSprite(animationName).Texture;
        Pivot = new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f);
        ResetAnimation();
    }

    public void ResetAnimation()
    {
        _timeCounter = 0;
        _indexX = 0;
        _indexY = 0;
    }

    public void PauseAnimation()
    {
        _isAnimating = false;
    }

    public void ResumeAnimation()
    {
        _isAnimating = true;
    }

    public void PlayAnimation(bool isLooping = true, int fps = 60)
    {
        ResetAnimation();
        
        _fps = fps;
        _isLooping = isLooping;
        _isAnimating = true;
    }

    private bool ShouldGetNextFrame(GameTime gameTime)
    {
        _timeCounter += gameTime.ElapsedGameTime.TotalSeconds;

        if (_timeCounter >= 1.0f / _fps)
        {
            return true;
        }
        
        return false;
    }

    private void MoveNextFrame()
    {
        if (_isLooping)
        {
            _indexX++;
            if (_indexX == _info.Columns)
            {
                _indexY++;
                _indexY %= _info.Rows;
            }
            _indexX %= _info.Columns;
        }
        else
        {
            //TODO: add logic for not looping
        }
    }
    
    public override void Update(GameTime gameTime)
    {
        if (_isAnimating)
        {
            if (ShouldGetNextFrame(gameTime))
            {
                _timeCounter = 0;
                MoveNextFrame();
            }
        }

        SourceRectangle = _spritesheet[_indexX, _indexY];
        
        base.Update(gameTime);
    }
}