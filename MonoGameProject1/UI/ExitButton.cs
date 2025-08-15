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
        Console.WriteLine("Enabled ExitButton");
        buttonConfig.Text = "Test";
        Position = ScreenPosition.MiddleCenter();
        OnButtonClick += Exit;
        base.Enable();
    }

    private void Exit()
    {
        SceneManager.Exit = true;
    }
    
}