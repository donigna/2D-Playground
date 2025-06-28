using UnityEngine;

namespace com.Kuwiku
{
    public class DragDropManager : MonoBehaviour
    {
        public static DragDropManager Instance { get; private set; }
        public IDraggable CurrentDraggable { get; private set; }

        public bool canDragging = true;
        public LayerMask draggableLayer;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        void Update()
        {
            if (canDragging)
            {
                switch (InputManager.Instance.GetInputReader(0).GetActionButtonState())
                {
                    case IInputReader.ActionButtonState.Pressed:
                        TryBeginDrag();
                        break;

                    case IInputReader.ActionButtonState.Held:
                        if (CurrentDraggable != null)
                        {
                            CurrentDraggable.OnDrag(PlayerController.Instance.GetPlayerPosition());
                        }
                        break;

                    case IInputReader.ActionButtonState.Released:
                        if (CurrentDraggable != null)
                        {
                            CurrentDraggable.OnDragEnd(PlayerController.Instance.GetPlayerPosition());
                            EndDrag(CurrentDraggable);
                        }
                        break;
                }
            }
        }

        public bool TryBeginDrag()
        {
            if (CurrentDraggable != null) return false;
            Vector3 targetPos = PlayerController.Instance.GetPlayerPosition();
            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero, draggableLayer);

            if (hit.collider != null)
            {
                IDraggable draggable = hit.collider.GetComponent<IDraggable>();
                if (draggable != null)
                {
                    CurrentDraggable = draggable;
                    draggable.OnDragStart(targetPos);
                    return true;
                }
            }
            return false;
        }

        public void EndDrag(IDraggable obj)
        {
            if (CurrentDraggable == obj)
                CurrentDraggable = null;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }

        private Vector3 GetMouseWorldPositionCenterBody(Transform body)
        {
            Vector3 worldPos = GetMouseWorldPosition();
            worldPos.x -= body.localScale.x;
            worldPos.y -= body.localScale.y;
            return worldPos;
        }

    }
}
