using Microsoft.Xna.Framework;

namespace MonoGameProject1;

public class Enemy : Sprite
{
    public Collider Collide;

    public Enemy() : base("pacman")
    {
        Position = new Vector2(400, 400);
        Collide = SceneManager.Create<Collider>();
        Collide.Rect = DestRectangle;
    }
}