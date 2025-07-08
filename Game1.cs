using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _oswaldFont;
    private readonly Vector2 _screenCenter;

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
        
        /*_logo = Content.LoadTexture2D>("Images/logo")
         _*/
        
        _oswaldFont = Content.Load<SpriteFont>("Fonts/Oswald");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        var text = "Hello, MonoGame!";
        var textCenter = _oswaldFont.MeasureString(text) * 0.5f;
        _spriteBatch.DrawString(
            _oswaldFont,
            text,
            _screenCenter,
            Color.White,
            0,
            textCenter,
            Vector2.One,
            SpriteEffects.None,
            0
            );
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}