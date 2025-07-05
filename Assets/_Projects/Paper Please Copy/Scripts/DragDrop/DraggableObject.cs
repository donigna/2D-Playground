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
        public IDraggable _letterObj;

        private bool _isHandlingEnd;

        public void LinkLetter(LetterObject letterObject)
        {
            _letterObj = letterObject;
        }

        public void OnDragStart(Vector2 position)
        {
            _returnTween?.Kill();
            _initialPosition = transform.position;
            transform.DOMove(new Vector3(position.x, position.y, _initialPosition.z), 0.2f);

            if (DragDropManager.Instance.CurrentDraggable != _letterObj)
            {
                _letterObj.OnDragStart(position);
            }
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

                if (DragDropManager.Instance.CurrentDraggable != _letterObj)
                {
                    _letterObj.OnDrag(position);
                }
            }
        }

        public void OnDragEnd(Vector2 position)
        {
            if (_isHandlingEnd) return;
            _isHandlingEnd = true;

            if (_returnTween == null || !_returnTween.IsActive())
            {
                Debug.Log("Object placed at: " + position + " is Empty Area");
                _returnTween = transform.DOMove(position, 0.2f)
                .SetEase(Ease.OutSine)
                .SetAutoKill(true).OnKill(() => _returnTween = null);
            }

            if (DragDropManager.Instance.CurrentDraggable != _letterObj)
            {
                _letterObj.OnDragEnd(position);
            }

            _isHandlingEnd = false;
        }

        private void ResetDrag()
        {
            _returnTween = null; // Reset the return tween
            _dragTween = null;
        }
    }
}
