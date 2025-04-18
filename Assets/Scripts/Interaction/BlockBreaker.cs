using UnityEngine;
using Utility;

namespace Interaction
{
    public class BlockBreaker : MonoBehaviour
    {
        public void CalculateBreaking(RaycastHit hit)
        {
            GameObject obj = hit.transform.gameObject;

            ObjectRepository.cubes.Remove(obj);
            Destroy(obj);
        }
    }
}