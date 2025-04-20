namespace UndoRedo
{
    using UnityEngine;
    public class UndoRedoInput : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z) && UndoRedoSystem.CanUndo)
                UndoRedoSystem.Undo();
            if (Input.GetKeyDown(KeyCode.Y) && UndoRedoSystem.CanRedo)
                UndoRedoSystem.Redo();
        }
    }

}