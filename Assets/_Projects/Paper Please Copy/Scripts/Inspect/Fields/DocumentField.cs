using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.Kuwiku
{

    public enum FieldType
    {
        UID,
        Name,
        Gender,
        DueDate,
        BirthDay,
        City,
    }


    public class DocumentField : MonoBehaviour, IField
    {
        public FieldType fieldType;
        [SerializeField] private TextMeshProUGUI textUI;
        [SerializeField] private Button button;
        [SerializeField] private GameObject highlight;

        protected Document _document;

        private string fieldCategory = "DOC";

        public string FIELDCATEGORY { get => fieldCategory; }

        public GameObject GetObject() => gameObject;

        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => TryCompare());
            SetFieldInteraction(false);

            FieldRegistry.Fields.Add(this);
        }

        void OnDestroy()
        {
            FieldRegistry.Fields.Remove(this);
        }

        void Start()
        {
            Highlight(false);
            _LocalStart();
        }

        protected virtual void _LocalStart()
        {
            DisplayText("Template Text");
        }

        public void LinkDocument(Document doc)
        {
            _document = doc;
        }

        public void TryCompare()
        {
            if (InspectManager.Instance.OnInspect)
            {
                Highlight(true);
                InspectManager.Instance.SetFieldToInspect(this);
            }
        }

        public void Highlight(bool value)
        {
            highlight.SetActive(value);
        }

        public void SetFieldInteraction(bool value)
        {
            button.interactable = value;
        }

        public void DisplayText(string value)
        {
            textUI.text = value;
        }
    }
}
