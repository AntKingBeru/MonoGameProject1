using System.Drawing;
using Microsoft.Xna.Framework;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class MainMenuTitle : GameObject
{
    private Text text;

    public MainMenuTitle(string name) : base(name)
    {
        var fontinfo = TextManager.GetFont("Oswald");
        var textConfig = new TextConfig(fontinfo);


        text = AddConfigComponent<Text, TextConfig>(textConfig);
        text.textConfig.Text = "FishKebab";
        Position = ScreenPosition.TopCenter() + new Vector2(0, 50);
    }
}