using Microsoft.Xna.Framework;

namespace MonoGameProject1;

public class Enemy:Sprite
{
    private Collider collider;
    public Enemy() : base("pacman")
    {
        Position = new Vector2(400, 400);
        collider = SceneManager.Create<Collider>();
        collider._rect = destRectangle;
    }
}