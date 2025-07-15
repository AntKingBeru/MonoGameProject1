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
    
    public Sprite(string spriteName)
    {
        Texture = SpriteManager.GetSprite(spriteName).Texture;
        Pivot = new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f);
    }
    
    public virtual void Update(GameTime gameTime)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
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
}