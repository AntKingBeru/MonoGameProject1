using Microsoft.Xna.Framework;
using MonoGameProject1.Core;

namespace MonoGameProject1;

public class StartButton : GameObject
{
    
    Button button;

    public StartButton(string name) : base(name)
    {
        
        button = AddComponent<Button>();
        EnableComponent(button);
        button.sprite.spriteConfig.SpriteInfo = SpriteManager.GetSprite("StartButton");

        Position = ScreenPosition.Center() - new Vector2(0, 200); // offset
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