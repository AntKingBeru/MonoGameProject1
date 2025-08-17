using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class GameScene : Scene
{
    private int backgroundAmount = 5;
    private float fishCatchTimer = 0f; //Time in seconds between each fish catch
    private float speed = 200f;
    private float speedLoseStartSpeed = 0f;
    private bool isSlowing = false;
    private List<GameObject> backgroundSprites = new List<GameObject>();

    private const float GRACEPERIOD = 1.5f;
    private const float TIMETHRESHOLD = GRACEPERIOD + 5f;
    private const float ORIGINSPEED = 200f;
    private const float MAXSPEED = 2500f;

    private static SpriteSheetInfo backgroundSpriteInfo = SpriteManager.GetSprite("Background");

    private GameObject player;
    private GameObject playerEdge;
    private SpriteSheetInfo playerSpriteInfo = SpriteManager.GetSprite("PlayerControl");
    private SpriteSheetInfo playerEdgeSpriteInfo = SpriteManager.GetSprite("PlayerCollider");

    public Queue<GameObject> Fishes;

    public override void OnEnable()
    {
        IsActive = true;
        SceneObjects = new Dictionary<int, GameObject>();

        CreateBackground();

        CreatePlayer();

        CreateFish();
        
        ArrangeSprites();

        Init();
    }

    private void CreateFish()
    {
        var testFish = new Fish("testFish");
        SceneObjects.Add(testFish.Index, testFish);
        
        Fishes = new Queue<GameObject>();
    }
    
    private void CreatePlayer()
    {
        playerEdge = new GameObject("PlayerEdge");
        SceneObjects.Add(playerEdge.Index, playerEdge);
        playerEdge.Scale = new Vector2(0.125f, 0.125f);
        var playerEdgeSpriteConfig = new SpriteConfig(playerEdgeSpriteInfo)
        {
            LayerDepth = 0.5f
        };
        playerEdge.AddComponent<Sprite, SpriteConfig>(playerEdgeSpriteConfig);
        var playerColliderConfig = new ColliderConfig(new Rectangle(
            50,
            100, 
            50, 
            75));
        var collider = playerEdge.AddComponent<Collider, ColliderConfig>(playerColliderConfig);
        var playerEdgeInput = playerEdge.AddComponent<Input>();
        playerEdgeInput.EnableMovement();
        
        player = new GameObject("Player");
        SceneObjects.Add(player.Index, player);
        player.Scale = playerEdge.Scale * 2f;
        player.Position = ScreenPosition.TopCenter();
        var playerSpriteConfig = new SpriteConfig(playerSpriteInfo)
        {
            LayerDepth = 0.5f
        };
        player.AddComponent<Sprite, SpriteConfig>(playerSpriteConfig);
        
        playerEdge.Position = player.Position + new Vector2(playerSpriteInfo.Texture.Width * 0.125f, playerSpriteInfo.Texture.Height * 0.05f);
        
        collider.OnCollision += CatchFish;
    }

    private void CreateBackground()
    {
        var backSpriteConfig = new SpriteConfig(backgroundSpriteInfo)
        {
            LayerDepth = 0.1f,
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        var outLine = new SpriteConfig(SpriteManager.GetSprite("OutLine"))
        {
            LayerDepth = 0.9f,
            Color = new Color(1f, 1f, 1f, 0.95f),
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        var highLight = new SpriteConfig(SpriteManager.GetSprite("HighLight"))
        {
            LayerDepth = 0.2f,
            Color = new Color(0.2f, 0.2f, 0.2f,0.1f),
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        var deepClipConfig = new SpriteConfig(SpriteManager.GetSprite("Shading"))
        {
            LayerDepth = 0.3f,
            Color = new Color(1f, 1f, 1f, 0.5f),
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        var dirtClip = new SpriteConfig(SpriteManager.GetSprite("DirtClip"))
        {
            LayerDepth = 0.3f,
            Color = new Color(1f, 1f, 1f, 1f),
            SourceRectangle = new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };
        
        for (var i = 0; i < backgroundAmount; i++)
        {
            var backgroundHandler = new GameObject("BackgroundHandler" + i);
            backgroundHandler.Scale = new Vector2(0.56f, 0.42f); // don't touch this, it is the correct scale for the background
            backgroundHandler.Position = ScreenPosition.TopLeft();
            SceneObjects.Add(backgroundHandler.Index, backgroundHandler);
            backgroundSprites.Add(backgroundHandler);
        
            var bgSprite = backgroundHandler.AddComponent<Sprite, SpriteConfig>(backSpriteConfig);
            bgSprite.spriteConfig.Origin = ScreenPosition.TopLeft();
            
            var nearClip = backgroundHandler.AddComponent<Sprite, SpriteConfig>(highLight);
            nearClip.spriteConfig.Origin = ScreenPosition.TopLeft();
            
            var outLineSprite = backgroundHandler.AddComponent<Sprite, SpriteConfig>(outLine);
            outLineSprite.spriteConfig.Origin = ScreenPosition.TopLeft();
            
            var farClip = backgroundHandler.AddComponent<Sprite, SpriteConfig>(deepClipConfig);
            farClip.spriteConfig.Origin = ScreenPosition.TopLeft();
            
            var dirtClipSprite = backgroundHandler.AddComponent<Sprite, SpriteConfig>(dirtClip);
            dirtClipSprite.spriteConfig.Origin = ScreenPosition.TopLeft();
        }
    }

    private void ArrangeSprites()
    {
        var screenHeight = ScreenPosition.ScreenHeight; // Assuming a fixed screen height of 1920 pixels for this example
        
        for (var i = 0; i < backgroundAmount; i++)
        {
            var yPos = ScreenPosition.TopLeft().Y - (i * screenHeight);
            backgroundSprites[i].Position = new Vector2(
                backgroundSprites[i].Position.X,
                yPos
            );
        }
    }

    private void CatchFish(Collider other)
    {
        fishCatchTimer = 0f;
        isSlowing = false;
        speed *= 1.4f;
        speed = MathHelper.Clamp(speed, 0f, MAXSPEED);
        //TODO return fish to pool
    }

    public override void Update(GameTime gameTime)
    { 
        if (!IsActive) return;
        
        player.Position = playerEdge.Position - new Vector2(playerSpriteInfo.Texture.Width * 0.125f, playerSpriteInfo.Texture.Height * 0.05f);
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (speed <= 1f)
        {
            speed = 1f;
            //TODO: add logic when speed reaches 0
        }

        else
        {
            if (fishCatchTimer >= GRACEPERIOD)
            {
                if (!isSlowing)
                {
                    isSlowing = true;
                    speedLoseStartSpeed = speed;
                }
                
                speed -= deltaTime * (speedLoseStartSpeed / TIMETHRESHOLD);
            }

            fishCatchTimer += deltaTime;
        }
        
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