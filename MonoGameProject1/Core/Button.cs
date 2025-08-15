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
        buttonConfig = new ButtonConfig();
        
        var spriteConfig = new SpriteConfig();
        spriteConfig.SpriteInfo = SpriteManager.GetSprite("Button");
            
        
        AddComponent<Sprite, SpriteConfig>(spriteConfig);
    }

    public void SetFontStyle(float scale = 1.0f, float rotationDegrees = 0f, Color? color = null)
    {
        buttonConfig.FontScale = scale;
        buttonConfig.FontRotation = MathHelper.ToRadians(rotationDegrees);
        if (color.HasValue) buttonConfig.TextColor = color.Value;
    }

    private Vector2 GetTextPosition()
    {
        return Position + buttonConfig.TextOffset;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
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