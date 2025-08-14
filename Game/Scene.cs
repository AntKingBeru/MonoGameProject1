using System.Collections.Generic;

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
}