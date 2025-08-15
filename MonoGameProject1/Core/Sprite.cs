using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class Sprite : Component
{
    private SpriteConfig config;

    public Sprite() : base()
    {
        
    }
    
    public override void Initialize<T>(T config)
    {
        base.Initialize(config);
        if (config is SpriteConfig spriteConfig)
        {
            this.config = spriteConfig;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var drawOrigin = gameObject.Origin;
        if (config.Texture != null)
        {
            var sourceRect = config.DestRectangle.IsEmpty
                ? new Rectangle(0, 0, config.Texture.Width, config.Texture.Height)
                : config.SourceRectangle;
          
        }

        spriteBatch.Draw(
            config.Texture,
            gameObject.Position,
            config.SourceRectangle,
            Color.White,
            MathHelper.ToRadians(gameObject.Rotation),
            drawOrigin, 
            gameObject.Scale,
            config.Effects,
            config.LayerDepth 
        );
    }

    public override void Update(GameTime gameTime)
    {
        
    }
}