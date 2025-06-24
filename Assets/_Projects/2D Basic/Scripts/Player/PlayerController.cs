using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    public class PlayerController : MonoBehaviour
    {
        public int playerId;
        private IInputReader _input;

        void Awake()
        {
            _input = GetComponent<IInputReader>();
        }

        void Update()
        {
            if (_input == null)
            {
                Debug.LogError("Input is not assigned or missing on PlayerController.");
                return;
            }

            // Read movement input
            Vector2 move = _input.ReadMovement();
            if (move != Vector2.zero)
            {
                Debug.Log($"Player {playerId} is moving: {move}");
            }

            // Check for jump input
            if (_input.IsJumpPerformed())
            {
                Debug.Log($"Player {playerId} jumped!");
            }
        }
    }
}
