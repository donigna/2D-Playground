using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.Kuwiku
{
    public enum RuleType
    {
        Age,
        City,
        Gender
    }

    public class RuleField : MonoBehaviour, IField
    {
        // Rule Field berisi id rule,
        // nantinya akan digunakan untuk melakukan pengecekan pada data document dengan rule yang ada

        protected RuleType ruleType;
        [SerializeField] private TextMeshProUGUI textUI;
        [SerializeField] private Button button;
        [SerializeField] private GameObject highlight;

        private string fieldCategory = "RULE";

        public string FIELDCATEGORY { get => fieldCategory; }

        public GameObject GetObject() => gameObject;

        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => TryCompare());
            SetFieldInteraction(false);

            FieldRegistry.Fields.Add(this);
        }

        void Start()
        {
            Highlight(false);
        }

        void OnDestroy()
        {
            FieldRegistry.Fields.Remove(this);
        }


        // Aturan dari setiap rule tulis dibawah sini
        // ...
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

        public virtual bool RuleValidity(GameObject obj)
        {
            return false;
        }

        public virtual void ShowHighlight()
        {
            highlight.SetActive(true);
        }

        public virtual void HideHighlight()
        {
            highlight.SetActive(false);
        }
    }
}
