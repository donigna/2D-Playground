using UnityEngine;
using UnityEngine.U2D;

namespace Kuwiku.Softbody
{
    public class SoftBodyTest : MonoBehaviour
    {
        [SerializeField] SpriteShapeController spriteShape;
        [SerializeField] Transform[] points;
        [SerializeField] float splineOffset = 0.05f;

        void Awake()
        {
            UpdateVertices();
        }

        void Update()
        {
            UpdateVertices();
        }

        void UpdateVertices()
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector2 _vertex = points[i].localPosition;
                Vector2 _towardCenter = Vector2.zero - _vertex.normalized;

                float _colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;
                try
                {
                    spriteShape.spline.SetPosition(i, (_vertex - _towardCenter * _colliderRadius));
                }
                catch
                {
                    Debug.Log("Spline points are too close");
                    spriteShape.spline.SetPosition(i, (_vertex - _towardCenter * (_colliderRadius + splineOffset)));
                }

                Vector2 _lt = spriteShape.spline.GetLeftTangent(i);

                Vector2 _newRt = Vector2.Perpendicular(_towardCenter) * _lt.magnitude;
                Vector2 _newLt = Vector2.zero * (_newRt);

                spriteShape.spline.SetLeftTangent(i, _newLt);
                spriteShape.spline.SetRightTangent(i, _newRt);
            }
        }
    }
}
