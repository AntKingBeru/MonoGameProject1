using Microsoft.Xna.Framework;
using MonoGameProject1.UI.TextGO;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class HowToPlayScene : Scene
{
    public override void OnEnable()
    {
        IsActive = true;

        var startButton = new StartButton("Start");
        AddActiveObject(startButton);

        var title = new HowToPlayTitle("HowToPlayTitle");
        AddActiveObject(title);

        var howToPlayText = new GameObject("HowToPlayText");
        var fontInfo = TextManager.GetFont("Oswald");
        var howToPlayConfig = new TextConfig(fontInfo);
        howToPlayConfig.Text = @"How to play
        
Your goal is to catch as many fish as you can, and thus reach the lowest depth.
Once you press 'Start', you will start going back and forth. Press 'Space' to release the harpoon.
The closer you are to the green part of the bar, the better the start you have.
Catch fish by piercing them, but be careful of the 'Abumnapha', it slows you down.
WASD / Arrow Keys to move.
Space to first launch the harpoon.
Once you slow down to 0, you lose the game.
";
        howToPlayText.AddConfigComponent<Text, TextConfig>(howToPlayConfig);
        AddActiveObject(howToPlayText);
            
        Init();
    }
}