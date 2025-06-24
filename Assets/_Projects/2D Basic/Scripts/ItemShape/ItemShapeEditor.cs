#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace com.Kuwiku.Basic2D
{
    [CustomEditor(typeof(ItemShape))]
    public class ItemShapeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ItemShape itemShape = (ItemShape)target;

            itemShape.shapeSize = EditorGUILayout.Vector2IntField("Shape Size", itemShape.shapeSize);

            // Ensure the shape array is initialized with the correct size       
            if (itemShape.shape == null ||
                itemShape.shape.GetLength(0) != itemShape.shapeSize.x ||
                itemShape.shape.GetLength(1) != itemShape.shapeSize.y)
            {
                itemShape.shape = new bool[itemShape.shapeSize.x, itemShape.shapeSize.y];
            }


            GUILayout.Label("Shape Editor", EditorStyles.boldLabel);
            for (int y = itemShape.shapeSize.y - 1; y >= 0; y--)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                for (int x = 0; x < itemShape.shapeSize.x; x++)
                {
                    // Draw a toggle for each cell in the shape
                    itemShape.shape[x, y] = EditorGUILayout.Toggle(itemShape.shape[x, y], GUILayout.Width(20));
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            if (GUI.changed)
            {
                // Mark the object as dirty to save changes
                itemShape.SyncToFlat();
                EditorUtility.SetDirty(itemShape);
            }
        }
    }
}
#endif
