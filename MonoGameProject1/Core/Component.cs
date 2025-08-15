using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class Component : IUpdateables, IDrawables
{
    public GameObject gameObject;
    public bool IsActive {get; private set;}
    
    protected Component()
    {
        
    }
    
    public virtual void Initialize<T>(T config) where T : ComponentConfig
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
        if (activeState)
            gameObject.EnableComponent(this);
        else
            gameObject.DisableComponent(this);
    }
    
    public virtual void Update(GameTime gameTime)
    {
        
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        
    }
}