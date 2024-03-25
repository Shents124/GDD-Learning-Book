using UnityEngine;
using UnityEngine.U2D;

namespace Utility
{
    public class LoadSpriteService
    {
        private static Sprite LoadSprite(string name)
        {
            return LoadResourceService.LoadAsset<Sprite>(name);
        }
        
        public static Sprite LoadSprite(string name, string atlas)
        {
            var spriteAtlas = LoadSpriteAtlas(atlas);

            return spriteAtlas.GetSprite(name);
        }

        private static SpriteAtlas LoadSpriteAtlas(string path)
        {
            return LoadResourceService.LoadAsset<SpriteAtlas>(path);
        }
    }
}