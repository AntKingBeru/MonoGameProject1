using Microsoft.Xna.Framework;
using MonoGameProject1.Core;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class ComboManager : GameObject
{
    private Text text;
    private const int MAXCOMBO = 10;
    private const int SCOREFISH = 1000;
    private const float MAX_COMBO_TIME = 3.0f;
    private int score = 0;
    private int combo = 0;
    private float comboTimer = 0;

    public ComboManager(string name) : base(name)
    {
        Scale = new Vector2(0.5f, 0.5f);

        var fontInfo = TextManager.GetFont("Oswald");
        var textConfig = new TextConfig(fontInfo);

        textConfig.Text = "Combo: " + combo;

        text = AddConfigComponent<Text, TextConfig>(textConfig);
        Position = ScreenPosition.TopLeft() + new Vector2(120, 20);
    }

    public override void Enable()

    {
        Fish.OnFishCaught += IncreaseCombo;
        base.Enable();
    }

    public override void Disable()
    {
        Fish.OnFishCaught -= IncreaseCombo;
        base.Disable();
    }

    private void IncreaseCombo(Fish fish = null, bool isAboomnpha = false)
    {
        if(isAboomnpha) return;
        if (combo >= MAXCOMBO) return;
        combo++;
        comboTimer = MAX_COMBO_TIME;
    }

    public override void Update(GameTime gameTime)
    {
        comboTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (comboTimer <= 0 && combo > 0)
        {
            combo = 0;
            comboTimer = 0;
        }

        text.textConfig.Text = "Combo: " + combo;

        base.Update(gameTime);
    }
}