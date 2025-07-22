using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class SceneManager : IUpdatable, IDrawable
{
    //Singleton
    private static SceneManager _instance;

    public static SceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneManager();
            }
            return _instance;
        }
    }

    //Lists
    private static List<IUpdatable> _updates = [];
    private static List<IDrawable> _draws = [];

    public static T Create<T> () where T : IUpdatable, new()
    {
        var obj = new T();
        _updates.Add(obj);

        if (obj is IDrawable drawable)
        {
            _draws.Add(drawable);
        }
        
        return obj;
    }

    public static void Remove<T>(T obj) where T : IUpdatable
    {
        if (!_updates.Contains(obj)) return;
        _updates.Remove(obj);

        if (obj is IDrawable drawable)
        {
            _draws.Remove(drawable);
        }
    }

    public void Update(GameTime gameTime)
    {
        /*foreach (var update in _updates)
        {
            update.Update(gameTime);
        }*/
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var draw in _draws)
        {
            draw.Draw(spriteBatch);
        }
    }
}