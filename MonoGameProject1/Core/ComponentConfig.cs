using Microsoft.Xna.Framework;

namespace MonoGameProject1;

public abstract class ComponentConfig
{
    
}

public class SpriteConfig : ComponentConfig
{
    public string TextureName { get; set; }
    public Rectangle? SourceRectangle { get; set; }
    public float LayerDepth { get; set; }
}

public class ColliderConfig : ComponentConfig
{
    public Rectangle Bounds { get; set; }
    public bool IsTrigger { get; set; }
}