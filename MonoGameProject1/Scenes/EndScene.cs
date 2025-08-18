namespace MonoGameProject1;

public class EndScene : Scene
{
    public override void OnEnable()
    {
        IsActive = true;
        
        var exitButton = new ExitButton("Exit");
        AddActiveObject(exitButton);
        var restartButton = new RestartButton("Restart");
        AddActiveObject(restartButton);
        var endTitle = new EndTitle("EndTitle");
        AddActiveObject(endTitle);
        var endScore = new EndScore("EndScore");
        AddActiveObject(endScore);
        
        Init();
    }
    
    
    
    
}