using UnityEngine;
using Utility;

namespace Interaction
{
    public class RotateSelected : MonoBehaviour
    {
        public static void RotateAroundX(int value)
        {
            Rotate(value, Vector3.right);
        }

        public static void RotateAroundY(int value)
        {
            Rotate(value, Vector3.up);
        }

        public static void RotateAroundZ(int value)
        {
            Rotate(value, Vector3.forward);
        }

        private static void Rotate(int value, Vector3 axis)
        {
            var baseRotation = 90;
            
            if (ObjectRepository.selectedCubes.Count == 0) return;

            Vector3 center = Vector3.zero;
            for (int i = 0; i < ObjectRepository.selectedCubes.Count; i++)
            {
                center += ObjectRepository.selectedCubes[i].transform.position;
            }
            center /= ObjectRepository.selectedCubes.Count;
            center = new Vector3(Mathf.Round(center.x), Mathf.Round(center.y), Mathf.Round(center.z));

            Quaternion rotation = Quaternion.AngleAxis(value * baseRotation, axis);

            for (int i = ObjectRepository.selectedCubes.Count - 1; i >= 0; i--)
            {
                var obj = ObjectRepository.selectedCubes[i];

                Vector3 offset = obj.transform.position - center;
                Vector3 newPos = rotation * offset + center;
                newPos = new Vector3(Mathf.Round(newPos.x), Mathf.Round(newPos.y), Mathf.Round(newPos.z));

                obj.transform.position = newPos;
                obj.transform.rotation = rotation * obj.transform.rotation;
            }
        }
    }
}