using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    public interface IInputReader
    {
        /// <summary>
        /// Reads the movement input as a Vector2.
        /// </summary>
        /// <returns>Vector2 representing the movement direction.</returns>
        Vector2 ReadMovement();

        /// <summary>
        /// Checks if the jump action has been performed.
        /// </summary>
        /// <returns>True if jump action is performed, otherwise false.</returns>
        bool IsJumpPerformed();

        enum ActionButtonState
        {
            Pressed,
            Held,
            Released
        }
        ActionButtonState GetActionButtonState();
    }
}
