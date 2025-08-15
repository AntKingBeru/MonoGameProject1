using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameProject1.Core;

public class SpriteData
{
    public string name;
    public string path;
    public Texture2D texture;
    public Vector2 position;
    public Vector2 scale;
    public Vector2 origin = Vector2.Zero;
    public Vector2 size;
    public Rectangle sourceRectangle;
    public Rectangle destRectangle;
    public Color color;
    public SpriteEffects effects;
    public float rotation;
    public float layerDepth = 0.5f;
}