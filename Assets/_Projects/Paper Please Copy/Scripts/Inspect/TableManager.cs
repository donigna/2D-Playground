using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace com.Kuwiku
{
    public class TableManager : MonoBehaviour
    {
        public static TableManager Instance;

        [SerializeField] private Transform _documentSpawnPoint;
        [SerializeField] private Transform _desk;
        [SerializeField] private LetterBox _letterBox;

        [Header("Document Prefabs")]
        [SerializeField] private Document prefabIDCard;

        private List<Document> _objectsOnTable;

        private Customer _currentCustomer;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            if (_objectsOnTable == null) _objectsOnTable = new List<Document>();
        }

        #region Customer Table
        public void SetCustomer(Customer customer)
        {
            _currentCustomer = customer;


            if (_currentCustomer != null)
            {
                ShelfManager.Instance.ListenCustomer(_currentCustomer);
                RequestDocumentFromCustomer(_currentCustomer);
            }
        }

        public bool TryGiveOrderedItemToCustomer(ShelfItemSO shelfItem)
        {
            if (_currentCustomer.GetOrder() == shelfItem)
            {
                _currentCustomer.CompleteOrder(true);
                StartCoroutine(SendCustomerExit());
                return true;
            }
            return false;
        }

        IEnumerator SendCustomerExit()
        {
            // Shelf
            ShelfManager shelf = ShelfManager.Instance;
            shelf.LockShelf(true);
            if (shelf.opened)
            {
                shelf.CloseShelf();
            }
            yield return new WaitForSeconds(0.2f);

            // Document
            SendbackAllDocuments();

            yield return new WaitForSeconds(0.2f);

            // Customer
            _currentCustomer.Exit();
        }

        #endregion

        public void StoreObject(Transform obj)
        {
            Vector3 storagePos = new Vector3(_documentSpawnPoint.position.x, _documentSpawnPoint.position.y, obj.position.z);
            if (Vector3.Distance(obj.position, storagePos) > 0.001f)
            {
                DOTween.Kill(obj.transform);
                obj.transform.DOMove(storagePos, 0.05f).SetEase(Ease.Flash);
            }
        }

        #region Document Table Handler

        private void RequestDocumentFromCustomer(Customer customer)
        {
            List<Document> documents = customer.GetListDocuments();

            foreach (var doc in documents)
            {
                SpawnDocument(doc);
            }
        }

        private void SpawnDocument(Document doc)
        {
            if (_documentSpawnPoint == null)
            {
                Debug.Log("Document Spawn Point not setted!");
                return;
            }


            // Spawn Document

            Document docPrefab = null;

            switch (doc.GetDocumentType())
            {
                case DocumentType.IDCard:
                    docPrefab = prefabIDCard;
                    break;
            }

            Vector3 spawnPoint = new Vector3(_documentSpawnPoint.position.x, _documentSpawnPoint.position.y, _documentSpawnPoint.position.z - _objectsOnTable.Count);

            Document spawnedDocument = Instantiate(docPrefab, spawnPoint, Quaternion.identity, _desk);
            spawnedDocument.owner = _currentCustomer;
            _objectsOnTable.Add(spawnedDocument);
            // Set Data to Document

            // Spawn Letter Document 
            _letterBox.SpawnDocument(spawnedDocument);

        }

        private void SendbackAllDocuments()
        {
            foreach (Document item in _objectsOnTable)
            {
                item.GetComponent<DraggableObject>()._letterObj.Sendback();
            }
            _objectsOnTable.Clear();
        }
        #endregion
    }
}
