using System;
using UnityEngine;

namespace com.Kuwiku
{
    [Serializable]
    public class DocumentSO : ScriptableObject
    {
        public string documentID;
        public DocumentType documentType;
        public Document prefab;
    }
}
