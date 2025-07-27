using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace com.Kuwiku
{
    public class ShelfManager : MonoBehaviour
    {
        public static ShelfManager Instance;

        public bool lockShelf;
        public bool opened;
        public Button openBtn;
        public Button closeBtn;
        public RectTransform container;

        [SerializeField] private List<ShelfItem> shelfItems;
        [SerializeField] private ShlefItemDraggable shelfItemDraggableTemp;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            openBtn.onClick.AddListener(OpenShelf);
            openBtn.gameObject.SetActive(false);
            closeBtn.onClick.AddListener(CloseShelf);
        }

        public void ListenCustomer(Customer customer)
        {
            Debug.Log(customer.name);
            customer.OnGivingOrder += () => LockShelf(false);
        }

        public void UnListenCustomer(Customer customer)
        {
            customer.OnGivingOrder -= () => LockShelf(true);
        }

        public void LockShelf(bool val)
        {
            if (val)
            {
                lockShelf = true;
                openBtn.gameObject.SetActive(false);
                return;
            }
            lockShelf = false;
            openBtn.gameObject.SetActive(true);
        }

        public void OpenShelf()
        {
            // Open Shelf when open button clicked   
            container.DOAnchorPosY(0, 0.2f).SetEase(Ease.OutQuad).SetAutoKill(true);
            opened = true;
        }

        public void CloseShelf()
        {
            // Close Shelf when close button clicked
            container.DOAnchorPosY(-1080, 0.2f).SetEase(Ease.OutQuad).SetAutoKill(true);
            opened = false;
        }

        public void DragItem(ShelfItemSO item)
        {
            DragDropManager.Instance.SetDraggedObject(shelfItemDraggableTemp);
            shelfItemDraggableTemp.Set(item);
            shelfItemDraggableTemp.gameObject.SetActive(true);
        }

        public void DropItem(Vector2 position, ShelfItemSO item)
        {
            shelfItemDraggableTemp.gameObject.SetActive(false);
            if (Sendbox.Instance.InsideSendbox2D(position))
            {
                // Buat Custome mendapatkan itemnya
                if (TableManager.Instance.TryGiveOrderedItemToCustomer(item))
                {
                    // Log Customer Get The Item
                }
                else
                {
                    // Log The Item is Wrong Item
                }
            }
        }

        // Ketika Customer Sudah mengirimkan orderan,
        // Aktifkan Button untuk membuka Shelf
        // Buka Shelf ketika button di klik
        // Shelf berisi item - item yang dijual oleh pemain
        // Pemain menggeser Item ke Send Box
        // Ketika item berhasil di kirimkan ke sendbox, Shelf akan Tertutup
        // Kotak Berisi Item akan berada di meja
        // Taruh kotak bersama berkas - berkas untuk mengembalikan item
        // Klik Close Button untuk menutup Shelf

    }
}
