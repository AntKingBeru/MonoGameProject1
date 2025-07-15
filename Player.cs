using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject1;

public class Player : Animation
{
    //Non-Dynamic variables
    private int _speed = 500;
    
    public Player() : base("bird1")
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
                    Position.Y -= _speed * deltaTime;
                    break;
                case Keys.A:
                    Position.X -= _speed * deltaTime;
                    Effect = SpriteEffects.None;
                    break;
                case Keys.S:
                    Position.Y += _speed * deltaTime;
                    break;
                case Keys.D:
                    Position.X += _speed * deltaTime;
                    Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Keys.NumPad0:
                    ChangeAnimation("bird1");
                    break;
                case Keys.NumPad1:
                    ChangeAnimation("bird2");
                    break;
                case Keys.NumPad2:
                    ChangeAnimation("bird3");
                    break;
                case Keys.NumPad3:
                    ChangeAnimation("bird4");
                    break;
            }
        }

        base.Update(gameTime);
    }
}