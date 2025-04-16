using UnityEngine;

namespace DefaultNamespace
{
    public class SelectCubes : MonoBehaviour
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
    }
}