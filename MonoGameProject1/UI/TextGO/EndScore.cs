using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class EndScore : GameObject
{
    private Text text;
    private TextConfig textConfig;

    public EndScore(string name) : base(name)
    {
        Position = ScreenPosition.Center() + new Vector2(0,-40 );
        Scale = new Vector2(1.05f, 1.05f);
        var fontInfo = TextManager.GetFont("Oswald");
        textConfig = new TextConfig(fontInfo);
        textConfig.EffectSettings.EnableColorCycle = true;
        textConfig.EffectSettings.ColorCycleSpeed = 0.5f;
        textConfig.EffectSettings.EnablePulseEffect = true;
        textConfig.EffectSettings.PulseSpeed = 1.5f;
        textConfig.EffectSettings.PulseAmount = 0.05f;

        textConfig.Text = $"Your Score: {ComboManager.BestScore}\r\n" +
                          $"Fish Caught: {ComboManager.TotalFishCaught}\r\n" +
                          $"Depth: {ComboManager.CurrentDepth:0.00}m";
        text = AddConfigComponent<Text, TextConfig>(textConfig);
    }
}