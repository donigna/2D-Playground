using DG.Tweening;
using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    [RequireComponent(typeof(ItemShape))]
    public class DraggableObject : MonoBehaviour, IDraggable
    {
        private Vector3 _initialPosition;
        private Tweener _dragTween;
        private Tweener _returnTween;
        private bool _attached = false;
        private Vector2Int _attachedCell;

        [SerializeField]
        private ItemShape _itemShape;
        [SerializeField]
        private DropArea _dropArea;


        private void Awake()
        {
            if (_itemShape == null) _itemShape = GetComponent<ItemShape>();
            if (_dropArea == null) _dropArea = FindAnyObjectByType<DropArea>();
        }

        public bool PointInsideObject(Vector3 point)
        {
            return _itemShape.IsPointInsadeShape(point);
        }

        public void OnDragStart(Vector2 position)
        {
            _returnTween?.Kill();
            _initialPosition = transform.position;
            if (_attached) _dropArea.ClearCellByShape(_itemShape, _attachedCell);
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
                if (_dropArea)
                {
                    _attachedCell = _dropArea.GetNearestCellPosition(position);
                    if (TryPlaceObjectInCell(_attachedCell))
                    {

                        // Logic to place the object at the position
                        // For example, snap to a grid or set a specific position
                        Vector3 cellWorldPosition = _dropArea.GetCellWorldPosition(_attachedCell);
                        Debug.Log("Object placed at: " + position + " is Empty Area");
                        _returnTween = transform.DOMove(cellWorldPosition, 0.2f)
                        .SetEase(Ease.OutSine)
                        .OnComplete(() =>
                         {
                             ResetDrag();// Reset the drag tween
                         });

                    }
                    else
                    {
                        _returnTween = transform.DOMove(_initialPosition, 0.2f)
                        .SetEase(Ease.OutSine)
                        .OnComplete(() =>
                         {
                             ResetDrag();// Reset the drag tween
                         });
                    }
                }
            }
        }

        private void ResetDrag()
        {
            _returnTween = null; // Reset the return tween
            _dragTween = null;
        }

        private bool TryPlaceObjectInCell(Vector2Int cellPosition)
        {
            // Implement logic to check if the object can be placed at the given position
            // For example, check against a grid or a drop area
            if (_itemShape == null) return false;
            if (!_dropArea.CanPlaceItem(_itemShape, cellPosition))
            {
                return false;
            }
            _attached = true;
            return true;
        }
    }
}
