using MonoGameProject1.Core;

namespace MonoGameProject1;

public class StartButton : Button
{
    public StartButton(string name) : base(name)
    {
    }


    public override void Enable()
    {
        
        
        Position = ScreenPosition.TopCenter();
        OnButtonClick += StartGame;
        
        base.Enable();
    }

    public override void Disable()
    {
        this.OnButtonClick -= StartGame;
        
        base.Disable();
    }

    public void StartGame()
    {
        SceneManager.EnableScene("Game Scene");
    }
}