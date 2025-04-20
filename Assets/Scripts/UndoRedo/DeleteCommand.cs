namespace UndoRedo
{
    using UnityEngine;

    public class DeleteCommand : ICommand
    {
        private readonly GameObject _backup;
        private bool                 _isRestored;

        public DeleteCommand(GameObject obj)
        {
            _backup = Object.Instantiate(obj);
            _backup.name = obj.name;           
            _backup.SetActive(false);         
        }

        public void Undo()
        {
            _backup.SetActive(true);
            _isRestored = true;
        }

        public void Redo()
        {
            if (_isRestored)
            {
                _backup.SetActive(false);
                _isRestored = false;
            }
        }
    }

}