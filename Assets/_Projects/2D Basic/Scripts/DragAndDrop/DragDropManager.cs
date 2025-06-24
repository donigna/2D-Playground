using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    public class DragDropManager : MonoBehaviour
    {
        public static DragDropManager Instance { get; private set; }
        public DraggableObject CurrentDraggable { get; private set; }

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
            switch (InputManager.Instance.GetInputReader(0).GetActionButtonState())
            {
                case IInputReader.ActionButtonState.Pressed:
                    if (TryBeginDrag())
                    {
                        if (CurrentDraggable)
                        {
                            CurrentDraggable.OnDragStart(GetMouseWorldPositionCenterBody(CurrentDraggable.transform));
                        }
                    }
                    break;

                case IInputReader.ActionButtonState.Held:
                    if (CurrentDraggable)
                    {
                        CurrentDraggable.OnDrag(GetMouseWorldPositionCenterBody(CurrentDraggable.transform));
                    }
                    break;

                case IInputReader.ActionButtonState.Released:
                    if (CurrentDraggable)
                    {
                        CurrentDraggable.OnDragEnd(CurrentDraggable.transform.position);
                        EndDrag(CurrentDraggable);
                    }
                    break;
            }
        }

        public bool TryBeginDrag()
        {
            if (CurrentDraggable != null) return false;
            DraggableObject[] draggables = FindObjectsByType<DraggableObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
            foreach (var draggable in draggables)
            {
                if (draggable.PointInsideObject(GetMouseWorldPosition()))
                {
                    CurrentDraggable = draggable;
                    return true;
                }
            }
            return false;
        }

        public void EndDrag(DraggableObject obj)
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
