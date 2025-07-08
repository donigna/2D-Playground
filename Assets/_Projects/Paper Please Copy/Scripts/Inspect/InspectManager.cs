using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace com.Kuwiku
{

    public class InspectManager : MonoBehaviour
    {
        public static InspectManager Instance;

        [SerializeField] bool _onInspenctionMode = false;

        private IField[] dataField = new IField[2];
        private RuleField _ruleValidator = null;

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
            foreach (var field in FieldRegistry.Fields)
            {
                field.SetFieldInteraction(true);
            }
        }

        public void EndInspectMode()
        {
            _onInspenctionMode = false;
            DragDropManager.Instance.canDragging = true;
            foreach (var field in FieldRegistry.Fields)
            {
                field.SetFieldInteraction(false);
            }
        }

        public void SetFieldToInspect(IField field)
        {
            if (dataField[0] == null)
            {
                dataField[0] = field;

                if (dataField[0].FIELDCATEGORY == "RULE")
                {
                    _ruleValidator = dataField[0].GetObject().GetComponent<RuleField>();
                }
            }
            else if (dataField[0] != field && dataField[1] == null)
            {
                dataField[1] = field;

                if (_ruleValidator != null)
                {
                    Validate(dataField[1].GetObject(), "Valid", "Invalid");
                    return;
                }
                else if (dataField[1].FIELDCATEGORY == "RULE")
                {
                    _ruleValidator = dataField[1].GetObject().GetComponent<RuleField>();
                    Validate(dataField[0].GetObject(), "Valid", "Invalid");
                    return;
                }
                else if (dataField[0] != null && dataField[1] != null)
                {
                    if (Compare(dataField[0], dataField[1]))
                    {
                        Debug.Log($"Data {dataField[0].GetObject()} sesuai dengan {dataField[1].GetObject()}");
                        ResetDocumentToInspect();
                    }
                    else
                    {
                        Debug.Log($"Data {dataField[0].GetObject()} tidak sesuai dengan {dataField[1].GetObject()}");
                        ResetDocumentToInspect();
                    }
                }
            }
            else if (dataField[0] == field)
            {
                Debug.Log("Field has been selected!");
            }
            else
            {
                ResetDocumentToInspect();
                Debug.Log("Document different!");
            }
        }

        public void ResetDocumentToInspect()
        {
            for (int i = 0; i < dataField.Length; i++)
            {
                if (dataField[i] != null)
                {
                    dataField[i].Highlight(false);
                    dataField[i] = null;
                }
            }
            _ruleValidator = null;
        }

        public void Validate(GameObject target, string validMessage, string invalidMessage)
        {
            if (_ruleValidator != null)
            {
                if (_ruleValidator.RuleValidity(target))
                {
                    Debug.Log(validMessage);
                    return;
                }
            }
            ResetDocumentToInspect();

            Debug.Log(invalidMessage);
        }

        public bool Compare(IField a, IField b)
        {
            // Type to cek
            DocumentField first = a.GetObject().GetComponent<DocumentField>();
            switch (first.fieldType)
            {
                case FieldType.UID:
                    break;
                case FieldType.Name:
                    if (
                        a.GetObject().TryGetComponent(out NameDocumentField nameA)
                        &&
                        b.GetObject().TryGetComponent(out NameDocumentField nameB))
                    {
                        if (nameA.firstName == nameB.lastName && nameB.firstName == nameB.lastName)
                        {
                            return true;
                        }
                    }

                    // Jika tidak maka tipe data tidak sesuai sehingga tidak bisa dicompare
                    break;
                case FieldType.Gender:

                    break;
                default:
                    return false;
            }
            return false;
        }
    }
}
