using Constant;
using Minigame.BlueColor;
using Minigame.YellowColor;
using UnityEngine;
using UnityEngine.U2D;

namespace Utility
{
    public static class LoadSpriteService
    {
        private static Sprite LoadSprite(string name)
        {
            return LoadResourceService.LoadAsset<Sprite>(name);
        }

        public static Sprite LoadYellowFood(YellowFood yellowFood)
        {
            return LoadSprite(yellowFood.ToString(), PathConstants.YELLOW_FOOD);
        }

        public static Sprite LoadDottedClothes(BlueClothesType blueClothesType)
        {
            return LoadSprite(blueClothesType.ToString(), PathConstants.BLUE_DOTTED_CLOTHES);
        }

        public static Sprite LoadFrontClothes(BlueClothesType blueClothesType)
        {
            return LoadSprite(blueClothesType.ToString(), PathConstants.BLUE_FRONT_CLOTHES);
        }
        
        public static Sprite LoadBeHindClothes(BlueClothesType blueClothesType)
        {
            return LoadSprite(blueClothesType.ToString(), PathConstants.BLUE_BEHIND_CLOTHES);
        }
        
        private static Sprite LoadSprite(string name, string atlas)
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