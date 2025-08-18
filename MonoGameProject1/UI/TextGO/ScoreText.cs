using Microsoft.Xna.Framework;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class ScoreText :GameObject
{
    public TextConfig TextConfig;
    public Text Text;
    
    public ScoreText(string name) : base(name)
    {
        var fontInfo = TextManager.GetFont("Oswald");

        // score text (top left, below combo)
        TextConfig = new TextConfig(fontInfo);

        TextConfig.Color = Color.Gold;

        TextConfig.EffectSettings.EnablePulseEffect = true;
        TextConfig.EffectSettings.PulseSpeed = 1.5f;
        TextConfig.EffectSettings.PulseAmount = 0.05f;

        Scale = new Vector2(0.4f, 0.4f);
        Position = ScreenPosition.TopLeft() + new Vector2(120, 80);
        Text = AddConfigComponent<Text, TextConfig>(TextConfig);
    }
}