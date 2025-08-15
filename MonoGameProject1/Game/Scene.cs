using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public abstract class Scene
{
    public delegate void SceneUnloadHandler();
    public event SceneUnloadHandler OnSceneUnload;

    public string Name { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<int, GameObject> SceneObjects { get; set; }

    public abstract void OnEnable();

    public abstract void Init();
    public abstract void OnDisable();

    public void Update(GameTime gameTime)
    {
        if (!IsActive) return;

        foreach (var gameObject in SceneObjects.Values)
        {
            if (gameObject.IsActive)
            {
                gameObject.Update(gameTime);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var gameObject in SceneObjects.Values)
        {
            if (gameObject.IsActive)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}