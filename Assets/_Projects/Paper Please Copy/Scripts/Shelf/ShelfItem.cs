using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.Kuwiku
{
    [RequireComponent(typeof(Button))]
    public class ShelfItem : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private ShelfItemSO shelfItemSO;

        public ShelfItemSO GetShelfItemSO()
        {
            return shelfItemSO;
        }

        public void Seleceted()
        {
            // spawn icon
            ShelfManager.Instance.DragItem(shelfItemSO);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Seleceted();
        }
    }
}
