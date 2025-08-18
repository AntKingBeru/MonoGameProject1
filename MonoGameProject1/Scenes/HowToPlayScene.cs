using Microsoft.Xna.Framework;
using MonoGameProject1.Core;
using MonoGameProject1.UI.TextGO;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class HowToPlayScene : Scene
{
    public TextConfig TextConfig;
    public Text Text;
    private Vector2 titlePosition;
    
    public override void OnEnable()
    {
        IsActive = true;
        
        var startButton = new StartButton("Start");
        startButton.Position = new Vector2(
            ScreenPosition.Center().X +
            ScreenPosition.BottomLeft().X, ScreenPosition.Center().Y + ScreenPosition.BottomLeft().Y)
            * 0.5f + new Vector2(0, 150);
        startButton.Scale = new Vector2(0.25f, 0.25f);
        AddActiveObject(startButton);
        
        var exitButton = new ExitButton("Exit");
        exitButton.Position = new Vector2(
                ScreenPosition.Center().X +
                ScreenPosition.BottomRight().X, ScreenPosition.Center().Y + ScreenPosition.BottomRight().Y)
            * 0.5f + new Vector2(0, 150);
        exitButton.Scale = new Vector2(0.25f, 0.25f);
        AddActiveObject(exitButton);

        var title = new HowToPlayTitle("HowToPlayTitle");
        titlePosition = title.Position;
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
        howToPlayText.Position = new Vector2(
            ScreenPosition.Center().X,
            titlePosition.Y + title.GetComponent<Sprite>().spriteConfig.SpriteInfo.Texture.Height * title.Scale.Y * 2.125f);
        howToPlayText.Scale = new Vector2(0.5f, 0.5f);
            
        Init();
    }
}