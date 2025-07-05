using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace com.Kuwiku
{
    public class TableManager : MonoBehaviour
    {
        public static TableManager Instance;

        [SerializeField] private Transform _documentSpawnPoint;
        [SerializeField] private LetterBox _letterBox;
        [SerializeField] private GameObject _desk;

        [Header("Document Prefabs")]
        [SerializeField] private Document prefabIDCard;

        private List<GameObject> _objectsOnTable;

        private Customer _currentCustomer;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

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

            Document spawnedDocument = Instantiate(docPrefab, spawnPoint, Quaternion.identity);
            _objectsOnTable.Add(spawnedDocument.gameObject);
            // Set Data to Document

            // Spawn Letter Document 
            _letterBox.SpawnDocument(spawnedDocument);

        }
        #endregion
    }
}
