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


    public Button(string name) : base(name)
    {
        buttonConfig = new ButtonConfig();

        var spriteConfig = new SpriteConfig();
        spriteConfig.SpriteInfo = SpriteManager.GetSprite("Button");

        AddComponent<Sprite, SpriteConfig>(spriteConfig);
        buttonConfig._clickArea = new Rectangle(0, 0, spriteConfig.SpriteInfo.Texture.Width,
            spriteConfig.SpriteInfo.Texture.Height);
    }


    public override void Update(GameTime gameTime)
    {
        buttonConfig._clickArea.X = (int)Position.X;
        buttonConfig._clickArea.Y = (int)Position.Y;

        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            if (buttonConfig.WasPressed)
            {
                return;
            }

            buttonConfig.WasPressed = true;
            if (IsMouseOver())
            {
                OnButtonClick?.Invoke();
            }
        }
        else
        {
            buttonConfig.WasPressed = false;
        }

        base.Update(gameTime);
    }

    private bool IsMouseOver()
    {
        return buttonConfig._clickArea.Contains(Mouse.GetState().Position);
    }
}