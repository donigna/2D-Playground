using UnityEngine;
using UnityEngine.UI;

namespace com.Kuwiku
{

    public class InspectManager : MonoBehaviour
    {
        public static InspectManager Instance;

        [SerializeField] bool _onInspenctionMode = false;

        private DocumentField[] dataField = new DocumentField[2];

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        public bool OnInspect => _onInspenctionMode;

        public void OnInspectButtonClick()
        {
            bool status = !_onInspenctionMode;
            if (status)
            {
                StartInpectMode();
            }
            else
            {
                EndInspectMode();
            }
            ResetDocumentToInspect();
        }

        public void StartInpectMode()
        {
            _onInspenctionMode = true;
            DragDropManager.Instance.canDragging = false;
            DocumentField[] documentFields = FindObjectsOfType<DocumentField>();
            foreach (var field in documentFields)
            {
                field.SetFieldInteraction(true);
            }
        }

        public void EndInspectMode()
        {
            _onInspenctionMode = false;
            DragDropManager.Instance.canDragging = true;
            DocumentField[] documentFields = FindObjectsOfType<DocumentField>();
            foreach (var field in documentFields)
            {
                field.SetFieldInteraction(false);
            }
        }

        public void SetDocumentToInspect(DocumentField documentField)
        {
            if (dataField[0] == null)
            {
                dataField[0] = documentField;

            }
            else if (documentField.fieldType == dataField[0].fieldType && dataField[0].gameObject != documentField.gameObject)
            {
                dataField[1] = documentField;
                if (Compare(dataField[0].id, dataField[1].id))
                {
                    Debug.Log("Comparasi Berhasil");
                    ResetDocumentToInspect();
                }
            }
            else
            {
                Debug.Log("Data tidak sesuai");
                ResetDocumentToInspect();
            }
        }

        public void ResetDocumentToInspect()
        {
            for (int i = 0; i < dataField.Length; i++)
            {
                dataField[i] = null;
            }
        }

        public bool Compare(string id1, string id2)
        {
            if (id1 == id2)
            {
                return true;
            }
            return false;
        }

    }
}
