using System.Collections.Generic;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MonoGameProject1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    //Text Related
    private SpriteFont _oswaldFont;
    private Text _mousePosition;
    //Textures
    private Texture2D _logo;
    private Texture2D _pongAtlas;
    private Texture2D _pacman;
    //Vectors
    private readonly Vector2 _screenCenter;
    
    List<IUpdateable> _updatables = new List<IUpdateable>();
    List<IDrawable> _drawables = new List<IDrawable>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
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
        
        _logo = Content.Load<Texture2D>("Images/logo");
        _pongAtlas = Content.Load<Texture2D>("Images/pong-atlas");
        _pacman = Content.Load<Texture2D> ("Images/pacman");

        /*Player pacman = new Player(_pacman);
        pacman.Position = _screenCenter;
        pacman.Scale = new Vector2(0.2f, 0.2f);
        
        _updatables.Add(pacman);
        _drawables.Add(pacman);*/
        
        _oswaldFont = Content.Load<SpriteFont>("Fonts/Oswald");
        
        /*_mousePosition.text = "Mouse Position";
        _mousePosition.Position = new Vector2();
        
        _updatables.Add(_mousePosition);
        _drawables.Add(_mousePosition);*/
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        /*foreach (var updateable in _updatables)
        {
            updateable.Update(gameTime);
        }*/

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);
        
        _spriteBatch.Begin();
        
        //Drawing to the screen while rotating, maximum parameters for Draw()
        /*_spriteBatch.Draw(
            _logo,
            _screenCenter,
            null,
            Color.White,
            MathHelper.ToRadians((float)gameTime.TotalGameTime.TotalMilliseconds * 0.25f),
            new Vector2(_logo.Width * 0.5f, _logo.Height * 0.5f),
            Vector2.One,
            SpriteEffects.None,
            0
            );*/

        //Sprite Atlas usage, useful for the fish and shit in the game, IMPORTANT NOTE: use grid instead of 1 line
        /*var index = 0;
        var cellCount = 2;
        _spriteBatch.Draw(
            _pongAtlas,
            Vector2.Zero,
            new Rectangle(new Point((int)(_pongAtlas.Width * ((float)index/cellCount)), 0), new Point((int)(_pongAtlas.Width * (1.0f/cellCount)), _pongAtlas.Height)),
            Color.White
            );*/
        
        /*foreach (IDrawable drawable in _drawables)
        {
            drawable.Draw(_spriteBatch);
        }*/
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}