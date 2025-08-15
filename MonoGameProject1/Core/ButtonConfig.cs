using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class ButtonConfig : ComponentConfig
{
    public string Text = "";
    public bool TintEnabled = false;
    
    public Rectangle _clickArea;
    


    // Font properties
    public SpriteFont Font;
    public Color TextColor = Color.Black;
    public float FontScale = 1.0f;
    public float FontRotation = 0f;
    public Vector2 TextOffset = Vector2.Zero;
    public SpriteEffects TextEffects = SpriteEffects.None;



}