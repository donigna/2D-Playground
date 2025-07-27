using UnityEngine;

namespace com.Kuwiku
{
    public class Sendbox : MonoBehaviour
    {
        public static Sendbox Instance;
        private RectTransform rect;


        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            rect = GetComponent<RectTransform>();
        }

        public bool InsideSendbox2D(Vector2 position)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(rect, position, null);

        }

    }
}
