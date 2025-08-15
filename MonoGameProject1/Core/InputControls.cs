using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameProject1.Core;

namespace MonoGameProject1.Core;

public class InputControls : Component
{
    //Non-Dynamic variables
    private int speed = 500;
    
    public InputControls() : base()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        var state = Keyboard.GetState();
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        foreach (var key in state.GetPressedKeys())
        {
            switch (key)
            {
                case Keys.W:
                    gameObject.Position.Y -= speed * deltaTime;
                    break;
                case Keys.A:
                    gameObject.Position.X -= speed * deltaTime;
                    break;
                case Keys.S:
                    gameObject.Position.Y += speed * deltaTime;
                    break;
                case Keys.D:
                    gameObject.Position.X += speed * deltaTime;
                    break;
            }
        }

        base.Update(gameTime);
    }
}