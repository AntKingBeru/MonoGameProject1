using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject1.Core;

public class Input : Component
{
    private float speed = 250f;
    private float originSpeed;
    private bool disableMovement = false; //TODO change to true after there are buttons to start the game and shit
    private const float HORIZONTALMULTIPLIER = 1f;
    private const float UPMULTIPLIER = 1.626f;
    private const float DOWNMULTIPLIER = 0.616f;
    //private const float SLOWMOTION = 0.7f;
    
    public Input() : base()
    {
        
    }
    
    public void EnableMovement()
    {
        disableMovement = false;
    }
    
    public void DisableMovement()
    {
        disableMovement = true;
    }
    
    public override void Update(GameTime gameTime)
    {
        var state = Keyboard.GetState();
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (!disableMovement)
        {
            foreach (var key in state.GetPressedKeys())
            {
                switch (key)
                {
                    //Movements
                    case Keys.Up:
                    case Keys.W:
                        gameObject.Position.Y -= speed * UPMULTIPLIER * deltaTime;
                        break;
                    case Keys.Left:
                    case Keys.A:
                        gameObject.Position.X -= speed * HORIZONTALMULTIPLIER * deltaTime;
                        break;
                    case Keys.Down:
                    case Keys.S:
                        gameObject.Position.Y += speed * DOWNMULTIPLIER * deltaTime;
                        break;
                    case Keys.Right:
                    case Keys.D:
                        gameObject.Position.X += speed * HORIZONTALMULTIPLIER * deltaTime;
                        break;
                    case Keys.Space:
                        //TODO add slow logic
                        break;
                }
                
                Console.WriteLine($"Pressed key: {key}");
            }
        }
        
        base.Update(gameTime);
    }
}