using System;
using System.Collections.Generic;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public static class GameObjectFactory
{
    private static readonly Dictionary<string, (Type ComponentType, ComponentConfig DefaultConfig)> ComponentRegistry = new();

    public static void RegisterComponent<T>(string name, ComponentConfig defaultConfig = null) 
        where T : Component, new()
    {
        ComponentRegistry[name] = (typeof(T), defaultConfig);
    }

    public static GameObject CreateGameObject(string name, params (string ComponentName, ComponentConfig Config)[] components)
    {
        var gameObject = new GameObject(name);

        foreach (var (componentName, config) in components)
        {
            if (!ComponentRegistry.TryGetValue(componentName, out var registration))
            {
                throw new ArgumentException($"Unknown component: {componentName}");
            }

            var method = typeof(GameObject)
                .GetMethod("AddComponent", [typeof(ComponentConfig)])!
                .MakeGenericMethod(registration.ComponentType);
            if (method == null)
            {
                throw new InvalidOperationException($"No suitable AddComponent method found for {registration.ComponentType.Name}");
            }

            var componentConfig = config ?? registration.DefaultConfig;
            method.Invoke(gameObject, new[] { componentConfig });
        }

        return gameObject;
    }
}
