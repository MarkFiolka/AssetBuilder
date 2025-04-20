namespace UndoRedo
{
    using System;
    using UnityEngine;

    public static class ChangeTracker
    {
        public static bool SuppressNotifications { get; set; }

        public static event Action<GameObject, Vector3, Vector3> PositionChanged;
        public static event Action<GameObject, Color, Color> ColorChanged;
        public static event Action<GameObject, bool> SelectionChanged;
        public static event Action<GameObject>                  ObjectDeleted;

        public static void NotifyPositionChanged(GameObject obj, Vector3 oldPos, Vector3 newPos)
        {
            if (!SuppressNotifications)
                PositionChanged?.Invoke(obj, oldPos, newPos);
        }

        public static void NotifyColorChanged(GameObject obj, Color oldC, Color newC)
        {
            if (!SuppressNotifications)
                ColorChanged?.Invoke(obj, oldC, newC);
        }

        public static void NotifySelectionChanged(GameObject obj, bool isSelected)
        {
            if (!SuppressNotifications)
                SelectionChanged?.Invoke(obj, isSelected);
        }
        
        public static void NotifyObjectDeleted(GameObject obj)
        {
            if (!SuppressNotifications) ObjectDeleted?.Invoke(obj);
        }
    }
}