using UnityEngine;
using UnityEngine.UI;

namespace com.Kuwiku
{

    public enum FieldType
    {
        UID,
        Name,
        Sex,
        DueDate,
        BirthDay,
    }


    public class DocumentField : MonoBehaviour
    {
        public string id;
        public string value;
        public FieldType fieldType;

        private Button button;

        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => TryCompare());
            SetFieldInteraction(false);
        }

        public void TryCompare()
        {
            if (InspectManager.Instance.OnInspect)
            {
                InspectManager.Instance.SetDocumentToInspect(this);
            }
        }

        public void SetFieldInteraction(bool value)
        {
            button.interactable = value;
        }
    }
}
