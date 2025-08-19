using Microsoft.Xna.Framework;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class EndTitle : GameObject
{
    
    Text text;
    TextConfig textConfig;
    public EndTitle(string name) : base(name) 
    {
        Position = ScreenPosition.TopCenter() + new Vector2(0,60);
        Scale = new Vector2(1.2f, 1.2f);
      
        
        var fontInfo = TextManager.GetFont("Oswald");
        
        textConfig = new TextConfig(fontInfo);
        textConfig.Text = "Game Over";
        textConfig.Color = Color.DarkRed;
        textConfig.EffectSettings.EnableShakeEffect = true;

        
        
        
        text = AddConfigComponent<Text, TextConfig>(textConfig);
        
        
        
    }
}