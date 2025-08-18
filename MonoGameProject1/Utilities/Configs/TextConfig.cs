using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameProject1.Visuals;

namespace MonoGameProject1.Utilities.Configs;

public class TextConfig : ComponentConfig
{
    private FontInfo fontInfo;

    public string Name { get; set; }
    public SpriteFont Font { get; set; }

    private string _text;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            UpdateTextCenter();
        }
    }

    public Color Color = Color.Black;
    public Vector2 TextCenter { get; set; }
    public SpriteEffects SpriteEffects { get; set; }
    public float LayerDepth { get; set; }

    public TextEffectSettings EffectSettings { get; set; } = new TextEffectSettings();

    public TextConfig(FontInfo info)
    {
        Font = info.Font;
        _text = "";
        Color = Color.Black;
        SpriteEffects = SpriteEffects.None;
        LayerDepth = 1f;
        Name = "";

        fontInfo = info;
        UpdateTextCenter();
    }

    private void UpdateTextCenter()
    {
        if (Font != null && !string.IsNullOrEmpty(_text))
        {
            Vector2 textSize = Font.MeasureString(_text);
            TextCenter = textSize / 2f;
        }
        else
        {
            TextCenter = Vector2.Zero;
        }
    }
}

public class TextEffectSettings
{
    // Wave effect
    public bool EnableWaveEffect { get; set; } = false;
    public float WaveAmplitude { get; set; } = 10f;
    public float WaveFrequency { get; set; } = 2f;
    public float WaveSpeed { get; set; } = 3f;
    public bool WavePerCharacter { get; set; } = true;

    // Scale effect
    public bool EnableScaleEffect { get; set; } = false;
    public float BaseScale { get; set; } = 1f;
    public float MaxScale { get; set; } = 1.5f;
    public float ScaleDuration { get; set; } = 0.5f;
    public int MaxScaleValue { get; set; } = 10; // For combo scaling

    // Fade effect
    public bool EnableFadeEffect { get; set; } = false;
    public float FadeAlpha { get; set; } = 1f;
    public float FadeDuration { get; set; } = 1f;
    public bool FadeIn { get; set; } = true;

    // Shake effect
    public bool EnableShakeEffect { get; set; } = false;
    public float ShakeIntensity { get; set; } = 2f;
    public float ShakeDuration { get; set; } = 0.3f;

    // Pulse effect
    public bool EnablePulseEffect { get; set; } = false;
    public float PulseSpeed { get; set; } = 2f;
    public float PulseAmount { get; set; } = 0.1f;

    // Color cycling
    public bool EnableColorCycle { get; set; } = false;
    public float ColorCycleSpeed { get; set; } = 1f;

    public Color[] ColorPalette { get; set; } =
    {
        Color.White,
        Color.Yellow,
        Color.Orange,
        Color.Red,
        Color.Purple
    };

    // Typewriter effect
    public bool EnableTypewriter { get; set; } = false;
    public float TypewriterSpeed { get; set; } = 10f; // Characters per second
    public string FullText { get; set; } = "";

    // Bounce effect
    public bool EnableBounceEffect { get; set; } = false;
    public float BounceHeight { get; set; } = 20f;
    public float BounceDuration { get; set; } = 0.5f;

    // Outline/Shadow
    public bool EnableOutline { get; set; } = false;
    public Color OutlineColor { get; set; } = Color.Black;
    public int OutlineThickness { get; set; } = 2;
    public bool EnableShadow { get; set; } = false;
    public Color ShadowColor { get; set; } = Color.Gray;
    public Vector2 ShadowOffset { get; set; } = new Vector2(2, 2);
}