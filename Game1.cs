using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject1;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _oswaldFont;
    private Text MousePosition;
    private Texture2D _logo;
    private Texture2D _pongAtlas;
    private Texture2D _pacman;
    private readonly Vector2 _screenCenter;
    
    List<IUpdateable> _updateables = new List<IUpdateable>();
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

        /*pacman = new Player(_pacman);
        pacman.Position = _screenCenter;
        pacman.Scale = new Vector2(0.2f, 0.2f);
        
        _updateables.Add(pacman);
        _drawables.Add(pacman);*/
        
        _oswaldFont = Content.Load<SpriteFont>("Fonts/Oswald");
        MousePosition.text = "Mouse Position";
        MousePosition.Position = new Vector2();
        
        _updateables.Add(MousePosition);
        _drawables.Add(MousePosition);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var updateable in _updateables)
        {
            updateable.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkSlateGray);
        _spriteBatch.Begin();
        
        foreach (IDrawable drawable in _drawables)
        {
            drawable.Draw(_spriteBatch);
        }
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}