namespace com.Kuwiku
{
    public interface IInputReader
    {
        enum ActionButtonState
        {
            Pressed,
            Held,
            Released
        }
        ActionButtonState GetActionButtonState();
    }
}
