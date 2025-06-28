using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{
    public class TableManager : MonoBehaviour
    {
        [SerializeField] private Transform _documentSpawnPoint;

        [Header("Document Prefabs")]
        [SerializeField] private Document prefabIDCard;

        private List<GameObject> _objectsOnTable;

        private Customer _currentCustomer;

        void Awake()
        {
            if (_objectsOnTable == null) _objectsOnTable = new List<GameObject>();
        }

        public void SetCustomer(Customer customer)
        {
            _currentCustomer = customer;

            if (_currentCustomer != null)
            {
                RequestDocumentFromCustomer(_currentCustomer);
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

            Document docPrefab = null;

            switch (doc.GetDocumentType())
            {
                case DocumentType.IDCard:
                    docPrefab = prefabIDCard;
                    break;
            }

            Vector3 spawnPoint = new Vector3(_documentSpawnPoint.position.x, _documentSpawnPoint.position.y, _documentSpawnPoint.position.z - _objectsOnTable.Count);

            Document spawnedDocument = Instantiate(docPrefab, spawnPoint, Quaternion.identity);
            _objectsOnTable.Add(spawnedDocument.gameObject);
            // Set Data to Document


        }
        #endregion

    }
}
