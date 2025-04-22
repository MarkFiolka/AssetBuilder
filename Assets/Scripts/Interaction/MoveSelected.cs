using UnityEngine;
using Utility;

namespace Interaction
{
    public class MoveSelected : MonoBehaviour
    {
        public static void MoveTowardsX(int value)
        {
            MoveSelectedCubes(new Vector3(value, 0, 0));
        }

        public static void MoveTowardsY(int value)
        {
            MoveSelectedCubes(new Vector3(0, value, 0));
        }

        public static void MoveTowardsZ(int value)
        {
            MoveSelectedCubes(new Vector3(0, 0, value));
        }

        private static void MoveSelectedCubes(Vector3 direction)
        {
            foreach (var cube in ObjectRepository.selectedCubes)
            {
                if (cube != null)
                {
                    cube.transform.position += direction;
                }
            }
        }
    }
}