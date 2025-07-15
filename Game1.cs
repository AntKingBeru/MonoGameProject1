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
    private Animation _player;
    
    //Textures
    
    //Text Related
    private SpriteFont _oswaldFont;
    
    //Vectors
    private static Vector2 _screenCenter;
    
    //Lists
    private List<IUpdatable> _updatables = [];
    private List<IDrawable> _drawables = [];

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
        
        //Texture load
        
        SpriteManager.AddSprite("logo", "Images/logo");
        SpriteManager.AddSprite("player", "Images/pacman");
        
        SpriteManager.AddSprite("bird1", "Images/Bird1_1", 4, 4);
        SpriteManager.AddSprite("bird2", "Images/Bird3_Egret4", 4, 4);
        SpriteManager.AddSprite("bird3", "Images/B4_s5", 4, 4);
        SpriteManager.AddSprite("bird4", "Images/Bird2 Duck_1", 4, 4);
        
        
        //Text-Related load
        _oswaldFont = Content.Load<SpriteFont>("Fonts/Oswald");

        //Temp content
        _player = new Player();
        _player.Position = _screenCenter;
        _player.Scale = new Vector2(0.25f, 0.25f);
        _player.PlayAnimation(true, 15);
        
        _updatables.Add(_player);
        _drawables.Add(_player);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var updatable in _updatables)
        {
            updatable.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);
        
        _spriteBatch.Begin();

        foreach (var drawable in _drawables)
        {
            drawable.Draw(_spriteBatch);
        }

        //Sprite Atlas usage, useful for the fish and shit in the game, IMPORTANT NOTE: use grid instead of 1 line
        /*const int index = 0;
        const int cellCount = 2;
        _spriteBatch.Draw(
            _pongAtlas,
            Vector2.Zero,
            new Rectangle(new Point((int)(_pongAtlas.Width * ((float)index/cellCount)), 0), new Point((int)(_pongAtlas.Width * (1.0f/cellCount)), _pongAtlas.Height)),
            Color.White
            );*/
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}