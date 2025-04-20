using UnityEngine;
using Utility;

namespace Interaction
{
    public class PaintSelected : MonoBehaviour
    {
        public static void RepaintSelectedBlocks(string hexColor)
        {
            Color color = HelperFunctions.ParseExternalColor(hexColor);

            for (int i = ObjectRepository.selectedCubes.Count - 1; i >= 0; i--)
            {
                var obj = ObjectRepository.selectedCubes[i];
                var rend = obj.GetComponent<Renderer>();

                rend.material.color = color;
                ObjectRepository.originalCubeColors[obj] = color;
            }
        }

    }
}