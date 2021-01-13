using System.Collections.Generic;
using UnityEngine;

namespace Code.Metrics
{
    public struct Triangle
    {
        public Vector3 Vertex1;
        public Vector3 Vertex2;
        public Vector3 Vertex3;

        public Triangle(Vector3 vertex1, Vector3 vertex2, Vector3 vector3)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vector3;
        }

        public IEnumerable<Vector3> GetVertexes()
        {
            yield return Vertex1;
            yield return Vertex2;
            yield return Vertex3;
        }
    }
}