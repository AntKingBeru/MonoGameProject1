
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
        Position = ScreenPosition.BottomCenter() + new Vector2(-200,-200);

        var spriteConfig = new SpriteConfig(SpriteManager.GetSprite("restartButton"));
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