using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class SceneManager : IUpdatable , IDrawable
{
    private static  List<IUpdatable> _updatables = new List<IUpdatable>();
    private static List<IDrawable> _drawables = new List<IDrawable>();

    private static SceneManager instance;
    public static SceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SceneManager();
            }
            return instance;
        }
    }

    public static T Create<T>() where T : IUpdatable, new()
    {
        T obj = new T();
        _updatables.Add(obj);
        
        if (obj is IDrawable drawable)
        {
            _drawables.Add(drawable);
        }
        
        return obj;
    }

    public static void Remove<T>(T obj) where T : IUpdatable
    {
        _updatables.Remove(obj);
        
        if (obj is IDrawable drawable)
        {
            _drawables.Remove(drawable);
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach (var updatable in _updatables)
        {
            updatable.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var drawable in _drawables)
        {
            drawable.Draw(spriteBatch);
        }
    }
}