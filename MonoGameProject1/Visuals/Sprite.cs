using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class Sprite : ConfigComponent
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
            base.Initialize(config);
        }
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (spriteConfig.SpriteInfo != null)
        {
            spriteConfig.SourceRectangle = spriteConfig.DestRectangle.IsEmpty
                ? new Rectangle(0, 0, spriteConfig.SpriteInfo.Texture.Width, spriteConfig.SpriteInfo.Texture.Height)
                : spriteConfig.SourceRectangle;
        }

        spriteBatch.Draw(
            spriteConfig.SpriteInfo.Texture,
            gameObject.Position,
            spriteConfig.SourceRectangle,
            spriteConfig.Color,
            MathHelper.ToRadians(gameObject.Rotation),
            spriteConfig.Origin, 
            gameObject.Scale,
            spriteConfig.Effects,
            spriteConfig.LayerDepth 
        );
    }

    public override void Update(GameTime gameTime)
    {
        
    }

    public override void SetActive(bool activeState)
    {
        if (!activeState)
        {
            base.SetActive(false);
            return;
        }

        if (spriteConfig.SpriteInfo != null)
        {
            base.SetActive(true);
        }
    }
}