using DG.Tweening;
using UnityEngine;

namespace com.Kuwiku
{
    public class ShlefItemDraggable : MonoBehaviour, IDraggable
    {
        [SerializeField] private SpriteRenderer _sprite;
        public Collider2D Collider { get; private set; }

        private Vector3 _initialPosition;
        private Tweener _dragTween;
        private Tweener _returnTween;
        private bool _isHandlingEnd;

        [SerializeField] private ShelfItemSO _shelfItemSO;

        public void Set(ShelfItemSO shelfItemSO)
        {
            _shelfItemSO = shelfItemSO;
            _sprite.sprite = shelfItemSO.image;
        }

        public void OnDragStart(Vector2 position)
        {
            _returnTween?.Kill();
            _initialPosition = transform.position;
            transform.DOMove(new Vector3(position.x, position.y, _initialPosition.z), 0.2f);
        }

        public void OnDrag(Vector2 position)
        {
            Vector3 targetPos = new Vector3(position.x, position.y, _initialPosition.z);

            if ((transform.position - targetPos).sqrMagnitude > 0.001f)
            {
                if (_dragTween != null || _dragTween.IsActive())
                {
                    _dragTween.ChangeEndValue(targetPos, true).SetEase(Ease.OutElastic).Restart();
                }
                else
                {
                    _dragTween = transform.DOMove(targetPos, 1f)
                        .SetEase(Ease.OutElastic).SetAutoKill(true).OnKill(() => _dragTween = null);
                }


            }
        }

        public void OnDragEnd(Vector2 position)
        {
            _dragTween?.Kill();
            if (_isHandlingEnd) return;
            _isHandlingEnd = true;

            if (_returnTween == null || !_returnTween.IsActive())
            {
                Debug.Log("Object placed at: " + position + " is Empty Area");
                _returnTween = transform.DOMove(position, 0.2f)
                .SetEase(Ease.OutSine)
                .SetAutoKill(true).OnKill(() => _returnTween = null);
            }

            // ketika posisi akhir merupakan posisi didalam sendbox,
            // maka trigger send di sendbox
            ShelfManager.Instance.DropItem(position, _shelfItemSO);

            _isHandlingEnd = false;
        }

        public void ForceMovoPosition(Vector3 position)
        {
            _dragTween?.Kill();
            _returnTween?.Kill();

            if (_returnTween == null || !_returnTween.IsActive())
            {
                _returnTween = transform.DOMove(position, 0.2f)
                .SetEase(Ease.OutSine)
                .SetAutoKill(true).OnKill(() => _returnTween = null);
            }
        }

        private void ResetDrag()
        {
            _returnTween = null; // Reset the return tween
            _dragTween = null;
        }
    }
}
