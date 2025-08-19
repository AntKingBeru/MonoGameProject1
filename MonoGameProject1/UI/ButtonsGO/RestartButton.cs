
using Microsoft.Xna.Framework;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class RestartButton : GameObject
{
    
    Sprite sprite;
    Button button;
    ButtonConfig buttonConfig;
 

    public RestartButton(string name) : base(name)
    {
        Position = ScreenPosition.BottomCenter() + new Vector2(-100,-300);
        Scale = new Vector2(0.25f, 0.25f);
        var spriteConfig = new SpriteConfig(SpriteManager.GetSprite("RestartButton"));
        sprite = AddConfigComponent<Sprite, SpriteConfig>(spriteConfig);
        buttonConfig = new ButtonConfig(sprite);
        button = AddConfigComponent<Button, ButtonConfig>(buttonConfig);
    }

    private void RestartGame()
    {
        ComboManager.Reset();
        SceneManager.EnableScene("Game Scene");
    }


    public override void Enable()
    {
        button.OnButtonClick += RestartGame;

        base.Enable();
    }
    public override void Disable()
    {
        button.OnButtonClick -= RestartGame;

        base.Disable();
    }
}