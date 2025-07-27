using System;
using UnityEngine;

namespace com.Kuwiku
{

    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Shelf")]
    public class ShelfItemSO : ScriptableObject
    {
        public string itemName;
        public Sprite image;
    }
}