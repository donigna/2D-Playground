using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{
    public class Customer : Entity
    {
        [SerializeField] private List<Document> _documents;

        private List<Document> _documentGived;

        public void OnPlace()
        {
            Debug.Log("Customer is on Place");
            GiveDocument();
        }

        #region Order Handler
        public void GiveOrder()
        {
            Debug.Log("I want ordering ....");
        }
        #endregion

        #region Document Handler
        public void SetDocument(Document[] newDocuments)
        {
            foreach (Document document in newDocuments)
            {
                _documents.Add(document);
            }
        }

        public void AddDocument(Document newDocument)
        {
            _documents.Add(newDocument);
        }

        public void ResetDocument()
        {
            _documents.Clear();
        }

        public List<Document> GetListDocuments()
        {
            return _documents;
        }

        public void RequestDocument(Document document)
        {
            if (_documentGived.Contains(document))
            {
                Debug.Log("Document Sudah diberikan");
                return;
            }

            if (!_documents.Contains(document))
            {
                Debug.Log("Document tidak ada");
                // Do something when document is null

            }
            else
            {
                Debug.Log("Document lupa untuk diberikan!");
                // Give the document and say something

            }
        }

        public void GiveDocument()
        {
            // Random Chance to give document 
            Debug.Log(
                "Giving Document"
            );
            FindAnyObjectByType<TableManager>().SetCustomer(this);
        }
        #endregion
    }
}
