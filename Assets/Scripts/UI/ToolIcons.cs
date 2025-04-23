using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "ToolIcons", menuName = "UI/ToolIcons")]
    public class ToolIcons : ScriptableObject
    {
        public List<ToolIcon> toolIcons;

        public Sprite GetIcon(string name)
        {
            ToolIcon iconEntry = toolIcons.Find(icon => icon.toolName == name);
            return iconEntry?.Icon;
        }
    }
}