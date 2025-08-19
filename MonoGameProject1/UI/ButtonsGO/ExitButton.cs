using System;
using Microsoft.Xna.Framework;

namespace MonoGameProject1;

public class ExitButton : GameObject
{
    Button button;
    
    public ExitButton(string name) : base(name)
    {
        button = AddComponent<Button>();
        EnableComponent(button);
        button.sprite.spriteConfig.SpriteInfo = SpriteManager.GetSprite("ExitButton");

        Position = ScreenPosition.Center() + new Vector2(0, 200); // offset
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