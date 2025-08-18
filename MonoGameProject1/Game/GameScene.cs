using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class GameScene : Scene
{
    private enum BoostZone
    {
        Red,
        Yellow,
        Green
    }
    
    private int backgroundAmount = 5;
    private float fishCatchTimer = 0f; //Time in seconds between each fish catch
    private float speed;
    private float speedLoseStartSpeed = 0f;
    private bool isSlowing = false;
    private bool isGameStarted = false;
    private List<GameObject> backgroundSprites = new List<GameObject>();
    private Queue<GameObject> fishPool = new Queue<GameObject>();
    private float miniTimer = 0f;
    private float fixedPlayerX = -1f; // -1 means not initialized

    private const float GRACEPERIOD = 1.5f;
    private const float TIMETHRESHOLD = GRACEPERIOD + 5f;
    private const float ORIGINSPEED = 200f;
    private const float MAXSPEED = 2500f;
    private const float SPEED_MULTIPLIER = 1.3f;
    private const int MAX_FISH = 4;
    private const float RED_BOOST = 5f;
    private const float YELLOW_BOOST = 7.5f;
    private const float GREEN_BOOST = 10f;

    private static SpriteSheetInfo backgroundSpriteInfo = SpriteManager.GetSprite("Background");

    private GameObject player;
    private GameObject playerEdge;
    private GameObject boostBar;
    private SpriteSheetInfo playerSpriteInfo = SpriteManager.GetSprite("PlayerControl");
    private SpriteSheetInfo playerEdgeSpriteInfo = SpriteManager.GetSprite("PlayerCollider");

    public override void OnEnable()
    {
        IsActive = true;
        CreateBackground();
        CreateFish(); // Fish has to be created before player for reference passing
        CreatePlayer();
        CreateBoostBar();
        ArrangeSprites();
        speed = ORIGINSPEED;
        miniTimer = 0f;

        var comboManager = new ComboManager("ComboManager");
        AddActiveObject(comboManager);        
        
        Init();
        
    }

    private static float RandomFloat(float min, float max)
    {
        return (float)new Random().NextDouble() * (max - min) + min;
    }

    private void ArrangeSprites()
    {
        var screenHeight =
            ScreenPosition.ScreenHeight; // Assuming a fixed screen height of 1920 pixels for this example

        for (var i = 0; i < backgroundAmount; i++)
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
        
        if (!isGameStarted)
        {
            var totalTime = (float)gameTime.TotalGameTime.TotalSeconds;
            
            // Horizontal movement (pendulum)
            if (fixedPlayerX < 0f)
            {
                playerEdge.Position = UpdatePlayerPosition(
                    ScreenPosition.LeftGameBoundary(),
                    ScreenPosition.RightGameBoundary(),
                    totalTime);
            }
            else
            {
                playerEdge.Position = new Vector2(fixedPlayerX, playerEdge.Position.Y);
            }
            
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space) || miniTimer > 0f)
            {
                miniTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                
                // Stop the pendulum and fix the horizontal position when space is pressed the first time
                if (fixedPlayerX < 0f)
                {
                    fixedPlayerX = playerEdge.Position.X;
                }
                
                // Vertical movement while pressing
                playerEdge.Position.Y += speed * miniTimer * 0.1f;
                
                // Once miniTimer reaches the threshold, apply speed boost
                if (miniTimer >= 0.25f)
                {
                    // Check which boost zone the player is in
                    var playerCollider = playerEdge.GetComponent<Collider>();
                    var boostCollider = boostBar.GetComponent<Collider>();
                    if (playerCollider != null && boostCollider != null)
                    {
                        var boostZone = GetBoostZone(playerCollider, boostCollider);
                        
                        // Apply multiplier
                        switch (boostZone)
                        {
                            case BoostZone.Green:
                                speed *= GREEN_BOOST;
                                Console.WriteLine("Green");
                                break;
                            case BoostZone.Yellow:
                                speed *= YELLOW_BOOST;
                                Console.WriteLine("Yellow");
                                break;
                            case BoostZone.Red:
                                speed *= RED_BOOST;
                                Console.WriteLine("Red");
                                break;
                        }
                        
                        speed = MathHelper.Clamp(speed, 0f, MAXSPEED);
                    }
                    
                    // Remove boost bar
                    boostBar.Disable();
                    
                    //Start the game
                    isGameStarted = true;
                }
            }
        }
        else
        {
            HandleTimer(deltaTime);
            MoveBackground(speed * deltaTime);
            CleanupIlligalFish();
            TopUpActiveFishInScene();

            foreach (var fish in ActiveSceneObjects.Values.OfType<Fish>())
            {
                fish.SetSpeed(speed);
            }
        }

        base.Update(gameTime);

        player.Position = playerEdge.Position - new Vector2(playerSpriteInfo.Texture.Width * 0.125f,
            playerSpriteInfo.Texture.Height * 0.667f - playerEdgeSpriteInfo.Texture.Height * playerEdge.Scale.Y * 0.5f);
        
        CollisionManager.DetectCollisions();
    }

    public static Vector2 UpdatePlayerPosition(Vector2 start, Vector2 end, float totalTime)
    {
        var t = (float)(Math.Sin(totalTime * 2f) * 0.5f + 0.5f);
        
        return Vector2.Lerp(start, end, t);
    }

    #region Fish Catch Logic
    private int GetActiveFishCount()
    {
        return ActiveSceneObjects.Count(potentialFish => potentialFish.Value is Fish);
    }
    
    private void CatchFishLogic(Fish fish, bool isAboomnpha = false)
    {
        if (isAboomnpha)
        {
            speed *= 0.5f;
        }
        else
        {
            fishCatchTimer = 0f;
            isSlowing = false;
            speed *= SPEED_MULTIPLIER;
        } 
        speed = MathHelper.Clamp(speed, 0f, MAXSPEED);
        fishPool.Enqueue(fish);
        fish.Disable();
    }

    private void TopUpActiveFishInScene()
    {
        if (GetActiveFishCount() < MAX_FISH)
        {
            var fish = fishPool.Dequeue() as Fish;
            fish.Position =
                new Vector2(RandomFloat(ScreenPosition.LeftGameBoundary().X, ScreenPosition.RightGameBoundary().X),
                    ScreenPosition.BottomGameBoundary().Y + ScreenPosition.ScreenHeight * 0.25f);
            fish.RandomizeSprite();
            fish.Enable();
        }
    }

    private void CleanupIlligalFish()
    {
        foreach (var fish in ActiveSceneObjects.Values.OfType<Fish>())
        {
            if (!(fish.Position.Y <= 0)) continue;
            fishPool.Enqueue(fish);
            fish.Disable();
        }
    }
    

    #endregion
    #region gameobject creation
    private void CreateFish()
    {
        for (var i = 0; i < 10; i++)
        {
            var fish = new Fish("fish" + i);
            fish.Disable();
            fishPool.Enqueue(fish);
            AddInactiveObject(fish);
        }
    }
    
    private void CreatePlayer()
    {
        playerEdge = new GameObject("PlayerEdge");
        AddActiveObject(playerEdge);
        playerEdge.Scale = new Vector2(0.125f, 0.125f);
        var playerEdgeSpriteConfig = new SpriteConfig(playerEdgeSpriteInfo)
        {
            LayerDepth = 0.5f
        };
        playerEdge.AddConfigComponent<Sprite, SpriteConfig>(playerEdgeSpriteConfig);
        var playerColliderConfig = new ColliderConfig(new Rectangle(
            50,
            100,
            50,
            75));
        var collider = playerEdge.AddConfigComponent<Collider, ColliderConfig>(playerColliderConfig);
        var playerEdgeInput = playerEdge.AddComponent<Input>();
        playerEdgeInput.EnableMovement();

        player = new GameObject("Player");
        AddActiveObject(player);
        player.Scale = playerEdge.Scale * 2f * new Vector2(1f, 10f);
        player.Position = ScreenPosition.TopCenter() - new Vector2(playerSpriteInfo.Texture.Width * 0.25f, ScreenPosition.ScreenHeight * 2f);
        var playerSpriteConfig = new SpriteConfig(playerSpriteInfo)
        {
            LayerDepth = 0.5f
        };
        player.AddConfigComponent<Sprite, SpriteConfig>(playerSpriteConfig);

        playerEdge.Position = player.Position + new Vector2(playerSpriteInfo.Texture.Width * 0.125f,
            playerSpriteInfo.Texture.Height * 0.667f - playerEdgeSpriteInfo.Texture.Height * playerEdge.Scale.Y * 0.5f);
        
        Fish.OnFishCaught += CatchFishLogic;
        FishPatterns.SetPlayer(playerEdge);
        FishBehaviour.SetPlayer(playerEdge);
    }

    private void CreateBoostBar()
    {
        boostBar = new GameObject("Boost Bar");
        AddActiveObject(boostBar);
        boostBar.Scale = new Vector2(0.75f, 0.03f);
        var redBoosterSpriteConfig = new SpriteConfig(SpriteManager.GetSprite("Pixel"))
        {
            LayerDepth = 0.8f
        };
        boostBar.AddConfigComponent<Sprite, SpriteConfig>(redBoosterSpriteConfig);
        boostBar.Position = new Vector2(
            RandomFloat(45, 175),
            playerEdge.GetComponent<Sprite>().spriteConfig.SpriteInfo.Texture.Height * playerEdge.Scale.Y * 1.25f);
        var redColliderConfig = new ColliderConfig(new Rectangle(
            0,
            0,
            385,
            5));
        boostBar.AddConfigComponent<Collider, ColliderConfig>(redColliderConfig);
    }

    private void CreateBackground()
    {
        var backSpriteConfig = new SpriteConfig(backgroundSpriteInfo)
        {
            LayerDepth = 0.1f,
            SourceRectangle =
                new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };

        var outLine = new SpriteConfig(SpriteManager.GetSprite("OutLine"))
        {
            LayerDepth = 0.9f,
            Color = new Color(1f, 1f, 1f, 0.95f),
            SourceRectangle =
                new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };

        var highLight = new SpriteConfig(SpriteManager.GetSprite("HighLight"))
        {
            LayerDepth = 0.2f,
            Color = new Color(0.2f, 0.2f, 0.2f, 0.1f),
            SourceRectangle =
                new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };

        var deepClipConfig = new SpriteConfig(SpriteManager.GetSprite("Shading"))
        {
            LayerDepth = 0.3f,
            Color = new Color(1f, 1f, 1f, 0.5f),
            SourceRectangle =
                new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };

        var dirtClip = new SpriteConfig(SpriteManager.GetSprite("DirtClip"))
        {
            LayerDepth = 0.3f,
            Color = new Color(1f, 1f, 1f, 1f),
            SourceRectangle =
                new Rectangle(0, 0, backgroundSpriteInfo.Texture.Width, backgroundSpriteInfo.Texture.Height),
        };

        for (var i = 0; i < backgroundAmount; i++)
        {
            var backgroundHandler = new GameObject("BackgroundHandler" + i);
            backgroundHandler.Scale =
                new Vector2(0.56f, 0.42f); // don't touch this, it is the correct scale for the background
            backgroundHandler.Position = ScreenPosition.TopLeft();
            AddActiveObject(backgroundHandler);
            backgroundSprites.Add(backgroundHandler);

            var bgSprite = backgroundHandler.AddConfigComponent<Sprite, SpriteConfig>(backSpriteConfig);
            bgSprite.spriteConfig.Origin = ScreenPosition.TopLeft();

            var nearClip = backgroundHandler.AddConfigComponent<Sprite, SpriteConfig>(highLight);
            nearClip.spriteConfig.Origin = ScreenPosition.TopLeft();

            var outLineSprite = backgroundHandler.AddConfigComponent<Sprite, SpriteConfig>(outLine);
            outLineSprite.spriteConfig.Origin = ScreenPosition.TopLeft();

            var farClip = backgroundHandler.AddConfigComponent<Sprite, SpriteConfig>(deepClipConfig);
            farClip.spriteConfig.Origin = ScreenPosition.TopLeft();

            var dirtClipSprite = backgroundHandler.AddConfigComponent<Sprite, SpriteConfig>(dirtClip);
            dirtClipSprite.spriteConfig.Origin = ScreenPosition.TopLeft();
        }
    }
    #endregion
    #region Game Logic

    private BoostZone GetBoostZone(Collider playerCollider, Collider booster)
    {
        // Get rectangles of each collider
        var playerRect = playerCollider.colliderConfig.Bounds;
        var boosterRect = booster.colliderConfig.Bounds;

        var boostLeft = boostBar.Position.X + boosterRect.X * boostBar.Scale.X;
        var boostRight = boostLeft + boosterRect.Width * boostBar.Scale.X;
        
        var playerLeft = playerEdge.Position.X + playerRect.X * playerEdge.Scale.X;
        var playerRight = playerLeft + playerRect.Width * playerEdge.Scale.X;
        
        var relativeLeft = (playerLeft - boostLeft) / (boostRight - boostLeft);
        var relativeRight = (playerRight - boostLeft) / (boostRight - boostLeft);

        // Define Zones
        const float greenStart = 0.40f; // middle 10%
        const float greenEnd = 0.60f;
        const float yellowStart = 0.20f; // middle 20%
        const float yellowEnd = 0.80f;

        if (relativeRight >= greenStart && relativeLeft <= greenEnd)
        {
            return BoostZone.Green;
        }
        else if (relativeRight >= yellowStart && relativeLeft <= yellowEnd)
        {
            return BoostZone.Yellow;
        }
        else
        {
            return BoostZone.Red;
        }
    }
    
    private void HandleTimer(float deltaTime)
    {
        if (speed <= 1f)
        {
            speed = 1f;
            //TODO: game over logic
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
    #endregion
}