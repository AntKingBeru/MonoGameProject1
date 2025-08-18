using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class ConfigurableComponent : Component
{
    // an extentions class for Component to handle configuration settings
    // this class can be used configured before enabling it
    public virtual void Initialize<T>(T config) where T : ComponentConfig
    {
        SetActive(true);
    }
    
    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
    }
}