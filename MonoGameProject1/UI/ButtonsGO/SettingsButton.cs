using System;

namespace MonoGameProject1;

public class SettingsButton : GameObject
{
    Button button;

    
    public SettingsButton(string name) : base(name)
    {
        button = AddComponent<Button>();
        EnableComponent(button);
        button.sprite.spriteConfig.SpriteInfo = SpriteManager.GetSprite("SettingsButton");

        Position = ScreenPosition.Center();

    }
    public override void Enable()
    {
        button.OnButtonClick += OpenSettingsMenu;
        base.Enable();
    }

    private void OpenSettingsMenu()
    {
        Console.WriteLine("Settings Menu"); //TODO add settings menu
    }

    public override void Disable()
    {
        button.OnButtonClick -= OpenSettingsMenu;
        base.Disable();
    }
}