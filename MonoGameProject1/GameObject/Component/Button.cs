using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class Button : ConfigurableComponent
{
    public delegate void ButtonClickHandler();

    public event ButtonClickHandler OnButtonClick;

    public ButtonConfig buttonConfig { get; set; }
    public Sprite sprite;
    private bool WasPressed = false;


    public override void Initialize<T>(T config)
    {
        if (config is ButtonConfig buttonConfig)
        {
            this.buttonConfig = buttonConfig;
            base.Initialize(config);
        }
    }

    protected override void OnEnable()
    {
        if(buttonConfig == null) return;
    }

    protected override void OnDisable()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        buttonConfig._clickArea.X = (int)(gameObject.Position.X - buttonConfig.Sprite.spriteConfig.SpriteInfo.Texture.Width * 0.5f);
        buttonConfig._clickArea.Y = (int)(gameObject.Position.Y - buttonConfig.Sprite.spriteConfig.SpriteInfo.Texture.Height * 0.5f);
        buttonConfig._clickArea.Width = (int)buttonConfig.Sprite.spriteConfig.SpriteInfo.Texture.Width;
        buttonConfig._clickArea.Height = (int)buttonConfig.Sprite.spriteConfig.SpriteInfo.Texture.Height;
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            if (WasPressed)
            {
                return;
            }

            WasPressed = true;
            if (IsMouseOver())
            {
                OnButtonClick?.Invoke();
            }
        }
        else
        {
            WasPressed = false;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        
    }


    private bool IsMouseOver()
    {
        return buttonConfig._clickArea.Contains(Mouse.GetState().Position);
    }
}