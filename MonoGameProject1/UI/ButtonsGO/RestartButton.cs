// using Microsoft.Xna.Framework;
// using MonoGameProject1.Core;
//
// namespace MonoGameProject1;
//
// public class RestartButton : GameObject
// {
//     Button button;
//
//
//     public RestartButton(string name) : base(name)
//     {
//         Position = ScreenPosition.BottomCenter() + new Vector2(0, -20);
//                                                     
//         var spriteSheetInfo = SpriteManager.GetSprite("RestartButton");
//         spriteConfig = new SpriteConfig(spriteSheetInfo);
//
//         btnSprite = AddConfigComponent<Sprite, SpriteConfig>(spriteConfig);
//         
//         button = AddComponent<Button>();
//         
//         
//     }
// }