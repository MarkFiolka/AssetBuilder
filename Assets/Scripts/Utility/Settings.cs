using UnityEngine;
using UnityEngine.Rendering;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }
    
    public Color color { get; set; } = Color.white;
    public Color placementGridPreviewColor { get; set; } = Color.gray;
    public Color placementPreviewColor { get; set; } = Color.gray;
    public bool placementGridPreview { get; set; } = false;
    public bool placeBlocks { get; set; } = false;
    public bool breakBlocks { get; set; } = false;
    public bool colorBlocks { get; set; } = false;
    public bool placementPreview { get; set; } = false;
    public bool halfPlacement { get; set; } = false;
    public bool selectCubes { get; set; } = false;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setMode(string mode)
    {
        placeBlocks = breakBlocks = colorBlocks = selectCubes = false;        

        switch (mode)
        {
            case "place":
                placeBlocks = true;
                break;
            case "break":
                breakBlocks = true;
                break;
            case "color":
                colorBlocks = true;
                break;
            case "select":
                selectCubes = true;
                break;
            case "none":
                break;
        }
    }

    public void setColor(string hexColor)
    {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color parsedColor))
        {
            color = parsedColor;
        }
        else
        {
            Debug.LogError($"Invalid color string: {hexColor}");
        }
    }
    
    public void setPlacementGridPreviewColor(string hexColor)
    {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color parsedColor))
        {
            placementGridPreviewColor = parsedColor;
        }
        else
        {
            Debug.LogError($"Invalid color string: {hexColor}");
        }
    }
    
    public void setPlacementPreviewColor(string hexColor)
    {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color parsedColor))
        {
            placementPreviewColor = parsedColor;
        }
        else
        {
            Debug.LogError($"Invalid color string: {hexColor}");
        }
    }

    public void setPlacementGridPreview(bool value)
    {
        placementGridPreview = value;
    }

    public void setPlacementPreview(bool value)
    {
        placementPreview = value;
    }
    
    public void setHalfPlacement(bool value)
    {
        halfPlacement = value;
    }

    public void setCubeSelection(bool value)
    {
        selectCubes = value;
    }
}