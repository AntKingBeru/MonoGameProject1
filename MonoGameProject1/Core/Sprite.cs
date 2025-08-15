using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class Sprite : Component
{
    protected SpriteData data;

    public Sprite() : base()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        /*Vector2 drawOrigin = Origin;
        if (Texture != null)
        {
            Rectangle sourceRect = SourceRectangle.IsEmpty
                ? new Rectangle(0, 0, Texture.Width, Texture.Height)
                : SourceRectangle;
          
        }

        spriteBatch.Draw(
            Texture,
            Position,
            SourceRectangle,
            Color.White,
            MathHelper.ToRadians(Rotation),
            drawOrigin, 
            Scale,
            Effect,
            LayerDepth 
        );*/
    }

    public override void Update(GameTime gameTime)
    {
    }
}