using UnityEngine;

namespace Utility
{
    public class HelperFunctions : MonoBehaviour
    {
        /// <summary>
        /// Returns the parsed color from an external hex string, defaulting to magenta (#FF00FF) if parsing fails.
        /// </summary>
        public static Color ParseExternalColor(string hexColor)
        {
            if (string.IsNullOrWhiteSpace(hexColor))
                return Color.magenta;

            string sanitizedHex = "#" + hexColor.Trim().TrimStart('#');
            return ColorUtility.TryParseHtmlString(sanitizedHex, out Color parsedColor) ? parsedColor : Color.magenta;
        }
    }
}