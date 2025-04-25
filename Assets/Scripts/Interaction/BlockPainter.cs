using UnityEngine;
using Utility;

namespace Interaction
{
    public class BlockPainter : MonoBehaviour
    {
        public void CalculateColoring(RaycastHit hit)
        {
            GameObject obj = hit.collider.gameObject;
            Renderer rend = obj.GetComponent<Renderer>();
            
            ColorCube(rend);
        }

        private void ColorCube(Renderer rend)
        {
            if (rend != null)
            {
                rend.material.color = Settings.Instance.color;
            }
        }
    }
}