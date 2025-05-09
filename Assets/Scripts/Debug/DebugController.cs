using System;
using UnityEngine;
using System.Collections.Generic;
using Interaction;
using Optimizing;
using UnityEditor.Rendering;
using UnityEngine.Rendering.Universal;
using Utility;

namespace Debug
{
    public class DebugController : MonoBehaviour
    {
        string input;
        bool showHelp = false;
        bool debugPanel = false;
        public static DebugCommand HELP;
        public static DebugCommand<bool> togglePlacement;
        public static DebugCommand<bool> toggleColoring;
        public static DebugCommand<bool> toggleBreaking;
        public static DebugCommand<bool> toggleSelection;
        public static DebugCommand<string> COLOR;
        public static DebugCommand<string> SCOLOR;
        public static DebugCommand<bool> PGP;
        public static DebugCommand<string> PGPC;
        public static DebugCommand<bool> PP;
        public static DebugCommand<string> PPC;
        public static DebugCommand<string> PPCB;
        public static DebugCommand<bool> halfPlacement;
        public static DebugCommand<string> paintSelected;
        public static DebugCommand<int> moveSelectedX;
        public static DebugCommand<int> moveSelectedY;
        public static DebugCommand<int> moveSelectedZ;
        public static DebugCommand<int> rotateAroundX;
        public static DebugCommand<int> rotateAroundY;
        public static DebugCommand<int> rotateAroundZ;
        public static DebugCommand breakSelected;
        public static DebugCommand startOptimize;
        public static DebugCommand revertOptimize;
        
        public List<object> commandList;
        private Vector2 scroll = Vector2.zero;

        private void Awake()
        {
            HELP = new DebugCommand("help", "Show help", "help", () => { showHelp = !showHelp; });

            toggleBreaking = new DebugCommand<bool>("toggleBreaking", "toggles the Placement", "toggleBreaking <true/false>", (x) =>
            {
                Settings.Instance.SetBreakBlocksToggle(x);
            });
            
            togglePlacement = new DebugCommand<bool>("togglePlacement", "toggles the Placement", "togglePlacement <true/false>", (x) =>
            {
                Settings.Instance.SetPlaceBlocksToggle(x);
            });
            
            toggleColoring = new DebugCommand<bool>("toggleColoring", "toggles the Placement", "toggleColoring <true/false>", (x) =>
            {
                Settings.Instance.SetPaintBlocksToggle(x);
            });
            
            toggleSelection = new DebugCommand<bool>("toggleSelection", "toggles the Placement", "toggleSelection <true/false>", (x) =>
            {
                Settings.Instance.SetSelectBlocksToggle(x);
            });
            
            COLOR = new DebugCommand<string>("color", "Sets color to a Hex value", "color <hex_value> (no # needed)",
                (x) =>
                {
                    Color color = HelperFunctions.ParseExternalColor(x);
                    Settings.Instance.SetColor(color);
                    Logger.Log("Color set to " + color);
                });

            SCOLOR = new DebugCommand<string>("scolor", "select the color with the objecs get selected with",
                "scolor <hex_value> (no # needed)",
                (x) =>
                {
                    Color color = HelperFunctions.ParseExternalColor(x);
                    Settings.Instance.SetSelectColor(color);
                    Logger.Log("SelectColor set to " + color);
                });

            PGPC = new DebugCommand<string>("pgpc", "sets the Placement Grid Preview Color",
                "pgpc <hex_value> (no # needed)", (x) =>
                {
                    Settings.Instance.SetPlacementGridPreviewColor("#" + x);
                    Logger.Log("PGPC set to " + x);
                });

            PPC = new DebugCommand<string>("ppc", "sets the Placement Preview Color", "ppc <hex_value> (no # needed)",
                (x) =>
                {
                    Settings.Instance.SetPlacementPreviewColor("#" + x);
                    Logger.Log("PPC set to " + x);
                });
            
            PPCB = new DebugCommand<string>("ppcb", "sets the Placement Preview Color Blocked", "ppc <hex_value> (no # needed)",
                (x) =>
                {
                    Settings.Instance.SetPlacementPreviewColorBlocked("#" + x);
                    Logger.Log("PPC set to " + x);
                });

            PGP = new DebugCommand<bool>("pgp", "toggles the Placement Preview", "pgp <true/false>", (x) =>
            {
                Settings.Instance.SetPlacementGridPreview(x);
                Logger.Log("PGP set to " + x);
            });

            PP = new DebugCommand<bool>("pp", "toggles the Placement Preview", "pp <true/false>", (x) =>
            {
                Settings.Instance.SetPlacementPreview(x);
                Logger.Log("PP set to " + x);
            });

            halfPlacement = new DebugCommand<bool>("halfPlacement", "toggles the half Placement",
                "halfPlacement <true/false>", (x) =>
                {
                    Settings.Instance.SetHalfPlacement(x);
                    Logger.Log("halfPlacement set to " + x);
                });

            breakSelected = new DebugCommand("breakSelected", "removes the selected object", "breakSelected", () =>
            {
                BreakSelected.BreakSelectedBlocks();
            });
            
            paintSelected = new DebugCommand<string>("paintSelected", "repaints the selected object", "paintSelected", (x) =>
            {
                PaintSelected.RepaintSelectedBlocks(x);
            });
            
            moveSelectedX = new DebugCommand<int>("moveSelectedX", "moves the selected object towards x", "moveSelectedX", (x) =>
            {
                MoveSelected.MoveTowardsX(x);
            });
            
            moveSelectedY = new DebugCommand<int>("moveSelectedY", "moves the selected object towards y", "moveSelectedY", (x) =>
            {
                MoveSelected.MoveTowardsY(x);
            });
            
            moveSelectedZ = new DebugCommand<int>("moveSelectedZ", "moves the selected object towards z", "moveSelectedZ", (x) =>
            {
                MoveSelected.MoveTowardsZ(x);
            });
            
            rotateAroundX = new DebugCommand<int>("rotateAroundX", "rotates the selected object around x", "rotateAroundX", (x) =>
            {
                RotateSelected.RotateAroundX(x);
            });
            
            rotateAroundY = new DebugCommand<int>("rotateAroundY", "rotates the selected object around y", "rotateAroundY", (x) =>
            {
                RotateSelected.RotateAroundY(x);
            });
            
            rotateAroundZ = new DebugCommand<int>("rotateAroundZ", "rotates the selected object around z", "rotateAroundZ", (x) =>
            {
                RotateSelected.RotateAroundZ(x);
            });
            
            startOptimize = new DebugCommand("startOptimize", "removes the selected object", "startOptimize", () =>
            {
                Optimize.StartOptimization();
            });
            
            revertOptimize = new DebugCommand("revertOptimize", "removes the selected object", "revertOptimize", () =>
            {
                Optimize.RevertOptimization();
            });
            
            commandList = new List<object>
            {
                HELP,
                togglePlacement,
                toggleBreaking,
                toggleSelection,
                toggleColoring,
                COLOR,
                SCOLOR,
                PGPC,
                PPC,
                PPCB,
                PGP,
                PP,
                halfPlacement,
                breakSelected,
                paintSelected,
                moveSelectedX,
                moveSelectedY,
                moveSelectedZ,
                rotateAroundX,
                rotateAroundY,
                rotateAroundZ,
                startOptimize,
                revertOptimize
            };
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                debugPanel = !debugPanel;
            }
        }

        private void OnGUI()
        {
            float y = 0f;
            if (debugPanel)
            {
                if (showHelp)
                {
                    GUI.Box(new Rect(0, y, Screen.width, 100), "");
                    float viewportHeight = Mathf.Max(20 * commandList.Count, 90);
                    Rect viewport = new Rect(0, 0, Screen.width - 30, viewportHeight);
                    scroll = GUI.BeginScrollView(new Rect(0, y + 5, Screen.width, 90), scroll, viewport);
                    for (int i = 0; i < commandList.Count; i++)
                    {
                        DebugCommandBase command = commandList[i] as DebugCommandBase;
                        if (command != null)
                        {
                            string label = $"{command.commandFormat} - {command.commandDescription}";
                            Rect labelRect = new Rect(5, 20 * i, viewport.width - 20, 20);
                            GUI.Label(labelRect, label);
                        }
                    }

                    GUI.EndScrollView();
                    y += 100;
                }

                GUI.Box(new Rect(0f, y, Screen.width, 30f), "");
                GUI.backgroundColor = new Color(0, 0, 0, 0);
                input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
                Event e = Event.current;
                if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Return)
                {
                    HandleInput();
                    input = "";
                }
            }
        }

        private void HandleInput()
        {
            string[] properties = input.Split(' ');
            if (properties.Length == 0) return;

            string commandToken = properties[0];

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
                if (commandToken.Equals(commandBase.commandId, StringComparison.OrdinalIgnoreCase))
                {
                    switch (commandList[i])
                    {
                        case DebugCommand debugCommand:
                            debugCommand.Invoke();
                            break;
                        case DebugCommand<int> debugCommandInt:
                            debugCommandInt.Invoke(int.Parse(properties[1]));
                            break;
                        case DebugCommand<string> debugCommandString:
                            debugCommandString.Invoke(properties[1]);
                            break;
                        case DebugCommand<bool> debugCommandBool:
                            if (bool.TryParse(properties[1], out bool parsedBool))
                            {
                                debugCommandBool.Invoke(parsedBool);
                            }
                            break;
                    }
                }
            }
        }
    }
}