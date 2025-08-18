using Microsoft.Xna.Framework;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class ComboText : GameObject
{
    public TextConfig TextConfig;
    public Text Text;

    public ComboText(string name) : base(name)
    {
        var fontInfo = TextManager.GetFont("Oswald");

        //  combo text (top left)
        TextConfig = new TextConfig(fontInfo);
        ConfigureComboEffects(TextConfig);
        Text = AddConfigComponent<Text, TextConfig>(TextConfig);
        Position = ScreenPosition.TopLeft()  + new Vector2(100, 40);
    }


    private void ConfigureComboEffects(TextConfig textConfig)
    {
        // Configure effects for combo text
        textConfig.EffectSettings.EnableWaveEffect = true;
        textConfig.EffectSettings.WaveAmplitude = 5f;
        textConfig.EffectSettings.WaveFrequency = 0.5f;
        textConfig.EffectSettings.WaveSpeed = 4f;
        textConfig.EffectSettings.WavePerCharacter = true;

        textConfig.EffectSettings.EnableScaleEffect = true;
        textConfig.EffectSettings.BaseScale = 1f;
        textConfig.EffectSettings.MaxScale = 1.5f;
        textConfig.EffectSettings.MaxScaleValue = ComboManager.MAXCOMBO;

        textConfig.EffectSettings.EnableShakeEffect = true;
        textConfig.EffectSettings.ShakeIntensity = 3f;
        textConfig.EffectSettings.ShakeDuration = 0.2f;

        textConfig.EffectSettings.EnableColorCycle = true;
        textConfig.EffectSettings.ColorCycleSpeed = 2f;
        textConfig.EffectSettings.ColorPalette = new Color[]
        {
            Color.White,
            Color.Yellow,
            Color.Orange,
            Color.Red
        };
    }
}