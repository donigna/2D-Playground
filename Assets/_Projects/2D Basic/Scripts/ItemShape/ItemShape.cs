using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    public class ItemShape : MonoBehaviour
    {
        public Vector2Int shapeSize = new Vector2Int(1, 1);
        public bool[,] shape;

        [SerializeField] private bool[] flatShape;

        void OnValidate()
        {
            SyncTo2D();
        }

        public void SyncTo2D()
        {
            if (flatShape == null || flatShape.Length != shapeSize.x * shapeSize.y)
            {
                flatShape = new bool[shapeSize.x * shapeSize.y];
            }

            shape = new bool[shapeSize.x, shapeSize.y];
            for (int x = 0; x < shapeSize.x; x++)
            {
                for (int y = 0; y < shapeSize.y; y++)
                {
                    int index = y * shapeSize.x + x;
                    if (index < flatShape.Length)
                    {
                        shape[x, y] = flatShape[index];
                    }
                    else
                    {
                        shape[x, y] = false;
                    }
                }
            }
        }

        public void SyncToFlat()
        {
            if (flatShape == null || flatShape.Length != shapeSize.x * shapeSize.y)
            {
                flatShape = new bool[shapeSize.x * shapeSize.y];
            }

            for (int x = 0; x < shapeSize.x; x++)
            {
                for (int y = 0; y < shapeSize.y; y++)
                {
                    int index = y * shapeSize.x + x;
                    if (index < flatShape.Length)
                    {
                        flatShape[index] = shape[x, y];
                    }
                }
            }
        }

        public bool IsPointInsadeShape(Vector3 pointWorld)
        {
            Vector2 local = pointWorld - transform.position; // relatif terhadap asal item
            int cellX = Mathf.FloorToInt(local.x / 1);
            int cellY = Mathf.FloorToInt(local.y / 1);

            // Cek batas grid
            if (cellX < 0 || cellX >= shapeSize.x || cellY < 0 || cellY >= shapeSize.y)
                return false;

            return shape[cellX, cellY];
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            if (shape != null)
            {
                for (int x = 0; x < shapeSize.x; x++)
                {
                    for (int y = 0; y < shapeSize.y; y++)
                    {
                        if (shape[x, y])
                        {
                            Vector3 position = transform.position + new Vector3(x + (Vector3.one.x / 2), y + (Vector3.one.y / 2), 0);
                            Gizmos.DrawWireCube(position, Vector3.one);
                        }
                    }
                }
            }
        }
    }
}