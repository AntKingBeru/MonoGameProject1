using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class ExitButton : Button
{

    public ExitButton(string name) : base(name)
    {
       
    }

    public override void Enable()
    {
        Position = ScreenPosition.Center();
        OnButtonClick += Exit;
        base.Enable();
    }

    
    public override void Disable()
    {
        OnButtonClick -= Exit;
        base.Disable();
    }
    private void Exit()
    {
        SceneManager.Exit = true;
    }
    
}