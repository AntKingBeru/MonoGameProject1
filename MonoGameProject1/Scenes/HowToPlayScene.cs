using Microsoft.Xna.Framework;
using MonoGameProject1.UI.TextGO;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class HowToPlayScene : Scene
{
    public TextConfig TextConfig;
    public Text Text;
    
    public override void OnEnable()
    {
        IsActive = true;

        var startButton = new StartButton("Start");
        startButton.Position = (ScreenPosition.Center() + ScreenPosition.BottomCenter()) * 0.5f + new Vector2(0, 50);
        startButton.Scale = new Vector2(0.25f, 0.25f);
        AddActiveObject(startButton);
        
        var exitButton = new ExitButton("Exit");
        exitButton.Position = startButton.Position + new Vector2(0, 100);
        exitButton.Scale = new Vector2(0.25f, 0.25f);
        AddActiveObject(exitButton);

        var title = new HowToPlayTitle("HowToPlayTitle");
        AddActiveObject(title);
        
        var fontInfo = TextManager.GetFont("Oswald");
        TextConfig = new TextConfig(fontInfo);
        TextConfig.Text = @"        
Catch as many fish as you can, reach as far down as you can.
After you click 'Start'.
Time your shot and press 'Space' to launch the harpoon.
Green means a big boost.
Yellow means a small boost.
Red means no boost.
Catch fish by piercing them.
Catching an 'Aboomnapha' slows you down.
Once your speed reaches 0, you lose.

Controls:
WASD / Arrow Keys to move.
";
        
        var howToPlayText = new GameObject("HowToPlayText");
        AddActiveObject(howToPlayText);
        howToPlayText.AddConfigComponent<Text, TextConfig>(TextConfig);
        howToPlayText.Position = (ScreenPosition.Center() + ScreenPosition.TopCenter()) * 0.5f + new Vector2(0, 150);
        howToPlayText.Scale = new Vector2(0.5f, 0.5f);
            
        Init();
    }
}