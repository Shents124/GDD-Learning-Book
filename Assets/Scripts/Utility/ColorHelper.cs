using System;
using Constant;
using UnityEngine;

namespace Utility
{
    public static class ColorHelper
    {
        public static Color GetColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Red:
                    return Color.red;
                
                case ColorType.Yellow:
                    return Color.yellow;
                
                case ColorType.Green:
                    return Color.green;
                
                case ColorType.Blue:
                    return Color.blue;
                
                default:
                    return Color.white;
            }
        }

        public static string GetColorName(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Red:
                    return "Red";
                case ColorType.Yellow:
                    return "Yellow";
                case ColorType.Green:
                    return "Green";
                case ColorType.Blue:
                    return "Blue";
                default:
                    return string.Empty;
            }
        }
    }
}