using UnityEngine;
using Utility;

namespace Interaction
{
    public class HandleSelectionCubes : MonoBehaviour
    {
        public void HandleHitCube(RaycastHit hit)
        {
            if (hit.collider.gameObject.name == "cube")
            {
                GameObject obj = hit.collider.gameObject;
                Renderer rend = obj.GetComponent<Renderer>();

                if (ObjectRepository.selectedCubes.Contains(obj))
                {
                    if (ObjectRepository.originalCubeColors.TryGetValue(obj, out Color originalColor))
                    {
                        rend.material.color = originalColor;
                        ObjectRepository.originalCubeColors.Remove(obj);
                    }
                    ObjectRepository.selectedCubes.Remove(obj);
                }
                else
                {
                    ObjectRepository.originalCubeColors[obj] = rend.material.color;
                    rend.material.color = Settings.Instance.selectColor;
                    ObjectRepository.selectedCubes.Add(obj);
                }
            }
        }

        public void DeselectAllSelectedCubes()
        {
            foreach (GameObject cube in ObjectRepository.selectedCubes)
            {
                Renderer rend = cube.GetComponent<Renderer>();
                if (ObjectRepository.originalCubeColors.TryGetValue(cube, out Color origColor))
                {
                    rend.material.color = origColor;
                }
                else
                {
                    Logger.LogWarning($"Original color not found for {cube.name}.");
                }
            }
    
            ObjectRepository.selectedCubes.Clear();
            ObjectRepository.originalCubeColors.Clear();
        }

    }
}