using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{
    public class Customer : Entity
    {
        public Action OnGivingOrder;

        public string CustomerID; // Gunakan ini untuk identifikasi unik
        public CustomerData customerData;
        public bool IsUnique;
        [SerializeField] private List<Document> _documents;
        [SerializeField] private ShelfItemSO shelfItemSO;
        private Animator anim;
        private List<Document> _documentGived;

        // flags
        private bool completOrder = false;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void InitializeData(CustomerData data)
        {
            customerData = data;
        }

        public void OnPlace()
        {
            Debug.Log("Customer is on Place");
            TableManager.Instance.SetCustomer(this);
        }

        public void Exit()
        {
            Debug.Log("Customer Going to Exit");
            anim.CrossFade("Exit", 0.2f);
        }

        public void OnExit()
        {
            CustomerManager.Instance.CreateNewCustomer();
            Destroy(gameObject);
        }

        #region Order Handler
        public void GiveOrder()
        {
            Debug.Log("I want ordering ....");
            OnGivingOrder?.Invoke();
        }

        public ShelfItemSO GetOrder()
        {
            return shelfItemSO;
        }

        public void CompleteOrder(bool val)
        {
            completOrder = val;
        }

        public bool GetOrderStatus()
        {
            return completOrder;
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
        #endregion
    }

    public class BobbyTheClown : Customer
    {
        // Unique Customer
    }
}
