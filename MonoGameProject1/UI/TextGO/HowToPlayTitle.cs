using Microsoft.Xna.Framework;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1.UI.TextGO;

public class HowToPlayTitle : GameObject
{
    private Text text;
    
    public HowToPlayTitle(string name) : base(name)
    {
        var fontInfo = TextManager.GetFont("Oswald");
        var textConfig = new TextConfig(fontInfo);


        text = AddConfigComponent<Text, TextConfig>(textConfig);
        text.textConfig.Text = "How to Play this shit";
        Position = ScreenPosition.TopCenter() + new Vector2(0, 50);
    }
}