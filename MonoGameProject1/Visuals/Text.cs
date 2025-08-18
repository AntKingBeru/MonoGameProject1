using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Utilities.Configs;

namespace MonoGameProject1;

public class Text : ConfigComponent
{
    public TextConfig textConfig { get; set; }

    public Text() : base()
    {
    }

    public override void Initialize<T>(T config)
    {
        if (config is TextConfig textConfig)
        {
            this.textConfig = textConfig;
        }

        base.Initialize(config);
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(
            textConfig.Font,
            textConfig.Text,
            gameObject.Position,
            textConfig.Color,
            MathHelper.ToRadians(gameObject.Rotation),
            textConfig.TextCenter,
            textConfig.Scale,
            textConfig.SpriteEffects,
            textConfig.Layer
        );
    }
}