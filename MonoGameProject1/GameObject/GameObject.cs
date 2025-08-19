using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class GameObject : IUpdateables, IDrawables, IColliderMethods
{
    // Represents a game object in the scene, which can have multiple components attached to it.
    public string Name;
    public readonly int Index;
    public bool IsActive = false;
    public Vector2 Position = Vector2.Zero;
    public Vector2 Scale = new Vector2(0.5f, 0.5f);
    public float Rotation;

    private List<Component> ActiveComponents;
    private List<Component> InactiveComponents;

    private static int GameObjectCounter = 0;
    
    public delegate void GameObjectHandler(GameObject gameObject);
    public event GameObjectHandler OnGameObjectDisable;
    public event GameObjectHandler OnGameObjectEnable;
    

    public GameObject(string name)
    {
        Name = name;
        Index = GameObjectCounter++;
        ActiveComponents = [];
        InactiveComponents = [];
    }

    public virtual void Enable()
    {
        if (IsActive) return; // Prevent re-enabling if already active
        OnGameObjectEnable?.Invoke(this);
        IsActive = true;
        //Enable Logic 
    }

    public virtual void Disable()
    {
        if (!IsActive) return; // Prevent re-disabling if already inactive
        OnGameObjectDisable?.Invoke(this);
        IsActive = false;
        //Disable Logic
    }

    public virtual void Update(GameTime gameTime)
    {
        if (!IsActive) return;

        foreach (Component component in ActiveComponents)
        {
            if (component.IsActive)
            {
                component.Update(gameTime);
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;

        foreach (Component component in ActiveComponents)
        {
            if (component.IsActive)
            {
                component.Draw(spriteBatch);
            }
        }
    }


    public T AddConfigComponent<T, TConfig>(TConfig config)
        where T : ConfigurableComponent, new()
        where TConfig : ComponentConfig
    {
        var component = AddComponent<T>();
        component.Initialize(config);
        return component;
    }

    public T AddComponent<T>() where T : Component, new()
    {
        var newComponent = new T();
        newComponent.gameObject = this;
        InactiveComponents.Add(newComponent);
        return newComponent;
    }

    public T AddSimpleComponent<T>() where T : SimpleComponent, new()
    {
        var newComponent = AddComponent<T>();
        newComponent.SetActive(true); // Automatically activate simple components
        return newComponent;
    }


    public void DisableComponent<T>(T component) where T : Component
    {
        foreach (var c in ActiveComponents)
        {
            if (c == component)
            {
                InactiveComponents.Add(c);
                ActiveComponents.Remove(c);
            }
        }
    }

    public void EnableComponent<T>(T component) where T : Component
    {
        foreach (var c in InactiveComponents.ToList())
        {
            if (c == component)
            {
                ActiveComponents.Add(c);
                InactiveComponents.Remove(c);
                component.SetActive(true); //Added by Matan to fix the activation of components without a config
            }
        }
    }
    
    public T GetComponent<T>() where T : Component
    {
        return ActiveComponents.OfType<T>().FirstOrDefault() ?? InactiveComponents.OfType<T>().FirstOrDefault();
    }

    #region Collision Methods
    public void OnCollisionEnter(Collider other)
    {
    }

    public void OnCollisionStay(Collider other)
    {
    }

    public void OnCollisionExit(Collider other)
    {
    }

    public void OnTriggerEnter(Collider other)
    {
    }

    public void OnTriggerStay(Collider other)
    {
    }

    public void OnTriggerExit(Collider other)
    {
    }
    

    #endregion
    
}