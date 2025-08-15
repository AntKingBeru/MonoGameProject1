// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using MonoGameProject1.Core;
//
// namespace MonoGameProject1;
//
// public class Collider : Sprite
// {
//     public ColliderConfig colliderConfig;
//     public delegate void CollisionHandler(Collider other);
//
//     public Collider() : base()
//     {
//         
//     }
//
//     public override void Initialize<T>(T config)
//     {
//         base.Initialize(config);
//         if (config is ColliderConfig colliderConfig)
//         {
//             this.colliderConfig = colliderConfig;
//         }
//     }
//
//     public event CollisionHandler OnCollision;
//     public event CollisionHandler OnTrigger;
//
//     public override void Draw(SpriteBatch _spriteBatch)
//     {A
//         int thickness = 5;
//         // Draw outline
//         
//         // top
//         _spriteBatch.Draw(Texture,
//             new Rectangle(
//                 colliderConfig.Bounds.X,
//                 colliderConfig.Bounds.Y,
//                 colliderConfig.Bounds.Width,
//                 thickness
//             ), 
//             Color.White);
//         
//         // left
//         _spriteBatch.Draw(Texture,
//             new Rectangle(
//                 colliderConfig.Bounds.X,
//                 colliderConfig.Bounds.Y,
//                 thickness,
//                 colliderConfig.Bounds.Height
//             ), 
//             Color.White);
//         
//         // right
//         _spriteBatch.Draw(Texture,
//             new Rectangle(
//                 colliderConfig.Bounds.X + colliderConfig.Bounds.Width - thickness,
//                 colliderConfig.Bounds.Y,
//                 thickness,
//                 colliderConfig.Bounds.Height
//             ), 
//             Color.White);
//         
//         // bottom
//         _spriteBatch.Draw(Texture,
//             new Rectangle(
//                 colliderConfig.Bounds.X,
//                 colliderConfig.Bounds.Y + colliderConfig.Bounds.Height - thickness,
//                 colliderConfig.Bounds.Width,
//                 thickness
//             ), 
//             Color.White);
//     }
//     
//     public override void Update(GameTime gameTime)
//     {
//         //destRectangle = GetDestRectangle(_rect);
//         Position = new Vector2(colliderConfig.Bounds.X, colliderConfig.Bounds.Y);
//         base.Update(gameTime);
//     }
//
//     public bool Intersect(Collider other)
//     {
//         return colliderConfig.Bounds.Intersects(other.colliderConfig.Bounds);
//     }
//
//     public void Notify(Collider other)
//     {
//         if (IsTrigger)
//         {
//             OnTrigger?.Invoke(other);
//         }
//         else
//         {
//             OnCollision?.Invoke(other);
//         }
//     }
//     
//     
// }