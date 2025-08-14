using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject1;

public class Player : Animation
{
    //Non-Dynamic variables
    private int _speed = 500;
    public Collider collider;
    
    public Player() : base("player")
    {
        //collider = SceneManager.Create<Collider>();
        collider.IsTrigger = true;
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
            }
        }

        base.Update(gameTime);
        collider._rect = GetDestRectangle(collider._rect);

    }
}