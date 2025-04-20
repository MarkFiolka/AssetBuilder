namespace UndoRedo
{
    public interface ICommand
    {
        void Undo();

        void Redo();
    }
}