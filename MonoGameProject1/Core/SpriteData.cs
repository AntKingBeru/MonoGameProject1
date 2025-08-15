using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class SpriteData
{
    public string name;
    public string path;
    public Texture2D texture;
    public Rectangle sourceRectangle;
    public Rectangle destRectangle;
    public Color color;
    public SpriteEffects effects;
    public float layerDepth = 0.5f;
}