using Microsoft.Xna.Framework;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class HowToPlayButton : GameObject
{
    Button button;
    ButtonConfig buttonConfig;
    Sprite sprite;


    public HowToPlayButton(string name) : base(name)
    {
        var spriteConfig = new SpriteConfig(SpriteManager.GetSprite("HowToPlayButton"));
        sprite = AddConfigComponent<Sprite, SpriteConfig>(spriteConfig);
        buttonConfig = new ButtonConfig(sprite);
        button = AddConfigComponent<Button, ButtonConfig>(buttonConfig);
        Scale = new Vector2(0.25f, 0.25f);
        Position = ScreenPosition.Center() + new Vector2(-120, 150);
    }

    public override void Enable()
    {
        button.OnButtonClick += HowToPlay;
        base.Enable();

    }

    public override void Disable()
    {
        button.OnButtonClick -= HowToPlay;
        base.Enable();

    }

    private void HowToPlay()
    {
SceneManager.EnableScene("How to Play");    }
}