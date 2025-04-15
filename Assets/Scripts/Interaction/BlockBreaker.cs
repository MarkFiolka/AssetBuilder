using UnityEngine;

public class BlockBreaker : MonoBehaviour
{
    public void CalculateBreaking(RaycastHit hit)
    {
        GameObject obj = hit.transform.gameObject;
        if (ObjectRepository.cubes.Contains(obj))
        {
            ObjectRepository.cubes.Remove(obj);
            Destroy(obj);
        }
        Destroy(obj);
    }
}
