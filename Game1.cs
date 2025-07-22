using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MonoGameProject1;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Enemy _enemy;
    
    //Textures
    
    //Text Related
    private SpriteFont _oswaldFont;
    
    //Vectors
    private static Vector2 _screenCenter;

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
        SpriteManager.AddSprite("pacman", "Images/pacman");
        SpriteManager.AddSprite("pixel", "Images/pixel");
        
        SpriteManager.AddSprite("bird1", "Images/Bird1_1", 4, 4);
        SpriteManager.AddSprite("bird2", "Images/Bird3_Egret4", 4, 4);
        SpriteManager.AddSprite("bird3", "Images/B4_s5", 4, 4);
        SpriteManager.AddSprite("bird4", "Images/Bird2 Duck_1", 4, 4);
        
        
        //Text-Related load
        _oswaldFont = Content.Load<SpriteFont>("Fonts/Oswald");

        //Temp content
        _player = SceneManager.Create<Player>();
        _enemy = SceneManager.Create<Enemy>(); 
        //Player setup
        _player.enemy = _enemy;
        _player.Position = _screenCenter;
        _player.Scale = new Vector2(0.25f, 0.25f);
        _player.PlayAnimation(true, 15);

        _player.Collide.OnTrigger += _player.OnTriggerEnter;
        _player.Collide.OnCollision += _player.OnCollisionEnter;
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