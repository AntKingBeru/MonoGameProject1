using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class Button : GameObject
{
    public delegate void ButtonClickHandler();

    public event ButtonClickHandler OnButtonClick;

    public ButtonConfig buttonConfig { get; set; }
    private bool WasPressed = false;


    public Button(string name) : base(name)
    {
        
        var buttonConfig = new ButtonConfig();
        buttonConfig.Text = "Button";
        buttonConfig.Font = SpriteManager.ContentMan.Load<SpriteFont>("Fonts/Arial");
        buttonConfig.TextColor = Color.White;
        buttonConfig.TextOffset = new Vector2(0, 0);
        buttonConfig.FontScale = 1.0f;
        buttonConfig.FontRotation = 0f;
        buttonConfig.TextEffects = SpriteEffects.None;
        
        var spriteConfig = new SpriteConfig();
        spriteConfig.SourceRectangle = new Rectangle(0, 0, 100, 100);
        spriteConfig.Color = Color.White;
        spriteConfig.Effects = SpriteEffects.None;
        
        
        SpriteManager.AddSprite(name,spriteConfig);
        AddComponent<Sprite>();
        
        
        
    }


    //

    // public override void Initialize<T>(T config)

    // {

    //     base.Initialize(config);

    //     if (config is ButtonConfig buttonConfig)

    //     {

    //         this.buttonConfig = buttonConfig;

    //     }

    // }

    //


    public void SetFontStyle(float scale = 1.0f, float rotationDegrees = 0f, Color? color = null)
    {
        buttonConfig.FontScale = scale;
        buttonConfig.FontRotation = MathHelper.ToRadians(rotationDegrees);
        if (color.HasValue) buttonConfig.TextColor = color.Value;
    }

    private Vector2 GetTextPosition()
    {
        return gameObject.Position + buttonConfig.TextOffset;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (!string.IsNullOrEmpty(buttonConfig.Text) && buttonConfig.Font != null)
        {
            Vector2 position = GetTextPosition();
            Vector2 origin = buttonConfig.Font.MeasureString(buttonConfig.Text) * 0.5f;
        }

        // Draw main text (always on top)
        spriteBatch.DrawString(buttonConfig.Font, buttonConfig.Text, gameObject.Position, buttonConfig.TextColor,
            buttonConfig.FontRotation, gameObject.Origin, buttonConfig.FontScale, buttonConfig.TextEffects, 1f);
    }


    public override void Update(GameTime gameTime)
    {
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            if (WasPressed)
            {
                return;
            }

            WasPressed = true;
        }
        else
        {
            WasPressed = false;
        }

        base.Update(gameTime);
    }
}