using DG.Tweening;
using UnityEngine;

namespace com.Kuwiku
{
    public struct Point
    {
        public float left;
        public float top;
        public float right;
        public float bottom;
    }

    public class LetterObject : MonoBehaviour, IDraggable
    {
        public DraggableObject _linkedDrag;
        public Document _linkedDoc;

        private Vector3 _initialPosition;
        private Tweener _dragTween;
        private Tweener _returnTween;
        private Material _instanceMaterial;

        private bool _showLinkedDoc;
        private bool _objStored;
        private bool _isInternalUpdate = false;
        private bool _isHandlingEnd = false;


        private Point point;

        public Collider2D Collider { get; private set; }

        void Awake()
        {
            point = new Point();
        }

        void Start()
        {
            _initialPosition = transform.position;
            _instanceMaterial = new Material(GetComponent<SpriteRenderer>().material);
            GetComponent<SpriteRenderer>().material = _instanceMaterial;
            UpdatePoint();
        }

        public void LinkDocument(Document doc)
        {
            _linkedDrag = doc.GetComponent<DraggableObject>();
            _linkedDrag.LinkLetter(this);
        }

        public void OnDragStart(Vector2 position)
        {
            UpdatePoint();
            _returnTween?.Kill();
            transform.DOMove(new Vector3(position.x, position.y, _initialPosition.z), 0.2f);
        }

        public void OnDrag(Vector2 position)
        {
            UpdatePoint();
            // if position outside of letterBox
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
                if (_instanceMaterial.GetFloat("_flowX") > 1 || _instanceMaterial.GetFloat("_flowX") < -1 || _instanceMaterial.GetFloat("_flowY") > 1 || _instanceMaterial.GetFloat("_flowY") < -1)
                {
                    _showLinkedDoc = true;
                    _objStored = false;
                }
                else
                {
                    _showLinkedDoc = false;
                }

                if (_showLinkedDoc)
                {
                    if (_isInternalUpdate) return;

                    _isInternalUpdate = true;
                    _linkedDrag.OnDrag(position);
                    _isInternalUpdate = false;
                }
                else
                {
                    if (_objStored) return;
                    TableManager.Instance.StoreObject(_linkedDrag.transform);
                    _objStored = true;
                }
            }
        }

        public void OnDragEnd(Vector2 position)
        {
            if (_isHandlingEnd) return;
            _isHandlingEnd = true;
            UpdatePoint();

            if (LetterBox.Instance.InsideLetterBox2D(position))
            {
                position = _initialPosition;
            }

            if (_returnTween == null || !_returnTween.IsActive())
            {
                Debug.Log("Object placed at: " + position + " is Empty Area");
                _returnTween = transform.DOMove(position, 0.2f)
                .SetEase(Ease.OutSine).OnKill(() => _returnTween = null);
            }

            if (_showLinkedDoc)
            {
                _linkedDrag.OnDragEnd(position);
            }

            _isHandlingEnd = false;
        }

        private void ResetDrag()
        {
            _returnTween = null; // Reset the return tween
            _dragTween = null;
        }

        private void UpdatePoint()
        {
            point.left = transform.position.x - transform.lossyScale.x / 2;
            point.right = transform.position.x + transform.lossyScale.x / 2;
            point.top = transform.position.y + transform.lossyScale.y / 2;
            point.bottom = transform.position.y - transform.lossyScale.y / 2;

            CheckDistancePointToBox();
        }

        private void CheckDistancePointToBox()
        {
            // Cek apakah point left, right, top, bottm apakah melebihi batas dari letter box atau tidak

            // Check Horizontal Clipping
            if (point.left < LetterBox.Instance.GetPoint().left)
            {
                // Kurangi flow x dari object
                float distance = LetterBox.Instance.GetPoint().left - point.left;
                float overlapArea = distance / transform.lossyScale.x * -1;
                _instanceMaterial.SetFloat("_flowX", overlapArea);
            }
            else if (point.right > LetterBox.Instance.GetPoint().right)
            {
                float distance = point.right - LetterBox.Instance.GetPoint().right;
                float overlapArea = distance / transform.lossyScale.x;
                _instanceMaterial.SetFloat("_flowX", overlapArea);
            }
            else
            {
                _instanceMaterial.SetFloat("_flowX", 0);
            }

            // Check Vertical Clipping
            if (point.top > LetterBox.Instance.GetPoint().top)
            {
                float distance = point.top - LetterBox.Instance.GetPoint().top;
                float overlapArea = distance / transform.lossyScale.y;
                _instanceMaterial.SetFloat("_flowY", overlapArea);
            }
            else if (point.bottom < LetterBox.Instance.GetPoint().bottom)
            {
                float distance = LetterBox.Instance.GetPoint().bottom - point.bottom;
                float overlapArea = distance / transform.lossyScale.y * -1;
                _instanceMaterial.SetFloat("_flowY", overlapArea);
            }
            else
            {
                _instanceMaterial.SetFloat("_flowY", 0);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(point.left, transform.position.y, 0), 0.05f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(point.right, transform.position.y, 0), 0.05f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(transform.position.x, point.top, 0), 0.05f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector3(transform.position.x, point.bottom, 0), 0.05f);
        }
    }
}
