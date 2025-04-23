using Interaction;
using UnityEngine;

namespace Utility
{
    public class HelperFunctions : MonoBehaviour
    {
        public static Color ParseExternalColor(string hexColor)
        {
            if (string.IsNullOrWhiteSpace(hexColor))
                return Color.magenta;

            string sanitizedHex = "#" + hexColor.Trim().TrimStart('#');
            return ColorUtility.TryParseHtmlString(sanitizedHex, out Color parsedColor) ? parsedColor : Color.magenta;
        }
        
        public static Color HexToColor(string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out Color color))
                return color;
            else
                throw new System.Exception("Invalid hex color format: " + hex);
        }

        public static void DisableAllModes()
        {
            //FutureMode();
            //Not implemented for now 
        }
    }
}