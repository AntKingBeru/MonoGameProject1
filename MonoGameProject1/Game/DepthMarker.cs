using Microsoft.Xna.Framework;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1.UI;

public class DepthMarker : GameObject
{
    public Text markerText;
    public TextConfig MarkerTextConfig;


    public DepthMarker(string name) : base(name)
    {
        var fontInfo = TextManager.GetFont("Oswald");

        // Marker text config (for current position indicator)
        MarkerTextConfig = new TextConfig(fontInfo);
        MarkerTextConfig.Color = Color.Gold; // Different color for the marker
        MarkerTextConfig.EffectSettings.EnableColorCycle = true;
        MarkerTextConfig.EffectSettings.ColorCycleSpeed = 3f;
        MarkerTextConfig.EffectSettings.ColorPalette = new Color[]
        {
            Color.Red
        };

        Scale = new Vector2(0.35f, 0.35f);
        markerText = AddConfigComponent<Text, TextConfig>(MarkerTextConfig);
    }
}