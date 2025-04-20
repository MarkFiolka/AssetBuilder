using Utility;

namespace UndoRedo
{
    using UnityEngine;

    public class SelectionChangeCommand : ICommand
    {
        private readonly GameObject _obj;
        private readonly bool _oldState, _newState;

        public SelectionChangeCommand(GameObject obj, bool oldState, bool newState)
        {
            _obj = obj;
            _oldState = oldState;
            _newState = newState;
        }

        public void Undo() => Apply(_oldState);
        public void Redo() => Apply(_newState);

        private void Apply(bool select)
        {
            var rend = _obj.GetComponent<Renderer>();
            if (select)
            {
                if (!ObjectRepository.selectedCubes.Contains(_obj))
                {
                    ObjectRepository.originalCubeColors[_obj] = rend.material.color;
                    rend.material.color = Settings.Instance.selectColor;
                    ObjectRepository.selectedCubes.Add(_obj);
                }
            }
            else
            {
                if (ObjectRepository.selectedCubes.Contains(_obj))
                {
                    rend.material.color = ObjectRepository.originalCubeColors[_obj];
                    ObjectRepository.originalCubeColors.Remove(_obj);
                    ObjectRepository.selectedCubes.Remove(_obj);
                }
            }
        }
    }
}