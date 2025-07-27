using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{
    public class Document : MonoBehaviour
    {
        public Customer owner;
        public IDMachine IDMachine;

        [SerializeField] private List<DocumentField> _documentFields;
        [SerializeField] private DocumentType _documentType;

        public DocumentType GetDocumentType() => _documentType;

        void Awake()
        {
            RegisterDocumentFields();
        }

        public void RegisterDocumentFields()
        {
            if (_documentFields.Count > 0)
            {
                // register all _document fields
                foreach (var field in _documentFields)
                {
                    field.LinkDocument(this);
                }
            }
        }

        // Set Document
        public void SetDocumentFields(DocumentField[] newDocumentFields)
        {
            foreach (var field in newDocumentFields)
            {
                _documentFields.Add(field);
            }
        }

        public void AddDocumentField(DocumentField newField)
        {
            _documentFields.Add(newField);
        }

        public void ResetField()
        {
            _documentFields.Clear();
        }

        // Get FieldsList
        public List<DocumentField> GetListFields()
        {
            return _documentFields;
        }
    }
}
