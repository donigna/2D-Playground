using DG.Tweening;
using UnityEngine;

namespace com.Kuwiku
{
    public class DraggableObject : MonoBehaviour, IDraggable
    {
        private Vector3 _initialPosition;
        private Tweener _dragTween;
        private Tweener _returnTween;
        public Collider2D Collider { get; private set; }

        public void OnDragStart(Vector2 position)
        {
            _returnTween?.Kill();
            _initialPosition = transform.position;
            transform.DOMove(new Vector3(position.x, position.y, _initialPosition.z), 0.2f);
        }

        public void OnDrag(Vector2 position)
        {
            if (_dragTween == null && !_dragTween.IsActive())
            {
                _dragTween = transform.DOMove(new Vector3(position.x, position.y, _initialPosition.z), 1f)
                    .SetEase(Ease.OutElastic);
            }
            else
            {
                _dragTween.ChangeEndValue(new Vector3(position.x, position.y, _initialPosition.z), true).SetEase(Ease.OutElastic).Restart();
            }
        }

        public void OnDragEnd(Vector2 position)
        {
            _dragTween?.Kill();
            if (_returnTween == null && !_returnTween.IsActive())
            {
                Debug.Log("Object placed at: " + position + " is Empty Area");
                _returnTween = transform.DOMove(position, 0.2f)
                .SetEase(Ease.OutSine)
                .OnComplete(() =>
                 {
                     ResetDrag();// Reset the drag tween
                 });
            }
        }

        private void ResetDrag()
        {
            _returnTween = null; // Reset the return tween
            _dragTween = null;
        }
    }
}
