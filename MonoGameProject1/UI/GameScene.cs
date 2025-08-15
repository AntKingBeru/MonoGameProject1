using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class GameScene : Scene
{
    public event SceneUnloadHandler OnSceneUnload;

    private float fishCatchTimer = 0f; //Time in seconds between each fish catch
    private float slowCooldown = 5f; //Time in seconds before the player starts slowing down
    private float speed = 10f;
    private List<Sprite> backgroundSprites = new List<Sprite>();
    private List<Sprite> dirtSprites = new List<Sprite>();
    
    private static SpriteSheetInfo backgroundSpriteInfo = SpriteManager.GetSprite("Background");

    private readonly Vector2 DisplayOrigin = ScreenPosition.MiddleCenter();
    private readonly Vector2 EmptyOrigin = new Vector2(
        ScreenPosition.MiddleCenter().X, ScreenPosition.MiddleCenter().Y - backgroundSpriteInfo.Texture.Height
    );
    private readonly Vector2 MiddleOrigin = new Vector2(
        ScreenPosition.MiddleCenter().X, ScreenPosition.MiddleCenter().Y + backgroundSpriteInfo.Texture.Height
    );
    private readonly Vector2 BottomOrigin = new Vector2(
        ScreenPosition.MiddleCenter().X, ScreenPosition.MiddleCenter().Y + (backgroundSpriteInfo.Texture.Height * 2f)
        );

    public override void OnEnable()
    {
        IsActive = true;
        SceneObjects = new Dictionary<int, GameObject>();
        
        /*var player = new GameObject("PlayerTest");
        SceneObjects.Add(player.Index, player);
        var spriteConfig = new SpriteConfig();
        spriteConfig.SpriteInfo = SpriteManager.GetSprite("Button");
        player.Position = new Vector2(ScreenPosition.MiddleCenter().X, spriteConfig.SpriteInfo.Texture.Width * 0.5f);
        player.Scale = new Vector2(0.5f, 0.5f);
        var spriteComponent = player.AddComponent<Sprite, SpriteConfig>(spriteConfig);
        spriteComponent.SetActive(true);
        var inputComponent = player.AddComponent<Input>();
        inputComponent.SetActive(true);*/
        
        var backgroundHandler = new GameObject("BackgroundHandler");
        SceneObjects.Add(backgroundHandler.Index, backgroundHandler);

        var backSpriteConfig = new SpriteConfig
        {
            SpriteInfo = backgroundSpriteInfo,
            LayerDepth = 0f
        };

        var dirtSpriteConfig = new SpriteConfig
        {
            SpriteInfo = SpriteManager.GetSprite("Dirt"),
            LayerDepth = 1f
        };

        for (var i = 0; i < 3; i++)
        {
            backgroundSprites.Add(backgroundHandler.AddComponent<Sprite, SpriteConfig>(backSpriteConfig));
            backgroundSprites[i].SetActive(true);
            backgroundSprites[i].gameObject.Scale = new Vector2(10f, 10f);
            dirtSprites.Add(backgroundHandler.AddComponent<Sprite, SpriteConfig>(dirtSpriteConfig));
            dirtSprites[i].SetActive(true);
            dirtSprites[i].gameObject.Scale = new Vector2(10f, 10f);
        }
        
        backgroundSprites[0].spriteConfig.Origin = DisplayOrigin;
        backgroundSprites[1].spriteConfig.Origin = MiddleOrigin;
        backgroundSprites[2].spriteConfig.Origin = BottomOrigin;
        
        /*ar depthMaskConfig = new SpriteConfig
        {
            SpriteInfo = SpriteManager.GetSprite("DepthMask"),
            LayerDepth = 0.1f
        };
        var depthMask = backgroundHandler.AddComponent<Sprite, SpriteConfig>(depthMaskConfig);
        depthMask.SetActive(true);
        
        var depthGradientConfig = new SpriteConfig
        {
            SpriteInfo = SpriteManager.GetSprite("Gradiant"),
            LayerDepth = 0.2f
        };
        var depthGradient = backgroundHandler.AddComponent<Sprite, SpriteConfig>(depthGradientConfig);
        depthGradient.SetActive(true);*/
        
        backgroundHandler.Position = ScreenPosition.MiddleCenter();
        //backgroundHandler.Scale = new Vector2(ScreenPosition.MiddleCenter().X * 2f, ScreenPosition.MiddleCenter().Y * 2f);
        
        Init();
    }

    public override void Update(GameTime gameTime)
    { 
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        MoveBackground(speed * deltaTime);
        
        for (int i = 0; i < 3; i++)
        {
            if (backgroundSprites[i].spriteConfig.Origin.Y >= EmptyOrigin.Y)
            {
                backgroundSprites[i].spriteConfig.Origin = BottomOrigin;
                dirtSprites[i].spriteConfig.Origin = BottomOrigin;
            }
        }
        
        base.Update(gameTime);
    }

    private void MoveBackground(float moveSpeed)
    {
        for (int i = 0; i < 3; i++)
        {
            backgroundSprites[i].spriteConfig.Origin.Y += moveSpeed;
            dirtSprites[i].spriteConfig.Origin.Y += moveSpeed;
        }
    }
}