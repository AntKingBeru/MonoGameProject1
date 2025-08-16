using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class FishBehaviour : Component
{
    private Func<GameObject, GameTime, Vector2> currentPattern;
    
    public void SetPattern(Func<GameObject, GameTime, Vector2> pattern)
    {
        IsActive = true;

        currentPattern = pattern;
    }


    protected override void OnEnable()
    {
        // Set default pattern
        IsActive = true;
        currentPattern = FishPatterns.SineWave;
        
    }

    protected override void OnDisable()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        
        if (!IsActive || currentPattern == null) return;
        
        gameObject.Position = currentPattern(gameObject, gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
    }
}