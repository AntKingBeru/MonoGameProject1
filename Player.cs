using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject1;

public class Player : Sprite
{
    private int _speed = 500;
    public Player(Texture2D texture) : base(texture)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        var state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.W))
        {
            Position.Y -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (state.IsKeyDown(Keys.A))
        {
            Position.X -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (state.IsKeyDown(Keys.S))
        {
            Position.Y += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (state.IsKeyDown(Keys.D))
        {
            Position.X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        base.Update(gameTime);
    }
}