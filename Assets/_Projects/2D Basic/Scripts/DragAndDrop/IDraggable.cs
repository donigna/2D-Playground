using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    public interface IDraggable
    {
        public void OnDragStart(Vector2 position);
        public void OnDrag(Vector2 position);
        public void OnDragEnd(Vector2 position);
    }
}
