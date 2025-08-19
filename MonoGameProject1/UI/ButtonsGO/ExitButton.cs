using System;
using Microsoft.Xna.Framework;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class ExitButton : GameObject
{
    Button button;
    ButtonConfig buttonConfig;
    Sprite sprite;
    
    public ExitButton(string name) : base(name)
    {

        Position = ScreenPosition.Center() + new Vector2(0, 200); // offset
        
        var spriteConfig = new SpriteConfig(SpriteManager.GetSprite("ExitButton"));
        sprite = AddConfigComponent<Sprite, SpriteConfig>(spriteConfig);
        buttonConfig = new ButtonConfig(sprite);
        button = AddConfigComponent<Button, ButtonConfig>(buttonConfig);
    }

    public override void Enable()
    {
        button.OnButtonClick += Exit;


        base.Enable();
    }


    public override void Disable()
    {
        button.OnButtonClick -= Exit;
        base.Disable();
    }

    private void Exit()
    {
        SceneManager.Exit = true;
    }
}