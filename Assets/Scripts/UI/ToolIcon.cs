using UnityEngine;

namespace UI
{
    [System.Serializable]
    public class ToolIcon
    {
        public string toolName;
        public string iconPath;

        private Sprite _cachedIcon;

        public Sprite Icon
        {
            get
            {
                if (_cachedIcon == null && !string.IsNullOrEmpty(iconPath))
                {
                    _cachedIcon = Resources.Load<Sprite>(iconPath);
                }

                return _cachedIcon;
            }
        }
    }
}