using UnityEngine;

namespace com.Kuwiku
{
    public class LetterBox : MonoBehaviour
    {
        public static LetterBox Instance;

        [Header("Document In LeterBox")]
        public LetterObject rulebookPrefab;
        public LetterObject idLetterCardPrefab;

        private Point point;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            point.left = transform.position.x - transform.localScale.x / 2;
            point.right = transform.position.x + transform.localScale.x / 2;
            point.top = transform.position.y + transform.localScale.y / 2;
            point.bottom = transform.position.y - transform.localScale.y / 2;

        }

        void Start()
        {
            idLetterCardPrefab.gameObject.SetActive(false);
            rulebookPrefab.gameObject.SetActive(true);
            // Set Point 
        }

        public void SpawnDocument(Document doc)
        {
            LetterObject letterObject = null;
            switch (doc.GetDocumentType())
            {
                case DocumentType.IDCard:
                    letterObject = idLetterCardPrefab;
                    break;
            }

            letterObject.gameObject.SetActive(true);
            letterObject.LinkDocument(doc);
            // Animate
        }

        public bool InsideLetterBox2D(Vector2 position)
        {
            float minX = transform.position.x - transform.localScale.x / 2;
            float maxX = transform.position.x + transform.localScale.x / 2;
            float minY = transform.position.y - transform.localScale.y / 2;
            float maxY = transform.position.y + transform.localScale.y / 2;

            if (
                position.x < minX || position.x > maxX || position.y < minY || position.y > maxY
            )
            {
                return false;
            }
            return true;
        }

        public Point GetPoint()
        {
            return point;
        }
    }
}
