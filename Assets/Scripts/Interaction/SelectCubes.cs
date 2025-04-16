using UnityEngine;

namespace DefaultNamespace
{
    public class SelectCubes : MonoBehaviour
    {
        public void HandleHitCube(RaycastHit hit)
        {
            if (hit.collider.gameObject.name == "cube")
            {
                //ObjectRepository.selectedCubes.Add(hit.collider.gameObject);
            }
        }
    }
}