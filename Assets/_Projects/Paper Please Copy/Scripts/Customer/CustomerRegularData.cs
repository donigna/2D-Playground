using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{
    [CreateAssetMenu(fileName = "CustomerRegularData", menuName = "Scriptable Object/List Regular Customer")]
    public class CustomerRegularData : ScriptableObject
    {
        public List<string> possibleNames;
        public List<Sprite> possibleFaces;
        public List<Country> possibleCountries;
        public List<string> possibleDialogues;
    }
}
