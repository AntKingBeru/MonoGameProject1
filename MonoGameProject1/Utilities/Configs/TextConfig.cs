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