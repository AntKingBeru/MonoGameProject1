using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private readonly Color backgroundColor = new(15, 76, 92 , 255); // Dark blue background color

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 720;
        _graphics.PreferredBackBufferHeight = 1000;
        _graphics.ApplyChanges();

        ManagersContentRegistration();
    }


    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ScreenPosition.InitializePos(GraphicsDevice);
        
        SceneManager.AddScene<MainMenuScene>("Main Menu");
        SceneManager.AddScene<HowToPlayScene>("How to Play");
        SceneManager.AddScene<GameScene>("Game Scene");
        SceneManager.AddScene<EndScene>("End Scene");
        
        SceneManager.EnableScene("Main Menu"); // For testing purposes;
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
            Keyboard.GetState().IsKeyDown(Keys.Escape) || SceneManager.Exit)
            Exit();

        SceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(backgroundColor);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        SceneManager.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
    
    private void ManagersContentRegistration()
    {
        AudioManager.ContentMan = Content;
        AudioManager.RegisterSFX("Boom","Audio/SFX/Aboomnpha_Sound");
        AudioManager.RegisterSFX("Bubbles","Audio/SFX/Bubblez");
        AudioManager.RegisterSFX("Spear","Audio/SFX/Spearthrow");
        AudioManager.RegisterSFX("Splat1","Audio/SFX/SplatSound");
        AudioManager.RegisterSFX("Splat2","Audio/SFX/SplatSound2");
        
        TextManager.ContentMan = Content;
        TextManager.AddFont("Oswald", "Fonts/Oswald");
        TextManager.AddFont("Baloo2", "Fonts/Baloo2-SemiBold");
        SpriteManager.ContentMan = Content;
        SpriteManager.AddSprite("Button", "Images/pacman");
        SpriteManager.AddSprite("ExitButton", "Images/Exit");
        SpriteManager.AddSprite("RestartButton", "Images/Restart");
        SpriteManager.AddSprite("SettingsButton", "Images/SettingsButton");
        SpriteManager.AddSprite("StartButton", "Images/Start");
        SpriteManager.AddSprite("HowToPlayButton", "Images/HowToPlay");
        SpriteManager.AddSprite("Background", "Images/PipeNew");
        SpriteManager.AddSprite("OutLine", "Images/PipeOutLine");
        SpriteManager.AddSprite("HighLight", "Images/WhiteCenterClip");
        SpriteManager.AddSprite("Shading", "Images/BlackSideClip");
        SpriteManager.AddSprite("Pixel", "Images/Pixel");
        SpriteManager.AddSprite("PlayerControl", "Images/HarpoonHandle");
        SpriteManager.AddSprite("PlayerCollider", "Images/HarpoonTipNew");
        SpriteManager.AddSprite("Player", "Images/pacman");
        SpriteManager.AddSprite("GoldFish", "Images/GoldFish");
        SpriteManager.AddSprite("ShrimpsPink", "Images/ShrimpsPink");
        SpriteManager.AddSprite("ShrimpsOrange", "Images/ShrimpsOrange");
        SpriteManager.AddSprite("ShrimpsRed", "Images/ShrimpsRed");
        SpriteManager.AddSprite("Abumnapha", "Images/Abuna");
        SpriteManager.AddSprite("DirtClip", "Images/DirtClip");
        SpriteManager.AddSprite("BoostBar", "Images/BoostBar");
        SpriteManager.AddSprite("TitleImage", "Images/TitleImage");
    }

}