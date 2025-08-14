using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class Sprite : IUpdatable, IDrawable
{
    //Semi-Dynamic variables
    protected Texture2D _texture;
    private Vector2 _pivot;
    protected Rectangle? sourceRectangle = null;
    
    //Dynamic variables
    public float Rotation = 0f;
    public Vector2 Position =  Vector2.Zero;
    public Vector2 Scale = Vector2.One;
    public SpriteEffects Effect = SpriteEffects.None;
    
    protected Vector2 Origin = Vector2.Zero;
    protected Rectangle destRectangle;
    
    public Sprite(string name)
    {
        ChangeSprite(name);

        Origin = new Vector2(_texture.Width * 0.5f, _texture.Height * 0.5f);
        
        destRectangle = GetDestRectangle(_texture.Bounds);
    }
    
    public void ChangeSprite(string name)
    {
        _texture = SpriteManager.GetSprite(name).Texture;
        _pivot = Origin;
    }
    
    public virtual void Update(GameTime gameTime)
    {
        
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _texture,
            Position,
            sourceRectangle,
            Color.White,
            MathHelper.ToRadians(Rotation),
            _pivot,
            Scale,
            Effect,
            0
            );
    }

    public Rectangle GetDestRectangle(Rectangle sourceRectangle)
    {
        int width = (int)(sourceRectangle.Width * Scale.X);
        int height = (int)(sourceRectangle.Height * Scale.Y);
        int pos_x = (int)(sourceRectangle.X - Origin.X * Scale.X);
        int pos_y = (int)(sourceRectangle.Y - Origin.Y * Scale.Y);
        return new Rectangle(pos_x, pos_y, width, height);
    }
}