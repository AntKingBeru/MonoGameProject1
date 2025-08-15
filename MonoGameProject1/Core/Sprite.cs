using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class Sprite : Component
{
    public SpriteData data;

    public Sprite() : base()
    {
        
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var drawOrigin = gameObject.Origin;
        if (data.texture != null)
        {
            var sourceRect = data.sourceRectangle.IsEmpty
                ? new Rectangle(0, 0, data.texture.Width, data.texture.Height)
                : data.sourceRectangle;
          
        }

        spriteBatch.Draw(
            data.texture,
            gameObject.Position,
            data.sourceRectangle,
            Color.White,
            MathHelper.ToRadians(gameObject.Rotation),
            drawOrigin, 
            gameObject.Scale,
            data.effects,
            data.layerDepth 
        );
    }

    public override void Update(GameTime gameTime)
    {
        
    }
}