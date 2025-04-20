using UnityEngine;
using Utility;

namespace Interaction
{
    public class BlockColorer : MonoBehaviour
    {
        public void CalculateColoring(RaycastHit hit)
        {
            GameObject obj = hit.collider.gameObject;
            Renderer rend = obj.GetComponent<Renderer>();

            switch (obj.name)
            {
                case "cube":
                    ColorCube(rend);
                    break;
                case "plane":
                    ColorPlanes(rend);
                    break;
                case "miniPlane":
                    ColorMiniPlanes(rend);
                    break;
                default:
                    Logger.Log("No valid object found to color.");
                    break;
            }
        }

        private void ColorCube(Renderer rend)
        {
            if (rend != null)
            {
                rend.material.color = Settings.Instance.color;
            }
        }

        private void ColorPlanes(Renderer rend)
        {
            if (rend != null)
            {
                rend.material.color = Settings.Instance.color;
                //add ways to color parent -> childs
            }
        }

        private void ColorMiniPlanes(Renderer rend)
        {
            if (rend != null)
            {
                rend.material.color = Settings.Instance.color;
                //add ways to color parent -> parent -> childs
            }
        }
    }
}