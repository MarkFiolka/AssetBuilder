namespace UndoRedo
{
    using UnityEngine;

    public class ColorChangeCommand : ICommand
    {
        private readonly GameObject _obj;
        private readonly Color _oldCol, _newCol;

        public ColorChangeCommand(GameObject obj, Color oldCol, Color newCol)
        {
            _obj = obj;
            _oldCol = oldCol;
            _newCol = newCol;
        }

        public void Undo() => _obj.GetComponent<Renderer>().material.color = _oldCol;
        public void Redo() => _obj.GetComponent<Renderer>().material.color = _newCol;
    }
}