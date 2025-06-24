using UnityEditor;
using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    public class DropArea : MonoBehaviour
    {
        [SerializeField, Tooltip("The size of the grid, X is width, Y is height."), Min(1)]
        Vector2Int gridSize = new Vector2Int(10, 10);
        [SerializeField, Tooltip("The size of each cell in the grid."), Min(0.1f)]
        Vector2 cellSize = new Vector2(1f, 1f);

        int minX, maxX, minY, maxY;

        bool[,] grid;

        void Awake()
        {
            maxX = Mathf.FloorToInt(transform.position.x + gridSize.x / 2);
            minY = Mathf.FloorToInt(transform.position.y - gridSize.y / 2);
            minX = Mathf.FloorToInt(transform.position.x - gridSize.x / 2);
            maxY = Mathf.FloorToInt(transform.position.y + gridSize.y / 2);

            grid = new bool[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    grid[x, y] = false; // Initialize all cells as empty
                }
            }
        }

        public bool CanPlaceItem(ItemShape itemShape, Vector2Int position)
        {
            if (itemShape == null) return false;

            if (position.x < 0 || position.x + itemShape.shapeSize.x > gridSize.x ||
                position.y < 0 || position.y + itemShape.shapeSize.y > gridSize.y)
            {
                return false; // Item is out of bounds
            }

            for (int x = 0; x < itemShape.shapeSize.x; x++)
            {
                for (int y = 0; y < itemShape.shapeSize.y; y++)
                {
                    if (itemShape.shape[x, y] && !IsCellEmpty(position.x + x, position.y + y))
                    {
                        return false;
                    }
                }
            }

            for (int x = 0; x < itemShape.shapeSize.x; x++)
            {
                for (int y = 0; y < itemShape.shapeSize.y; y++)
                {
                    SetCell(position.x + x, position.y + y, itemShape.shape[x, y]);
                }
            }
            return true;
        }

        public void ClearCellByShape(ItemShape itemShape, Vector2Int position)
        {
            for (int x = 0; x < itemShape.shapeSize.x; x++)
            {
                for (int y = 0; y < itemShape.shapeSize.y; y++)
                {
                    if (itemShape.shape[x, y])
                    {
                        SetCell(position.x + x, position.y + y, false);
                    }
                }
            }
        }

        public Vector2Int GetNearestCellPosition(Vector3 worldPosition)
        {
            // Examples
            // worldPos = {20.4,10.2,0}
            // transform pos = {2,2,0}
            // localPos = {18.4,8.2,0}
            // int x = 18
            // int y = 8
            Vector3 localPosition = worldPosition - transform.position;
            int x = Mathf.RoundToInt(localPosition.x / cellSize.x + gridSize.x / 2);
            int y = Mathf.RoundToInt(localPosition.y / cellSize.y + gridSize.y / 2);
            return new Vector2Int(x, y);
        }

        public Vector2Int GetCellPosition(Vector3 worldPosition)
        {
            Vector3 localPosition = worldPosition - transform.position;
            int x = Mathf.FloorToInt(localPosition.x / cellSize.x + gridSize.x / 2);
            int y = Mathf.FloorToInt(localPosition.y / cellSize.y + gridSize.y / 2);
            return new Vector2Int(x, y);
        }

        public Vector3 GetCellWorldPosition(Vector2Int cellPosition)
        {
            float x = Mathf.Floor(minX + cellPosition.x * cellSize.x);
            float y = Mathf.Floor(minY + cellPosition.y * cellSize.y);

            return new Vector3(x, y, 0);
        }

        public bool IsCellEmpty(int x, int y)
        {
            if (x < 0 || x >= gridSize.x || y < 0 || y >= gridSize.y) return false;
            return !grid[x, y];
        }

        public void SetCell(int x, int y, bool value)
        {
            if (x < 0 || x >= gridSize.x || y < 0 || y >= gridSize.y) return;
            grid[x, y] = value;
        }

        private void OnDrawGizmosSelected()
        {
            maxX = Mathf.FloorToInt(transform.position.x + gridSize.x / 2);
            minY = Mathf.FloorToInt(transform.position.y - gridSize.y / 2);
            minX = Mathf.FloorToInt(transform.position.x - gridSize.x / 2);
            maxY = Mathf.FloorToInt(transform.position.y + gridSize.y / 2);
            Gizmos.color = Color.green;

            // Draw grid lines
            for (float x = minX; x <= maxX; x += cellSize.x)
            {
                Gizmos.DrawLine(new Vector3(x, minY, 0), new Vector3(x, maxY, 0));
            }

            // Draw horizontal lines
            for (float y = minY; y <= maxY; y += cellSize.y)
            {
                Gizmos.DrawLine(new Vector3(minX, y, 0), new Vector3(maxX, y, 0));
            }
        }
    }
}
