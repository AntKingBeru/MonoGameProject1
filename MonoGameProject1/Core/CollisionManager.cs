using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoGameProject1.Core;

public static class CollisionManager
{
    private static List<Collider> colliders = new List<Collider>();
    private static List<Collider> triggers = new List<Collider>();
    
    private static Stack<Collision> collisions = new Stack<Collision>();
    private static Stack<Collision> wasChecked = new Stack<Collision>();

    public static void DetectCollisions()
    {
        CheckCollisionStay();

        foreach (var colliderA in colliders)
        {
            if (!colliderA.IsActive || !colliderA.gameObject.IsActive || colliderA.colliderConfig.IsTrigger) continue;
            foreach (var colliderB in colliders)
            {
                if (!colliderB.IsActive || !colliderB.gameObject.IsActive ||
                    colliderA.gameObject.Index  == colliderB.gameObject.Index) continue;
                if (colliderA.colliderConfig.Bounds.Intersects(colliderB.colliderConfig.Bounds))
                {
                    if (!CompareCollisionToWasChecked(colliderA, colliderB))
                    {
                        Collision newCollision = new Collision(colliderA, colliderB);
                        collisions.Push(newCollision);
                        wasChecked.Push(newCollision);
                        colliderA.gameObject.OnCollisionEnter(newCollision.ColliderB);
                        colliderB.gameObject.OnCollisionEnter(newCollision.ColliderA);
                        // Invoke collision enter on both colliders
                    }
                }
            }

            foreach (var triggerB in triggers)
            {
                if (!triggerB.IsActive || !triggerB.gameObject.IsActive || 
                    colliderA.gameObject.Index == triggerB.gameObject.Index) continue;
                if (colliderA.colliderConfig.Bounds.Intersects(triggerB.colliderConfig.Bounds))
                {
                    if (colliderA.colliderConfig.Bounds.Intersects(triggerB.colliderConfig.Bounds))
                    {
                        if (!CompareCollisionToWasChecked(colliderA, triggerB))
                        {
                            Collision newCollision = new Collision(colliderA, triggerB);
                            wasChecked.Push(newCollision);
                            triggerB.gameObject.OnTriggerEnter(newCollision.ColliderA);
                            // Invoke trigger enter on collider b
                        }
                    }
                }
            }
        }
        while (wasChecked.Count > 0)
        {
            var c = wasChecked.Pop();
            collisions.Push(c);
        }
    }

    private static bool CompareCollisionToWasChecked(Collider colliderA, Collider colliderB)
    {
        bool result = wasChecked.Any(c => c.CheckIfCollisionIsMe(colliderA, colliderB));;
        return result;
    }

    private static void CheckCollisionStay()
    {
        while (collisions.Count > 0)
        {
            var c = collisions.Pop();
            if (c.ColliderA.colliderConfig.Bounds.Intersects(c.ColliderB.colliderConfig.Bounds))
            {
                wasChecked.Push(c);
                if (c.CollisionType == CollisionType.Collision)
                {
                    c.ColliderA.gameObject.OnCollisionStay(c.ColliderB);
                    c.ColliderB.gameObject.OnCollisionStay(c.ColliderA);
                    // invoke collision stay on both colliders
                }
                else if (c.CollisionType == CollisionType.Trigger)
                {
                    c.ColliderB.gameObject.OnTriggerStay(c.ColliderA);
                    // invoke trigger stay on collider b
                }
            }
            else
            {
                if (c.CollisionType == CollisionType.Collision)
                {
                    c.ColliderA.gameObject.OnCollisionExit(c.ColliderB);
                    c.ColliderB.gameObject.OnCollisionExit(c.ColliderA);
                    
                    // invoke collision exit on both colliders
                }
                else if (c.CollisionType == CollisionType.Trigger)
                {
                    c.ColliderB.gameObject.OnTriggerExit(c.ColliderA);
                    // invoke trigger exit on collider b
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

internal class Collision
{
    public readonly Collider ColliderA;
    public readonly Collider ColliderB;
    public readonly CollisionType CollisionType;

    private readonly int collisionIDA;
    private readonly int collisionIDB;

    public Collision(Collider colliderA, Collider colliderB)
    {
        ColliderA = colliderA;
        ColliderB = colliderB;
        collisionIDA = colliderA.gameObject.Index;
        collisionIDB = colliderB.gameObject.Index;
        CollisionType = colliderB.colliderConfig.IsTrigger ? CollisionType.Trigger : CollisionType.Collision;
    }

    public bool CheckIfCollisionIsMe(Collider colliderA, Collider colliderB)
    {
        if (colliderA == null || colliderB == null) return false;
        return (colliderA.gameObject.Index == collisionIDA && colliderB.gameObject.Index == collisionIDB) ||
               (colliderA.gameObject.Index == collisionIDB && colliderB.gameObject.Index == collisionIDA);
    }
}

public enum CollisionType
{
    Collision,
    Trigger
}