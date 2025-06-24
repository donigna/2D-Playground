using System.Collections.Generic;
using Kuwiku.Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kuwiku.Softbody
{
    public class VertexTest : MonoBehaviour
    {
        public Mesh mesh;
        public bool drawTexture;
        public Vector3[] vertices;
        public int centePoint;
        public int verticesCount;
        public List<GameObject> points;
        public GameObject toBeInstantiated;

        #region Callbacks
        void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            vertices = mesh.vertices;
            verticesCount = vertices.Length;

            if (drawTexture)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    GameObject childObject;
                    childObject = Instantiate(toBeInstantiated, gameObject.transform.position + vertices[i], Quaternion.identity);

                    childObject.transform.parent = gameObject.transform;
                    points.Add(childObject);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            HingeJoint2D hjoint;
            DistanceJoint2D djoint;
            // Menghubungkan titik-titik luar secara melingkar
            if (drawTexture)
            {

                for (int i = 0; i < points.Count - 1; i++) // Mengulangi semua kecuali titik terakhir
                {
                    hjoint = points[i].GetComponent<HingeJoint2D>();
                    djoint = points[i].GetComponent<DistanceJoint2D>();
                    if (hjoint != null && djoint != null && i != 0)
                    {
                        hjoint.connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                        djoint.connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                    }
                    else
                    {
                        Destroy(hjoint);
                        Destroy(djoint);
                    }
                }

                // Last outside point
                hjoint = points[points.Count - 1].GetComponent<HingeJoint2D>();
                djoint = points[points.Count - 1].GetComponent<DistanceJoint2D>();
                if (hjoint != null && djoint != null)
                {
                    hjoint.connectedBody = points[1].GetComponent<Rigidbody2D>();
                    djoint.connectedBody = points[1].GetComponent<Rigidbody2D>();
                }

                // Jika Anda memiliki titik tengah dan ingin menghubungkannya ke titik-titik luar
                // Ini hanya contoh, Anda perlu menentukan index titik tengah
                Rigidbody2D centerRb = points[0].GetComponent<Rigidbody2D>();
                if (centerRb != null)
                {
                    for (int i = 1; i < points.Count; i++) // Hubungkan titik tengah ke semua titik luar
                    {
                        // Anda mungkin perlu menambahkan komponen HingeJoint2D lain ke titik tengah
                        // atau menggunakan SpringJoint2D untuk tarikan.
                        // Ini akan tergantung pada bagaimana Anda ingin titik tengah berinteraksi.
                        // Contoh dengan SpringJoint2D dari titik luar ke titik tengah:
                        for (int j = 0; j < 3; j++)
                        {
                            int index;
                            SpringJoint2D spring = points[i].gameObject.AddComponent<SpringJoint2D>();
                            switch (j)
                            {
                                case 0:
                                    spring.connectedBody = points[0].gameObject.GetComponent<Rigidbody2D>();
                                    break;
                                case 1:
                                    index = i + 1 - 2;
                                    Tools.Logger.Log($"{spring.gameObject} : connect to neighbor index of {points.Count + index - 1}");
                                    spring.connectedBody = points[index > 0 ? index : points.Count + index - 1].gameObject.GetComponent<Rigidbody2D>();
                                    break;
                                case 2:
                                    index = i + 2;
                                    Tools.Logger.Log($"{spring.gameObject} : connect to neighbor index of {index}");
                                    spring.connectedBody = points[index < points.Count ? index : index - points.Count].gameObject.GetComponent<Rigidbody2D>();
                                    break;
                            }
                            spring.distance = 2f;
                            spring.dampingRatio = 0f;
                            spring.frequency = 1.0f;
                        }
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (drawTexture)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    // if (i == 0)
                    // {
                    //     points[0].transform.localPosition = Vector3.zero;
                    // }
                    vertices[i] = points[i].transform.localPosition;
                }
                mesh.vertices = vertices;
            }
        }
    }
}

#endregion
