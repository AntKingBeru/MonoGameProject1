using System.Collections.Generic;

namespace MonoGameProject1.Core;

public static class CollisionManager
{
    private static List<Collider> colliders = new List<Collider>();
    private static List<Collider> triggers = new List<Collider>();
    
    public static void DetectCollisions()
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            Collider colliderA = colliders[i];
            if (!colliderA.IsActive) continue;

            for (int j = i + 1; j < colliders.Count; j++)
            {
                Collider colliderB = colliders[j];
                if (!colliderB.IsActive) continue;

                if (colliderA.colliderConfig.Bounds.Intersects(colliderB.colliderConfig.Bounds))
                {
                    colliderA.Notify(colliderB);
                    colliderB.Notify(colliderA);
                }
            }
        }
        
        for (int i = 0; i < colliders.Count; i++)
        {
            Collider colliderA = colliders[i];
            if (!colliderA.IsActive) continue;

            for (int j = 0; j < triggers.Count; j++)
            {
                Collider triggerB = triggers[j];
                if (!triggerB.IsActive) continue;

                if (colliderA.colliderConfig.Bounds.Intersects(triggerB.colliderConfig.Bounds))
                {
                    colliderA.Notify(triggerB);
                    triggerB.Notify(colliderA);
                }
            }
        }
    }
    
    public static void RegisterCollider(Collider collider)
    {
        if (collider.colliderConfig.IsTrigger)
        {
            triggers.Add(collider);
        }
        else
        {
            colliders.Add(collider);
        }
    }
}