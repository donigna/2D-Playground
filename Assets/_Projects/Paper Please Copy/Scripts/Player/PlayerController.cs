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
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Hitung batas kamera
            float camHeight = Camera.main.orthographicSize * 2f;
            float camWidth = camHeight * Camera.main.aspect;

            Vector3 camPos = Camera.main.transform.position;

            float minX = camPos.x - camWidth / 2f;
            float maxX = camPos.x + camWidth / 2f;
            float minY = camPos.y - camHeight / 2f;
            float maxY = camPos.y + camHeight / 2f;

            // Clamp posisi agar tidak keluar kamera
            worldPos.x = Mathf.Clamp(worldPos.x, minX, maxX);
            worldPos.y = Mathf.Clamp(worldPos.y, minY, maxY);
            worldPos.z = 0f; // Pastikan tetap di z = 0 (2D)

            // Pindahkan player ke posisi tersebut
            transform.position = worldPos;
        }

        public Vector3 GetPlayerPosition()
        {
            Vector3 pos = transform.position;
            pos.z = 0;
            return pos;
        }

    }
}
