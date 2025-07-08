using UnityEngine;

namespace com.Kuwiku
{
    public class IDMachine : MonoBehaviour
    {
        [SerializeField] private Document idDoc;

        public void VerifyPosition()
        {
            if (idDoc != null)
            {
                float distanceToDoc = Vector2.Distance(idDoc.transform.position, transform.position);
                if (distanceToDoc > 3f)
                {
                    DetachID();
                }
                else
                {
                    MoveToPivot(idDoc.gameObject);
                }
            }
        }

        // Ketika ada document id masuk,
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ID") && idDoc == null)
            {
                if (collision.TryGetComponent(out Document doc))
                {
                    if (doc.GetDocumentType() == DocumentType.IDCard)
                    {
                        idDoc = doc;
                        doc.IDMachine = this;
                        // Pindahkan posisi posisi document id ke pivot dari ini
                        MoveToPivot(collision.gameObject);
                        // Customer melakukan order
                        doc.owner.GiveOrder();
                    }
                }
            }
        }

        private void MoveToPivot(GameObject obj)
        {
            DraggableObject draggable = obj.GetComponent<DraggableObject>();
            DragDropManager.Instance.canDragging = false;
            DragDropManager.Instance.EndDrag(DragDropManager.Instance.CurrentDraggable);
            draggable._letterObj.ForceMovoPosition(transform.position);
            DragDropManager.Instance.canDragging = true;
        }

        public void DetachID()
        {
            idDoc.IDMachine = null;
            idDoc = null;
        }

        // Buka Storage
    }
}
