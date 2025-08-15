using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public class ColliderConfig : ComponentConfig
{
    public Rectangle Bounds;
    public bool IsTrigger = false;
    
    public ColliderConfig(Rectangle bounds, bool isTrigger = false)
    {
        Bounds = bounds;
        IsTrigger = isTrigger;
    }
}