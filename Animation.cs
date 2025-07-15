using Microsoft.Xna.Framework;

namespace MonoGameProject1;

public class Animation : Sprite
{
    SpriteSheet spriteSheet;
    private SpriteSheetInfo spriteSheetInfo;
    private double timeCounter = 0f;
    private int fps;
    private bool isLooped = true;
    private bool isPlaying = false;
    
    private int index_x = 0;
    private int index_y = 0;
    
    public Animation(string name) : base(name)
    {
        ChangeAnimation(name);
    }

    public void ChangeAnimation(string name)
    {
        ResetAnimation();
        spriteSheetInfo = SpriteManager.GetSprite(name);
        spriteSheet = new SpriteSheet(spriteSheetInfo);    
    }

    public override void Update(GameTime gameTime)
    {
        if (isPlaying)
        {
            if (CheckIfNextFrame(gameTime))
            {
                timeCounter = 0f;
                NextFrame();
            }
        }

        sourceRectangle = spriteSheet[index_x, index_y];
        base.Update(gameTime);
    }
    
    public void PlayAnimation(bool isLooped = true, int fps = 60)
    {
        ResetAnimation();
        this.fps = fps;
        this.isLooped = isLooped;
        isPlaying = true;
        timeCounter = 0f;
    }
    
    public void ResetAnimation()
    {
        timeCounter = 0f;
        index_x = 0;
        index_y = 0;
    }

    public void PauseAnimation()
    {
        isPlaying = false;
    }

    public void ResumeAnimation()
    {
        isPlaying = true;
    }
    
    public void StopAnimation()
    {
        isPlaying = false;
        ResetAnimation();
    }

    private bool CheckIfNextFrame(GameTime gameTime)
    {
        timeCounter += gameTime.ElapsedGameTime.TotalSeconds;
        if (timeCounter >= 1.0f/fps)
        {
            return true;
        }
        return false;
    }
    
    private void NextFrame()
    {
        if (isLooped)
        {
            index_x++;
            if (index_x == spriteSheetInfo.columns)
            {
                index_y++;
                index_y %= spriteSheetInfo.rows;
            }
            index_x %= spriteSheetInfo.columns;
        }
    }
}