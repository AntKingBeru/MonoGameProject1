using Microsoft.Xna.Framework;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class StartButton : GameObject
{
    Button button;
    Sprite sprite;
    ButtonConfig buttonConfig;
    
    public StartButton(string name) : base(name)
    {
        Position = ScreenPosition.Center() - new Vector2(0, 200); // offset

        var spriteConfig = new SpriteConfig(SpriteManager.GetSprite("StartButton"));
        sprite = AddConfigComponent<Sprite, SpriteConfig>(spriteConfig);
        buttonConfig = new ButtonConfig(sprite);
        button = AddConfigComponent<Button, ButtonConfig>(buttonConfig);
    }


    public override void Enable()
    {
        button.OnButtonClick += StartGame;

        base.Enable();
    }

    public override void Disable()
    {
        button.OnButtonClick -= StartGame;

        base.Disable();
    }

    public void StartGame()
    {
        SceneManager.EnableScene("Game Scene");
    }
}