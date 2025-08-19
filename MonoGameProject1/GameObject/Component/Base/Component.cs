using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class Component : IUpdateables, IDrawables
{
    // Base class for all components that can be attached to a GameObject.
    public GameObject gameObject;
    public bool IsActive {get;  set;}
    
    protected Component()
    {
        
    }
    

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    public virtual void SetActive(bool activeState)
    {
        if (activeState)
        {
            IsActive = true;
            gameObject.EnableComponent(this);    
            OnEnable();
        }

        else
        {
            IsActive = false;
            gameObject.DisableComponent(this);
            OnDisable();
        }
    }

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch spriteBatch);
}