using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class SimpleComponent : Component
{
    /// <summary>
    /// a simple component that does not need any configuration
    /// this component can be used as a base class for other components that do not require configuration
    /// </summary>
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