using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MonoGameProject1;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Animation pacman;
    
    //Textures
    private Texture2D _logo;
    private Texture2D _pongAtlas;
    private Texture2D _pacman;
    
    //Text Related
    private SpriteFont _oswaldFont;
    private Text _mousePosition;
    
    //Vectors
    private static Vector2 _screenCenter;
    
    //Lists
   

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        SpriteManager.Content = Content;
        
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;

        _screenCenter = new Vector2(_graphics.PreferredBackBufferWidth * 0.5f, _graphics.PreferredBackBufferHeight * 0.5f);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // //Texture load
        // _pongAtlas = Content.Load<Texture2D>("Images/pong-atlas");
        //
        //
        // SpriteManager.AddSprite("logo", "Images/logo");
        // SpriteManager.AddSprite("pixel", "Images/pixel");
        // SpriteManager.AddSprite("pong-atlas", "Images/pong-atlas", 2, 1);
        // SpriteManager.AddSprite("player", "Images/pacman");
        // SpriteManager.AddSprite("logo", "Images/logo");
        //
        // SpriteManager.AddSprite("bird1", "Images/Bird1_1" , 4,4);
        // SpriteManager.AddSprite("bird2", "Images/Bird1_3", 4,4);
        // SpriteManager.AddSprite("bird3", "Images/Bird2 Duck_1", 4,4);
        // SpriteManager.AddSprite("bird4", "Images/Bird2 Duck_3", 4,4);
        // SpriteManager.AddSprite("bird5", "Images/Bird3_Egret2", 4,4);
        //
        //
        // //Text-Related load
        // _oswaldFont = Content.Load<SpriteFont>("Fonts/Oswald");
        //
        // //Temp content
        // pacman = SceneManager.Create<Player>();
        // pacman.Position = _screenCenter;
        // pacman.Scale = new Vector2(0.25f, 0.25f);
        // pacman.PlayAnimation(true , 10);
        //
        // _mousePosition = new MousePosition(_oswaldFont);
        // _mousePosition._Text = "Mouse  Position";
        // _mousePosition.Position = new Vector2(_screenCenter.X, 50);
        //
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        SceneManager.Instance.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);
        
        _spriteBatch.Begin();

        SceneManager.Instance.Draw(_spriteBatch);
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}