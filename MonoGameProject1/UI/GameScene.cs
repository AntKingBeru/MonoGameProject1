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
    private float speed = 200f;
    private List<GameObject> backgroundSprites = new List<GameObject>();
    
    private static SpriteSheetInfo backgroundSpriteInfo = SpriteManager.GetSprite("Background");

    public override void OnEnable()
    {
        IsActive = true;
        SceneObjects = new Dictionary<int, GameObject>();
        
        var player = new GameObject("Player");
        SceneObjects.Add(player.Index, player);
        player.Scale = new Vector2(0.2f, 0.2f);
        player.Position = ScreenPosition.Center();
        var playerSpriteInfo = SpriteManager.GetSprite("Player");
        var playerSpriteConfig = new SpriteConfig(playerSpriteInfo)
        {
            LayerDepth = 0.5f
        };
        player.AddComponent<Sprite, SpriteConfig>(playerSpriteConfig);
        var playerInput = player.AddComponent<Input>();
        playerInput.EnableMovement();
        
        
        var playerEdge = new GameObject("PlayerEdge");
        SceneObjects.Add(playerEdge.Index, playerEdge);
        playerEdge.Scale = Vector2.One;
        playerEdge.Position = player.Position + new Vector2(playerSpriteInfo.Texture.Width * 0.25f * 0.2f, playerSpriteInfo.Texture.Height * 0.2f);
        var playerEdgeInput = playerEdge.AddComponent<Input>();
        playerEdgeInput.EnableMovement();
        var playerColliderConfig = new ColliderConfig(new Rectangle(
            50,
            100, 
            50, 
            50));
        playerEdge.AddComponent<Collider, ColliderConfig>(playerColliderConfig);
        
        CreateBackground();
        
        ArrangeSprites();
        
        Init();
    }

    private void CreateBackground()
    {
        var obj = new GameObject("Test");
        SceneObjects.Add(obj.Index, obj);
        obj.Scale = new Vector2(0.2f, 0.2f);
        obj.Position = new Vector2(200, 600);
        
        var info = SpriteManager.GetSprite("Button");
        var spriteConfig = new SpriteConfig(info);
        
        obj.AddComponent<Sprite, SpriteConfig>(spriteConfig);
        var input = obj.AddComponent<Input>();
        input.EnableMovement();
        var colliderConfig = new ColliderConfig( new Rectangle(0, 0, 100, 100));
        obj.AddComponent<Collider, ColliderConfig>(colliderConfig);
        
        
        var backSpriteConfig = new SpriteConfig(backgroundSpriteInfo)
        {
            LayerDepth = 0.1f,
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        var deepClipConfig = new SpriteConfig(SpriteManager.GetSprite("Shading"))
        {
            LayerDepth = 0.3f,
            Color = new Color(1f, 1f, 1f, 0.4f),
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        var highLight = new SpriteConfig(SpriteManager.GetSprite("HighLight"))
        {
            LayerDepth = 0.2f,
            Color = new Color(0.2f, 0.2f, 0.2f,0f),
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        var outLine = new SpriteConfig(SpriteManager.GetSprite("OutLine"))
        {
            LayerDepth = 1f,
            Color = new Color(1f, 1f, 1f, 0.95f),
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        for (var i = 0; i < 3; i++)
        {
            var backgroundHandler = new GameObject("BackgroundHandler" + i);
            backgroundHandler.Scale = new Vector2(0.56f, 0.42f); // don't touch this, it is the correct scale for the background
            backgroundHandler.Position = ScreenPosition.TopLeft();
            SceneObjects.Add(backgroundHandler.Index, backgroundHandler);
            backgroundSprites.Add(backgroundHandler);
        
            var bgSprite = backgroundHandler.AddComponent<Sprite, SpriteConfig>(backSpriteConfig);
            bgSprite.spriteConfig.Origin = ScreenPosition.TopLeft();
        
            var farClip = backgroundHandler.AddComponent<Sprite, SpriteConfig>(deepClipConfig);
            farClip.spriteConfig.Origin = ScreenPosition.TopLeft();
        
            var nearClip = backgroundHandler.AddComponent<Sprite, SpriteConfig>(highLight);
            nearClip.spriteConfig.Origin = ScreenPosition.TopLeft();
            
            var outLineSprite = backgroundHandler.AddComponent<Sprite, SpriteConfig>(outLine);
            outLineSprite.spriteConfig.Origin = ScreenPosition.TopLeft();
        }
    }

    private void ArrangeSprites()
    {
        var screenHeight = ScreenPosition.ScreenHeight; // Assuming a fixed screen height of 1920 pixels for this example
        
        for (var i = 0; i < 3; i++)
        {
            var yPos = ScreenPosition.TopLeft().Y - (i * screenHeight);
            backgroundSprites[i].Position = new Vector2(
                backgroundSprites[i].Position.X,
                yPos
            );
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
        var screenHeight = ScreenPosition.ScreenHeight; 
    
    
        for (var i = 0; i < 3; i++)
        {
            backgroundSprites[i].Position = new Vector2(
                backgroundSprites[i].Position.X,
                backgroundSprites[i].Position.Y - moveSpeed
            );
        }
    
        for (var i = 0; i < 3; i++)
        {
            if (backgroundSprites[i].Position.Y + screenHeight <= 0)
            {
                // Find the current bottom-most sprite
                var maxY = backgroundSprites.Max(s => s.Position.Y);
    
                // Place this sprite exactly below the bottom-most one
                var newY = maxY + screenHeight;
    
                backgroundSprites[i].Position = new Vector2(
                    backgroundSprites[i].Position.X,
                    newY
                );
            }
        }
    }
}