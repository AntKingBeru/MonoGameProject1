using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject1.Core;

public class Input : Component
{
    private float speed = 375f;
    private float originSpeed;

    private Vector2 moveVector = Vector2.Zero;
    
    private bool disableMovement = false; //TODO change to true after there are buttons to start the game and shit
    private const float HORIZONTALMULTIPLIER = 1f;
    private const float UPMULTIPLIER = 1.4f;
    private const float DOWNMULTIPLIER = 0.6f;
    private const float SLOWMOTION = 0.7f;
    
    public Input() : base()
    {
        originSpeed = speed;
    }
    
    public void EnableMovement()
    {
        disableMovement = false;
        SetActive(true);
    }
    
    public void DisableMovement()
    {
        disableMovement = true;
        SetActive(false);
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    public override void Update(GameTime gameTime)
    {
        moveVector = Vector2.Zero;
        
        var state = Keyboard.GetState();
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (!disableMovement) 
        {
            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
            {
                moveVector -= Vector2.UnitY;
            }

            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                moveVector += Vector2.UnitY;
            }
            
            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
            {
                moveVector -= Vector2.UnitX;
            }
            
            if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                moveVector += Vector2.UnitX;
            }
            
            if (moveVector != Vector2.Zero)
                moveVector.Normalize();
            
            if (moveVector.Y > 0)
            {
                moveVector.Y *= DOWNMULTIPLIER;
            }
            else if (moveVector.Y < 0)
            {
                moveVector.Y *= UPMULTIPLIER;
            }
            
            moveVector.X *= HORIZONTALMULTIPLIER;

            moveVector *= speed * deltaTime;
            
            if (state.IsKeyDown(Keys.Space))
            {
                //TODO add logic for slowdown mode
            }
            
            gameObject.Position += moveVector;
        }

        gameObject.Position = ScreenPosition.ClampInBoundaries(gameObject.Position);
    }
    
    

    public override void Draw(SpriteBatch spriteBatch)
    {
    }
}