using Microsoft.Xna.Framework;

namespace MonoGameProject1.Core;

public class Player : GameObject
{
    private SpriteSheetInfo spriteInfo;
    private SpriteConfig spriteConfig;
    private Input input;
    private ColliderConfig colliderConfig;
    
    public Player() : base("player")
    {
        spriteInfo = SpriteManager.GetSprite("Player");
        spriteConfig = new SpriteConfig(spriteInfo)
        {
            LayerDepth = 0.5f
        };
        Scale = new Vector2(0.2f, 0.2f);
        Position = new Vector2(200, 0);
        AddComponent<Sprite, SpriteConfig>(spriteConfig);
        input = AddComponent<Input>();
        input.EnableMovement();
        colliderConfig = new ColliderConfig( new Rectangle(0, 0, 100, 100));
        AddComponent<Collider, ColliderConfig>(colliderConfig);
    }
}