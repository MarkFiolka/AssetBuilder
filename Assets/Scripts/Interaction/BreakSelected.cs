using UnityEngine;
using Utility;

namespace Interaction
{
    public class BreakSelected : MonoBehaviour
    {
        public static void BreakSelectedBlocks()
        {
            for (int i = ObjectRepository.selectedCubes.Count - 1; i >= 0; i--)
            {
                var obj = ObjectRepository.selectedCubes[i];
                Destroy(obj);
                ObjectRepository.UnregisterCube(obj);
            }
        }
    }
}