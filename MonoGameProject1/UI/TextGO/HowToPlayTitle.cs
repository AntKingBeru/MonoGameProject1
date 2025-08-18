using Microsoft.Xna.Framework;
using MonoGameProject1.Core;
using MonoGameProject1.Utilities.Configs;

namespace MonoGameProject1.UI.TextGO;

public class HowToPlayTitle : GameObject
{
    public HowToPlayTitle(string name) : base(name)
    {
        var playerEdgeSpriteConfig = new SpriteConfig(SpriteManager.GetSprite("TitleImage"))
        {
            LayerDepth = 0.9f
        };
        
        AddConfigComponent<Sprite, SpriteConfig>(playerEdgeSpriteConfig);
        var playerColliderConfig = new ColliderConfig(new Rectangle(
            50,
            100,
            50,
            75));
        
        Position = ScreenPosition.TopCenter() + new Vector2(0, 50);
        Scale = new Vector2(1.25f, 1.25f);
    }
}