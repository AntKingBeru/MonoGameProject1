using System;
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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 1080;
        _graphics.PreferredBackBufferHeight = 1920;
        _graphics.ApplyChanges();
        SpriteManager.ContentMan = Content; 
        SpriteManager.AddSprite("Button", "Images/pacman");
        SpriteManager.AddSprite("Background", "Images/pixel");
        SpriteManager.AddSprite("Dirt", "Images/pixel");
        // SpriteManager.AddSprite("Gradiant", "Images/pixel");
        // SpriteManager.AddSprite("DepthMask", "Images/pixel");
        SpriteManager.AddSprite("AnimTest", "Images/Bird2 Duck_1", 4, 4);
        SpriteManager.AddSprite("Pixel", "Images/Pixel");
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ScreenPosition.InitializePos(GraphicsDevice);
        MainMenuScene mainMenuScene = new MainMenuScene();
        SceneManager.AddScene("Main Menu", mainMenuScene);
        SceneManager.EnableScene("Main Menu");*/
        var gameScene = new GameScene();
        SceneManager.AddScene("Game Scene", gameScene);
        SceneManager.EnableScene("Game Scene");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        SceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        SceneManager.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}