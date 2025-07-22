using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Sprite : IUpdatable, IDrawable
{
    //Semi-Dynamic variables
    protected Texture2D Texture;
    protected Vector2 Pivot;
    
    //Dynamic variables
    public float Rotation = 0f;
    public Vector2 Position =  Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    protected SpriteEffects Effect = SpriteEffects.None;
    
    protected Rectangle? SourceRectangle = null;
    protected Rectangle DestRectangle;
    
    public Sprite(string spriteName)
    {
        Texture = SpriteManager.GetSprite(spriteName).Texture;
        Pivot = new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f);
        
        DestRectangle =  GetDestRectangle(Texture.Bounds);
    }

    protected Sprite()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update(GameTime gameTime)
    {
        
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            Position,
            SourceRectangle,
            Color.White,
            MathHelper.ToRadians(Rotation),
            Pivot,
            Scale,
            Effect,
            0
            );
    }

    public Rectangle GetDestRectangle(Rectangle sourceRectangle)
    {
        int width = (int)(sourceRectangle.Width * Scale.X);
        int height = (int)(sourceRectangle.Height * Scale.Y);
        
        int pos_x =  (int)(sourceRectangle.X - Pivot.X * Scale.X);
        int pos_y =  (int)(sourceRectangle.Y - Pivot.Y * Scale.Y);
        
        return  new Rectangle(pos_x, pos_y, width, height);
    }
}