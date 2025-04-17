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

        public static void DisableAllModes()
        {
            //FutureMode();
            //Not implemented for now 
        }
    }
}