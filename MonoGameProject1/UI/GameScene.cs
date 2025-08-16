using System.Collections.Generic;
using System.Linq;
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
        backgroundHandler.Position = ScreenPosition.TopLeft();
        
        SceneObjects.Add(backgroundHandler.Index, backgroundHandler);

        var backSpriteConfig = new SpriteConfig(backgroundSpriteInfo)
        {
            LayerDepth = 0.1f,
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };

        // var dirtSpriteConfig = new SpriteConfig(SpriteManager.GetSprite("Dirt"))
        // {
        //     LayerDepth = 0f,
        //     SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        // };

        for (var i = 0; i < 3; i++)
        {
            var bgSprite = backgroundHandler.AddComponent<Sprite, SpriteConfig>(backSpriteConfig);
            bgSprite.SetActive(true);
            bgSprite.gameObject.Scale = new Vector2(0.55f, 0.55f);
            backgroundSprites.Add(bgSprite);
            bgSprite.spriteConfig.Origin = new Vector2(0,0);

            // var dirtSprite = backgroundHandler.AddComponent<Sprite, SpriteConfig>(dirtSpriteConfig);
            // dirtSprite.SetActive(true);
            // dirtSprite.gameObject.Scale = Vector2.One;
            // dirtSprites.Add(dirtSprite);
            // dirtSprite.spriteConfig.Origin = new Vector2(bgSprite.spriteConfig.SpriteInfo.Texture.Width * 0.5f, 
            //     bgSprite.spriteConfig.SpriteInfo.Texture.Height * 0.5f);
        }
        
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
        
        ArrangeSprites();
        
        Init();
    }
    
    private void ArrangeSprites()
    {
        var screenHeight = 800; // Assuming a fixed screen height of 1920 pixels for this example
        
        for (var i = 0; i < 3; i++)
        {
            var yPos = ScreenPosition.TopLeft().Y - (i * screenHeight);
            backgroundSprites[i].spriteConfig.Origin = new Vector2(
                backgroundSprites[i].spriteConfig.Origin.X,
                yPos
            );
            // dirtSprites[i].spriteConfig.Origin = new Vector2(
            //     ScreenPosition.MiddleCenter().X,
            //     yPos
            // );
        }
    }

    public override void Update(GameTime gameTime)
    { 
        if (!IsActive) return;

        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        MoveBackground(speed * deltaTime);

        base.Update(gameTime);
    }

    private void MoveBackground(float moveSpeed)
    {
        var screenHeight = 800; // Assuming a fixed screen height of 1920 pixels for this example


        for (var i = 0; i < 3; i++)
        {
            backgroundSprites[i].gameObject.Position = new Vector2(
                backgroundSprites[i].gameObject.Position.X,
                backgroundSprites[i].gameObject.Position.Y - moveSpeed
            );

            // dirtSprites[i].gameObject.Position = new Vector2(
            //     dirtSprites[i].gameObject.Position.X,
            //     dirtSprites[i].gameObject.Position.Y - moveSpeed
            // );
        }

        for (var i = 0; i < 3; i++)
        {
            if (backgroundSprites[i].gameObject.Position.Y + (screenHeight * 0.5f) <= 0)
            {
                // Find the current bottom-most sprite
                var maxY = backgroundSprites.Max(s => s.gameObject.Position.Y);

                // Place this sprite exactly below the bottom-most one
                var newY = maxY + screenHeight;

                backgroundSprites[i].gameObject.Position = new Vector2(
                    backgroundSprites[i].gameObject.Position.X,
                    newY
                );

                // dirtSprites[i].gameObject.Position = new Vector2(
                //     dirtSprites[i].gameObject.Position.X,
                //     newY
                // );
            }
        }
    }
}