using UnityEngine;

namespace Utility
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance { get; private set; }

        public Color color { get; set; } = Color.white;
        public Color placementGridPreviewColor { get; set; } = new Color(1f, 1f, 0f, 0.35f);
        public Color placementPreviewColor { get; set; } = new Color(1f, 1f, 1f, 0.3f);
        public Color placementPreviewColorBlocked { get; set; } = new Color(1f, 0f, 0f, 0.3f);
        public Color selectColor { get; set; } = Color.yellow;
        public bool placementGridPreview { get; set; } = false;
        public bool placeBlocks { get; set; } = false;
        public bool breakBlocks { get; set; } = false;
        public bool selectBlocks { get; set; } = false;

        public bool paintBlocks { get; set; } = false;
        public bool placementPreview { get; set; } = false;
        public bool halfPlacement { get; set; } = false;
        public bool isMoving { get; set; } = false;
        public bool isDragging { get; set; } = false;
        public bool clickedOnBlock { get; set; } = false;


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

        public void SetPlaceBlocksToggle(bool value)
        {
            placeBlocks = value;
        }

        public void SetPaintBlocksToggle(bool value)
        {
            paintBlocks = value;
        }

        public void SetBreakBlocksToggle(bool value)
        {
            breakBlocks = value;
        }

        public void SetSelectBlocksToggle(bool value)
        {
            selectBlocks = value;
        }
    }
}