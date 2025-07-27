using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{
    public static class FieldRegistry
    {
        public static readonly List<IField> Fields = new List<IField>();
    }

    public interface IField
    {
        public string FIELDCATEGORY { get; }

        public GameObject GetObject();

        public void TryCompare();

        public void SetFieldInteraction(bool value);

        public void DisplayText(string value);

        public void Highlight(bool value);
    }
}