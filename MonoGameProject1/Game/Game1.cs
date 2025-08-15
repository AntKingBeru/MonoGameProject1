using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameProject1.Core;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MonoGameProject1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private GameObject testAnim = new  GameObject("Test Animation");

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();

        SpriteManager.ContentMan = Content;
        SpriteManager.GraphicsDevice = GraphicsDevice;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ScreenPosition.InitializePos(GraphicsDevice);
        MainMenuScene mainMenuScene = new MainMenuScene();
        SceneManager.AddScene("Main Menu", mainMenuScene);
        SceneManager.EnableScene(mainMenuScene);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        AnimConfig animConfig = new AnimConfig();
        SpriteSheetInfo spriteSheetInfo = new  SpriteSheetInfo();
        spriteSheetInfo.Texture = Content.Load<Texture2D>("Images/Bird2 Duck_1");
        spriteSheetInfo.Columns = 4;
        spriteSheetInfo.Rows = 4;
        animConfig.Name = "Test Animation";
        animConfig.SpriteInfo = spriteSheetInfo;
        animConfig.SourceRectangle = null;
        animConfig.LayerDepth = 0.5f;

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        /*for (int i = 0; i < SceneManager.CurrentScene.SceneObjects.Count; i++)
        {
            // Check if index is still valid (in case objects were removed)
            if (i >= SceneManager.CurrentScene.SceneObjects.Count) break;

            var obj = SceneManager.CurrentScene.SceneObjects[i];
            if (obj == null) continue;
            if (!obj.IsActive) continue;

            obj.Update(gameTime);
        }*/
        
        testAnim.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        /*if (SceneManager.CurrentScene?.SceneObjects != null)
        {
            for (int i = 0; i < SceneManager.CurrentScene.SceneObjects.Count; i++)
            {
                // Check if index is still valid
                if (i >= SceneManager.CurrentScene.SceneObjects.Count) break;

                var obj = SceneManager.CurrentScene.SceneObjects[i];
                if (obj == null) continue;
                if (!obj.IsActive) continue;

                obj.Draw(_spriteBatch);
            }
        }*/
        
        testAnim.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}