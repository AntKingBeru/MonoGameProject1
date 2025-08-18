using Microsoft.Xna.Framework;
using MonoGameProject1.Core;
using MonoGameProject1.Utilities.Configs;

namespace MonoGameProject1.UI.TextGO;

public class HowToPlayTitle : GameObject
{
    public HowToPlayTitle(string name) : base(name)
    {
        var titleSpriteConfig = new SpriteConfig(SpriteManager.GetSprite("TitleImage"))
        {
            LayerDepth = 0.9f
        };
        
        AddConfigComponent<Sprite, SpriteConfig>(titleSpriteConfig);
        var playerColliderConfig = new ColliderConfig(new Rectangle(
            50,
            100,
            50,
            75));
        Scale = new Vector2(0.25f, 0.25f);
        Position = ScreenPosition.TopCenter() - new Vector2(titleSpriteConfig.SpriteInfo.Texture.Width * 0.5f * Scale.X, 0f);
    }
}