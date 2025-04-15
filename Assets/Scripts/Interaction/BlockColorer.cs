using UnityEngine;

public class BlockColorer : MonoBehaviour
{
    public void CalculateColoring(RaycastHit hit)
    {
        GameObject obj = hit.collider.gameObject;
        Renderer rend = obj.GetComponent<Renderer>();

        // Use object name or tag for the switch
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
                Debug.Log("No valid object found to color.");
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
            
        }
    }
    
    private void ColorMiniPlanes(Renderer rend)
    {
        if (rend != null)
        {
            
        }
    }
}