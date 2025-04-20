namespace UndoRedo
{
    using System.Collections.Generic;
    using UnityEngine;

    [DefaultExecutionOrder(-100)]
    public class UndoRedoSystem : MonoBehaviour
    {
        private static readonly Stack<ICommand> _undoStack = new();
        private static readonly Stack<ICommand> _redoStack = new();

        void OnEnable()
        {
            ChangeTracker.PositionChanged += OnPositionChanged;
            ChangeTracker.ColorChanged += OnColorChanged;
            ChangeTracker.SelectionChanged += OnSelectionChanged;
            ChangeTracker.ObjectDeleted += OnObjectDeleted; // subscribe!
        }

        void OnDisable()
        {
            ChangeTracker.PositionChanged -= OnPositionChanged;
            ChangeTracker.ColorChanged -= OnColorChanged;
            ChangeTracker.SelectionChanged -= OnSelectionChanged;
            ChangeTracker.ObjectDeleted -= OnObjectDeleted;
        }

        private void OnPositionChanged(GameObject obj, Vector3 oldP, Vector3 newP)
        {
            _undoStack.Push(new PositionChangeCommand(obj, oldP, newP));
            _redoStack.Clear();
        }

        private void OnColorChanged(GameObject obj, Color oldC, Color newC)
        {
            _undoStack.Push(new ColorChangeCommand(obj, oldC, newC));
            _redoStack.Clear();
        }

        private void OnSelectionChanged(GameObject obj, bool isSel)
        {
            bool oldSel = !isSel;
            _undoStack.Push(new SelectionChangeCommand(obj, oldSel, isSel));
            _redoStack.Clear();
        }

        // NEW!
        private void OnObjectDeleted(GameObject obj)
        {
            _undoStack.Push(new DeleteCommand(obj));
            _redoStack.Clear();
        }

        public static bool CanUndo => _undoStack.Count > 0;
        public static bool CanRedo => _redoStack.Count > 0;

        public static void Undo()
        {
            if (!CanUndo) return;
            ChangeTracker.SuppressNotifications = true;
            var cmd = _undoStack.Pop();
            cmd.Undo();
            _redoStack.Push(cmd);
            ChangeTracker.SuppressNotifications = false;
        }

        public static void Redo()
        {
            if (!CanRedo) return;
            ChangeTracker.SuppressNotifications = true;
            var cmd = _redoStack.Pop();
            cmd.Redo();
            _undoStack.Push(cmd);
            ChangeTracker.SuppressNotifications = false;
        }
    }
}