using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class Sprite : Component
{
    public SpriteConfig spriteConfig { get; private set; }

    public Sprite() : base()
    {

    }
    
    public override void Initialize<T>(T config)
    {
        if (config is SpriteConfig spriteConfig)
        {
            this.spriteConfig = spriteConfig;
        }
        base.Initialize(config);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var drawOrigin = gameObject.Origin;
        if (spriteConfig.SpriteInfo != null)
        {
            var sourceRect = spriteConfig.DestRectangle.IsEmpty
                ? new Rectangle(0, 0, spriteConfig.SpriteInfo.Texture.Width, spriteConfig.SpriteInfo.Texture.Height)
                : spriteConfig.SourceRectangle;
          
        }

        spriteBatch.Draw(
            spriteConfig.SpriteInfo.Texture,
            gameObject.Position,
            spriteConfig.SourceRectangle,
            Color.White,
            MathHelper.ToRadians(gameObject.Rotation),
            drawOrigin, 
            gameObject.Scale,
            spriteConfig.Effects,
            spriteConfig.LayerDepth 
        );
    }

    public override void Update(GameTime gameTime)
    {
        
    }
}