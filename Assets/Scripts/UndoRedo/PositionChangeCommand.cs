namespace UndoRedo
{
    using UnityEngine;

    public class PositionChangeCommand : ICommand
    {
        private readonly GameObject _obj;
        private readonly Vector3 _oldPos, _newPos;

        public PositionChangeCommand(GameObject obj, Vector3 oldPos, Vector3 newPos)
        {
            _obj = obj;
            _oldPos = oldPos;
            _newPos = newPos;
        }

        public void Undo() => _obj.transform.position = _oldPos;
        public void Redo() => _obj.transform.position = _newPos;
    }
}