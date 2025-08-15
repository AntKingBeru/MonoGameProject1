using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class ButtonConfig : ComponentConfig
{
    public Rectangle _clickArea;
    



    public ButtonConfig()
    {
      _clickArea = new Rectangle(0, 0, 100, 100);
    }

}