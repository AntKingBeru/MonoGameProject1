using Microsoft.Xna.Framework;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1.UI;

public class DepthMeter : GameObject
{
    private TextConfig TextConfig;
    private TextConfig MarkerTextConfig;
    public Text depthText;
    private DepthMarker depthMarker;

    public DepthMeter(string name, DepthMarker _depthMarker) : base(name)
    {
        var fontInfo = TextManager.GetFont("Oswald");

        // Main depth meter text (vertical, right side)
        TextConfig = new TextConfig(fontInfo);
        TextConfig.Color = Color.CadetBlue;
        TextConfig.EffectSettings.EnableWaveEffect = true;
        TextConfig.EffectSettings.WaveAmplitude = 2f;
        TextConfig.EffectSettings.WaveSpeed = 1f;
        TextConfig.EffectSettings.WavePerCharacter = false;

        Scale = new Vector2(0.35f, 0.35f);
        Position = ScreenPosition.MiddleRight() - new Vector2(20, 0);
        depthText = AddConfigComponent<Text, TextConfig>(TextConfig);
        depthMarker = _depthMarker;

        MarkerTextConfig = _depthMarker.markerText.textConfig;
    }


    public void UpdateDepthDisplay(float currentDepth)
    {
        int depthMeters = (int)currentDepth;
        string regularDepthDisplay = "";
        string markerDisplay = "";

        // Create a vertical depth meter with markers
        int displayRange = 20;
        int startDepth = System.Math.Max(0, depthMeters - 10); // Center the current depth in the display

        int markerLineIndex = -1;

        for (int i = 0; i < displayRange; i++)
        {
            int depth = startDepth + i;
            if (depth == depthMeters)
            {
                regularDepthDisplay += "     \n"; // Empty space where marker will be
                markerDisplay = $">{depth}m<"; // Current depth marker
                markerLineIndex = i;
            }
            else if (depth % 5 == 0)
            {
                regularDepthDisplay += $"-{depth}m-\n";
            }
            else
            {
                regularDepthDisplay += "  |\n";
            }
        }

        // Update the main depth text
        TextConfig.Text = regularDepthDisplay;

        // Update the marker text and p
        // osition it correctly
        if (depthMarker.markerText != null && MarkerTextConfig != null && markerLineIndex >= 0)
        {
            MarkerTextConfig.Text = markerDisplay;

            // Calculate proper positioning
            float lineHeight = TextConfig.Font.LineSpacing * Scale.Y;

            // Get the actual text bounds to calculate the starting position
            Vector2 textSize = TextConfig.Font.MeasureString(regularDepthDisplay) * Scale;

            // Calculate the top position of the text block
            Vector2 textStartPos = Position - TextConfig.TextCenter * Scale;

            // Calculate the position for the specific line where the marker should appear
            float markerY = textStartPos.Y + (markerLineIndex * lineHeight);

            // Add a small vertical offset to align with the empty space
            float verticalOffset = lineHeight * 0.5f; // Adjust this value to fine-tune position

            // Position the marker text
            depthMarker.Position = new Vector2(
                Position.X - 15, // Slight offset to avoid overlap
                markerY + verticalOffset // Add the vertical offset here
            );
            depthMarker.markerText.gameObject.IsActive = true;
        }
        else if (depthMarker.markerText != null)
        {
            // Hide marker if current depth is not in the display range
            depthMarker.markerText.gameObject.IsActive = false;
        }
    }
}