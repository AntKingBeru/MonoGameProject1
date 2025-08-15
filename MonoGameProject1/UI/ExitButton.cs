using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class ExitButton : Button
{

    public ExitButton(string name) : base(name)
    {
        
        
        OnButtonClick += Exit;
    }

    private void Exit()
    {
        SceneManager.Exit = true;
    }
    
}