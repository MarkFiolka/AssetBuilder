using UnityEngine;
using UnityEngine.Rendering;

namespace Utility
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance { get; private set; }

        public Color color { get; set; } = Color.white;
        public Color placementGridPreviewColor { get; set; } = new Color(1f, 1f, 0f, 0.35f);
        public Color placementPreviewColor { get; set; } = new Color(1f, 1f, 1f, 0.3f);
        public Color placementPreviewColorBlocked { get; set; } = new Color(1f, 0f, 0f, 0.3f);
        public Color selectColor { get; set; } = Color.magenta;
        public bool placementGridPreview { get; set; } = false;
        public bool placeBlocks { get; set; } = false;
        public bool breakBlocks { get; set; } = false;
        public bool paintBlocks { get; set; } = false;
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

        public void SetMode(Mode mode)
        {
            placeBlocks = breakBlocks = paintBlocks = selectCubes = false;

            HelperFunctions.DisableAllModes();

            switch (mode)
            {
                case Mode.PLACE:
                    placeBlocks = true;
                    break;
                case Mode.BREAK:
                    breakBlocks = true;
                    break;
                case Mode.PAINT:
                    paintBlocks = true;
                    break;
                case Mode.SELECT:
                    selectCubes = true;
                    break;
            }
        }

        public void SetColor(Color hexColor)
        {
            color = hexColor;
        }

        public void SetSelectColor(Color hexColor)
        {
            selectColor = hexColor;
        }

        public void SetPlacementGridPreviewColor(string hexColor)
        {
            if (ColorUtility.TryParseHtmlString(hexColor, out Color parsedColor))
            {
                placementGridPreviewColor = parsedColor;
            }
            else
            {
                Logger.LogError($"Invalid color string: {hexColor}");
            }
        }

        public void SetPlacementPreviewColor(string hexColor)
        {
            if (ColorUtility.TryParseHtmlString(hexColor, out Color parsedColor))
            {
                placementPreviewColor = parsedColor;
            }
            else
            {
                Logger.LogError($"Invalid color string: {hexColor}");
            }
        }
        
        public void SetPlacementPreviewColorBlocked(string hexColor)
        {
            if (ColorUtility.TryParseHtmlString(hexColor, out Color parsedColor))
            {
                placementPreviewColorBlocked = parsedColor;
            }
            else
            {
                Logger.LogError($"Invalid color string: {hexColor}");
            }
        }

        public void SetPlacementGridPreview(bool value)
        {
            placementGridPreview = value;
        }

        public void SetPlacementPreview(bool value)
        {
            placementPreview = value;
        }

        public void SetHalfPlacement(bool value)
        {
            halfPlacement = value;
        }
    }
}