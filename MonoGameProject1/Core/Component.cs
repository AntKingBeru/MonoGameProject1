using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class Component : IUpdateables, IDrawables
{
    protected GameObject gameObject;
    protected bool isActive = true;

    protected Component()
    {
        
    }

    protected virtual void OnEnable()
    {
        
    }
    
    protected virtual void OnDisable()
    {
        
    }

    protected virtual void SetActive(bool activeState)
    {
        
    }
    
    public virtual void Update(GameTime gameTime)
    {
        
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        
    }
}