using UnityEngine;
using System.Collections.Generic;

namespace UI
{
    public static class ToolPaths
    {
        public static readonly Dictionary<string, string> Paths = new Dictionary<string, string>
        {
            { "place", "place" },
            { "select", "select" },
            { "erase", "eraser" },
            { "download", "download" },
            { "bucket", "bucket" },
        };
    }

    public class ToolUIManager : MonoBehaviour
    {
        private class ToolData
        {
            public string toolName;
            public string iconPath;
            public Texture2D iconTexture;
        }

        private List<ToolData> tools = new List<ToolData>();
        private int selectedIndex = 0;

        private Vector2 fixedButtonSize = new Vector2(32, 32);
        private Vector2 buttonSize;
        private Vector2 buttonStart = new Vector2(8, 8);
        private float buttonSpacing = 4f;
        private int buttonsPerRow = 5;
        private float minimumSidebarWidth = 300f;

        private GUIStyle flatButtonStyle;
        private GUIStyle flatBoxStyle;
        private GUIStyle backgroundStyle;
        private bool stylesInitialized = false;

        void Awake()
        {
            foreach (var entry in ToolPaths.Paths)
            {
                string toolName = entry.Key;
                string path = entry.Value;

                Texture2D tex = Resources.Load<Texture2D>(path) ?? Texture2D.whiteTexture;

                if (tex != null)
                {
                    tools.Add(new ToolData
                    {
                        toolName = toolName,
                        iconPath = path,
                        iconTexture = tex
                    });
                    Logger.Log($"Loaded icon: {toolName} from path '{path}'");
                }
                else
                {
                    Logger.LogWarning($"Failed to load texture: {toolName} from path '{path}'");
                }
            }
        }

        void InitializeStyles()
        {
            if (stylesInitialized) return;

            flatButtonStyle = new GUIStyle(GUI.skin.button)
            {
                normal = { background = null },
                hover = { background = null },
                active = { background = null },
                focused = { background = null },
                border = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0)
            };

            flatBoxStyle = new GUIStyle(GUI.skin.box)
            {
                normal = { background = Texture2D.blackTexture },
                border = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(4, 4, 4, 4)
            };

            Texture2D bgTex = new Texture2D(1, 1);
            bgTex.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 1f));
            bgTex.Apply();

            backgroundStyle = new GUIStyle();
            backgroundStyle.normal.background = bgTex;

            stylesInitialized = true;
        }

        void OnGUI()
        {
            InitializeStyles();

            if (tools.Count == 0) return;

            float sidebarWidth = Mathf.Max(Screen.width * 0.2f, minimumSidebarWidth);
            float sidebarHeight = Screen.height;
            Rect sidebarRect = new Rect(0, 0, sidebarWidth, sidebarHeight);
            GUI.Box(sidebarRect, GUIContent.none, backgroundStyle);

            float rowWidth = sidebarWidth - 2 * buttonStart.x - (buttonsPerRow - 1) * buttonSpacing;
            float maxButtonWidth = rowWidth / buttonsPerRow;
            float scaleFactor = Mathf.Min(1f, maxButtonWidth / fixedButtonSize.x);

            buttonSize = fixedButtonSize * scaleFactor;

            for (int i = 0; i < tools.Count; i++)
            {
                int row = i / buttonsPerRow;
                int col = i % buttonsPerRow;

                float x = buttonStart.x + col * (buttonSize.x + buttonSpacing);
                float y = buttonStart.y + row * (buttonSize.y + buttonSpacing);

                ToolData icon = tools[i];
                Rect btnRect = new Rect(x, y, buttonSize.x, buttonSize.y);

                if (GUI.Button(btnRect, new GUIContent(icon.iconTexture), flatButtonStyle))
                {
                    selectedIndex = i;
                    Logger.Log($"Selected tool: {icon.toolName}");
                }
            }

            if (tools.Count > selectedIndex)
            {
                ToolData selected = tools[selectedIndex];
                float panelY = buttonStart.y + ((tools.Count - 1) / buttonsPerRow + 1) * (buttonSize.y + buttonSpacing) + 10;

                GUI.BeginGroup(new Rect(buttonStart.x, panelY, sidebarWidth - 2 * buttonStart.x, 300));

                GUI.Label(new Rect(0, 0, 200, 20), $"Tool Panel: {selected.toolName}");

                switch (selected.toolName)
                {
                    case "place":
                        GUI.Label(new Rect(0, 30, 120, 20), "Size:");
                        float newValue = GUI.HorizontalSlider(new Rect(50, 30, 100, 20), 1.0f, 0.1f, 5.0f);
                        break;

                    case "select":
                        GUI.Button(new Rect(0, 30, 120, 20), "Select Mode");
                        break;

                    case "erase":
                        GUI.Toggle(new Rect(0, 30, 150, 20), true, "Enable Eraser");
                        break;

                    case "bucket":
                        GUI.Button(new Rect(0, 30, 120, 20), "Color and Recolor Blocks in Szene");
                        break;

                    case "download":
                        GUI.Label(new Rect(0, 30, 200, 20), "Download File");
                        break;
                }

                GUI.EndGroup();
            }
        }
    }
}
