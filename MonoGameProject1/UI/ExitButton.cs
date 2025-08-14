using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1;

public class ExitButton : GameObject
{
    Button _exitButton;

    public ExitButton(string name) : base(name)
    {
        SpriteManager.AddSprite(
            "ExitButton",
            new Rectangle(0, 0, 150, 150),
            Color.Red);

        var btnSprite = SpriteManager.GetSprite("ExitButton");

        _exitButton = Button.CreateButton(name, "Exit", btnSprite);

        _exitButton.OnButtonClick += Exit;

        Position = ScreenPosition.MiddleCenter();
        _exitButton.Scale = new Vector2(0.5f, 0.5f);
        _exitButton.Size = new Vector2(btnSprite.Texture.Width, btnSprite.Texture.Height);
        _exitButton.Position = Position;
        _exitButton.Origin = new Vector2(btnSprite.Texture.Width / 0.5f, btnSprite.Texture.Height / 0.5f);
        _exitButton.LayerDepth = 0.9f;
        _exitButton.TintEnabled = true;
    }

    private void Exit()
    {
        SceneManager.Exit = true;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _exitButton.Draw(spriteBatch);

        base.Draw(spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        _exitButton.Update(gameTime);
        base.Update(gameTime);
    }
}