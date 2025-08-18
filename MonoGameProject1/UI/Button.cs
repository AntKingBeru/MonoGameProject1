using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class Button : ConfigComponent
{
    public delegate void ButtonClickHandler();

    public event ButtonClickHandler OnButtonClick;

    public ButtonConfig buttonConfig { get; set; }
    public Sprite sprite;
    private bool WasPressed = false;

    protected override void OnEnable()
    {
        buttonConfig = new ButtonConfig();

        var spriteInfo = SpriteManager.GetSprite("Button");
        var spriteConfig = new SpriteConfig(spriteInfo);

        sprite = gameObject.AddConfigComponent<Sprite, SpriteConfig>(spriteConfig);
            
        
        
    }

    protected override void OnDisable()
    {
    }

    public override void Update(GameTime gameTime)
    {
        buttonConfig._clickArea.X = (int)(gameObject.Position.X - sprite.spriteConfig.SpriteInfo.Texture.Width * 0.5f);
        buttonConfig._clickArea.Y = (int)(gameObject.Position.Y- sprite.spriteConfig.SpriteInfo.Texture.Height * 0.5f);
        buttonConfig._clickArea.Width = (int)sprite.spriteConfig.SpriteInfo.Texture.Width;
        buttonConfig._clickArea.Height = (int)sprite.spriteConfig.SpriteInfo.Texture.Height;
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