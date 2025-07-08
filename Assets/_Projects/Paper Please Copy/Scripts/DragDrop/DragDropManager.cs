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
                            CurrentDraggable.OnDrag(GetDraggingPosition());
                        }
                        break;

                    case IInputReader.ActionButtonState.Released:
                        if (CurrentDraggable != null)
                        {
                            CurrentDraggable.OnDragEnd(GetDraggingPosition());
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
                    if (hit.collider.TryGetComponent(out DraggableObject draggableObject))
                    {
                        if (draggableObject._letterObj != null) draggable = draggableObject._letterObj;
                    }
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


        private Vector3 GetDraggingPosition()
        {
            Vector3 playerPos = PlayerController.Instance.GetPlayerPosition();

            return playerPos;
        }

    }
}
