using UnityEngine;

namespace com.Kuwiku
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        public int playerId;
        private IInputReader _input;

        void Awake()
        {
            // Instance 
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            // Component
            _input = GetComponent<IInputReader>();
        }

        void Update()
        {
            // Make player position equal to mouse position
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public Vector3 GetPlayerPosition()
        {
            Vector3 pos = transform.position;
            pos.z = 0;
            return pos;
        }

    }
}
